using Items;
using Mapping;
using Outputer;
using Saver;
using Worlding;

namespace Desiring.Tests
{
    public class DesiringMapped : Mapped, IWorldMapped
    {
        public DesiringMapped(string id, Externality externality) 
            : base(id, externality)
        {
        }

        public object Clone()
        {
            var clone = new DesiringMapped(Id, Externality);
            clone.Exits = (Exits)Exits.Clone();
            clone.Agents = (Mapping.Agents)Agents.Clone();
            clone.Items = (Inventory)Items.Clone();

            return clone;
        }

        public void Load(Save save)
        {
        }

        public Output OnTurnPassed(IWorld world, int turns) => null;

        public Output OnTurnPassed(IWorld world, uint turns) =>
            Output.Empty;

        public Save ToSave() => null;
    }
}
