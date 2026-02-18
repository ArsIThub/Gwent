namespace Gwent.Cards.Interfaces.AbilityInterfaces
{
    public interface IMedic
    {
        int MaxReviveCount { get; }

        void ExecuteMedicAbility();
    }
}