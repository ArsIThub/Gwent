namespace Gwent.Cards.Interfaces.AbilityInterfaces
{
    public interface IAvengerCall
    {
        string AvengerCardName { get; }

        void ExecuteAvengerCallAbility();
        void OnCardRemoved();
    }
}