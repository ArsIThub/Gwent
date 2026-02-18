namespace Gwent.Cards.Interfaces.AbilityInterfaces
{
    public interface ISpy
    {
        int CardsToDraw { get; }

        void ExecuteSpyAbility();
    }
}