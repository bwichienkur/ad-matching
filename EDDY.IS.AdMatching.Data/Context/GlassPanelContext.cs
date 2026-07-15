using EDDY.IS.AdMatching.Entities;
using Microsoft.EntityFrameworkCore;
using TimeZone = EDDY.IS.AdMatching.Entities.TimeZone;

namespace EDDY.IS.AdMatching.Data.Context
{
    public partial class GlassPanelContext : DbContext
    {
        public GlassPanelContext(DbContextOptions<GlassPanelContext> options) : base(options)
        {
            //we do  track the changes to entities retrieved from the database into context
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public virtual DbSet<Ad> Ads { get; set; } = null!;
        public virtual DbSet<AdGroup> AdGroups { get; set; } = null!;
        public virtual DbSet<AdGroupAd> AdGroupAds { get; set; } = null!;
        public virtual DbSet<AdMatchingServiceParameter> AdMatchingServiceParameters { get; set; } = null!;
        public virtual DbSet<AdProvider> AdProviders { get; set; } = null!;
        public virtual DbSet<AdType> AdTypes { get; set; } = null!;
        public virtual DbSet<Campaign> Campaigns { get; set; } = null!;
        public virtual DbSet<CampaignCapReason> CampaignCapReasons { get; set; } = null!;
        public virtual DbSet<CampaignCapType> CampaignCapTypes { get; set; } = null!;
        public virtual DbSet<CampaignLevel> CampaignLevels { get; set; } = null!;
        public virtual DbSet<CampaignRelationship> CampaignRelationships { get; set; } = null!;
        public virtual DbSet<CampaignSchedule> CampaignSchedules { get; set; } = null!;
        public virtual DbSet<CampaignSource> CampaignSources { get; set; } = null!;
        public virtual DbSet<CampaignSpend> CampaignSpends { get; set; } = null!;
        public virtual DbSet<CampaignStop> CampaignStops { get; set; } = null!;
        public virtual DbSet<CampaignType> CampaignTypes { get; set; } = null!;
        public virtual DbSet<ClientAdAccount> ClientAdAccounts { get; set; } = null!;
        public virtual DbSet<ClientAdAccountBudget> ClientAdAccountBudgets { get; set; } = null!;
        public virtual DbSet<ClientAdAccountDefaultParameter> ClientAdAccountDefaultParameters { get; set; } = null!;
        public virtual DbSet<ClientAdAccountParameter> ClientAdAccountParameters { get; set; } = null!;
        public virtual DbSet<ClientAdAccountSpend> ClientAdAccountSpends { get; set; } = null!;
        public virtual DbSet<ClientAdAccountStop> ClientAdAccountStops { get; set; } = null!;
        public virtual DbSet<ConversionPixel> ConversionPixels { get; set; } = null!;
        public virtual DbSet<ConversionPixelType> ConversionPixelTypes { get; set; } = null!;
        public virtual DbSet<LineItem> LineItems { get; set; } = null!;
        public virtual DbSet<LineItemSubSource> LineItemSubSources { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<PermissionRole> PermissionRoles { get; set; } = null!;
        public virtual DbSet<Placement> Placements { get; set; } = null!;
        public virtual DbSet<PlacementGroup> PlacementGroups { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<ReportSchedule> ReportSchedules { get; set; } = null!;
        public virtual DbSet<ReportScheduleEmail> ReportScheduleEmails { get; set; } = null!;
        public virtual DbSet<ReportScheduleFrequency> ReportScheduleFrequencies { get; set; } = null!;
        public virtual DbSet<ReportType> ReportTypes { get; set; } = null!;
        public virtual DbSet<ScheduleOption> ScheduleOptions { get; set; } = null!;
        public virtual DbSet<Source> Sources { get; set; } = null!;
        public virtual DbSet<SourceClientAccess> SourceClientAccess { get; set; } = null!;
        public virtual DbSet<SourceGroup> SourceGroups { get; set; } = null!;
        public virtual DbSet<SourceGroupSource> SourceGroupSources { get; set; } = null!;
        public virtual DbSet<SourceProductType> SourceProductTypes { get; set; } = null!;
        public virtual DbSet<SubSource> SubSources { get; set; } = null!;
        public virtual DbSet<SubSourcePixelClientAdAccount> SubSourcePixelClientAdAccounts { get; set; } = null!;
        public virtual DbSet<TargetingRule> TargetingRules { get; set; } = null!;
        public virtual DbSet<TimeZone> TimeZones { get; set; } = null!;
        public virtual DbSet<StateTimeZone> StateTimeZones { get; set; } = null!;
        public virtual DbSet<TrackingParameter> TrackingParameters { get; set; } = null!;
        public virtual DbSet<VwAd> VwAds { get; set; } = null!;
        public virtual DbSet<VwAdGroup> VwAdGroups { get; set; } = null!;
        public virtual DbSet<VwAdGroupAd> VwAdGroupAds { get; set; } = null!;
        public virtual DbSet<VwAdLibrary> VwAdLibraries { get; set; } = null!;
        public virtual DbSet<VwAdProvider> VwAdProviders { get; set; } = null!;
        public virtual DbSet<VwAdsAm> VwAdsAms { get; set; } = null!;
        public virtual DbSet<SlimAd> SlimAds { get; set; } = null!;
        public virtual DbSet<VwAssignedPermission> VwAssignedPermissions { get; set; } = null!;
        public virtual DbSet<VwAvailableRole> VwAvailableRoles { get; set; } = null!;
        public virtual DbSet<VwCampaign> VwCampaigns { get; set; } = null!;
        public virtual DbSet<VwCampaignRelationship> VwCampaignRelationships { get; set; } = null!;
        public virtual DbSet<VwClientAdAccount> VwClientAdAccounts { get; set; } = null!;
        public virtual DbSet<VwClientRelationship> VwClientRelationships { get; set; } = null!;
        public virtual DbSet<VwNexusCampaign> VwNexusCampaigns { get; set; } = null!;
        public virtual DbSet<VwPlacement> VwPlacements { get; set; } = null!;
        public virtual DbSet<VwRole> VwRoles { get; set; } = null!;
        public virtual DbSet<VwRoleAdServer> VwRoleAdServers { get; set; } = null!;
        public virtual DbSet<VwRulesAreaOfStudy> VwRulesAreaOfStudies { get; set; } = null!;
        public virtual DbSet<VwRulesBrowser> VwRulesBrowsers { get; set; } = null!;
        public virtual DbSet<VwRulesBrowserPlatform> VwRulesBrowserPlatforms { get; set; } = null!;
        public virtual DbSet<VwRulesCollegeCredit> VwRulesCollegeCredits { get; set; } = null!;
        public virtual DbSet<VwRulesConnectionType> VwRulesConnectionTypes { get; set; } = null!;
        public virtual DbSet<VwRulesCountry> VwRulesCountries { get; set; } = null!;
        public virtual DbSet<VwRulesDegreeLevel> VwRulesDegreeLevels { get; set; } = null!;
        public virtual DbSet<VwRulesDeviceType> VwRulesDeviceTypes { get; set; } = null!;
        public virtual DbSet<VwRulesEducationLevel> VwRulesEducationLevels { get; set; } = null!;
        public virtual DbSet<VwRulesGender> VwRulesGenders { get; set; } = null!;
        public virtual DbSet<VwRulesHighSchoolGpa> VwRulesHighSchoolGpas { get; set; } = null!;
        public virtual DbSet<VwRulesIncome> VwRulesIncomes { get; set; } = null!;
        public virtual DbSet<VwRulesLearningPreference> VwRulesLearningPreferences { get; set; } = null!;
        public virtual DbSet<VwRulesMarketingUnit> VwRulesMarketingUnits { get; set; } = null!;
        public virtual DbSet<VwRulesMilitaryBranch> VwRulesMilitaryBranches { get; set; } = null!;
        public virtual DbSet<VwRulesPlannedStart> VwRulesPlannedStarts { get; set; } = null!;
        public virtual DbSet<VwRulesState> VwRulesStates { get; set; } = null!;
        public virtual DbSet<VwRulesSubChannel> VwRulesSubChannels { get; set; } = null!;
        public virtual DbSet<VwRulesSubject> VwRulesSubjects { get; set; } = null!;
        public virtual DbSet<VwSource> VwSources { get; set; } = null!;
        public virtual DbSet<VwSourceByCampaignAms> VwSourceByCampaigns { get; set; } = null!;
        public virtual DbSet<VwStandardClientReport> VwStandardClientReports { get; set; } = null!;
        public virtual DbSet<VwStandardSourceReport> VwStandardSourceReports { get; set; } = null!;
        public virtual DbSet<VwStandardStateReport> VwStandardStateReports { get; set; } = null!;
        public virtual DbSet<VwSubSource> VwSubSources { get; set; } = null!;
        public virtual DbSet<VwUsState> VwUsStates { get; set; } = null!;
        public virtual DbSet<VwUsZipCodeAm> VwUsZipCodeAms { get; set; } = null!;
        public virtual DbSet<VwUser> VwUsers { get; set; } = null!;
        public virtual DbSet<VwUserClaim> VwUserClaims { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                // optionsBuilder.UseSqlServer("Server=gp15-sql1;Database=GlassPanel;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ad>(entity =>
            {
                entity.ToTable("Ad", "AdServer");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsDeleted)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.BackupUrl).HasMaxLength(1024);

            });

            modelBuilder.Entity<AdGroup>(entity =>
            {
                entity.ToTable("AdGroup", "AdServer");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(250);

            });

            modelBuilder.Entity<AdGroupAd>(entity =>
            {
                entity.ToTable("AdGroupAd", "AdServer");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

            });

            
            modelBuilder.Entity<AdMatchingServiceParameter>(entity =>
            {
                entity.ToTable("AdMatchingServiceParameters", "AdServer");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ParameterName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParameterValue).IsUnicode(false);

                entity.Property(e => e.UpdatedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<AdProvider>(entity =>
            {
                entity.ToTable("AdProvider", "AdServer");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RequestAction)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceToken)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceUrl)
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasColumnName("ServiceURL");

                entity.Property(e => e.TestServiceUrl)
                    .HasMaxLength(2048)
                    .IsUnicode(false)
                    .HasColumnName("TestServiceURL");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<AdType>(entity =>
            {
                entity.ToTable("AdType", "AdServer");

                entity.Property(e => e.AdTypeId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Campaign>(entity =>
            {
                entity.ToTable("VW_CampaignAMS", "AdServer");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

            });
            
            modelBuilder.Entity<CampaignCapReason>(entity =>
            {
                entity.ToTable("CampaignCapReason", "AdServer");

                entity.Property(e => e.Description)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CampaignCapType>(entity =>
            {
                entity.ToTable("CampaignCapType", "AdServer");

                entity.Property(e => e.CampaignCapTypeId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CampaignLevel>(entity =>
            {
                entity.ToTable("CampaignLevel", "AdServer");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CampaignRelationship>(entity =>
            {
                entity.ToTable("CampaignRelationship", "AdServer");

            });

            modelBuilder.Entity<CampaignSchedule>(entity =>
            {
                entity.ToTable("CampaignSchedule", "AdServer");

                entity.HasOne(d => d.ScheduleOption)
                    .WithMany(p => p.CampaignSchedules)
                    .HasForeignKey(d => d.ScheduleOptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CampaignSchedule_ScheduleOption");
            });

            modelBuilder.Entity<CampaignSource>(entity =>
            {
                entity.ToTable("CampaignSource", "AdServer");

                entity.Property(e => e.BidMultiplier).HasColumnType("decimal(18, 4)");

            });

            modelBuilder.Entity<CampaignSpend>(entity =>
            {
                entity.ToTable("CampaignSpend", "AdServer");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.MonthlyClickCount).HasDefaultValueSql("((0))");

                entity.Property(e => e.MonthlySpend)
                    .HasColumnType("decimal(18, 4)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Spend).HasColumnType("decimal(18, 4)");

            });

            modelBuilder.Entity<CampaignStop>(entity =>
            {
                entity.ToTable("CampaignStop", "AdServer");

                entity.Property(e => e.BeginStop).HasColumnType("datetime");

                entity.Property(e => e.EndStop).HasColumnType("datetime");

                entity.Property(e => e.TimeZone)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

            });

            modelBuilder.Entity<CampaignType>(entity =>
            {
                entity.ToTable("CampaignType", "AdServer");

                entity.Property(e => e.CampaignTypeId).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ClientAdAccount>(entity =>
            {
                entity.ToTable("ClientAdAccount", "AdServer");

                entity.Property(e => e.InstitutionAlias)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

            });

            modelBuilder.Entity<ClientAdAccountBudget>(entity =>
            {
                entity.ToTable("ClientAdAccountBudget", "AdServer");

                entity.Property(e => e.AllocationType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InitialMonthAllocation)
                    .HasColumnType("decimal(18, 4)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.NextMonthAllocation).HasColumnType("decimal(18, 2)");


            });

            modelBuilder.Entity<ClientAdAccountDefaultParameter>(entity =>
            {
                entity.HasKey(e => e.ClientAdAccountDefaultParametersId);

                entity.ToTable("ClientAdAccountDefaultParameters", "AdServer");

                entity.Property(e => e.Category)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.IsEnabled).HasDefaultValueSql("((1))");

                entity.Property(e => e.Macro).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Notes).HasMaxLength(100);
            });

            modelBuilder.Entity<ClientAdAccountParameter>(entity =>
            {
                entity.HasKey(e => e.ClientAdAccountParametersId)
                    .HasName("PK_clientadaccountparameters");

                entity.ToTable("ClientAdAccountParameters", "AdServer");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IsRollingDates).HasDefaultValueSql("((0))");

                entity.Property(e => e.ParameterName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ParameterValue)
                    .HasMaxLength(1000)
                    .IsUnicode(false);


            });

            modelBuilder.Entity<ClientAdAccountSpend>(entity =>
            {
                entity.ToTable("ClientAdAccountSpend", "AdServer");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.MonthlySpend).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Spend).HasColumnType("decimal(18, 4)");

            });

            modelBuilder.Entity<ClientAdAccountStop>(entity =>
            {
                entity.ToTable("ClientAdAccountStop", "AdServer");

                entity.Property(e => e.BeginStop).HasColumnType("datetime");


                entity.Property(e => e.EndStop).HasColumnType("datetime");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.Property(e => e.StartTime).HasColumnType("time(2)");

                entity.Property(e => e.StopDate).HasColumnType("date");

                entity.Property(e => e.StopTime).HasColumnType("time(2)");

                entity.Property(e => e.TimeZone)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();


            });

            modelBuilder.Entity<ConversionPixel>(entity =>
            {
                entity.ToTable("ConversionPixel", "AdServer");

                entity.Property(e => e.Accronym)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PixelName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.HasOne(d => d.ConversionPixelType)
                    .WithMany(p => p.ConversionPixels)
                    .HasForeignKey(d => d.ConversionPixelTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ConversionPixelType_ConversionPixel");
            });

            modelBuilder.Entity<ConversionPixelType>(entity =>
            {
                entity.ToTable("ConversionPixelType", "AdServer");

                entity.Property(e => e.CostPerUnit)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GroupName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PixelTemplate)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.TypeName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.ToTable("LineItem", "AdServer");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.Placement)
                    .WithMany(p => p.LineItems)
                    .HasForeignKey(d => d.PlacementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineItem_Placement");
            });

            modelBuilder.Entity<LineItemSubSource>(entity =>
            {
                entity.ToTable("LineItemSubSource", "AdServer");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.LineItem)
                    .WithMany(p => p.LineItemSubSources)
                    .HasForeignKey(d => d.LineItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineItemSubSource_LineItem");

                entity.HasOne(d => d.SubSource)
                    .WithMany(p => p.LineItemSubSources)
                    .HasForeignKey(d => d.SubSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LineItemSubSource_SubSource");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission", "AdServer");

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PermissionRole>(entity =>
            {
                entity.HasKey(e => new { e.PermissionId, e.RoleId });

                entity.ToTable("PermissionRole", "AdServer");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.PermissionRoles)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PermissionRole_Permission");
            });

            modelBuilder.Entity<Placement>(entity =>
            {
                entity.ToTable("Placement", "AdServer");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Dedupe)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PlacementGroup>(entity =>
            {
                entity.ToTable("PlacementGroup", "AdServer");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.ToTable("ProductType", "AdServer");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReportSchedule>(entity =>
            {
                entity.ToTable("ReportSchedule", "AdServer");

                entity.Property(e => e.ContactEmail)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomDeliveryDate).HasColumnType("date");

                entity.Property(e => e.DeliveryTime).HasColumnType("time(0)");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastDeliveryDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.ReportDetailsJson).IsUnicode(false);

                entity.Property(e => e.UpdatedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.ReportScheduleFrequency)
                    .WithMany(p => p.ReportSchedules)
                    .HasForeignKey(d => d.ReportScheduleFrequencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportSchedule_ReportFrecuency");
            });

            modelBuilder.Entity<ReportScheduleEmail>(entity =>
            {
                entity.ToTable("ReportScheduleEmail", "AdServer");

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.ReportSchedule)
                    .WithMany(p => p.ReportScheduleEmails)
                    .HasForeignKey(d => d.ReportScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ReportScheduleEmail_ReportSchedule");
            });

            modelBuilder.Entity<ReportScheduleFrequency>(entity =>
            {
                entity.ToTable("ReportScheduleFrequency", "AdServer");

                entity.Property(e => e.ReportScheduleFrequencyId).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ReportType>(entity =>
            {
                entity.ToTable("ReportType", "AdServer");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ReportGroup)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<ScheduleOption>(entity =>
            {
                entity.ToTable("ScheduleOption", "AdServer");

                entity.Property(e => e.DayOfWeek)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Source>(entity =>
            {
                entity.ToTable("Source", "AdServer");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SourceGroup>(entity =>
            {
                entity.ToTable("SourceGroup", "AdServer");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(1000);

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SourceGroupSource>(entity =>
            {
                entity.ToTable("SourceGroupSource", "AdServer");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.SourceGroup)
                    .WithMany(p => p.SourceGroupSources)
                    .HasForeignKey(d => d.SourceGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SourceGroupSource_SourceGroup");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.SourceGroupSources)
                    .HasForeignKey(d => d.SourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SourceGroupSource_Source");
            });

            modelBuilder.Entity<SourceProductType>(entity =>
            {
                entity.ToTable("SourceProductType", "AdServer");

            });

            modelBuilder.Entity<SubSource>(entity =>
            {
                entity.ToTable("SubSource", "AdServer");

                entity.Property(e => e.BaseCpc)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("BaseCPC");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.LogVendorServiceCall).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name).HasMaxLength(255);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.AdProvider)
                    .WithMany(p => p.SubSources)
                    .HasForeignKey(d => d.AdProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubSource_AdProvider");

                entity.HasOne(d => d.Source)
                    .WithMany(p => p.SubSources)
                    .HasForeignKey(d => d.SourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubSource_Source");
            });

            modelBuilder.Entity<SubSourcePixelClientAdAccount>(entity =>
            {
                entity.ToTable("SubSourcePixelClientAdAccount", "AdServer");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.HasOne(d => d.SubSource)
                    .WithMany(p => p.SubSourcePixelClientAdAccounts)
                    .HasForeignKey(d => d.SubSourceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SubSource__SubSo__68F2894D");
            });

            modelBuilder.Entity<TargetingRule>(entity =>
            {
                entity.ToTable("TargetingRule", "AdServer");

                entity.Property(e => e.DynamicBoostPercent)
                    .HasColumnType("decimal(18, 4)")
                    .HasDefaultValueSql("((0))");

            });

            modelBuilder.Entity<TimeZone>(entity =>
            {
                entity.ToTable("TimeZone", "AdServer");

                entity.Property(e => e.Code)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StateTimeZone>(entity =>
            {
                entity.ToTable("StateTimeZone", "Common");

                entity.Property(e => e.StateCode)
                    .HasMaxLength(5)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.TimeZoneCode)
                    .HasMaxLength(50)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TrackingParameter>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("TrackingParameter", "AdServer");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MapTo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Source)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwAd>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Ad", "AdServer");

                entity.Property(e => e.AdType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Assignments)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignName).HasMaxLength(500);

                entity.Property(e => e.ClientAccountName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayUrl)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Headline)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwAdGroup>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_AdGroup", "AdServer");

                entity.Property(e => e.AveragePosition).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Bid).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CampaignName).HasMaxLength(500);

                entity.Property(e => e.Cpc)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("CPC");

                entity.Property(e => e.Cpgl)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("CPGL");

                entity.Property(e => e.Cptl)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("CPTL");

                entity.Property(e => e.Cpuip)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("CPUIP");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Crgl)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("CRGL");

                entity.Property(e => e.Crtl)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("CRTL");

                entity.Property(e => e.Cruip)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("CRUIP");

                entity.Property(e => e.Ctr)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("CTR");

                entity.Property(e => e.DestinationUrl)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Spend).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Tlroas)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("TLROAS");

                entity.Property(e => e.TotalLeadsRev).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UipixelCount).HasColumnName("UIPixelCount");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwAdGroupAd>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_AdGroupAds", "AdServer");

                entity.Property(e => e.AdGroupName).HasMaxLength(250);

                entity.Property(e => e.Assignments)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignName).HasMaxLength(500);

                entity.Property(e => e.ClientAccountName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayUrl)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Headline)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwAdLibrary>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_AdLibrary", "AdServer");

                entity.Property(e => e.AdGroupName).HasMaxLength(250);

                entity.Property(e => e.Assignments)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignName).HasMaxLength(500);

                entity.Property(e => e.ClientAccountName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Description).IsUnicode(false);

                entity.Property(e => e.DisplayUrl)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Headline)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwAdProvider>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_AdProvider", "AdServer");

                entity.Property(e => e.AdProviderId).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwAdsAm>(entity =>
            {
                entity.ToView("VW_AdsAMS", "AdServer");

                entity.Property(e => e.AdGroupName).HasMaxLength(250);

                entity.Property(e => e.BackupUrl)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignName)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.ClickUrl)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.ClientAccountName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Cpc)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("CPC");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.DisplayUrl)
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Headline)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(51)
                    .IsUnicode(false);

                entity.Property(e => e.ImageUrl)
                    .IsRequired()
                    .HasMaxLength(1024)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PopularPrograms).IsUnicode(false);

                entity.Property(e => e.RankMultiplier).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SchoolMultiplier).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<SlimAd>(entity =>
            {
                entity.ToView("VW_SlimAdsAMS", "AdServer");

                entity.Property(e => e.Id)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.SourceBid).HasColumnType("decimal(18, 4)");
            });

            modelBuilder.Entity<VwAssignedPermission>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_AssignedPermission", "AdServer");
            });

            modelBuilder.Entity<VwAvailableRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_AvailableRoles", "AdServer");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<VwCampaign>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Campaign", "AdServer");

                entity.Property(e => e.Allocation).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AllocationType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BeginStop).HasColumnType("datetime");

                entity.Property(e => e.CappedOutAt).HasColumnType("datetime");

                entity.Property(e => e.CpaValue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Cpc).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Cvr).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.DailyCap).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.EndStop).HasColumnType("datetime");

                entity.Property(e => e.Fill).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.Name).HasMaxLength(500);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Spend).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwCampaignRelationship>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_CampaignRelationships", "AdServer");

                entity.Property(e => e.CampaignType)
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(1003);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<VwClientAdAccount>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ClientAdAccount", "AdServer");

                entity.Property(e => e.Allocation).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BeginStop).HasColumnType("datetime");

                entity.Property(e => e.ClicksRep).HasMaxLength(129);

                entity.Property(e => e.ClientAccountName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.ClientServicesRep).HasMaxLength(129);

                entity.Property(e => e.EndStop).HasColumnType("datetime");

                entity.Property(e => e.Fill).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.InstitutionAlias)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Spend).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwClientRelationship>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_ClientRelationship", "AdServer");

                entity.Property(e => e.ClientName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.InstitutionId).HasColumnName("institutionId");

                entity.Property(e => e.InstitutionName).HasMaxLength(255);

                entity.Property(e => e.Name).HasMaxLength(268);

                entity.Property(e => e.UserManager).HasMaxLength(129);
            });

            modelBuilder.Entity<VwNexusCampaign>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_NexusCampaign", "AdServer");

                entity.Property(e => e.CampaignName)
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.DirectoryName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwPlacement>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Placement", "AdServer");

                entity.Property(e => e.LineItemCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.LineItemCreative)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.LineItemName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LineItemSearches)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PlacementCreatedDate).HasColumnType("datetime");

                entity.Property(e => e.PlacementCreative)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.PlacementName)
                    .HasMaxLength(300)
                    .IsUnicode(false);

                entity.Property(e => e.PlacementSearches)
                    .HasMaxLength(1)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRole>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Role", "AdServer");

                entity.Property(e => e.FirstName).HasMaxLength(64);

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.IsuserId).HasColumnName("ISUserId");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.RoleId).HasMaxLength(128);
            });

            modelBuilder.Entity<VwRoleAdServer>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_RoleAdServer", "AdServer");

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            modelBuilder.Entity<VwRulesAreaOfStudy>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_AreaOfStudy", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesBrowser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_Browser", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesBrowserPlatform>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_BrowserPlatform", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(7)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesCollegeCredit>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_CollegeCredits", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesConnectionType>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_ConnectionType", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(9)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesCountry>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_Country", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(14)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesDegreeLevel>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_DegreeLevel", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesDeviceType>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_DeviceType", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(8)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesEducationLevel>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_EducationLevel", "AdServer");

                entity.Property(e => e.Text)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesGender>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_Gender", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesHighSchoolGpa>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_HighSchoolGPA", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesIncome>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_Income", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(18)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesLearningPreference>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_LearningPreference", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesMarketingUnit>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_MarketingUnit", "AdServer");

                entity.Property(e => e.Text)
                    .HasMaxLength(323)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesMilitaryBranch>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_MilitaryBranch", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesPlannedStart>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_PlannedStart", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesState>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_State", "AdServer");

                entity.Property(e => e.Key)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwRulesSubChannel>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_SubChannel", "AdServer");

                entity.Property(e => e.Text)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Value).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwRulesSubject>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Rules_Subject", "AdServer");

                entity.Property(e => e.DummyId)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.GroupKey)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.GroupName)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Key)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Text)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Value)
                    .HasMaxLength(12)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSource>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_Source", "AdServer");

                entity.Property(e => e.ClientAccess)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(250);

                entity.Property(e => e.InstitutionInclusionExclusion)
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Products)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.Revenue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SourceGroups)
                    .HasMaxLength(3000)
                    .IsUnicode(false);

                entity.Property(e => e.SourceId).ValueGeneratedOnAdd();

                entity.Property(e => e.SourceName).HasMaxLength(100);

                entity.Property(e => e.Status)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.SubSources)
                    .HasMaxLength(3000)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwSourceByCampaignAms>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_SourceByCampaignAms", "AdServer");

                entity.Property(e => e.BidMultiplier).HasColumnType("decimal(18, 4)");

            });

            modelBuilder.Entity<VwStandardClientReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_StandardClientReport", "AdServer");

                entity.Property(e => e.AdCtr).HasColumnName("AdCTR");

                entity.Property(e => e.AdGroupName).HasMaxLength(250);

                entity.Property(e => e.AdName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AllocationType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CampaignName).HasMaxLength(500);

                entity.Property(e => e.ClientAccountName)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CrgoodLeads).HasColumnName("CRGoodLeads");

                entity.Property(e => e.CrtotalLeads).HasColumnName("CRTotalLeads");

                entity.Property(e => e.Cruipixel).HasColumnName("CRUIPixel");

                entity.Property(e => e.DeviceType)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.LandingPage)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.ProductType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Time)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.UipixelCount).HasColumnName("UIPixelCount");

                entity.Property(e => e.UserCity)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.UserCounty)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.UserState)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Vendor)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.VendorCreative)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.VendorLineItem)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.VendorPlacement)
                    .HasMaxLength(9)
                    .IsUnicode(false);

                entity.Property(e => e.VendorSubDeal)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.WeekdayOrWeekend)
                    .HasMaxLength(7)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwStandardSourceReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_StandardSourceReport", "AdServer");

                entity.Property(e => e.Ips).HasColumnName("IPS");

                entity.Property(e => e.Rpc).HasColumnName("RPC");

                entity.Property(e => e.Rps).HasColumnName("RPS");

                entity.Property(e => e.Source).HasMaxLength(100);

                entity.Property(e => e.SubSource).HasMaxLength(255);
            });

            modelBuilder.Entity<VwStandardStateReport>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_StandardStateReport", "AdServer");

                entity.Property(e => e.Cpc)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("CPC");

                entity.Property(e => e.Cpu)
                    .HasColumnType("decimal(18, 4)")
                    .HasColumnName("CPU");

                entity.Property(e => e.Cruip).HasColumnName("CRUIP");

                entity.Property(e => e.Ctr).HasColumnName("CTR");

                entity.Property(e => e.Spend).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).ValueGeneratedOnAdd();

                entity.Property(e => e.UipixelCount).HasColumnName("UIPixelCount");
            });

            modelBuilder.Entity<VwSubSource>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_SubSource", "AdServer");

                entity.Property(e => e.AdProviderName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Revenue).HasColumnType("decimal(18, 4)");

                entity.Property(e => e.SourceName).HasMaxLength(100);

                entity.Property(e => e.SubSourceName).HasMaxLength(255);
            });

            modelBuilder.Entity<VwUsState>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_US_State", "AdServer");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StateId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<VwUsZipCodeAm>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_US_Zip_Code_AMS", "AdServer");

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(5)
                    .HasColumnName("ZIPCode");
            });

            modelBuilder.Entity<VwUser>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_User", "AdServer");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(64);

                entity.Property(e => e.Id).HasMaxLength(128);

                entity.Property(e => e.IsuserId).HasColumnName("ISUserId");

                entity.Property(e => e.LastName).HasMaxLength(64);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<VwUserClaim>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("VW_UserClaims", "AdServer");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FirstName).HasMaxLength(64);

                entity.Property(e => e.IsuserId).HasColumnName("ISUserId");

                entity.Property(e => e.Name).HasMaxLength(256);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
