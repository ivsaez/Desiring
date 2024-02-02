using Saver;
using Worlding;

namespace Desiring
{
    public sealed class Desires: ISavable, ICloneable
    {
        private ISet<string> desires;

        public Desires(params Desire[] desires)
        {
            this.desires = new HashSet<string>(desires.Select(d => d.Id));
        }

        public void Add(Desire desire)
        {
            desires.Add(desire.Id);
        }

        public void Remove(Desire desire)
        {
            if(desires.Contains(desire.Id))
                desires.Remove(desire.Id);
        }

        public int Heuristic(IWorld world, DesireVault vault) =>
            desires.Sum(desireId => vault.Has(desireId) ? vault.Get(desireId).Heuristic(world) : 0);

        public object Clone()
        {
            var clone = new Desires();
            clone.desires = new HashSet<string>(desires);

            return clone;
        }

        public Save ToSave() =>
            new Save(GetType().Name)
                .WithArray(nameof(desires), desires);

        public void Load(Save save)
        {
            desires = new HashSet<string>(save.GetStringArray(nameof(desires)));
        }
    }
}
