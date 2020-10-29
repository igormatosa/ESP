using Aucxis.Eprw.Reporting.Dataservice.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aucxis.Eprw.Reporting.Dataservice.Services
{
    public class TestService : EntityService<Entities.TestEntity, Models.TestModel>
    {
        public TestService(
            IDatabaseScope database,
            IGenericRepository<Entities.TestEntity> repository
        ) : base(database, repository)
        {
        }
    }
}
