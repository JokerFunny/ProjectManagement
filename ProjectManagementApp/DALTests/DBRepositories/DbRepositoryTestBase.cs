using DAL;
using Moq;
using System.Collections.Generic;

namespace DALTests.DBRepositories
{
    public abstract class DbRepositoryTestBase<IRepository, Model>
        where Model : class
    {
        protected ProjectManagementContext _targetContext;
        protected Mock<ProjectManagementContext> _targetMockedContext;
        protected IRepository _targetRepository;
        protected readonly List<Model> _rTestData;

        public DbRepositoryTestBase()
        {
            _rTestData = GetTestData();
        }

        public abstract void InitContainer();

        public abstract List<Model> GetTestData();
    }
}
