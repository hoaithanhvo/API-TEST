namespace MCSAndroidAPI.Models
{
    public class ResponseModel<T>
    {
        public string? status { get; set; }
        public string? message { get; set; }
        public T? data { get; set; }
    }
}
