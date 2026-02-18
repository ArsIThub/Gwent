namespace Gwent.Cards.Interfaces.AbilityInterfaces
{
    public interface IPretence
    {
        string[] PossibleDisguises { get; }

        void ExecutePretenceAbility();
    }
}