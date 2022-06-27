﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KingITProject.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class KingITDBEntities : DbContext
    {
        public KingITDBEntities()
            : base("name=KingITDBEntities1")
        {
        }
        public KingITDBEntities(string s)
            : base(s)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<employer> employers { get; set; }
        public virtual DbSet<hall> halls { get; set; }
        public virtual DbSet<mall> malls { get; set; }
        public virtual DbSet<poste> postes { get; set; }
        public virtual DbSet<rent> rents { get; set; }
        public virtual DbSet<status> statuses { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
        public virtual DbSet<tenant> tenants { get; set; }
        public virtual DbSet<getHallsView> getHallsViews { get; set; }
        public virtual DbSet<statMallView> statMallViews { get; set; }
    
        [DbFunction("KingITDBEntities1", "getHalls")]
        public virtual IQueryable<getHalls_Result> getHalls(Nullable<int> current)
        {
            var currentParameter = current.HasValue ?
                new ObjectParameter("current", current) :
                new ObjectParameter("current", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<getHalls_Result>("[KingITDBEntities1].[getHalls](@current)", currentParameter);
        }
    
        [DbFunction("KingITDBEntities1", "getMalls")]
        public virtual IQueryable<getMalls_Result> getMalls()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<getMalls_Result>("[KingITDBEntities1].[getMalls]()");
        }
    
        public virtual int change_rent_date()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("change_rent_date");
        }
    
        public virtual int rentHall(Nullable<int> hall_id, Nullable<bool> status_action, string hall_number, Nullable<int> mall_id, Nullable<System.DateTime> start_date, Nullable<System.DateTime> end_date, Nullable<int> tenant_id, Nullable<int> employer_id)
        {
            var hall_idParameter = hall_id.HasValue ?
                new ObjectParameter("hall_id", hall_id) :
                new ObjectParameter("hall_id", typeof(int));
    
            var status_actionParameter = status_action.HasValue ?
                new ObjectParameter("status_action", status_action) :
                new ObjectParameter("status_action", typeof(bool));
    
            var hall_numberParameter = hall_number != null ?
                new ObjectParameter("hall_number", hall_number) :
                new ObjectParameter("hall_number", typeof(string));
    
            var mall_idParameter = mall_id.HasValue ?
                new ObjectParameter("mall_id", mall_id) :
                new ObjectParameter("mall_id", typeof(int));
    
            var start_dateParameter = start_date.HasValue ?
                new ObjectParameter("start_date", start_date) :
                new ObjectParameter("start_date", typeof(System.DateTime));
    
            var end_dateParameter = end_date.HasValue ?
                new ObjectParameter("end_date", end_date) :
                new ObjectParameter("end_date", typeof(System.DateTime));
    
            var tenant_idParameter = tenant_id.HasValue ?
                new ObjectParameter("tenant_id", tenant_id) :
                new ObjectParameter("tenant_id", typeof(int));
    
            var employer_idParameter = employer_id.HasValue ?
                new ObjectParameter("employer_id", employer_id) :
                new ObjectParameter("employer_id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("rentHall", hall_idParameter, status_actionParameter, hall_numberParameter, mall_idParameter, start_dateParameter, end_dateParameter, tenant_idParameter, employer_idParameter);
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
    
        public virtual ObjectResult<statMall_Result> statMall()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<statMall_Result>("statMall");
        }
    }
}
