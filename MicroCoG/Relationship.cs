namespace MicroCoG
{
    public class Relationship
    {
        public readonly Character From;
        public readonly Character To;
        public readonly RelationshipType Type;
        public readonly SocialEvent[] SocialHistory;

        public bool IsFamily => Class == RelationshipClass.Family;

        public RelationshipClass Class =>
            Type switch
            {
                RelationshipType.Self => RelationshipClass.Self,
                RelationshipType.Acquaintance => RelationshipClass.Acquaintance,
                _ => RelationshipClass.Family
            };

        public enum RelationshipType
        {
            Self=0, Spouse=1, Child=2, Mother=3, Father=4, Sibling=5, MotherSib=6, FatherSib=7, SibChild=8, Cousin=9, Acquaintance=10
        }

        public enum RelationshipClass
        {
            Self, Family, Acquaintance
        }

        public Relationship(Character from, Character to, RelationshipType type, SocialEvent[]? socialHistory)
        {
            From = from;
            To = to;
            Type = type;
            SocialHistory = socialHistory??Array.Empty<SocialEvent>();
        }

        public override string ToString()
        {
            return $"<{From} {Type} {To}>";
        }
    }
}
