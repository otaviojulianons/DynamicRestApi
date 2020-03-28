namespace Infrastructure.Dynamic
{
    public class TestEntity : IDynamicEntity<int, TestModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public void Map(int id, TestModel model)
        {
            Id = id;
            Name = model.Name;
        }
    }
}
