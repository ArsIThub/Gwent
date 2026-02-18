namespace Gwent.Cards.Interfaces.PropertyInterfaces
{
    public interface ILeader
    {
        string LeaderAbilityName { get; }
        string LeaderAbilityDescription { get; }
        bool IsUltimateReady { get; }

        void ActivateUltimate();
        void ResetUltimate();
    }
}