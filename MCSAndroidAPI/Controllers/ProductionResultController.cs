using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MCSAndroidAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductionResultController : ControllerBase
    {

        private readonly IRepositoryWrapper _repository;
        public ProductionResultController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet("divisions")]
        public async Task<ActionResult<string>> GetDivisions()
        {
            var response = await _repository.ProductionResult.GetDivisionsAsync();

            return Generation.GenerateJson(response);
        }

        [HttpGet("processes")]
        public async Task<ActionResult<string>> GetProcesses([FromQuery]string divisionCd)
        {
            var response = await _repository.ProductionResult.GetProcessesAsync(divisionCd);

            return Generation.GenerateJson(response);
        }

        [HttpGet("routings-shifts-lines")]
        public async Task<ActionResult<string>> GetRoutingsShiftsLines([FromQuery] DivisionCdAndProcessCdModel model)
        {
            var response = await _repository.ProductionResult.GetRoutingsShiftsLinesAsync(model);

            return Generation.GenerateJson(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<string>> GetItems([FromQuery] ProductionResultSearchModel model)
        {
            var response = await _repository.ProductionResult.GetItemsAsync(model);

            return Generation.GenerateJson(response);
        }
    }
}
