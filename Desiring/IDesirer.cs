using Agents;

namespace Desiring
{
    public interface IDesirer : IAgent
    {
        Desires Desires { get; } 

        Decider Decider { get; }
    }
}
