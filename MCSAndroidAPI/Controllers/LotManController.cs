using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Diagnostics;
using System.Net.WebSockets;
using System.Reflection.PortableExecutable;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace MCSAndroidAPI.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LotManController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILogger _logger;

        public LotManController(IRepositoryWrapper repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<LotManController>();
        }


        [HttpGet]
        public async Task<ActionResult<string>> GetItems([FromQuery] LotManModel model)
        {
            var response = await _repository.LotMan.GetItemsAsync(model);
          
            return Generation.GenerateJson(response);
        }

        [HttpGet("jobs")]
        public async Task<ActionResult<string>> GetJobs([FromQuery] DivisionCdAndProcessCdModel model)
        {
            var response = await _repository.LotMan.GetJobsAsync(model);

            return Generation.GenerateJson(response);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] LotManModel model)
        {
            var response = new ResponseModel<object>();

            // skip checking required with fields
            string[] skipFields = [LotManFields.ReportId, LotManFields.JobCdName, LotManFields.LotWorkerNote];
            string message;
            if (!Validation.ValidateLotManModel(model, out message, skipFields))
            {
                _logger.LogWarning(message);
                Generation.GenerateResponse(ref response, null, false, message);
            }
            else
            {
                try
                {
                    var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

                    response = await _repository.LotMan.CreateAsync(model, jwtToken);

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
        public async Task<ActionResult<string>> Update([FromBody] LotManModel model)
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
            string[] skipFields = [LotManFields.JobCdName, LotManFields.LotWorkerNote];
            string message;
            if (!Validation.ValidateLotManModel(model, out message, skipFields))
            {
                _logger.LogWarning(message);
                Generation.GenerateResponse(ref response, null, false, message);
            }
            else
            {
                try
                {
                    var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

                    response = await _repository.LotMan.UpdateAsync(model, jwtToken);

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
        public async Task<ActionResult<string>> Delete([FromQuery] LotManModel model)
        {
            if (model.ReportId == null)
            {
                ModelState.AddModelError("ReportId", SystemConstants.Message.FIELD_IS_REQUIRED.Replace("{0}", "ReportId"));
            }

            if (!ModelState.IsValid)
            { return BadRequest(ModelState); }

            var response = new ResponseModel<object>();

            try
            {
                var item = await _repository.LotMan.FindByCondition(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd &&
                    x.MaterialCd == model.ProductNo && x.LotNo == model.LotNo && x.ReportId == model.ReportId).FirstOrDefaultAsync();

                if (item == null)
                {
                    _logger.LogWarning(SystemConstants.Message.NOT_FOUND);
                    Generation.GenerateResponse(ref response, null, false, SystemConstants.Message.NOT_FOUND);
                }
                else
                {
                    _repository.LotMan.Delete(item);

                    await _repository.SaveAsync();

                    _logger.LogInformation(SystemConstants.Message.DELETED);

                    Generation.GenerateResponse(ref response, null);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return Generation.GenerateJson(response);
        }
    }
}
