using System.Collections;
using System.Diagnostics;

namespace MicroCoG
{
    [DebuggerDisplay("{Name}")]
    public class Character : Entity
    {
        public static List<Character> AllCharacters = new();
#pragma warning disable CS8618
        public static Character Player;
#pragma warning restore CS8618

        public readonly string FirstName;
        public readonly string LastName;
        public readonly Gender Gender;
        public readonly SionDate Birth;
        public readonly SionDate? Died;
        public readonly List<Relationship> Relationships = new();
        public readonly string? Crew;

        public Character(string id, string firstName, string lastName, Gender gender, SionDate birth, SionDate? died, string? crew)
        {
            FirstName = firstName;
            LastName = lastName;
            Birth = birth;
            Died = died;
            Crew = crew;
            Gender = gender;
            AllCharacters.Add(this);
            Entity.IdToEntity[id] = this;
        }

        public bool IsAlive => !Died.HasValue;

        public string Name => $"{FirstName} {LastName}";

        public override string ToString() => Name;

        public static void LoadCharacters()
        {
            // ReSharper disable once StringLiteralTypo
            foreach (Hashtable e in SaveFile.Data.Lookup<ArrayList>("entityman", "entities"))
            {
                var payload = (Hashtable?)e["data"];
                if (payload == null)
                    continue;
                var person = (Hashtable?)payload["person"];
                if (person == null)
                    continue;
                var id = payload.Lookup<string>("ident", "id");
                
                SionDate? died = null;
                if (person.ContainsKey("died"))
                    died = (SionDate)person.Lookup<int>("died", "days");

                string? crew = null;
                if (payload["agent"] is Hashtable agent)
                    if (agent["pid"] is string pid)
                        crew= pid;
                var character = new Character(id, (string)person["first"]!, (string)person["last"]!, (Gender)(int)person["g"]!,
                    (SionDate)person.Lookup<int>("born", "days"),
                    died,
                    crew);
             
                // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
                if (crew is "PID_1" && Player == null)
                    Player = character;
            }

            foreach (Hashtable personRels in SaveFile.Data.Lookup<ArrayList>("simman", "rels", "data"))
            {
                foreach (Hashtable rel in (ArrayList)personRels["data"]!)
                {
                    var from = (Character)IdToEntity[(string)rel["from"]!];
                    var type = (Relationship.RelationshipType)((int)rel["type"]!/10);
                    var to = (Character)IdToEntity[(string)rel["to"]!];

                    // ReSharper disable once StringLiteralTypo
                    var history = (Hashtable?)rel["socialhistory"];
                    var items = (ArrayList?)history?["items"];
                    var formatted = items != null
                        ? items.ToArray().Select(h => new SocialEvent(to, from, (Hashtable)h!))
                            .ToArray()
                        : null;
                    //if (formatted is { Length: > 3 }) Debugger.Break();
                    
                    from.Relationships.Add(
                        new Relationship(from, to, type, formatted));
                }
            }
        }

        public static Character Named(string name)
        {
            return AllCharacters.FirstOrDefault(c => string.Equals(c.Name, name, StringComparison.CurrentCultureIgnoreCase))
                ??throw new KeyNotFoundException($"There is no character named {name}");
        }
    }
}
