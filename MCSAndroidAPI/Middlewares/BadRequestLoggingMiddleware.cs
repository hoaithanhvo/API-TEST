namespace MCSAndroidAPI.Middlewares
{
    public class BadRequestLoggingMiddleware : IMiddleware
    {
        private readonly ILogger<BadRequestLoggingMiddleware> _logger;

        public BadRequestLoggingMiddleware(ILogger<BadRequestLoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            // Capture the original response stream
            var originalBodyStream = context.Response.Body;

            // Create a new memory stream to capture response
            using (var responseBody = new MemoryStream())
            {
                // Replace the response stream
                context.Response.Body = responseBody;

                // Continue processing the request pipeline
                await next(context);

                // Rewind the memory stream to read the response
                responseBody.Seek(0, SeekOrigin.Begin);
                var responseBodyContent = await new StreamReader(responseBody).ReadToEndAsync();

                // Log bad requests
                if (context.Response.StatusCode == StatusCodes.Status400BadRequest)
                {
                    _logger.LogWarning($"Bad Request: {context.Request.Method} {context.Request.Path} - {responseBodyContent}");
                }

                // Copy the captured response back to the original stream
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
