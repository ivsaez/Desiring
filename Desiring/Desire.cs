using Identification;
using Worlding;

namespace Desiring
{
    public delegate int Heuristic(IWorld world);

    public class Desire : Identifiable
    {
        public Heuristic Heuristic { get; }

        public Desire(string id, Heuristic heuristic)
            : base(id)
        {
            Heuristic = heuristic;
        }
    }
}