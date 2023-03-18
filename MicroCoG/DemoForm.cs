using TELL;
using static TELL.Language;

namespace MicroCoG
{
    public partial class DemoForm : Form
    {
        public static DemoForm Singleton;
        public DemoForm()
        {
            InitializeComponent();
            SocialInference.Initialize();
            SocialInference.Program.Repl.ResolveConstant = ResolveExternal;
            SocialInference.Program.Repl.MaxSolutions = 15000;
            // ReSharper disable once CoVariantArrayConversion
            SelectedAgent.Items.AddRange(Character.AllCharacters.ToArray());
            SelectedTarget.Items.AddRange(Character.AllCharacters.ToArray());
            var actions = Character.AllCharacters.SelectMany(c=>c.Relationships).SelectMany(r=>r.SocialHistory).Select(e=>e.Action).Where(s=>!s.EndsWith("-inf")).Distinct().ToArray();
            Array.Sort(actions);
            // ReSharper disable once CoVariantArrayConversion
            SelectedAction.Items.AddRange(actions);
            Singleton = this;
            ShowHelp();
        }

        public static Character Agent => (Character)Singleton.SelectedAgent.SelectedItem;

        private void SelectedCharacter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInferences();
        }

        public static string Action => (string)Singleton.SelectedAction.SelectedItem;
        private void SelectedAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInferences();
        }
        
        public static Character Target => (Character)Singleton.SelectedTarget.SelectedItem;

        private void SelectedTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateInferences();
        }

        private void UpdateInferences()
        {
            var character = (Character?)SelectedAgent.SelectedItem;
            if (character == null) return;

            var action = (string?)SelectedAction.SelectedItem;
            if (action == null) return;

            Results.Items.Clear();
            var results = SocialInference.OnClick().ToArray();
            Results.Items.AddRange(results);
        }

        private Term ResolveExternal(string text)
        {
            switch (text)
            {
                case "character":
                    case "agent":
                    return Constant((Character)SelectedAgent.SelectedItem);

                case "action":
                    return Constant((string)SelectedAction.SelectedItem);

                case "target":
                    return Constant((string)SelectedTarget.SelectedItem);

                default:
                    if (Enum.TryParse(typeof(Relationship.RelationshipType), text, true, out var t))
                        return Constant((Relationship.RelationshipType)t!);
                    if (Enum.TryParse(typeof(Relationship.RelationshipClass), text, true, out var c))
                        return Constant((Relationship.RelationshipClass)c!);
                    return Constant(Character.Named(text));
            }
        }
        
        private void textBox1_TextChanged(object sender, EventArgs ea)
        {
            Results.Items.Clear();
            try
            {
                // ReSharper disable once CoVariantArrayConversion
                Results.Items.AddRange(SocialInference.Program.Repl.Solutions(textBox1.Text).Select(PrintArray).ToArray());
            }
            catch (Exception e)
            {
                Results.Items.Add(e.Message);
            }
        }

        private string PrintArray(object?[] a) => string.Join(", ", a.Select(o => o == null?"null":o.ToString()));

        private void ShowPredicatesButton_Click(object sender, EventArgs e)
        {
            Results.Items.Clear();
            Results.Items.AddRange(SocialInference.Program.Predicates.OrderBy(p=>p.Name).Select(p => p.ManualEntry()).ToArray());
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            ShowHelp();
        }

        private void ShowHelp()
        {
            Results.Items.Clear();
            Results.Items.AddRange(new []
            {
                "Simple TELL reimplementation of social inference from City of Gangsters",
                "",
                "- Choose an agent, target, and action, to see inferred reactions (try violence or killing)",
                "- Type a TELL query in the query box to display its results",
                "  WARNING: current .NET won't let me kill a thread, so don't type queries that will run for minutes",
                "  In queries, $agent, $action, $target mean the things selected in the UI",
                "  Or $mother, $father, $family, etc. for values of enumerated types",
                "  Or $\"name\" for the character with the specified name",
                "  Or click one of these queries, to copy it to the query box",
                "    - Character[c]",
                "      See all characters",
                "    - RelationshipOf[$agent, r]",
                "      See relationships of selected character.  Must choose a character under Agent first",
                "    - Character[c], RelationshipOf[c, r], RelationshipType[r, $mother]",
                "      Show just mother relationships",
                "    - RelationshipOf[$agent, r], RelationshipClass[r, $family]",
                "      Show family of selected agent; must select an agent in the UI first",
                "    - Murdered[c]",
                "      Show everyone who was murdered",


                "- Press Show Predicates for a list of all predicates",
                "- Press Help for this message"
            });
        }

        private void Results_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = Results.SelectedItem as string;
            if (item == null) return;
            if (!item.StartsWith("    - ")) return;
            textBox1.Text = item.Replace("    - ", "");
            textBox1_TextChanged(textBox1, null);
        }
    }
}