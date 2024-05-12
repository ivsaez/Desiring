using Outputer.Choicing;
using Rand;
using Rolling;
using Stories;
using Worlding;

namespace Desiring
{
    public class Decider
    {
        public int Decide(
            World world, 
            Choices choices, 
            Roles roles, 
            Historic historic, 
            Desires desires, 
            DesireVault vault) =>
            desires.Any
                ? getDesiredSelection(world, choices, roles, historic, desires, vault)
                : getPrioritySelection(choices);

        private int getPrioritySelection(Choices choices)
        {
            var participations = new List<int>();
            foreach (var option in choices.Options)
            {
                for (int i = 0; i < option.Priority; i++)
                    participations.Add(option.Index);
            }

            return participations.Random();
        }

        private int getDesiredSelection(
            World world, 
            Choices choices, 
            Roles roles, 
            Historic historic, 
            Desires desires, 
            DesireVault vault)
        {
            var heuristics = new List<(int HeuristicValue, IndexedOption Option)>();
            foreach (var option in choices.Options)
            {
                var worldClone = (World)world.Clone();
                var clonedRoles = getClonedRoles(worldClone, roles);
                var historicClone = (Historic)historic.Clone();

                var interaction = (Interaction)option.Function();
                interaction.Execute(new PredefinedPostconditions(worldClone, clonedRoles, historicClone));

                var heuristicValue = desires.Heuristic(worldClone, vault);
                heuristics.Add((heuristicValue, option));
            }

            (int HeuristicValue, IndexedOption Option) selected = heuristics.First();
            foreach (var item in heuristics)
            {
                if (item.HeuristicValue >= selected.HeuristicValue)
                    selected = item;
            }

            return selected.Option.Index;
        }

        private Roles getClonedRoles(World clonedWorld, Roles roles)
        {
            var clonedRoles = new Roles(roles.RoleNames.ToHashSet());
            var clonedExistents = clonedWorld.Existents;

            foreach (var role in roles.RoleNames)
            {
                var identifiable = roles.Get(role)!;
                var something = clonedExistents.GetSomething(identifiable.Id);
                clonedRoles.Match(role, something);
            }

            return clonedRoles;
        }
    }
}
