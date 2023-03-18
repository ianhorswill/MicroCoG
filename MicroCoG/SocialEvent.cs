using System.Collections;

namespace MicroCoG
{
    public class SocialEvent
    {
        public Character Agent;
        public Entity? Target;
        public Character Experiencer;
        public string Action;
        public int Valence;


        public SocialEvent(Character agent, Character experiencer, Hashtable saveData)
        {
            Agent = agent;
            Experiencer = experiencer;
            Target = Entity.IdToEntityOrNull((string)saveData["entityCtx"]!);
            Action = (string)saveData["defid"]!;
            var v = saveData["valence"];
            if (v is int i)
                Valence = i;
            else
                Valence = 0;
        }
    }
}
