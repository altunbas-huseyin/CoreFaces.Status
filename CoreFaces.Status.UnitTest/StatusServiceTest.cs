using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CoreFaces.Status.UnitTest
{
    [TestClass]
    public class StatusServiceTest : BaseTest
    {
        [TestMethod]
        public void InsertStatusCodeList()
        {
            Models.Domain.Status statusWaitingForApproval = new Models.Domain.Status { Name = "WaitingForApproval", Description = "" };
            Models.Domain.Status statusActive = new Models.Domain.Status { Name = "Active", Description = "" };
            Models.Domain.Status statusPassive = new Models.Domain.Status { Name = "Passive", Description = "" };
            Models.Domain.Status statusWaiting = new Models.Domain.Status { Name = "Waiting", Description = "" };
            Models.Domain.Status statusDeleted = new Models.Domain.Status { Name = "Deleted", Description = "" };
            Models.Domain.Status statusReserve = new Models.Domain.Status { Name = "Reserve", Description = "" };

            if (_statusService.GetByName("WaitingForApproval") == null)
                _statusService.Save(statusWaitingForApproval);

            if (_statusService.GetByName("Active") == null)
                _statusService.Save(statusActive);

            if (_statusService.GetByName("Passive") == null)
                _statusService.Save(statusPassive);

            if (_statusService.GetByName("Waiting") == null)
                _statusService.Save(statusWaiting);

            if (_statusService.GetByName("Deleted") == null)
                _statusService.Save(statusDeleted);
            if (_statusService.GetByName("Reserve") == null)
                _statusService.Save(statusReserve);

        }

        [TestMethod]
        public void DropTable()
        {
            //bool result = _statusService.DropTables();
            //Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void EnsureCreated()
        {
            //bool result = _statusService.EnsureCreated();
            //Assert.AreEqual(result, true);
        }
    }
}
