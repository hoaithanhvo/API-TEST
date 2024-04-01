using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MCSAndroidAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LotScrapController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILogger _logger;

        public LotScrapController(IRepositoryWrapper repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<LotScrapController>();
        }

        [HttpGet] 
        public async Task<ActionResult<string>> GetItems([FromQuery] LotScrapModel model)
        {
            var response = await _repository.LotScrap.GetItemsAsync(model);

            return Generation.GenerateJson(response);
        }

        [HttpGet("types-reasons")]
        public async Task<ActionResult<string>> GetTypesAndReasons([FromQuery] DivisionCdAndProcessCdModel model, [FromQuery] string productNo)
        {
            if (string.IsNullOrEmpty(productNo))
            {
                ModelState.AddModelError("productNo", SystemConstants.Message.FIELD_IS_REQUIRED.Replace("{0}", "productNo"));
            }        

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }   

            var response = await _repository.LotScrap.GetTypesAndReasonsAsync(model, productNo);

            return Generation.GenerateJson(response);
        }

        [HttpGet("list-item-no")]
        public async Task<ActionResult<string>> GetListItemNo([FromQuery] string productNo, [FromQuery] string type)
        {
            if (string.IsNullOrEmpty(productNo))
            {
                ModelState.AddModelError("productNo", SystemConstants.Message.FIELD_IS_REQUIRED.Replace("{0}", "productNo"));
            }
            if (string.IsNullOrEmpty(type))
            {
                ModelState.AddModelError("type", SystemConstants.Message.FIELD_IS_REQUIRED.Replace("{0}", "type"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _repository.LotScrap.GetListItemNoAsync(productNo, type);

            return Generation.GenerateJson(response);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody]  LotScrapModel model)
        {
            var response = new ResponseModel<object>();

            // skip checking required with fields
            string[] skipFields = [LotScrapFields.ReportId, LotScrapFields.ScrapRsnName, LotScrapFields.UnitPrice, LotScrapFields.ScrapAmt, LotScrapFields.ScrapNote];
            string message;

            if (!Validation.ValidateLotScrapModel(model, out message, skipFields))
            {
                _logger.LogWarning(message);
                Generation.GenerateResponse(ref response, null, false, message);
            }
            else
            {
                try
                {
                    var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

                    response = await _repository.LotScrap.CreateAsync(model, jwtToken);

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
        public async Task<ActionResult<string>> Update([FromBody] LotScrapModel model)
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
            string[] skipFields = [LotScrapFields.ScrapRsnName, LotScrapFields.UnitPrice, LotScrapFields.ScrapAmt, LotScrapFields.ScrapNote];
            string message;

            if (!Validation.ValidateLotScrapModel(model, out message, skipFields))
            {
                _logger.LogWarning(message);
                Generation.GenerateResponse(ref response, null, false, message);
            }
            else
            {

                try
                {
                    var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

                    response = await _repository.LotScrap.UpdateAsync(model, jwtToken);

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
        public async Task<ActionResult<string>> Delete([FromQuery] LotScrapModel model)
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
                var item = await _repository.LotScrap.FindByCondition(x => x.DivisionCd == model.DivisionCd && x.ProcessCd == model.ProcessCd &&
                    x.MaterialCd == model.ProductNo && x.LotNo == model.LotNo && x.ReportId == model.ReportId).FirstOrDefaultAsync();

                if (item == null)
                {
                    _logger.LogWarning(SystemConstants.Message.NOT_FOUND);
                    Generation.GenerateResponse(ref response, null, false, SystemConstants.Message.NOT_FOUND);
                }
                else
                {
                    _repository.LotScrap.Delete(item);

                    await _repository.SaveAsync();

                    _logger.LogInformation(SystemConstants.Message.DELETED);

                    Generation.GenerateResponse(ref response, null);
                }
            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return Generation.GenerateJson(response);
        }
    }
}
