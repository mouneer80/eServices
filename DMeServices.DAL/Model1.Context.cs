//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DMeServices.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class eServicesEntities : DbContext
    {
        public eServicesEntities()
            : base("name=eServicesEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<BldAttachmentTypes> BldAttachmentTypes { get; set; }
        public virtual DbSet<BldPermits> BldPermits { get; set; }
        public virtual DbSet<BldPermitsAttachmentDetails> BldPermitsAttachmentDetails { get; set; }
        public virtual DbSet<Groups> Groups { get; set; }
        public virtual DbSet<HsContractAttachments> HsContractAttachments { get; set; }
        public virtual DbSet<HsInspectors> HsInspectors { get; set; }
        public virtual DbSet<HsPermitActivity> HsPermitActivity { get; set; }
        public virtual DbSet<HsPermitAdvertisement> HsPermitAdvertisement { get; set; }
        public virtual DbSet<HsPermitDuration> HsPermitDuration { get; set; }
        public virtual DbSet<HsPermits> HsPermits { get; set; }
        public virtual DbSet<HsPermitsInspection> HsPermitsInspection { get; set; }
        public virtual DbSet<HsPermitSubActivity> HsPermitSubActivity { get; set; }
        public virtual DbSet<HsPermitType> HsPermitType { get; set; }
        public virtual DbSet<LookupTypes> LookupTypes { get; set; }
        public virtual DbSet<MociData> MociData { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<BldPermitsAttachments> BldPermitsAttachments { get; set; }
        public virtual DbSet<RevBanks> RevBanks { get; set; }
        public virtual DbSet<RevDepartments> RevDepartments { get; set; }
        public virtual DbSet<RevFinancialItems> RevFinancialItems { get; set; }
        public virtual DbSet<RevFinancialSubItems> RevFinancialSubItems { get; set; }
        public virtual DbSet<RevLeaseContracts> RevLeaseContracts { get; set; }
        public virtual DbSet<RevLeaseContractsLog> RevLeaseContractsLog { get; set; }
        public virtual DbSet<RevMainPayments> RevMainPayments { get; set; }
        public virtual DbSet<RevMainPaymentsLog> RevMainPaymentsLog { get; set; }
        public virtual DbSet<RevWelyat> RevWelyat { get; set; }
        public virtual DbSet<States> States { get; set; }
        public virtual DbSet<ISIC4_LEVEL_1> ISIC4_LEVEL_1 { get; set; }
        public virtual DbSet<ISIC4_LEVEL_4> ISIC4_LEVEL_4 { get; set; }
        public virtual DbSet<ISIC4_LEVEL_5> ISIC4_LEVEL_5 { get; set; }
        public virtual DbSet<ISIC4_LEVEL_2> ISIC4_LEVEL_2 { get; set; }
        public virtual DbSet<ISIC4_LEVEL_3> ISIC4_LEVEL_3 { get; set; }
        public virtual DbSet<ISIC4_LEVEL_6> ISIC4_LEVEL_6 { get; set; }
        public virtual DbSet<BldRegions> BldRegions { get; set; }
        public virtual DbSet<BldWelyat> BldWelyat { get; set; }
        public virtual DbSet<BldBuildingTypes> BldBuildingTypes { get; set; }
        public virtual DbSet<BldLandUseTypes> BldLandUseTypes { get; set; }
        public virtual DbSet<BldSquareLetters> BldSquareLetters { get; set; }
        public virtual DbSet<BldLandSurvey> BldLandSurvey { get; set; }
        public virtual DbSet<docs> docs { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<keys> keys { get; set; }
        public virtual DbSet<objs> objs { get; set; }
        public virtual DbSet<pdfs> pdfs { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<FinancialItems> FinancialItems { get; set; }
        public virtual DbSet<pges> pges { get; set; }
        public virtual DbSet<BldIndustrialAreasSurvey> BldIndustrialAreasSurvey { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<EmployeeGroups> EmployeeGroups { get; set; }
        public virtual DbSet<Employees> Employees { get; set; }
        public virtual DbSet<BldWorkersHousingDetails> BldWorkersHousingDetails { get; set; }
        public virtual DbSet<BldSupervisionServices> BldSupervisionServices { get; set; }
        public virtual DbSet<BldSupervisionServicesTypes> BldSupervisionServicesTypes { get; set; }
        public virtual DbSet<ConsultantOccupations> ConsultantOccupations { get; set; }
        public virtual DbSet<BldPaymentDetails> BldPaymentDetails { get; set; }
        public virtual DbSet<BldServiceFees> BldServiceFees { get; set; }
        public virtual DbSet<BldPayment> BldPayment { get; set; }
        public virtual DbSet<BldAudits> BldAudits { get; set; }
        public virtual DbSet<BldPermits_Log> BldPermits_Log { get; set; }
        public virtual DbSet<BldSupervisionContractors> BldSupervisionContractors { get; set; }
        public virtual DbSet<BldTransactions> BldTransactions { get; set; }
        public virtual DbSet<BldTransactionTypes> BldTransactionTypes { get; set; }
        public virtual DbSet<BldOwners> BldOwners { get; set; }
    
        [DbFunction("eServicesEntities", "GetPermitsDuration")]
        public virtual IQueryable<GetPermitsDuration_Result> GetPermitsDuration(Nullable<int> permitId)
        {
            var permitIdParameter = permitId.HasValue ?
                new ObjectParameter("PermitId", permitId) :
                new ObjectParameter("PermitId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetPermitsDuration_Result>("[eServicesEntities].[GetPermitsDuration](@PermitId)", permitIdParameter);
        }
    
        [DbFunction("eServicesEntities", "GetPermitsDurationRequests")]
        public virtual IQueryable<GetPermitsDurationRequests_Result> GetPermitsDurationRequests(Nullable<int> permitId)
        {
            var permitIdParameter = permitId.HasValue ?
                new ObjectParameter("PermitId", permitId) :
                new ObjectParameter("PermitId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<GetPermitsDurationRequests_Result>("[eServicesEntities].[GetPermitsDurationRequests](@PermitId)", permitIdParameter);
        }
    
        public virtual ObjectResult<MOCI_Permit_BY_MunicipalNo_HeadSection_Result> MOCI_Permit_BY_MunicipalNo_HeadSection(Nullable<long> municipalNo)
        {
            var municipalNoParameter = municipalNo.HasValue ?
                new ObjectParameter("MunicipalNo", municipalNo) :
                new ObjectParameter("MunicipalNo", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOCI_Permit_BY_MunicipalNo_HeadSection_Result>("MOCI_Permit_BY_MunicipalNo_HeadSection", municipalNoParameter);
        }
    
        public virtual ObjectResult<MOCI_Permit_Inspector_Result> MOCI_Permit_Inspector(Nullable<int> inspectorNo)
        {
            var inspectorNoParameter = inspectorNo.HasValue ?
                new ObjectParameter("InspectorNo", inspectorNo) :
                new ObjectParameter("InspectorNo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOCI_Permit_Inspector_Result>("MOCI_Permit_Inspector", inspectorNoParameter);
        }
    
        public virtual ObjectResult<MOCI_Permits_BY_CIVILID_Result> MOCI_Permits_BY_CIVILID(string civilID)
        {
            var civilIDParameter = civilID != null ?
                new ObjectParameter("CivilID", civilID) :
                new ObjectParameter("CivilID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOCI_Permits_BY_CIVILID_Result>("MOCI_Permits_BY_CIVILID", civilIDParameter);
        }
    
        public virtual ObjectResult<MOCI_Permits_BY_MunicipalNo_Result> MOCI_Permits_BY_MunicipalNo(Nullable<long> municipalNo, string cIVIL_ID)
        {
            var municipalNoParameter = municipalNo.HasValue ?
                new ObjectParameter("MunicipalNo", municipalNo) :
                new ObjectParameter("MunicipalNo", typeof(long));
    
            var cIVIL_IDParameter = cIVIL_ID != null ?
                new ObjectParameter("CIVIL_ID", cIVIL_ID) :
                new ObjectParameter("CIVIL_ID", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOCI_Permits_BY_MunicipalNo_Result>("MOCI_Permits_BY_MunicipalNo", municipalNoParameter, cIVIL_IDParameter);
        }
    
        public virtual ObjectResult<MOCI_Permits_BY_MunicipalNo_HeadSection_Result> MOCI_Permits_BY_MunicipalNo_HeadSection(Nullable<long> municipalNo)
        {
            var municipalNoParameter = municipalNo.HasValue ?
                new ObjectParameter("MunicipalNo", municipalNo) :
                new ObjectParameter("MunicipalNo", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOCI_Permits_BY_MunicipalNo_HeadSection_Result>("MOCI_Permits_BY_MunicipalNo_HeadSection", municipalNoParameter);
        }
    
        public virtual ObjectResult<MOCI_Permits_BY_MunicipalNo_Revenue_Result> MOCI_Permits_BY_MunicipalNo_Revenue(Nullable<long> municipalNo)
        {
            var municipalNoParameter = municipalNo.HasValue ?
                new ObjectParameter("MunicipalNo", municipalNo) :
                new ObjectParameter("MunicipalNo", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOCI_Permits_BY_MunicipalNo_Revenue_Result>("MOCI_Permits_BY_MunicipalNo_Revenue", municipalNoParameter);
        }
    
        public virtual ObjectResult<MOCI_Permits_HeadSection_Result> MOCI_Permits_HeadSection()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOCI_Permits_HeadSection_Result>("MOCI_Permits_HeadSection");
        }
    
        public virtual ObjectResult<MOCI_Permits_Inspector_Result> MOCI_Permits_Inspector(Nullable<int> inspectorNo)
        {
            var inspectorNoParameter = inspectorNo.HasValue ?
                new ObjectParameter("InspectorNo", inspectorNo) :
                new ObjectParameter("InspectorNo", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOCI_Permits_Inspector_Result>("MOCI_Permits_Inspector", inspectorNoParameter);
        }
    
        public virtual ObjectResult<MOCI_Permits_Renew_Finish_Result> MOCI_Permits_Renew_Finish()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOCI_Permits_Renew_Finish_Result>("MOCI_Permits_Renew_Finish");
        }
    
        public virtual ObjectResult<MOCI_Permits_Revenue_Result> MOCI_Permits_Revenue()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MOCI_Permits_Revenue_Result>("MOCI_Permits_Revenue");
        }
    
        public virtual ObjectResult<PermitAdvertisement_Result> PermitAdvertisement(Nullable<long> municipalNo)
        {
            var municipalNoParameter = municipalNo.HasValue ?
                new ObjectParameter("MunicipalNo", municipalNo) :
                new ObjectParameter("MunicipalNo", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<PermitAdvertisement_Result>("PermitAdvertisement", municipalNoParameter);
        }
    
        public virtual int sp_alterdiagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_alterdiagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_creatediagram(string diagramname, Nullable<int> owner_id, Nullable<int> version, byte[] definition)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var versionParameter = version.HasValue ?
                new ObjectParameter("version", version) :
                new ObjectParameter("version", typeof(int));
    
            var definitionParameter = definition != null ?
                new ObjectParameter("definition", definition) :
                new ObjectParameter("definition", typeof(byte[]));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_creatediagram", diagramnameParameter, owner_idParameter, versionParameter, definitionParameter);
        }
    
        public virtual int sp_dropdiagram(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_dropdiagram", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagramdefinition_Result> sp_helpdiagramdefinition(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagramdefinition_Result>("sp_helpdiagramdefinition", diagramnameParameter, owner_idParameter);
        }
    
        public virtual ObjectResult<sp_helpdiagrams_Result> sp_helpdiagrams(string diagramname, Nullable<int> owner_id)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_helpdiagrams_Result>("sp_helpdiagrams", diagramnameParameter, owner_idParameter);
        }
    
        public virtual int sp_renamediagram(string diagramname, Nullable<int> owner_id, string new_diagramname)
        {
            var diagramnameParameter = diagramname != null ?
                new ObjectParameter("diagramname", diagramname) :
                new ObjectParameter("diagramname", typeof(string));
    
            var owner_idParameter = owner_id.HasValue ?
                new ObjectParameter("owner_id", owner_id) :
                new ObjectParameter("owner_id", typeof(int));
    
            var new_diagramnameParameter = new_diagramname != null ?
                new ObjectParameter("new_diagramname", new_diagramname) :
                new ObjectParameter("new_diagramname", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_renamediagram", diagramnameParameter, owner_idParameter, new_diagramnameParameter);
        }
    
        public virtual int sp_upgraddiagrams()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_upgraddiagrams");
        }
    }
}
