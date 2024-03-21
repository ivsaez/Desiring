using AgentRelations;
using Agents;
using Desiring.Tests.Lang;
using ItemsLang.Lang;
using Languager;
using Outputer;
using Outputer.Choicing;
using Rolling;
using StateMachine;
using Stories;
using Stories.Builders;
using Worlding;

namespace Desiring.Tests
{
    public class DeciderShould
    {
        DesiringAgent agent1;
        DesiringAgent agent2;

        World world;

        DesireVault desireVault;

        public DeciderShould()
        {
            configureDictionary();

            agent1 = new DesiringAgent("agent1", "Agent", "One", Importance.Main);
            agent2 = new DesiringAgent("agent2", "Agent", "Two", Importance.Crowd);

            agent1.Relations.Add(agent2.Id, RelationFactory.Get(RelationKind.Neutral));
            agent2.Relations.Add(agent1.Id, RelationFactory.Get(RelationKind.Neutral));

            var mapped = new DesiringMapped("mapped");

            world = new World(MachineBuilder.Create()
                .WithState("Initial")
                .EndState()
                .Build());

            world.Agents.Add(agent1);
            world.Agents.Add(agent2);

            world.Map.Add(mapped);

            world.Map.Ubicate(agent1, mapped);
            world.Map.Ubicate(agent2, mapped);

            desireVault = new DesireVault();
        }

        [Fact]
        public void SelectOptionThatImprovesFriendship()
        {
            var desire = new Desire("desire", world =>
            {
                var agent2 = world.Agents.GetOne("agent2") as DesiringAgent;
                return agent2!.Relations.Get("agent1").Metrics.Friendship;
            });

            desireVault.Add(desire);
            agent1.Desires.Add(desire);

            var storylet = BuildFriendshipStorylet();

            var roles = new Roles(storylet.InvolvedRoles)
                .Match(Descriptor.MainRole, agent1);

            var story = storylet.Execute(world, roles, new Historic());
            var step = story.Interact(Input.Void);

            int decisionIndex = agent1.Decider.Decide(world, step.Choices, roles, agent1.Desires, desireVault);

            Assert.Equal(1, decisionIndex);
        }

        [Fact]
        public void SelectOptionThatDegradatesFriendship()
        {
            var desire = new Desire("desire", world =>
            {
                var agent2 = world.Agents.GetOne("agent2") as DesiringAgent;
                return 100 - agent2!.Relations.Get("agent1").Metrics.Friendship;
            });

            desireVault.Add(desire);
            agent1.Desires.Add(desire);

            var storylet = BuildFriendshipStorylet();

            var roles = new Roles(storylet.InvolvedRoles)
                .Match(Descriptor.MainRole, agent1);

            var story = storylet.Execute(world, roles, new Historic());
            var step = story.Interact(Input.Void);

            int decisionIndex = agent1.Decider.Decide(world, step.Choices, roles, agent1.Desires, desireVault);

            Assert.Equal(2, decisionIndex);
        }

        private void configureDictionary()
        {
            var dictionary = new Dictionary(Language.ES);
            dictionary.Load(new TestDictionaryProvider());
            dictionary.Load(new ItemsDictionaryProvider());

            Translator.Instance.Dictionary = dictionary;
        }

        private Storylet BuildFriendshipStorylet() =>
            StoryletBuilder.Create("storylet")
                .BeingGlobalSingle()
                .ForMachines()
                .WithInteraction((world, roles) =>
                {
                    return Output.FromTexts("Choose");
                })
                    .WithSubinteraction((world, roles) =>
                    {
                        var agent2 = world.Agents.GetOne("agent2") as DesiringAgent;
                        agent2!.Relations.Get("agent1").Metrics.IncreaseFriendship(100);

                        return Output.FromTexts("GOOD");
                    })
                    .Build()
                    .WithSubinteraction((world, roles) =>
                    {
                        var agent2 = world.Agents.GetOne("agent2") as DesiringAgent;
                        agent2!.Relations.Get("agent1").Metrics.DecreaseFriendship(100);

                        return Output.FromTexts("BAD");
                    })
                    .Build()
                .SetAsRoot()
                .Finish();
    }
}