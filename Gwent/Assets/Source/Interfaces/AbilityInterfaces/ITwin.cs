namespace Gwent.Cards.Interfaces.AbilityInterfaces
{
    public interface ITwin
    {
        string TwinCardName { get; }

        void ExecuteTwinAbility();
    }
}
