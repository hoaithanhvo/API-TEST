using AutoMapper;
using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using MCSAndroidAPI.Utility;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MCSAndroidAPI.Repositories
{
    public class ProductionResultRepository : IProductionResultRepository
    {
        private ICommonRepository _commonRepository;
        private readonly NidecMCSContext _context;
        private readonly ILogger _logger;
        public ProductionResultRepository(NidecMCSContext context, IMapper mapper, ILogger logger) 
        {
            _context = context;
            _logger = logger;
            _commonRepository = new CommonRepository(_context, mapper, _logger);
        }
        public async Task<ResponseModel<List<ProductionResultModel>>> GetItemsAsync(ProductionResultSearchModel model)
        {
            var response = new ResponseModel<List<ProductionResultModel>>();

            var data = await SearchAsync(model);

            _logger.LogInformation($"[GetItems] Count: {data.Count}");
            Generation.GenerateResponse(ref response, data);

            return response;
        }

        public async Task<ResponseModel<List<DivisionModel>>> GetDivisionsAsync()
        {
            return await _commonRepository.GetDivisionsAsync();
        }

        public async Task<ResponseModel<List<ProcessModel>>> GetProcessesAsync(string divisionCd)
        {
            return await _commonRepository.GetProcessesAsync(divisionCd);
        }

        public async Task<ResponseModel<object>> GetRoutingsShiftsLinesAsync(DivisionCdAndProcessCdModel model)
        {
            var response = new ResponseModel<object>();

            var shifts = await _commonRepository.GetShiftsAsync(model);
            var lines = await _commonRepository.GetLinesAsync(model);
            var routings = await _commonRepository.GetRoutingsAsync(model);

            if (shifts.status == SystemConstants.Status.SUCCESS && lines.status == SystemConstants.Status.SUCCESS
                && routings.status == SystemConstants.Status.SUCCESS) 
            {
                var data = new { routings = routings.data, shifts = shifts.data, lines = lines.data };

                _logger.LogInformation($"[GetRoutingsShiftsLines]\nroutings: count = {data.routings?.Count}\nshifts: count = {data.shifts?.Count}\nlines: count {data.lines?.Count}");

                Generation.GenerateResponse(ref response, data);
            } else
            {
                var message = "";
                message += lines.message;
                message += shifts.message;
                message += routings.message;

                _logger.LogWarning($"[GetRoutingsShiftsLines] {message}");
                Generation.GenerateResponse(ref response, null, false, message);
            }

            return response;
        }

        private async Task<List<ProductionResultModel>> SearchAsync(ProductionResultSearchModel model)
        {
            List<ProductionResultModel>  data = new List<ProductionResultModel>();
            try
            {
                IQueryable<TLotProduct> product = _context.TLotProducts.AsQueryable();
                IQueryable<TLotGood> good = _context.TLotGoods.AsQueryable();
                IQueryable<AMaterial> material = _context.AMaterials.AsQueryable();
                IQueryable<MShift> shift = _context.MShifts.AsQueryable();
                IQueryable<MLine> line = _context.MLines.AsQueryable();
                IQueryable<MProcess> process = _context.MProcesses.AsQueryable();
                IQueryable<MDivision> division = _context.MDivisions.AsQueryable();
                IQueryable<TLotGoodDetail> good_detail = _context.TLotGoodDetails.AsQueryable();
                
                IQueryable<MWorker> m_worker = _context.MWorkers.AsQueryable();
                if (model != null)
                {
                    if (model.DivisionCd == "0")
                    {
                        if (!Validation.isNullOrEmpty(model.ProdDateFrom) && model.ProdDateFrom != DateTime.MinValue
                            && !Validation.isNullOrEmpty(model.ProdDateTo) && model.ProdDateTo != DateTime.MinValue)
                        {
                            var product_date_from = model.ProdDateFrom!.Value.ToString("yyyyMMdd");
                            var product_date_to = model.ProdDateTo!.Value.ToString("yyyyMMdd");
                            product = product.Where(s => s.ProductDay != null && s.ProductDay.CompareTo(product_date_from) >= 0 && s.ProductDay.CompareTo(product_date_to) <= 0);
                        }
                        if (!Validation.isNullOrEmpty(model.RoutingCd))
                        {
                            good_detail = good_detail.Where(s => s.RoutingCd == model.RoutingCd);
                        }
                    }
                    else
                    {
                        if (!Validation.isNullOrEmpty(model.DivisionCd))
                        {
                            product = product.Where(s => s.DivisionCd == model.DivisionCd);
                            division = division.Where(s => s.DivisionCd == model.DivisionCd);
                            good = good.Where(s => s.DivisionCd == model.DivisionCd);

                        }

                        if (!Validation.isNullOrEmpty(model.ProcessCd))      
                        {
                            process = process.Where(s => s.ProcessCd == model.ProcessCd);
                            product = product.Where(s => s.ProcessCd == model.ProcessCd);
                            good = good.Where(s => s.ProcessCd == model.ProcessCd);
                        }
                        if (!Validation.isNullOrEmpty(model.ProdDateFrom) && model.ProdDateFrom != DateTime.MinValue
                            && !Validation.isNullOrEmpty(model.ProdDateTo) && model.ProdDateTo != DateTime.MinValue)
                        {
                            var product_date_from = model.ProdDateFrom!.Value.ToString("yyyyMMdd");
                            var product_date_to = model.ProdDateTo!.Value.ToString("yyyyMMdd");
                            product = product.Where(s => s.ProductDay != null && s.ProductDay.CompareTo(product_date_from) >= 0 && s.ProductDay.CompareTo(product_date_to) <= 0);
                        }
                        if (!Validation.isNullOrEmpty(model.ShiftCd))
                        {
                            shift = shift.Where(s => s.ShiftCd == model.ShiftCd);
                            product = product.Where(s => s.ShiftCd == model.ShiftCd);
                        }

                        if (!Validation.isNullOrEmpty(model.LineCd))
                        {
                            line = line.Where(s => s.LineCd == model.LineCd);
                            product = product.Where(s => s.LineCd == model.LineCd);
                        }

                        if (!Validation.isNullOrEmpty(model.RoutingCd))
                        {
                            good_detail = good_detail.Where(s => s.RoutingCd == model.RoutingCd);
                        }

                        if (!Validation.isNullOrEmpty(model.ProductNo))
                        {
                            product = product.Where(s => s.ProductNo == model.ProductNo);
                        }
                        if (!Validation.isNullOrEmpty(model.LotNo))
                        {
                            product = product.Where(s => s.LotNo == model.LotNo);
                        }
                    }

                }

                var goodTotal = (from _good in good
                                join _good_detail in good_detail
                                on new { _good.DivisionCd, _good.ProcessCd, _good.MaterialCd, _good.LotNo, _good.BoxNo }
                                equals new { _good_detail.DivisionCd, _good_detail.ProcessCd, _good_detail.MaterialCd, _good_detail.LotNo, _good_detail.BoxNo }
                                group _good by new { _good.DivisionCd, _good.ProcessCd, _good.MaterialCd, _good.LotNo } into g

                                select new
                                {
                                    g.Key.DivisionCd,

                                    g.Key.ProcessCd,

                                    g.Key.MaterialCd,

                                    g.Key.LotNo,

                                    ProdQty = g.Sum(s => s.ProdQty),

                                    SapRemainQty = g.Sum(s => s.SapRemainQty)
                                }).AsQueryable();

                var lstdata = (from _product in product
                              join _goodtotal in goodTotal
                              on new { _product.DivisionCd, _product.ProcessCd, _product.ProductNo, _product.LotNo } 
                                equals new { _goodtotal.DivisionCd, _goodtotal.ProcessCd, ProductNo = _goodtotal.MaterialCd, _goodtotal.LotNo } into ps
                              
                              from dt in ps.DefaultIfEmpty()
                              join _shift in shift
                              on new { _product.ShiftCd, _product.DivisionCd, _product.ProcessCd } equals new { _shift.ShiftCd, _shift.DivisionCd, _shift.ProcessCd }
                              join _line in line
                               on new { _product.LineCd, _product.DivisionCd, _product.ProcessCd } equals new { _line.LineCd, _line.DivisionCd, _line.ProcessCd }
                              join _material in material
                              on _product.ProductNo equals _material.MaterialCd
                              join _process in process
                              on new { _product.ProcessCd, _product.DivisionCd } equals new { _process.ProcessCd, _process.DivisionCd }
                              join _division in division
                              on _process.DivisionCd equals _division.DivisionCd
                              join _worker in m_worker
                              on _product.LeaderWorkerNo equals _worker.WorkerCd into wk
                              from _worker in wk.DefaultIfEmpty()
                              orderby _product.ProductNo

                              select new ProductionResultModel
                              {
                                  ShiftCd = _product.ShiftCd,

                                  ShiftName = _shift.ShiftName,

                                  LineCd = _product.LineCd,

                                  LineName = _line.LineName,

                                  ProductNo = _product.ProductNo,

                                  ProductName = _material.MaterialName,

                                  DivisionCd = _product.DivisionCd,

                                  ProcessCd = _product.ProcessCd,

                                  MoNo = _product.MoNo,

                                  Status = _product.LotStatusCode.HasValue == false ? CData.ProductionStatus.New
                                  : (_product.LotStatusCode!.Value == CData.ProductionStatus.Start_CD ? CData.ProductionStatus.Start
                                  : (_product.LotStatusCode!.Value == CData.ProductionStatus.End_CD ? CData.ProductionStatus.End
                                  : (_product.LotStatusCode!.Value == CData.ProductionStatus.Approve_CD ? CData.ProductionStatus.Approve
                                  : CData.ProductionStatus.New))),

                                  LotNo = _product.LotNo,

                                  PlanQty = _product.PlanQty ?? 0,

                                  ProductDay = _product.ProductDay,

                                  Finish = dt.ProdQty ?? 0,

                                  Sap = (dt.ProdQty ?? 0) - (dt.SapRemainQty ?? 0),

                                  ProdStartTime = _product.ProductStrDt,

                                  ProdEndTime = _product.ProductEndDt,

                                  RestTime = _product.RestHr ?? 0,

                                  DefectQty = _product.DefectQty ?? 0,

                                  WorkerNum = _product.WorkerNum ?? 0,

                                  WorkunitNum = _product.WorkunitNum ?? 0,

                                  ProdTact = _product.TactNum ?? 0,

                                  StopHr = _product.StopHr ?? 0,

                                  OnlineRate = _product.OnlineRate ?? 0,

                                  GoodQty = _product.GoodQty ?? 0,

                                  ProcessName = _process.ProcessName,

                                  Type = _material.MaterialType,

                                  LeaderNo = _worker.WorkerCd,

                                  LeaderName = _worker.WorkerName,
                              }).AsQueryable();

                data = await lstdata.OrderBy(s => s.ProductDay).ThenBy(s => s.ShiftName).ThenBy(s => s.LineName).ThenBy(s => s.ProdStartTime).ToListAsync();

                foreach (var item in data)
                {
                    if (item.ProdStartTime.HasValue && item.ProdEndTime.HasValue)
                    {
                        item.TotalTime = Conversion.parseDecimal((item.ProdEndTime.Value - item.ProdStartTime.Value).TotalMinutes);
                    }    
                    item.WorkingTime = item.TotalTime - item.RestTime;
                    item.ManHour = Math.Round(item.WorkingTime / 60, 0, MidpointRounding.AwayFromZero);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                data = new List<ProductionResultModel>();
            }

            return data;
        }
    }
}
