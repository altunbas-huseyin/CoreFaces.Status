using CoreFaces.Status.Models.Models;
using CoreFaces.Status.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace CoreFaces.Status.Services
{
    public interface IStatusService : IBaseService<Models.Domain.Status>
    {
        Models.Domain.Status GetByName(string name);
        List<Models.Domain.Status> GetByGroupName(string name);
        List<Models.Domain.Status> GetAll();

    }
    public class StatusService : IStatusService
    {
        private readonly IStatusRepository _statusRepository;
        public StatusService(StatusDatabaseContext databaseContext, IOptions<StatusSettings> statusSettings, IHttpContextAccessor iHttpContextAccessor)
        {
            _statusRepository = new StatusRepository(databaseContext, statusSettings, iHttpContextAccessor);
        }

        public Models.Domain.Status GetById(int id)
        {
            return _statusRepository.GetById(id);
        }

        public int Save(Models.Domain.Status status)
        {
            _statusRepository.Save(status);
            return status.Id;
        }

        public bool Delete(int id)
        {
            return _statusRepository.Delete(id);
        }

        public bool Update(Models.Domain.Status status)
        {
            return _statusRepository.Update(status);

        }

        public Models.Domain.Status GetByName(string name)
        {
            return _statusRepository.GetByName(name);
        }

        public List<Models.Domain.Status> GetByGroupName(string name)
        {
            return _statusRepository.GetByGroupName(name);
        }

        public List<Models.Domain.Status> GetAll()
        {
            return _statusRepository.GetAll();
        }

        public bool DropTables()
        {
            return _statusRepository.DropTables();
        }

        public bool EnsureCreated()
        {
            return _statusRepository.EnsureCreated();
        }
    }

}
