using Azure;
using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MCSAndroidAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LotStopController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILogger _logger;

        public LotStopController(IRepositoryWrapper repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<LotStopController>();
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetItems([FromQuery] LotStopModel model)
        {
            var response = await _repository.LotStop.GetItemsAsync(model);
            return Generation.GenerateJson(response);
        }

        [HttpGet("reasons")]
        public async Task<ActionResult<string>> GetReasons([FromQuery] DivisionCdAndProcessCdModel model)
        {
            var response = await _repository.LotStop.GetReasonsAsync(model);

            return Generation.GenerateJson(response);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] LotStopModel model)
        {
            var response = new ResponseModel<object>();

            // skip checking required with fields
            string[] skipFields = [LotStopFields.ReportId, LotStopFields.StopRsnName, LotStopFields.StopNote];
            string message;

            if (!Validation.ValidateLotStopModel(model, out message, skipFields))
            {
                _logger.LogWarning(message);
                Generation.GenerateResponse(ref response, null, false, message);
            }
            else
            {
                
                try
                {
                    var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

                    response = await _repository.LotStop.CreateAsync(model, jwtToken);

                    await _repository.SaveAsync();

                    _logger.LogInformation(SystemConstants.Message.CREATED);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    Generation.GenerateResponse(ref response, null, false);
                }
            }

            return Generation.GenerateJson(response);
        }

        [HttpPut]
        public async Task<ActionResult<string>> Update([FromBody] LotStopModel model)
        {
            if (model.ReportId == null)
            {
                ModelState.AddModelError("ReportId", SystemConstants.Message.FIELD_IS_REQUIRED.Replace("{0}", "ReportId"));
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new ResponseModel<object>();

            // skip checking required with fields
            string[] skipFields = [LotStopFields.StopRsnName, LotStopFields.StopNote];
            string message;

            if (!Validation.ValidateLotStopModel(model, out message, skipFields))
            {
                _logger.LogWarning(message);
                Generation.GenerateResponse(ref response, null, false, message);
            }
            else
            {
                try
                {
                    var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
                    response = await _repository.LotStop.UpdateAsync(model, jwtToken);

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

        [HttpDelete]
        public async Task<ActionResult<string>> Delete([FromQuery] LotStopModel model)
        {
            if (model.ReportId == null)
            {
                ModelState.AddModelError("ReportId", SystemConstants.Message.FIELD_IS_REQUIRED.Replace("{0}", "ReportId"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = new ResponseModel<object>();

            try
            {
                var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];
                response = await _repository.LotStop.DeleteAsync(model, jwtToken);

                await _repository.SaveAsync();

                _logger.LogInformation(SystemConstants.Message.DELETED);
            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return Generation.GenerateJson(response);
        }
    }
}
