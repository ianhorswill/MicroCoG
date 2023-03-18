using TELL;
using static TELL.Language;
using static MicroCoG.Character;
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedVariable

namespace MicroCoG
{
    public static class SocialInference
    {
        public static TELL.Program Program;

        public static string PathTo(string filename)
        {
            if (File.Exists(filename))
                // This is a build and the file in in the directory with us
                return filename;
            // We're running inside Visual Studio
            return Path.Combine("../../../", filename);
        }
        public static void Initialize()
        {
            Program = new TELL.Program("SocialInference");
            Program.Begin();
            var character = (Var<Character>)"character";
            var agent = (Var<Character>)"actor";
            var target = (Var<Character>)"target";
            var other = (Var<Character>)"other";
            var reactor = (Var<Character>)"reactor";
            var rel = (Var<Relationship>)"rel";
            var e = (Var<SocialEvent>)"e";
            var action = (Var<string>)"action";
            var reaction = (Var<string>)"response";
            var relationshipType = (Var<Relationship.RelationshipType>)"relationshipType";
            var relationshipClass = (Var<Relationship.RelationshipClass>)"relationshipClass";
            var spreadTo = (Var<Relationship.RelationshipClass>)"spreadTo";
            var buff = (Var<float>)"buff";
            var buffTotal = (Var<float>)"buffTotal";

            // User interface
            var SelectedAgent = Predicate("SelectedAgent", () => DemoForm.Agent)
                .Documentation("Character is the agent chosen in the UI");
            var SelectedAction = Predicate("SelectedAction", () => DemoForm.Action)
                .Documentation("String is the action chosen in the UI.");
            var SelectedTarget = Predicate("SelectedTarget", () => DemoForm.Target)
                .Documentation("Character is the target chosen in the UI");
            
            // Low-level data structure access

            var Character = Predicate<Character>("Character",
                    ModeDispatch("Character",
                        _ => true, // normally, this would be c is Character, but static checking means this can only be called with a character.
                        () => AllCharacters))
                .Documentation("Argument is a character in the game");

            var Alive = Predicate<Character>("Alive",
                    ModeDispatch("Alive",
                        c => c.IsAlive,
                        () => AllCharacters.Where(c => c.IsAlive)))
                .Documentation("Character is alive");

            var RelationshipOf =
                Predicate("RelationshipOf", (Enumerator<Character, Relationship>)(c => c.Relationships))
                    .Documentation("Relationship is a relationship of the Character");

            var RelationshipBetween = Predicate("RelationshipBetween",
                    (Character from, Character to) => from.Relationships.FirstOrDefault(r => r.To == to),
                    true)
                .Documentation("Relationship is the relationship between the two characters, if any.");

            var RelationshipType = Predicate("RelationshipType", (Relationship r) => r.Type)
                .Documentation("The relationship has that type (mother, sibling, acquaintance, etc.)");

            var RelationshipClass = Predicate("RelationshipClass", (Relationship r) => r.Class)
                .Documentation("The relationship's type is of that class (family/not family)");

            var RelationshipTo = Predicate("RelationshipTo", (Relationship r) => r.To)
                .Documentation("The relationship is from some character to the specified character");

            var History = Predicate("History", (Enumerator<Relationship, SocialEvent>)(r => r.SocialHistory))
                .Documentation("Relationship's history includes SocialEvent");

            var TargetCharacter = Predicate("TargetCharacter", (SocialEvent ev) => ev.Target as Character, true)
                .Documentation("The event involved an action targeted at Character");

            var Action = Predicate("Action", (SocialEvent ev) => ev.Action)
                .Documentation("The event involved the specified kind of action");

            // If somebody does ACTION, the friends of the target will respond with REACITON
            // but only those of the type SPREADTO will really care
            var ReactionType = Predicate("ReactionType", action, reaction, spreadTo)
                .FromFile(PathTo("ReactionTypes.csv"))
                .Documentation(
                    "If a character performs action on a target character, the target's friends of relationshipClass will respond with response");

            var ReactionBuff = Predicate("ReactionBuff", reaction, relationshipClass, relationshipType, buff)
                .FromFile(PathTo("ReactionBuffs.csv"))
                .Documentation(
                    "A response to an action against a friend of the specified class and type will result in the specified buff to a character's trust of the agent performing the action");

            // If AGENT does ACTION to TARGET, then REACTOR has REACTION, changing their trust of AGENT by BUFFTOTAL
            var ReactionToAction = Predicate("ReactionToAction", agent, action, target, reactor, reaction, buffTotal)
                // ACTIONs provide REACTION in the target's network, but only among RELATIONSHIPCLASS (e.g. family)
                .If(ReactionType[action, reaction, relationshipClass],
                    // REL is a relationship in the target's network
                    RelationshipOf[target, rel],
                    // Of the required CLASS
                    RelationshipClass[rel, relationshipClass],
                    // And its specific type is RELATIONSHIPTYPE
                    RelationshipType[rel, relationshipType],
                    // And Reactor is the person in the network
                    RelationshipTo[rel, reactor],
                    // BUFFTOTAL is the sum of all BUFFs applicable to this reaction/class/type
                    Sum[buff,
                        ReactionBuff[reaction, relationshipClass, relationshipType, buff],
                        buffTotal])
                .Documentation(
                    "If agent performs action on target, the reactor reacts with response and the specified buff to their trust of agent.");

            var Murdered = Predicate("Murdered", character)
                .If(Character[character],
                    RelationshipOf[character, rel],
                    History[rel, e],
                    Action[e, "killing"],
                    TargetCharacter[e, character!])
                .Documentation("Character is a murder victim.");

            var RelatedTo = Predicate("RelatedTo", character, other, relationshipType)
                .If(RelationshipBetween[character, other, rel],
                    RelationshipType[rel, relationshipType])
                .Documentation("The two characters have the specified type of relationship.");

            var Reaction = Predicate("Reaction", reactor, reaction, buff)
                .If(SelectedAgent[agent], SelectedAction[action], SelectedTarget[target],
                    ReactionToAction[agent, action, target, reactor, reaction, buff])
                .Documentation(
                    "Reactor has response to the action selected in the UI, along with the buff to their trust of the agent");

            OnClick = () =>
                Reaction[reactor, reaction, buff].SolutionsUntyped;
                //RelationshipOf[c, rel].SolveForAll(rel).Select(rel => rel.To.Name);
                //PerpetratorsOf[c, a, character].SolveForAll(character);
                //PerpetratorsOf[c, action, character].Solutions.Cast<object>();
                //Murdered[character].Solutions;
                //RelatedTo[c, character, relationshipType].Solutions.Cast<object>();

            Program.End();
        }

        public static Func<IEnumerable<object>> OnClick;
    }
}
