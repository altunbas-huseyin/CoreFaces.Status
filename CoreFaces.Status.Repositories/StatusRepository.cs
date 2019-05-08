using CoreFaces.Licensing;
using CoreFaces.Status.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreFaces.Status.Repositories
{
    public interface IStatusRepository : IBaseRepository<Models.Domain.Status>
    {
        Models.Domain.Status GetByName(string name);
       List<Models.Domain.Status> GetByGroupName(string name);
        List<Models.Domain.Status> GetAll();
    }
    public class StatusRepository : Licence, IStatusRepository
    {

        private readonly StatusDatabaseContext _databaseContext;

        public StatusRepository(StatusDatabaseContext databaseContext, IOptions<StatusSettings> statusSettings, IHttpContextAccessor iHttpContextAccessor) : base("Status", iHttpContextAccessor, statusSettings.Value.StatusLicenseDomain, statusSettings.Value.StatusLicenseKey)
        {
            _databaseContext = databaseContext;
        }

        public bool Delete(int id)
        {
            Models.Domain.Status model = this.GetById(id);
            _databaseContext.Remove(model);
            int result = _databaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public Models.Domain.Status GetByName(string name)
        {
            Models.Domain.Status model = _databaseContext.Set<Models.Domain.Status>().Where(p => p.Name == name).FirstOrDefault();
            return model;
        }

        public List<Models.Domain.Status> GetByGroupName(string name)
        {
            List<Models.Domain.Status> model = _databaseContext.Set<Models.Domain.Status>().Where(p => p.GroupName == name).ToList();
            return model;
        }

        public Models.Domain.Status GetById(int id)
        {
            Models.Domain.Status model = _databaseContext.Set<Models.Domain.Status>().Where(p => p.Id == id).FirstOrDefault();
            return model;
        }

        public int Save(Models.Domain.Status status)
        {
            _databaseContext.Add(status);
            _databaseContext.SaveChanges();
            return status.Id;
        }

        public bool Update(Models.Domain.Status status)
        {
            _databaseContext.Update(status);
            int result = _databaseContext.SaveChanges();
            return Convert.ToBoolean(result);
        }

        public List<Models.Domain.Status> GetAll()
        {
            return _databaseContext.Set<Models.Domain.Status>().ToList();
        }

        public bool DropTables()
        {
            int result = _databaseContext.Database.ExecuteSqlCommand("DROP TABLE Status;");
            if (result == 0)
                return true;
            else
                return false;
        }

        public bool EnsureCreated()
        {
            RelationalDatabaseCreator databaseCreator = (RelationalDatabaseCreator)_databaseContext.Database.GetService<IDatabaseCreator>();
            databaseCreator.CreateTables();
            return true;
        }
    }

}
