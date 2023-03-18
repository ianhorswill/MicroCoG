namespace MicroCoG
{
    public class Entity
    {
        public static readonly Dictionary<string, Entity> IdToEntity = new Dictionary<string, Entity>();

        public static Entity? IdToEntityOrNull(string id) => IdToEntity.TryGetValue(id, out var e) ? e : null;
    }
}
