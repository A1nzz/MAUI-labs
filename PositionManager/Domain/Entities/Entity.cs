namespace PositionManager.Domain.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {

        }

        protected Entity(string name)
        {
            Name = name;
        }
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;

    }
}
