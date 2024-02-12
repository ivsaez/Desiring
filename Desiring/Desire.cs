using Agents;
using Identification;
using Items;
using Mapping;
using Saver;
using Worlding;

namespace Desiring
{
    public delegate int Heuristic<A, I, M>(IWorld<A, I, M> world)
        where A : IAgent, ITimed, ISavable, ICloneable
        where I : IItem, ITimed, ISavable, ICloneable
        where M : IMapped, ITimed, ISavable, ICloneable;

    public class Desire<A, I, M> : Identifiable
        where A : IAgent, ITimed, ISavable, ICloneable
        where I : IItem, ITimed, ISavable, ICloneable
        where M : IMapped, ITimed, ISavable, ICloneable
    {
        public Heuristic<A, I, M> Heuristic { get; }

        public Desire(string id, Heuristic<A, I, M> heuristic)
            : base(id)
        {
            Heuristic = heuristic;
        }
    }
}