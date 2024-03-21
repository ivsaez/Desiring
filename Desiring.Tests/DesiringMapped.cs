using Items;
using Mapping;
using Outputer;
using Saver;
using Worlding;

namespace Desiring.Tests
{
    public class DesiringMapped : Mapped, IWorldMapped
    {
        public DesiringMapped(string id) 
            : base(id)
        {
        }

        public object Clone()
        {
            var clone = new DesiringMapped(Id);
            clone.Exits = (Exits)Exits.Clone();
            clone.Agents = (Mapping.Agents)Agents.Clone();
            clone.Items = (Inventory)Items.Clone();

            return clone;
        }

        public void Load(Save save)
        {
        }

        public Output OnTurnPassed(IWorld world, int turns) => null;

        public Save ToSave() => null;
    }
}
