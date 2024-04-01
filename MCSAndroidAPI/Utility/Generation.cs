using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Models;
using System.Net.NetworkInformation;
using System.Text.Json;

namespace MCSAndroidAPI.Utility
{
    public static class Generation
    {
        /// <summary>
        /// generate json with response
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns>json</returns>
        public static string GenerateJson<T>(ResponseModel<T> response)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.Create(System.Text.Unicode.UnicodeRanges.All),
                AllowTrailingCommas = true,
                ReadCommentHandling = JsonCommentHandling.Skip
            };

            return JsonSerializer.Serialize(response, options);
        }

        /// <summary>
        /// Generate response.
        /// If an exception occurs, the default message is an internal server error
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="data"></param>
        /// <param name="isSuccess"></param>
        /// <param name="message"></param>
        public static void GenerateResponse<T>(ref ResponseModel<T> response, T? data, bool isSuccess = true, string? message = null)
        {
            if (isSuccess)
            {
                response.status = SystemConstants.Status.SUCCESS;
                if (message != null)
                {
                    response.message = message;
                }
            }
            else
            {
                response.status = SystemConstants.Status.ERROR;
                
                if (message != null)
                {
                    response.message = message;
                }
                else
                {
                    //set the default message is internal server error
                    response.message = SystemConstants.Message.SERVER_ERROR;
                }
            }

            response.data = data;
        }

        public static string GetClientMAC()
        {
            string addr = "";
            foreach (NetworkInterface n in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (n.OperationalStatus == OperationalStatus.Up)
                {
                    addr += n.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return addr;
        }
    }
}
