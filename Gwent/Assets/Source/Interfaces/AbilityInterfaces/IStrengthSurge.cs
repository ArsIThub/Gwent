namespace Gwent.Cards.Interfaces.AbilityInterfaces
{
    public interface IStrengthSurge
    {
        int SurgeMultiplier { get; }

        void ExecuteStrengthSurgeAbility();
    }
}
