using AutoMapper;
using MCSAndroidAPI.Contracts;
using MCSAndroidAPI.Data;
using Microsoft.Extensions.Logging;

namespace MCSAndroidAPI.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly NidecMCSContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private ILoggerFactory _loggerFactory;

        private IAuthenticateRepository? _authenticate;
        private IProductionResultRepository? _productionResult;
        private ILotManRepository? _lotMan;
        private ILotDefectRepository? _lotDefect;
        private ILotScrapRepository? _lotScrap;
        private ILotStopRepository? _lotStop;
        private ILotStartRepository? _lotStart;
        private IDefectDetailRepository? _defectDetail;


        public RepositoryWrapper(NidecMCSContext context,IMapper mapper , IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
            _loggerFactory = loggerFactory;
        }
        #region Properties
        public IAuthenticateRepository Authenticate
        {
            get
            {
                if (_authenticate == null)
                {
                    var logger = _loggerFactory.CreateLogger<AuthenticateRepository>();
                    _authenticate = new AuthenticateRepository(_context, _mapper, _configuration, logger);
                }
                return _authenticate;
            }
        }
        public IProductionResultRepository ProductionResult
        {
            get
            {
                if (_productionResult == null)
                {
                    var logger = _loggerFactory.CreateLogger<ProductionResultRepository>();
                    _productionResult = new ProductionResultRepository(_context, _mapper, logger);
                }
                return _productionResult;
            }
        }
        public ILotManRepository LotMan
        {
            get
            {
                if (_lotMan == null)
                {
                    var logger = _loggerFactory.CreateLogger<LotManRepository>();
                    _lotMan = new LotManRepository(_context, _mapper, logger);
                }
                return _lotMan;
            }
        }

        public ILotDefectRepository LotDefect
        {
            get
            {
               if ( _lotDefect == null)
                {
                    var logger = _loggerFactory.CreateLogger<LotDefectRepository>();
                    _lotDefect = new LotDefectRepository(_context, _mapper, logger);
                }
               return _lotDefect;
            }
        }

        public ILotScrapRepository LotScrap
        {
            get
            {
                if (_lotScrap == null)
                {
                    var logger = _loggerFactory.CreateLogger< LotScrapRepository>();
                    _lotScrap = new LotScrapRepository(_context, _mapper, logger);
                }
                return _lotScrap;
            }
        }

        public ILotStopRepository LotStop
        {
            get
            {
                if (_lotStop == null)
                {
                    var logger = _loggerFactory.CreateLogger<LotStopRepository>();
                    _lotStop = new LotStopRepository(_context, _mapper, logger);
                }
                return _lotStop;
            }
        }

        public ILotStartRepository LotStart
        {
            get
            {
                if (_lotStart == null)
                {
                    var logger = _loggerFactory.CreateLogger<LotStartRepository>();
                    _lotStart = new LotStartRepository(_context, _mapper, logger);
                }
                return _lotStart;
            }
        }

        public IDefectDetailRepository DefectDetail
        {
            get
            {
                if (_defectDetail == null)
                {
                    var logger = _loggerFactory.CreateLogger<DefectDetailRepository>();
                    _defectDetail = new DefectDetailRepository(_context, _mapper, logger);
                }

                return _defectDetail;
            }
        }
        #endregion

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
