using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hunter.Common.Interfaces;
using Hunter.DataAccess.Entities;
using Hunter.DataAccess.Entities.Entites;
using Hunter.DataAccess.Interface;
using Hunter.Services.Services;
using Hunter.Services.Services.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Hunter.Tests.Services
{
    class RoleMappingServiceTests
    {
        IRoleMappingService _roleMappingService;
        IRoleMappingRepository _roleMappingRepository;
        IUserRoleRepository _userRoleRepository;
        ILogger _logger;

        public RoleMappingServiceTests()
        {
            _roleMappingRepository = Substitute.For<IRoleMappingRepository>();
            _userRoleRepository = Substitute.For<IUserRoleRepository>();
            _logger = Substitute.For<ILogger>();

            _roleMappingService = new RoleMappingService(_roleMappingRepository, _userRoleRepository, _logger);
        }

        [SetUp]
        public void TestSetup()
        {
            UserRole _recruiterRole = new UserRole() { Id = 1,Name = "Recruiter" };
            UserRole _technicalRole = new UserRole() { Id = 2, Name = "TechnicalSpecialist" };
            UserRole _adminRole = new UserRole() { Id = 3, Name = "Admin" };
            UserRole _defaultRole = new UserRole() { Id = 4, Name = "Default" };

            _roleMappingRepository.Query().Returns(new List<RoleMapping>()
            {
                new RoleMapping { Id = 1, Position = "HR", RoleId = 1, UserRole = _recruiterRole},
                new RoleMapping { Id = 2, Position = "Developer", RoleId = 2, UserRole = _technicalRole}
                //new RoleMapping { Id = 3, Position = "Admin", RoleId = 3, UserRole = _adminRole},
            }.AsQueryable());

            _userRoleRepository.Get(Arg.Any<Func<UserRole, bool>>()).Returns(_defaultRole);

        }

        [Test]
        public void Transfer_Role_Should_correctly_returns_When_existed_auth_role()
        {
            // Arrange
            var authRole = "Developer";

            // Act
            var hunerRole = _roleMappingService.TransferRole(authRole);

            // Assert
            Assert.AreEqual("TechnicalSpecialist", hunerRole);
        }


        [Test]
        public void Transfer_Role_Should_correctly_returns_When_new_auth_role()
        {
            // Arrange
            var authRole = "CEO";

            // Act
            var hunerRole = _roleMappingService.TransferRole(authRole);

            // Assert
            Assert.AreEqual("Default", hunerRole);
        }

        [Test]
        public void Transfer_Role_Should_correctly_returns_When_auth_role_is_ADMIN()
        {
            // Arrange
            var authRole = "ADMIN";

            // Act
            var hunerRole = _roleMappingService.TransferRole(authRole);

            // Assert
            Assert.AreEqual("Admin", hunerRole);
        }
    }
}
