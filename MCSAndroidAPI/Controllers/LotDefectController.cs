using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MCSAndroidAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LotDefectController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILogger _logger;

        public LotDefectController(IRepositoryWrapper repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<LotDefectController>();
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetItems([FromQuery] LotDefectModel model)
        {
            var response = await _repository.LotDefect.GetItemsAsync(model);

            return Generation.GenerateJson(response);
        }

        [HttpGet("reasons")]
        public async Task<ActionResult<string>> GetReasons([FromQuery] DivisionCdAndProcessCdModel model)
        {
            var response = await _repository.LotDefect.GetReasonsAsync(model);

            return Generation.GenerateJson(response);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] LotDefectModel model)
        {
            var response = new ResponseModel<object>();

            // skip checking required with fields
            string[] skipFields = [LotDefectFields.ReportId, LotDefectFields.DefectRsnName, LotDefectFields.DefectNote];

            string message;

            if (!Validation.ValidateLotDefectModel(model, out message, skipFields))
            {
                _logger.LogWarning(message);
                Generation.GenerateResponse(ref response, null, false, message);
            }
            else
            {
                try
                {
                    var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

                    response = await _repository.LotDefect.CreateAsync(model, jwtToken);

                    await _repository.SaveAsync();

                    _logger.LogInformation(SystemConstants.Message.CREATED);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    Generation.GenerateResponse(ref response, null, false, ex.Message);
                }
            }

            return Generation.GenerateJson(response);
        }

        [HttpPut]
        public async Task<ActionResult<string>> Update([FromBody] LotDefectModel model)
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
            string[] skipFields = [LotDefectFields.DefectRsnName, LotDefectFields.DefectNote];

            string message;

            if (!Validation.ValidateLotDefectModel(model, out message, skipFields))
            {
                _logger.LogWarning(message);
                Generation.GenerateResponse(ref response, null, false, message);
            }
            else
            {

                try
                {
                    var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

                    response = await _repository.LotDefect.UpdateAsync(model, jwtToken);

                    await _repository.SaveAsync();

                    _logger.LogInformation(SystemConstants.Message.UPDATED);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.ToString());
                    Generation.GenerateResponse(ref response, null, false, ex.Message);
                }
            }

            return Generation.GenerateJson(response);
        }

        [HttpDelete]
        public async Task<ActionResult<string>> Delete([FromQuery] LotDefectModel model)
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
                response = await _repository.LotDefect.DeleteAsync(model, jwtToken);

                await _repository.SaveAsync();

                _logger.LogInformation(SystemConstants.Message.DELETED);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false, ex.Message);
            }

            return Generation.GenerateJson(response);
        }
    }
}
