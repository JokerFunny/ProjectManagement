using Autofac;
using BLL;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DALTests.DBRepositories;
using FluentAssertions;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BLLTests
{
    public class CompanyServiceTest : ServiceTestBase<ICompanyService, Company>
    {
        public CompanyServiceTest() : base()
        {
            var mockContex = new Mock<ProjectManagementContext>();
            mockContex.Setup(c => c.Companies).ReturnsDbSet(_rTestData);
            mockContex.Setup(c => c.Companies.Remove(It.IsAny<Company>()))
                .Callback<Company>((entity) => _rTestData.Remove(entity));

            _targetContext = mockContex.Object;

            InitContainer();

            _targetService = IoC.Container.Resolve<ICompanyService>();
        }

        [Fact]
        public void GetAllCompanies_Works_Fine()
        {
            var res = _targetService.GetAllCompanies().ToList();

            res.Should().NotBeNull()
                .And.HaveCount(3)
                .And.BeEquivalentTo(_rTestData);
        }

        [Fact]
        public void DeleteCompany_Empty_Name_Works_Fine()
        {
            var res = _targetService.DeleteCompany(null, out string error);

            res.Should().BeFalse();

            error.Should().NotBeEmpty()
                .And.Be("Company name can`t be null or empty!");
        }

        [Fact]
        public void DeleteCompany_Not_Found_Works_Fine()
        {
            string targetIncorrectName = "Incorrect name";

            var res = _targetService.DeleteCompany(targetIncorrectName, out string error);

            res.Should().BeFalse();

            error.Should().NotBeEmpty()
                .And.Be($"Company with name {targetIncorrectName} don`t exist in Companies");
        }

        [Fact]
        public void DeleteCompany_Works_Fine()
        {
            string companyName = _rTestData[0].Name;

            var res = _targetService.DeleteCompany(companyName, out string error);

            res.Should().BeTrue();

            error.Should().NotBeNull()
                .And.BeEmpty();

            Company deletedCompany = _targetContext.Companies
                .Where(c => c.Name == companyName)
                .FirstOrDefault();

            deletedCompany.Should().BeNull();
        }

        [Fact]
        public void GetAllCompaniesNames_Works_Fine()
        {
            var res = _targetService.GetAllCompaniesNames();

            res.Should().NotBeNull()
                .And.HaveCount(3)
                .And.BeEquivalentTo(_rTestData.Select(c => c.Name));
        }

        [Fact]
        public void GetCompanyNameById_For_Empty_Id_Works_Fine()
        {
            Action act = () => _targetService.GetCompanyNameById(Guid.Empty);

            var exception = Assert.Throws<ArgumentNullException>(act);

            Assert.Equal("Company id can`t be empty", exception.ParamName);
        }

        [Fact]
        public void GetCompanyNameById_Id_Works_Fine()
        {
            var res = _targetService.GetCompanyNameById(_rTestData[0].Id);

            res.Should().NotBeNullOrWhiteSpace()
                .And.BeSameAs(_rTestData[0].Name);
        }

        [Fact]
        public void GetCompanyIdByName_For_Empty_Id_Works_Fine()
        {
            Action act = () => _targetService.GetCompanyIdByName(null);

            var exception = Assert.Throws<ArgumentNullException>(act);

            Assert.Equal("Company name can`t be null or empty", exception.ParamName);
        }

        [Fact]
        public void GetCompanyIdByName_Works_Fine()
        {
            var res = _targetService.GetCompanyIdByName(_rTestData[0].Name);

            res.Should().NotBe(Guid.Empty)
                .And.Be(_rTestData[0].Id);
        }

        [Fact]
        public void UpdateCompany_With_Empty_Company_Works_Fine()
        {
            Action act = () => _targetService.UpdateCompany(null, out string error);

            var exception = Assert.Throws<ArgumentNullException>(act);

            Assert.Equal("Company can`t be null!", exception.ParamName);
        }

        [Fact]
        public void UpdateCompany_With_Empty_Company_Name_Works_Fine()
        {
            Company updatedCompany = _rTestData[0];

            string testName = string.Empty;
            updatedCompany.Name = testName;

            var res = _targetService.UpdateCompany(updatedCompany, out string error);

            res.Should().BeFalse();

            error.Should().NotBeNullOrWhiteSpace()
                .And.Be("Company name can`t be null or empty!");
        }

        [Fact]
        public void UpdateCompany_That_Dont_Exist_Works_Fine()
        {
            Company updatedCompany = new Company()
            {
                Name = _rTestData[0].Name,
                Description = _rTestData[0].Description,
                CountryOfFoundation = _rTestData[0].CountryOfFoundation,
                Photo = _rTestData[0].Photo
            };

            string testName = Guid.NewGuid().ToString();
            updatedCompany.Name = testName;

            var res = _targetService.UpdateCompany(updatedCompany, out string error);

            res.Should().BeFalse();

            error.Should().NotBeNullOrWhiteSpace()
                .And.Be($"Company witn name {testName} don`t exist");
        }

        [Fact]
        public void UpdateCompany_Works_Fine()
        {
            Company updatedCompany = new Company()
            {
                Id = _rTestData[0].Id,
                Name = _rTestData[0].Name,
                Description = _rTestData[0].Description,
                CountryOfFoundation = _rTestData[0].CountryOfFoundation,
                Photo = _rTestData[0].Photo
            };

            string newTestDesc = "New test description";
            updatedCompany.Description = newTestDesc;

            var res = _targetService.UpdateCompany(updatedCompany, out string error);

            res.Should().BeTrue();

            error.Should().NotBeNull()
                .And.BeEmpty();

            Company updatedCompanyFromContext = _targetContext.Companies
                .Where(c => c.Name == updatedCompany.Name && c.Description == newTestDesc)
                .FirstOrDefault();

            updatedCompanyFromContext.Should().NotBeNull()
                .And.BeEquivalentTo(updatedCompany);
        }

        public override void InitContainer()
        {
            var builder = new ContainerBuilder();

            // DBContext
            builder.RegisterInstance(_targetContext).As<ProjectManagementContext>();

            // DAL registration
            builder.RegisterType<CompanyDBRepository>().As<ICompanyRepository>();

            // Service registration
            builder.RegisterType<CompanyService>().As<ICompanyService>();

            IoC.BuildContainer(builder);
        }

        public override List<Company> GetTestData()
        {
            return new List<Company>()
            {
                new Company() { Id = Guid.NewGuid(), Name = "TOVARIS4", Description  = "Development for U", CountryOfFoundation = Guid.NewGuid(), Photo = "" },
                new Company() { Id = Guid.NewGuid(), Name = "Sobratia", Description  = "Family company", CountryOfFoundation = Guid.NewGuid(), Photo = "" },
                new Company() { Id = Guid.NewGuid(), Name = "HimPromTorg", Description  = "Chemicals for everebody!", CountryOfFoundation = Guid.NewGuid(), Photo = "" }
            };
        }
    }
}
