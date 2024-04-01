using System;
using System.Collections.Generic;
using System.Configuration;
using MCSAndroidAPI.Constants;
using Microsoft.EntityFrameworkCore;

namespace MCSAndroidAPI.Data;

public partial class NidecMCSContext : DbContext
{
    private readonly IConfiguration _configuration;
    public NidecMCSContext()
    {
        this._configuration = null!;
    }

    public NidecMCSContext(DbContextOptions<NidecMCSContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<AMaterial> AMaterials { get; set; }

    public virtual DbSet<MBomMaterial> MBomMaterials { get; set; }

    public virtual DbSet<MDefectReason> MDefectReasons { get; set; }

    public virtual DbSet<MDivision> MDivisions { get; set; }

    public virtual DbSet<MLine> MLines { get; set; }

    public virtual DbSet<MProcess> MProcesses { get; set; }

    public virtual DbSet<MProcessOpe> MProcessOpes { get; set; }

    public virtual DbSet<MProcessStage> MProcessStages { get; set; }

    public virtual DbSet<MRouting> MRoutings { get; set; }

    public virtual DbSet<MScrapReason> MScrapReasons { get; set; }

    public virtual DbSet<MShift> MShifts { get; set; }

    public virtual DbSet<MStage> MStages { get; set; }

    public virtual DbSet<MStageMaterial> MStageMaterials { get; set; }

    public virtual DbSet<MStopReason> MStopReasons { get; set; }

    public virtual DbSet<MUser> MUsers { get; set; }

    public virtual DbSet<MWorker> MWorkers { get; set; }

    public virtual DbSet<TDefectDetail> TDefectDetails { get; set; }

    public virtual DbSet<TLotDefect> TLotDefects { get; set; }

    public virtual DbSet<TLotGood> TLotGoods { get; set; }

    public virtual DbSet<TLotGoodDetail> TLotGoodDetails { get; set; }

    public virtual DbSet<TLotProduct> TLotProducts { get; set; }

    public virtual DbSet<TLotScrap> TLotScraps { get; set; }

    public virtual DbSet<TLotStop> TLotStops { get; set; }

    public virtual DbSet<TLotWorker> TLotWorkers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_configuration != null)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString(SystemConstants.MainConnectionString));
        }    
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AMaterial>(entity =>
        {
            entity.HasKey(e => e.MaterialCd);

            entity.ToTable("A_MATERIAL");

            entity.Property(e => e.MaterialCd)
                .HasMaxLength(30)
                .HasColumnName("MATERIAL_CD");
            entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.Division)
                .HasMaxLength(10)
                .HasColumnName("DIVISION");
            entity.Property(e => e.DrawingNo)
                .HasMaxLength(30)
                .HasColumnName("DRAWING_NO");
            entity.Property(e => e.MaterialName)
                .HasMaxLength(100)
                .HasColumnName("MATERIAL_NAME");
            entity.Property(e => e.MaterialType)
                .HasMaxLength(5)
                .HasColumnName("MATERIAL_TYPE");
            entity.Property(e => e.MrpController)
                .HasMaxLength(20)
                .HasColumnName("MRP_CONTROLLER");
            entity.Property(e => e.NumSpec01)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("NUM_SPEC01");
            entity.Property(e => e.NumSpec02)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("NUM_SPEC02");
            entity.Property(e => e.NumSpec03)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("NUM_SPEC03");
            entity.Property(e => e.NumSpec04)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("NUM_SPEC04");
            entity.Property(e => e.NumSpec05)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("NUM_SPEC05");
            entity.Property(e => e.NumSpec06)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("NUM_SPEC06");
            entity.Property(e => e.NumSpec07)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("NUM_SPEC07");
            entity.Property(e => e.NumSpec08)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("NUM_SPEC08");
            entity.Property(e => e.NumSpec09)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("NUM_SPEC09");
            entity.Property(e => e.NumSpec10)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("NUM_SPEC10");
            entity.Property(e => e.PackingNum)
                .HasColumnType("numeric(15, 0)")
                .HasColumnName("PACKING_NUM");
            entity.Property(e => e.ProdRef01)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF01");
            entity.Property(e => e.ProdRef02)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF02");
            entity.Property(e => e.ProdRef03)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF03");
            entity.Property(e => e.ProdRef04)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF04");
            entity.Property(e => e.ProdRef05)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF05");
            entity.Property(e => e.ProdRef06)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF06");
            entity.Property(e => e.ProdRef07)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF07");
            entity.Property(e => e.ProdRef08)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF08");
            entity.Property(e => e.ProdRef09)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF09");
            entity.Property(e => e.Spec01)
                .HasMaxLength(100)
                .HasColumnName("SPEC01");
            entity.Property(e => e.Spec02)
                .HasMaxLength(100)
                .HasColumnName("SPEC02");
            entity.Property(e => e.Spec03)
                .HasMaxLength(100)
                .HasColumnName("SPEC03");
            entity.Property(e => e.Spec04)
                .HasMaxLength(100)
                .HasColumnName("SPEC04");
            entity.Property(e => e.Spec05)
                .HasMaxLength(100)
                .HasColumnName("SPEC05");
            entity.Property(e => e.Spec06)
                .HasMaxLength(100)
                .HasColumnName("SPEC06");
            entity.Property(e => e.Spec07)
                .HasMaxLength(100)
                .HasColumnName("SPEC07");
            entity.Property(e => e.Spec08)
                .HasMaxLength(100)
                .HasColumnName("SPEC08");
            entity.Property(e => e.Spec09)
                .HasMaxLength(100)
                .HasColumnName("SPEC09");
            entity.Property(e => e.Spec10)
                .HasMaxLength(100)
                .HasColumnName("SPEC10");
            entity.Property(e => e.Unit)
                .HasMaxLength(5)
                .HasColumnName("UNIT");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("numeric(13, 4)")
                .HasColumnName("UNIT_PRICE");
            entity.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.ValuationClass)
                .HasMaxLength(10)
                .HasColumnName("VALUATION_CLASS");
        });

        modelBuilder.Entity<MBomMaterial>(entity =>
        {
            entity.HasKey(e => new { e.ModelNo, e.MaterialNo }).HasName("PK__M_BOM_MA__8F37B9B351FA155C");

            entity.ToTable("M_BOM_MATERIAL");

            entity.Property(e => e.ModelNo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MODEL_NO");
            entity.Property(e => e.MaterialNo)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("MATERIAL_NO");
            entity.Property(e => e.BomTxt)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BOM_TXT");
            entity.Property(e => e.BomTxt10)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("BOM_TXT10");
            entity.Property(e => e.BomTxt11)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("BOM_TXT11");
            entity.Property(e => e.BomTxt2)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("BOM_TXT2");
            entity.Property(e => e.BomTxt3)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("BOM_TXT3");
            entity.Property(e => e.BomTxt4)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("BOM_TXT4");
            entity.Property(e => e.BomTxt5)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("BOM_TXT5");
            entity.Property(e => e.BomTxt6)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("BOM_TXT6");
            entity.Property(e => e.BomTxt7)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("BOM_TXT7");
            entity.Property(e => e.BomTxt8)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("BOM_TXT8");
            entity.Property(e => e.BomTxt9)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("BOM_TXT9");
            entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.MakeBuyDiv)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("MAKE_BUY_DIV");
            entity.Property(e => e.MaterialNum)
                .HasColumnType("numeric(15, 6)")
                .HasColumnName("MATERIAL_NUM");
            entity.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("UPDATED_BY");
        });

        modelBuilder.Entity<MDefectReason>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.DefectRsnCd, e.ProcessCd }).HasName("PK__M_DEFECT__890C70E7202DAF9E");

            entity.ToTable("M_DEFECT_REASON");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.DefectRsnCd)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("DEFECT_RSN_CD");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.DefectRsnName)
                .HasMaxLength(100)
                .HasColumnName("DEFECT_RSN_NAME");
            entity.Property(e => e.ReasonBy)
                .HasMaxLength(1)
                .HasColumnName("REASON_BY");
            entity.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("UPDATED_BY");
        });

        modelBuilder.Entity<MDivision>(entity =>
        {
            entity.HasKey(e => e.DivisionCd);

            entity.ToTable("M_DIVISION");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.DivisionName)
                .HasMaxLength(50)
                .HasColumnName("DIVISION_NAME");
        });

        modelBuilder.Entity<MLine>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ProcessCd, e.LineCd });

            entity.ToTable("M_LINE");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(5)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.LineCd)
                .HasMaxLength(5)
                .HasColumnName("LINE_CD");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.LineName)
                .HasMaxLength(50)
                .HasColumnName("LINE_NAME");
            entity.Property(e => e.StdOnlineRate)
                .HasColumnType("numeric(9, 4)")
                .HasColumnName("STD_ONLINE_RATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<MProcess>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ProcessCd });

            entity.ToTable("M_PROCESS");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(5)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.Costcenter2Cd)
                .HasMaxLength(10)
                .HasColumnName("COSTCENTER2_CD");
            entity.Property(e => e.Costcenter3Cd)
                .HasMaxLength(10)
                .HasColumnName("COSTCENTER3_CD");
            entity.Property(e => e.CostcenterCd)
                .HasMaxLength(10)
                .HasColumnName("COSTCENTER_CD");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasDefaultValue("SYSTEM")
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.HandyMenu)
                .HasMaxLength(5)
                .HasColumnName("HANDY_MENU");
            entity.Property(e => e.ProcessMovexCd)
                .HasMaxLength(5)
                .HasColumnName("PROCESS_MOVEX_CD");
            entity.Property(e => e.ProcessName)
                .HasMaxLength(50)
                .HasColumnName("PROCESS_NAME");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasDefaultValue("SYSTEM")
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<MProcessOpe>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ProcessCd, e.OperationCd });

            entity.ToTable("M_PROCESS_OPE");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(5)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.OperationCd)
                .HasMaxLength(5)
                .HasColumnName("OPERATION_CD");
            entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.OperationName)
                .HasMaxLength(500)
                .HasColumnName("OPERATION_NAME");
            entity.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("UPDATED_BY");
        });

        modelBuilder.Entity<MProcessStage>(entity =>
        {
            entity.ToTable("M_PROCESS_STAGE");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
            entity.Property(e => e.DeletedFlg)
                .HasMaxLength(1)
                .HasColumnName("DELETED_FLG");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(5)
                .IsFixedLength()
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.StageCd)
                .HasMaxLength(20)
                .HasColumnName("STAGE_CD");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.UpdatedDate).HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<MRouting>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ProcessCd, e.RoutingCd });

            entity.ToTable("M_ROUTING");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(5)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.RoutingCd)
                .HasMaxLength(5)
                .HasColumnName("ROUTING_CD");
            entity.Property(e => e.Costcenter2Cd)
                .HasMaxLength(10)
                .HasColumnName("COSTCENTER2_CD");
            entity.Property(e => e.Costcenter3Cd)
                .HasMaxLength(10)
                .HasColumnName("COSTCENTER3_CD");
            entity.Property(e => e.CostcenterCd)
                .HasMaxLength(10)
                .HasColumnName("COSTCENTER_CD");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
            entity.Property(e => e.ProductionFlg)
                .HasMaxLength(5)
                .HasColumnName("PRODUCTION_FLG");
            entity.Property(e => e.RoutingEndFlg)
                .HasMaxLength(5)
                .HasColumnName("ROUTING_END_FLG");
            entity.Property(e => e.RoutingName)
                .HasMaxLength(50)
                .HasColumnName("ROUTING_NAME");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.UpdatedDate).HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<MScrapReason>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ScrapRsnCd, e.ProcessCd }).HasName("PK__M_SCRAP___D2BB1C412C9DB440");

            entity.ToTable("M_SCRAP_REASON");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ScrapRsnCd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("SCRAP_RSN_CD");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.ScrapRsnName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SCRAP_RSN_NAME");
            entity.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("UPDATED_BY");
        });

        modelBuilder.Entity<MShift>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ProcessCd, e.ShiftCd });

            entity.ToTable("M_SHIFT");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(5)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.ShiftCd)
                .HasMaxLength(5)
                .HasColumnName("SHIFT_CD");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("CREATED_DATE");
            entity.Property(e => e.End1).HasColumnName("END1");
            entity.Property(e => e.End2).HasColumnName("END2");
            entity.Property(e => e.End3).HasColumnName("END3");
            entity.Property(e => e.End4).HasColumnName("END4");
            entity.Property(e => e.End5).HasColumnName("END5");
            entity.Property(e => e.ShiftName)
                .HasMaxLength(30)
                .HasColumnName("SHIFT_NAME");
            entity.Property(e => e.Start1).HasColumnName("START1");
            entity.Property(e => e.Start2).HasColumnName("START2");
            entity.Property(e => e.Start3).HasColumnName("START3");
            entity.Property(e => e.Start4).HasColumnName("START4");
            entity.Property(e => e.Start5).HasColumnName("START5");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.UpdatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<MStage>(entity =>
        {
            entity.HasKey(e => e.StageCd);

            entity.ToTable("M_STAGE");

            entity.Property(e => e.StageCd)
                .HasMaxLength(20)
                .HasColumnName("STAGE_CD");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
            entity.Property(e => e.StageName).HasColumnName("STAGE_NAME");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.UpdatedDate).HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<MStageMaterial>(entity =>
        {
            entity.HasKey(e => new { e.ModelCd, e.ProcessStageId, e.MaterialCd });

            entity.ToTable("M_STAGE_MATERIAL");

            entity.Property(e => e.ModelCd)
                .HasMaxLength(30)
                .HasColumnName("MODEL_CD");
            entity.Property(e => e.ProcessStageId).HasColumnName("PROCESS_STAGE_ID");
            entity.Property(e => e.MaterialCd)
                .HasMaxLength(30)
                .HasColumnName("MATERIAL_CD");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
            entity.Property(e => e.DeletedFlg)
                .HasMaxLength(1)
                .HasColumnName("DELETED_FLG");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.UpdatedDate).HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<MStopReason>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.StopRsnCd, e.ProcessCd }).HasName("PK__M_STOP_R__BA394B5D4352C357");

            entity.ToTable("M_STOP_REASON");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.StopRsnCd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("STOP_RSN_CD");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.StopRsnName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("STOP_RSN_NAME");
            entity.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("UPDATED_BY");
        });

        modelBuilder.Entity<MUser>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK_sys_user");

            entity.ToTable("M_USER");

            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.CreateBy).HasMaxLength(50);
            entity.Property(e => e.CreateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Department).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.PassWord).HasMaxLength(255);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.UpdateBy).HasMaxLength(50);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserCode).HasMaxLength(50);
        });

        modelBuilder.Entity<MWorker>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.WorkerCd }).HasName("PK__tmp_ms_x__91C824E64376EBDB");

            entity.ToTable("M_WORKER");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.WorkerCd)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("WORKER_CD");
            entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.DeptCd)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("DEPT_CD");
            entity.Property(e => e.Photo)
                .HasColumnType("image")
                .HasColumnName("PHOTO");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(50)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.RankCd)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("RANK_CD");
            entity.Property(e => e.RegistFlg)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("REGIST_FLG");
            entity.Property(e => e.SearchFlg)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("SEARCH_FLG");
            entity.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.VdEndDay).HasColumnName("VD_END_DAY");
            entity.Property(e => e.VdStrDay).HasColumnName("VD_STR_DAY");
            entity.Property(e => e.WorkerGrpCd)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("WORKER_GRP_CD");
            entity.Property(e => e.WorkerName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("WORKER_NAME");
        });

        modelBuilder.Entity<TDefectDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_T_DEFECT_DETAIL_1");

            entity.ToTable("T_DEFECT_DETAIL");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
            entity.Property(e => e.DefectCd)
                .HasMaxLength(10)
                .HasColumnName("DEFECT_CD");
            entity.Property(e => e.DeleteFlg)
                .HasMaxLength(1)
                .HasColumnName("DELETE_FLG");
            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.Leader).HasColumnName("LEADER");
            entity.Property(e => e.LineCd)
                .HasMaxLength(5)
                .HasColumnName("LINE_CD");
            entity.Property(e => e.LotNo)
                .HasMaxLength(30)
                .HasColumnName("LOT_NO");
            entity.Property(e => e.Materials).HasColumnName("MATERIALS");
            entity.Property(e => e.Model)
                .HasMaxLength(30)
                .HasColumnName("MODEL");
            entity.Property(e => e.NgQty)
                .HasColumnType("numeric(13, 3)")
                .HasColumnName("NG_QTY");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(5)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.ShiftCd)
                .HasMaxLength(5)
                .HasColumnName("SHIFT_CD");
            entity.Property(e => e.StageCd)
                .HasMaxLength(20)
                .HasColumnName("STAGE_CD");
            entity.Property(e => e.StatusFlg)
                .HasMaxLength(1)
                .HasColumnName("STATUS_FLG");
            entity.Property(e => e.TerminalNo)
                .HasMaxLength(32)
                .HasColumnName("TERMINAL_NO");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.UpdatedDate).HasColumnName("UPDATED_DATE");
            entity.Property(e => e.WorkerCd).HasColumnName("WORKER_CD");
        });

        modelBuilder.Entity<TLotDefect>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ReportId, e.ProcessCd, e.MaterialCd, e.LotNo }).HasName("PK__T_LOT_DE__8445F823188C8DD6");

            entity.ToTable("T_LOT_DEFECT");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ReportId)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("REPORT_ID");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(10)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.MaterialCd)
                .HasMaxLength(30)
                .HasColumnName("MATERIAL_CD");
            entity.Property(e => e.LotNo)
                .HasMaxLength(50)
                .HasColumnName("LOT_NO");
            entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.DefectNote)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DEFECT_NOTE");
            entity.Property(e => e.DefectQty)
                .HasColumnType("numeric(9, 2)")
                .HasColumnName("DEFECT_QTY");
            entity.Property(e => e.DefectRsnCd)
                .HasMaxLength(10)
                .HasColumnName("DEFECT_RSN_CD");
            entity.Property(e => e.InputDiv)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("INPUT_DIV");
            entity.Property(e => e.ReportTime).HasColumnName("REPORT_TIME");
            entity.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .HasColumnName("UPDATED_BY");
        });

        modelBuilder.Entity<TLotGood>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ProcessCd, e.MaterialCd, e.LotNo, e.BoxNo });

            entity.ToTable("T_LOT_GOOD");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(5)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.MaterialCd)
                .HasMaxLength(30)
                .HasColumnName("MATERIAL_CD");
            entity.Property(e => e.LotNo)
                .HasMaxLength(50)
                .HasColumnName("LOT_NO");
            entity.Property(e => e.BoxNo)
                .HasMaxLength(10)
                .HasColumnName("BOX_NO");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
            entity.Property(e => e.MoNo)
                .HasMaxLength(10)
                .HasColumnName("MO_NO");
            entity.Property(e => e.ProdDate).HasColumnName("PROD_DATE");
            entity.Property(e => e.ProdEndTime).HasColumnName("PROD_END_TIME");
            entity.Property(e => e.ProdQty)
                .HasColumnType("decimal(10, 0)")
                .HasColumnName("PROD_QTY");
            entity.Property(e => e.ProdStartTime).HasColumnName("PROD_START_TIME");
            entity.Property(e => e.ProdStatus)
                .HasMaxLength(5)
                .HasColumnName("PROD_STATUS");
            entity.Property(e => e.ProdTact)
                .HasColumnType("decimal(14, 4)")
                .HasColumnName("PROD_TACT");
            entity.Property(e => e.ProdWorkunit)
                .HasColumnType("decimal(14, 4)")
                .HasColumnName("PROD_WORKUNIT");
            entity.Property(e => e.SapLinkResult)
                .HasMaxLength(255)
                .HasColumnName("SAP_LINK_RESULT");
            entity.Property(e => e.SapLinkStatus)
                .HasMaxLength(10)
                .HasColumnName("SAP_LINK_STATUS");
            entity.Property(e => e.SapRemainQty)
                .HasColumnType("decimal(13, 2)")
                .HasColumnName("SAP_REMAIN_QTY");
            entity.Property(e => e.SapUpdateFlg)
                .HasMaxLength(5)
                .HasColumnName("SAP_UPDATE_FLG");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.UpdatedDate).HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<TLotGoodDetail>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ProcessCd, e.MaterialCd, e.LotNo, e.BoxNo, e.RoutingCd });

            entity.ToTable("T_LOT_GOOD_DETAIL");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(10)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.MaterialCd)
                .HasMaxLength(30)
                .HasColumnName("MATERIAL_CD");
            entity.Property(e => e.LotNo)
                .HasMaxLength(50)
                .HasColumnName("LOT_NO");
            entity.Property(e => e.BoxNo)
                .HasMaxLength(10)
                .HasColumnName("BOX_NO");
            entity.Property(e => e.RoutingCd)
                .HasMaxLength(5)
                .HasColumnName("ROUTING_CD");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
            entity.Property(e => e.ProdPicCd)
                .HasMaxLength(10)
                .HasColumnName("PROD_PIC_CD");
            entity.Property(e => e.ProdScanDate).HasColumnName("PROD_SCAN_DATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.UpdatedDate).HasColumnName("UPDATED_DATE");
        });

        modelBuilder.Entity<TLotProduct>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ProcessCd, e.ProductNo, e.LotNo });

            entity.ToTable("T_LOT_PRODUCT");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(5)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.ProductNo)
                .HasMaxLength(30)
                .HasColumnName("PRODUCT_NO");
            entity.Property(e => e.LotNo)
                .HasMaxLength(50)
                .HasColumnName("LOT_NO");
            entity.Property(e => e.BoxNoCount)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("BOX_NO_COUNT");
            entity.Property(e => e.Cavity).HasColumnName("CAVITY");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.CreatedDate).HasColumnName("CREATED_DATE");
            entity.Property(e => e.Defect2Qty)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("DEFECT2_QTY");
            entity.Property(e => e.Defect2Rate)
                .HasColumnType("numeric(9, 4)")
                .HasColumnName("DEFECT2_RATE");
            entity.Property(e => e.DefectQty)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("DEFECT_QTY");
            entity.Property(e => e.DefectRate)
                .HasColumnType("numeric(9, 4)")
                .HasColumnName("DEFECT_RATE");
            entity.Property(e => e.GoodQty)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("GOOD_QTY");
            entity.Property(e => e.LeaderWorkerNo)
                .HasMaxLength(10)
                .HasColumnName("LEADER_WORKER_NO");
            entity.Property(e => e.LineCd)
                .HasMaxLength(5)
                .HasColumnName("LINE_CD");
            entity.Property(e => e.LotStatusCode)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("LOT_STATUS_CODE");
            entity.Property(e => e.MoNo)
                .HasMaxLength(10)
                .HasColumnName("MO_NO");
            entity.Property(e => e.OnlineHr)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("ONLINE_HR");
            entity.Property(e => e.OnlineRate)
                .HasColumnType("numeric(9, 4)")
                .HasColumnName("ONLINE_RATE");
            entity.Property(e => e.PlanQty)
                .HasColumnType("numeric(9, 0)")
                .HasColumnName("PLAN_QTY");
            entity.Property(e => e.ProdRef01)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF01");
            entity.Property(e => e.ProdRef02)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF02");
            entity.Property(e => e.ProdRef03)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF03");
            entity.Property(e => e.ProdRef04)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF04");
            entity.Property(e => e.ProdRef05)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF05");
            entity.Property(e => e.ProdRef06)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF06");
            entity.Property(e => e.ProdRef07)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF07");
            entity.Property(e => e.ProdRef08)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF08");
            entity.Property(e => e.ProdRef09)
                .HasMaxLength(50)
                .HasColumnName("PROD_REF09");
            entity.Property(e => e.ProductDay)
                .HasMaxLength(8)
                .HasColumnName("PRODUCT_DAY");
            entity.Property(e => e.ProductEndDt).HasColumnName("PRODUCT_END_DT");
            entity.Property(e => e.ProductStrDt).HasColumnName("PRODUCT_STR_DT");
            entity.Property(e => e.RestHr)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("REST_HR");
            entity.Property(e => e.ShiftCd)
                .HasMaxLength(5)
                .HasColumnName("SHIFT_CD");
            entity.Property(e => e.StopHr)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("STOP_HR");
            entity.Property(e => e.TactNum)
                .HasColumnType("numeric(14, 4)")
                .HasColumnName("TACT_NUM");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.UpdatedDate).HasColumnName("UPDATED_DATE");
            entity.Property(e => e.WorkerNum)
                .HasColumnType("decimal(10, 3)")
                .HasColumnName("WORKER_NUM");
            entity.Property(e => e.WorkunitNum)
                .HasColumnType("numeric(14, 4)")
                .HasColumnName("WORKUNIT_NUM");
        });

        modelBuilder.Entity<TLotScrap>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ReportId, e.ProcessCd, e.MaterialCd, e.LotNo }).HasName("PK__T_LOT_SC__8445F823568D6ACE");

            entity.ToTable("T_LOT_SCRAP");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ReportId)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("REPORT_ID");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(10)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.MaterialCd)
                .HasMaxLength(30)
                .HasColumnName("MATERIAL_CD");
            entity.Property(e => e.LotNo)
                .HasMaxLength(50)
                .HasColumnName("LOT_NO");
            entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.InputDiv)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("INPUT_DIV");
            entity.Property(e => e.ItemName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ITEM_NAME");
            entity.Property(e => e.ItemNo)
                .HasMaxLength(15)
                .HasColumnName("ITEM_NO");
            entity.Property(e => e.ReportTime).HasColumnName("REPORT_TIME");
            entity.Property(e => e.ScrapAmt)
                .HasColumnType("numeric(11, 4)")
                .HasColumnName("SCRAP_AMT");
            entity.Property(e => e.ScrapNote)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SCRAP_NOTE");
            entity.Property(e => e.ScrapQty)
                .HasColumnType("numeric(9, 2)")
                .HasColumnName("SCRAP_QTY");
            entity.Property(e => e.ScrapRsnCd)
                .HasMaxLength(5)
                .HasColumnName("SCRAP_RSN_CD");
            entity.Property(e => e.UnitPrice)
                .HasColumnType("numeric(11, 4)")
                .HasColumnName("UNIT_PRICE");
            entity.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .HasColumnName("UPDATED_BY");
        });

        modelBuilder.Entity<TLotStop>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ReportId, e.ProcessCd, e.MaterialCd, e.LotNo }).HasName("PK__T_LOT_ST__8445F823FD78D308");

            entity.ToTable("T_LOT_STOP");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ReportId)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("REPORT_ID");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(10)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.MaterialCd)
                .HasMaxLength(30)
                .HasColumnName("MATERIAL_CD");
            entity.Property(e => e.LotNo)
                .HasMaxLength(50)
                .HasColumnName("LOT_NO");
            entity.Property(e => e.CreateDate).HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(10)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.InputDiv)
                .HasColumnType("numeric(1, 0)")
                .HasColumnName("INPUT_DIV");
            entity.Property(e => e.StopEndTime).HasColumnName("STOP_END_TIME");
            entity.Property(e => e.StopHr)
                .HasColumnType("numeric(6, 2)")
                .HasColumnName("STOP_HR");
            entity.Property(e => e.StopNote)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("STOP_NOTE");
            entity.Property(e => e.StopRsnCd)
                .HasMaxLength(5)
                .HasColumnName("STOP_RSN_CD");
            entity.Property(e => e.StopStrTime).HasColumnName("STOP_STR_TIME");
            entity.Property(e => e.UpdateDate).HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(10)
                .HasColumnName("UPDATED_BY");
        });

        modelBuilder.Entity<TLotWorker>(entity =>
        {
            entity.HasKey(e => new { e.DivisionCd, e.ProcessCd, e.MaterialCd, e.LotNo, e.ReportId });

            entity.ToTable("T_LOT_WORKER");

            entity.Property(e => e.DivisionCd)
                .HasMaxLength(5)
                .HasColumnName("DIVISION_CD");
            entity.Property(e => e.ProcessCd)
                .HasMaxLength(10)
                .HasColumnName("PROCESS_CD");
            entity.Property(e => e.MaterialCd)
                .HasMaxLength(30)
                .HasColumnName("MATERIAL_CD");
            entity.Property(e => e.LotNo)
                .HasMaxLength(50)
                .HasColumnName("LOT_NO");
            entity.Property(e => e.ReportId)
                .HasColumnType("numeric(5, 0)")
                .HasColumnName("REPORT_ID");
            entity.Property(e => e.CreateDate)
                .HasColumnType("datetime")
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("CREATED_BY");
            entity.Property(e => e.InputDiv)
                .HasMaxLength(5)
                .HasColumnName("INPUT_DIV");
            entity.Property(e => e.JobCd)
                .HasMaxLength(5)
                .HasColumnName("JOB_CD");
            entity.Property(e => e.LotWorkerNote)
                .HasMaxLength(100)
                .HasColumnName("LOT_WORKER_NOTE");
            entity.Property(e => e.MachineCd)
                .HasMaxLength(5)
                .HasColumnName("MACHINE_CD");
            entity.Property(e => e.ReportTime)
                .HasColumnType("datetime")
                .HasColumnName("REPORT_TIME");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("UPDATE_DATE");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(50)
                .HasColumnName("UPDATED_BY");
            entity.Property(e => e.WorkerCd)
                .HasMaxLength(10)
                .HasColumnName("WORKER_CD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
