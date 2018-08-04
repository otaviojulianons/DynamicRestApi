using SharedKernel.Repository;

namespace Domain
{
    public class EntityDomain : IEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Area { get; set; }
    
    }
}
