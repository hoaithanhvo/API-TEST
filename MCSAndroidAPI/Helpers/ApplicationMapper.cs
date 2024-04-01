using AutoMapper;
using MCSAndroidAPI.Data;
using MCSAndroidAPI.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace MCSAndroidAPI.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        { 
            CreateMap<LotManModel, TLotWorker>()
                .ForMember(destination => destination.MaterialCd,
                options => options.MapFrom(source => source.ProductNo));

            CreateMap<LotDefectModel, TLotDefect>()
                .ForMember(destination => destination.MaterialCd,
                options => options.MapFrom(source => source.ProductNo));

            CreateMap<LotStopModel, TLotStop>()
                .ForMember(destination => destination.MaterialCd,
                options => options.MapFrom(source => source.ProductNo));

            CreateMap<LotScrapModel, TLotScrap>()
                .ForMember(destination => destination.MaterialCd,
                options => options.MapFrom(source => source.ProductNo));

            CreateMap<DefectDetailModel, TDefectDetail>();

            CreateMap<TLotProduct, LotStartModel>();
            CreateMap<MDivision, DivisionModel>();
            CreateMap<MProcess, ProcessModel>();
            CreateMap<MShift, ShiftModel>();
            CreateMap<MLine, LineModel>();
            CreateMap<MRouting, RoutingModel>();
            CreateMap<MProcessOpe, ProcessOpeModel>();
            CreateMap<MDefectReason, DefectReasonModel>();
            CreateMap<MStopReason, StopReasonModel>();
            CreateMap<MWorker, WorkerModel>();
            CreateMap<MScrapReason, ScrapReasonModel>();
            CreateMap<MStage, StageModel>();
            CreateMap<MBomMaterial, MaterialModel>();
        }
    }
}
