using DAL;
using System.Collections.Generic;

namespace BLLTests
{
    public abstract class ServiceTestBase<IService, Model>
        where Model : class
    {
        protected ProjectManagementContext _targetContext;
        protected IService _targetService;
        protected readonly List<Model> _rTestData;

        public ServiceTestBase()
        {
            _rTestData = GetTestData();
        }

        public abstract void InitContainer();

        public abstract List<Model> GetTestData();
    }
}
