using AgentRelations;
using Agents;
using Outputer;
using Saver;
using Worlding;

namespace Desiring.Tests
{
    public class DesiringAgent : Agent, IWorldAgent, IDesirer, IRelated
    {
        public DesiringAgent(string id, string name, string surname, Importance importance)
            : base(id, name, surname, importance)
        {
            Desires = new Desires();
            Decider = new Decider();
            Relations = new RelationSet();
        }

        public Desires Desires { get; private set; }

        public Decider Decider { get; private set; }

        public RelationSet Relations { get; private set; }

        public object Clone()
        {
            var clone = new DesiringAgent(Id, Name, Surname, Importance);
            clone.Desires = (Desires)Desires.Clone();
            clone.Status = (Status)Status.Clone();
            clone.Position = (Position)Position.Clone();

            if (Actioner == Actioner.Human)
                clone.BecomeHuman();
            else
                clone.BecomeIA();

            return clone;
        }

        public void Load(Save save)
        {
        }

        public Output OnTurnPassed(IWorld world, int turns) => null;

        public Output OnTurnPassed(IWorld world, uint turns) => Output.Empty;

        public Save ToSave() => null;
    }
}
