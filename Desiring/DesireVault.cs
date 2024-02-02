namespace Desiring
{
    public sealed class DesireVault
    {
        private readonly Dictionary<string, Desire> desires;

        public DesireVault()
        {
            desires = new Dictionary<string, Desire>();
        }

        public void Add(Desire desire) => desires[desire.Id] = desire;

        public bool Has(string desireId) => desires.ContainsKey(desireId);

        public Desire Get(string id)
        {
            if(!desires.ContainsKey(id))
                throw new ArgumentException(nameof(id));

            return desires[id];
        }
    }
}
