using System.ComponentModel.DataAnnotations.Schema;

namespace Aucxis.Eprw.Reporting.Dataservice.Entities
{
    [Table("TestEntities", Schema = "eps")]
    public partial class TestEntity: IEntity
    {
        public TestEntity()
        {
            
        }

        public long Id { get; set; }

        public string PropertyOne { get; set; }

    }
}
