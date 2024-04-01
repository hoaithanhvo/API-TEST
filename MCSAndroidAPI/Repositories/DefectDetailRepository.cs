using AutoMapper;
using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MCSAndroidAPI.Repositories
{
    public class DefectDetailRepository : RepositoryBase<TDefectDetail>, IDefectDetailRepository
    {
        private ICommonRepository _commonRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        public DefectDetailRepository(NidecMCSContext nidecMCSContext, IMapper mapper, ILogger logger) : base(nidecMCSContext, mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _commonRepository = new CommonRepository(nidecMCSContext, mapper, _logger);
        }

        public ResponseModel<object> Create(DefectDetailModel model, string token)
        {
            var response = new ResponseModel<object>();
            try
            {
                var product = new TLotProduct();

                if (ValidateData(ref response, ref product, model.Model, model.LotNo))
                {

                    // get the username using the access token
                    var principal = Validation.ValidateToken(token);
                    var username = principal?.Identity?.Name;

                    var item = _mapper.Map<TDefectDetail>(model);
                    item.DivisionCd = product.DivisionCd;
                    item.ProcessCd = product.ProcessCd;
                    item.LineCd = product.LineCd;
                    item.ShiftCd = product.ShiftCd;
                    item.CreatedDate = DateTime.Now;
                    item.CreatedBy = username;
                    item.UpdatedDate = DateTime.Now;
                    item.UpdatedBy = username;
                    item.StatusFlg = "0";
                    item.DeleteFlg = "0";
                    item.TerminalNo = Generation.GetClientMAC();

                    this.Create(item);

                    Generation.GenerateResponse(ref response, null);
                }

            } catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }

            return response;
        }

        public async Task<ResponseModel<object>> GetListAsync(string model, string lotNo)
        {
            var response = new ResponseModel<object>();
            try
            {
                var product = new TLotProduct();
                if (ValidateData(ref response, ref product, model, lotNo))
                {
                    var defectReasonResult = await _commonRepository.GetDefectReasonsAsync(product.DivisionCd, product.ProcessCd);

                    if (defectReasonResult.status == SystemConstants.Status.ERROR)
                    {
                        _logger.LogWarning($"[GetList] {defectReasonResult.message}");
                        Generation.GenerateResponse(ref response, null, false, defectReasonResult.message);

                        return response;
                    }

                    var reasons = defectReasonResult.data;

                    var division = await NidecMCSContext.MDivisions.FirstOrDefaultAsync(x => x.DivisionCd == product.DivisionCd);
                    var divisionName = division?.DivisionName;

                    var process = await NidecMCSContext.MProcesses.FirstOrDefaultAsync(x => x.DivisionCd == product.DivisionCd && x.ProcessCd == product.ProcessCd);
                    var processName = process?.ProcessName;

                    var line = await NidecMCSContext.MLines.FirstOrDefaultAsync(x => x.DivisionCd == product.DivisionCd && x.ProcessCd == product.ProcessCd && x.LineCd == product.LineCd);
                    var lineName = line?.LineName;

                    var shift = await NidecMCSContext.MShifts.FirstOrDefaultAsync(x => x.DivisionCd == product.DivisionCd && x.ProcessCd == product.ProcessCd && x.ShiftCd == product.ShiftCd);
                    var shiftName = shift?.ShiftName;

                    var stages = await NidecMCSContext.MStageMaterials
                        .Where(x => x.ModelCd == model.ToUpper())
                        .Join(NidecMCSContext.MProcessStages, sm => sm.ProcessStageId, ps => ps.Id, (sm, ps) => ps)
                        .Join(NidecMCSContext.MStages, ps => ps.StageCd, s => s.StageCd, (p, s) => s)
                        .Distinct()
                        .Select(s => _mapper.Map<StageModel>(s)).ToListAsync();

                    var data = new
                    {
                        divisionName,
                        processName,
                        lineName,
                        shiftName,
                        stages,
                        reasons,
                    };

                    _logger.LogInformation($"[GetList]\ndivisionName: {divisionName}\nprocessName: {processName}\nlineName: {lineName}\n" +
                        $"shiftName: {shiftName}\nstages: count = {stages.Count}\nreasons: count = {reasons?.Count}");

                    Generation.GenerateResponse(ref response, data);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }
            return response;
        }

        public async Task<ResponseModel<List<MaterialModel>>> GetMaterialsAsync(string model, string stageCd)
        {
            var response = new ResponseModel<List<MaterialModel>>();

            try
            {
                var models = await NidecMCSContext.MProcessStages
                            .Where(x => x.StageCd == stageCd)
                            .Join(NidecMCSContext.MStageMaterials, ps => ps.Id, sm => sm.ProcessStageId, (ps, sm) => sm)
                            .Where(x => x.ModelCd == model)
                            .Join(NidecMCSContext.MBomMaterials, sm => sm.MaterialCd, bm => bm.MaterialNo, (sm, bm) => bm)
                            .Select(b => _mapper.Map<MaterialModel>(b)).ToListAsync();

                _logger.LogInformation($"[GetMaterials] Count: {models.Count}");

                Generation.GenerateResponse(ref response, models);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                Generation.GenerateResponse(ref response, null, false);
            }
            return response;
        }

        #region Private method
        private bool CheckModel(string? model)
        {
            if (string.IsNullOrEmpty(model)) return false;
            return NidecMCSContext.MStageMaterials.Any(n => n.ModelCd.Equals(model.ToUpper()));
        }

        private TLotProduct? FindProduct(string? divisionCd, string? processCd, string? productNo, string? lotNo)
        {
            if (isNullOrEmpty([divisionCd, processCd, productNo, lotNo]))
            {
                _logger.LogWarning($"[FindProduct] One of [divisionCd, processCd, productNo, lotNo] is NullOrEmpty");
                return null;
            }    

            var product = NidecMCSContext.TLotProducts.FirstOrDefault(x => x.DivisionCd == divisionCd && x.ProcessCd == processCd &&
                            x.ProductNo == productNo && x.LotNo == lotNo);

            return product;
        }

        private bool ValidateData(ref ResponseModel<object> response, ref TLotProduct product, string? model, string? lotNo)
        {
            if (!CheckModel(model))
            {
                _logger.LogWarning($"[ValidateData] {SystemConstants.Message.MODEL_NOT_FOUND}");
                Generation.GenerateResponse(ref response, null, false, SystemConstants.Message.MODEL_NOT_FOUND);
                return false;
            }

            var material = NidecMCSContext.AMaterials.FirstOrDefault(x => x.MaterialCd == model!.ToUpper());

            if (material == null)
            {
                _logger.LogWarning($"[ValidateData] {SystemConstants.Message.MATERIAL_NOT_FOUND}");
                Generation.GenerateResponse(ref response, null, false, SystemConstants.Message.MATERIAL_NOT_FOUND);

                return false;
            }

            var result = FindProduct(material.Division, material.MrpController, material.MaterialCd, lotNo);

            if (result == null)
            {
                _logger.LogWarning($"[ValidateData] {SystemConstants.Message.PRODUCT_NOT_FOUND}");
                Generation.GenerateResponse(ref response, null, false, SystemConstants.Message.PRODUCT_NOT_FOUND);

                return false;
            }
            else
            {
                product = result;
            }    

            return true;
        }

        private bool isNullOrEmpty(string?[] arrStr)
        {
            foreach (string? str in arrStr)
            {
                if (string.IsNullOrEmpty(str))
                {
                    return true;
                }    
            }

            return false;
        }
        #endregion

    }
}
