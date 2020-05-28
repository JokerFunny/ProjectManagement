using Autofac;
using DAL;
using DAL.Interfaces;
using FluentAssertions;
using Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DALTests.DBRepositories
{
    public class CompanyDBRepositoryTest : DbRepositoryTestBase<ICompanyRepository, Company>
    {
        public CompanyDBRepositoryTest() : base()
        {
            _targetMockedContext = new Mock<ProjectManagementContext>();
            _targetMockedContext.Setup(c => c.Companies).ReturnsDbSet(_rTestData);
            _targetMockedContext.Setup(c => c.Companies.Remove(It.IsAny<Company>()))
                .Callback<Company>((entity) => _rTestData.Remove(entity));

            _targetContext = _targetMockedContext.Object;

            InitContainer();

            _targetRepository = IoC.Container.Resolve<ICompanyRepository>();
        }

        [Fact]
        public void GetAllCompanies_Works_Fine()
        {
            var res = _targetRepository.GetAllCompanies().ToList();

            res.Should().NotBeNull()
                .And.HaveCount(3)
                .And.BeEquivalentTo(_rTestData);
        }

        [Fact]
        public void GetCompanyNameById_Works_Fine()
        {
            var res = _targetRepository.GetCompanyNameById(_rTestData[0].Id);

            res.Should().NotBeNull()
                .And.BeSameAs(_rTestData[0].Name);
        }

        [Fact]
        public void UpdateCompany_Works_Fine()
        {
            Company updatedCompany = _rTestData[0];

            string testName = "New test name";
            updatedCompany.Name = testName;

            var res = _targetRepository.UpdateCompany(updatedCompany);

            res.Should().BeTrue();

            Company updatedCompanyFromContext = _targetContext.Companies
                .Where(c => c.Id == updatedCompany.Id && c.Name == testName)
                .FirstOrDefault();

            updatedCompanyFromContext.Should().NotBeNull()
                .And.BeEquivalentTo(updatedCompany);
        }

        [Fact]
        public void DeleteCompany_Works_Fine()
        {
            string companyName = _rTestData[0].Name;

            var res = _targetRepository.DeleteCompany(companyName);

            res.Should().BeTrue();

            Company deletedCompany = _targetContext.Companies
                .Where(c => c.Name == companyName)
                .FirstOrDefault();

            deletedCompany.Should().BeNull();
        }

        [Fact]
        public void GetAllCompaniesNames_Works_Fine()
        {
            var res = _targetRepository.GetAllCompaniesNames();

            res.Should().NotBeNull()
                .And.HaveCount(3)
                .And.BeEquivalentTo(_rTestData.Select(c => c.Name));
        }

        [Fact]
        public void GetCompanyByName_Works_Fine()
        {
            string targetName = _rTestData[0].Name;

            var res = _targetRepository.GetCompanyByName(targetName);

            res.Should().NotBeNull()
                .And.BeEquivalentTo(_rTestData[0]);
        }

        [Fact]
        public void GetCompanyIdByName_Works_Fine()
        {
            string targetName = _rTestData[0].Name;

            var res = _targetRepository.GetCompanyIdByName(targetName);

            res.Should().NotBe(Guid.Empty)
                .And.Be(_rTestData[0].Id);
        }

        public override void InitContainer()
        {
            var builder = new ContainerBuilder();

            // DBContext
            builder.RegisterInstance(_targetContext).As<ProjectManagementContext>();

            // DAL registration
            builder.RegisterType<CompanyDBRepository>().As<ICompanyRepository>();

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
