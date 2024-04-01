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
    public class DefectDetailController : ControllerBase
    {
        private readonly IRepositoryWrapper _repository;
        private readonly ILogger _logger;

        public DefectDetailController(IRepositoryWrapper repository, ILoggerFactory loggerFactory)
        {
            _repository = repository;
            _logger = loggerFactory.CreateLogger<DefectDetailController>();
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get([FromQuery] string model, [FromQuery] string lotNo)
        {
            var response = await _repository.DefectDetail.GetListAsync(model, lotNo);

            return Generation.GenerateJson(response);
        }

        [HttpGet("materials")]
        public async Task<ActionResult<string>> GetMaterials([FromQuery] string model, [FromQuery] string stageCd)
        {
            var response = await _repository.DefectDetail.GetMaterialsAsync(model, stageCd);
            return Generation.GenerateJson(response);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] DefectDetailModel model)
        {
            var response = new ResponseModel<object>();
            string message;

            if (!Validation.ValidateDefectDetailModel(model, out message))
            {
                _logger.LogWarning(message);
                Generation.GenerateResponse(ref response, null, false, message);
            }
            else
            {
                try
                {
                    var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

                    response = _repository.DefectDetail.Create(model, jwtToken);

                    await _repository.SaveAsync();

                    _logger.LogInformation(SystemConstants.Message.CREATED);

                    var id = _repository.DefectDetail.FindAll().OrderByDescending(x => x.Id).Select(x => x.Id).FirstOrDefault();

                    response.data = new { id = id };

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
