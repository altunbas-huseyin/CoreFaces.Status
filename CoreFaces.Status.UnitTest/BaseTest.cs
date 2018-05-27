using CoreFaces.Status.Models.Models;
using CoreFaces.Status.Repositories;
using CoreFaces.Status.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Status.UnitTest
{
    public class BaseTest
    {
        public StatusDatabaseContext _statusDatabaseContext;

        public readonly IStatusService _statusService;


        public BaseTest()
        {
            // var serviceProvider = new ServiceCollection()
            ////.AddEntityFrameworkSqlServer()
            //.AddEntityFrameworkNpgsql()
            //.AddTransient<ITestService, TestService>()
            //.BuildServiceProvider();

            DbContextOptionsBuilder<StatusDatabaseContext> builder = new DbContextOptionsBuilder<StatusDatabaseContext>();
            var connectionString = "server=localhost;userid=root;password=12345678;database=StatusDatabase;";
            builder.UseMySql(connectionString);
            //.UseInternalServiceProvider(serviceProvider); //burası postgress ile sıkıntı çıkartmıyor, fakat mysql'de çalışmıyor test esnasında hata veriyor.

            _statusDatabaseContext = new StatusDatabaseContext(builder.Options);
            //_context.Database.Migrate();

            StatusSettings _statusSettings = new StatusSettings() { FileUploadFolderPath = "c:/" };
            IOptions<StatusSettings> statusOptions = Options.Create(_statusSettings);
            IHttpContextAccessor iHttpContextAccessor = new HttpContextAccessor { HttpContext = new DefaultHttpContext() };

            _statusService = new StatusService(_statusDatabaseContext, statusOptions, iHttpContextAccessor);

        }
    }

}
