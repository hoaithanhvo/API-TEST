using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Net.WebSockets;

namespace MCSAndroidAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LotStartController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILogger _logger;

        public LotStartController(IRepositoryWrapper repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<LotStartController>();
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetItem([FromQuery] LotStartModel model)
        {
            var response = await _repository.LotStart.GetItemAsync(model);

            return Generation.GenerateJson(response);
        }

        [HttpPut]
        public async Task<ActionResult<string>> Update([FromBody] LotStartModel model)
        {
            var response = new ResponseModel<object>();

            string message;
            if (!Validation.ValidateLotStartModel(model, out message))
            {
                _logger.LogWarning(message);
                Generation.GenerateResponse(ref response, null, false, message);
            }
            else
            {
                try
                {
                    var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

                    response = await _repository.LotStart.UpdateAsync(model, jwtToken);

                    await _repository.SaveAsync();

                    _logger.LogInformation(SystemConstants.Message.UPDATED);
                }
                catch (Exception ex)
                {
                   _logger.LogError(ex.ToString());
                    Generation.GenerateResponse(ref response, null, false);
                }
            }

            return Generation.GenerateJson(response);
        }
    }
}
