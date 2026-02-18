namespace Gwent.Cards.Interfaces.AbilityInterfaces
{
    public interface IBerserk
    {
        string TransformIntoCardName { get; }
        int TransformStrength { get; }

        void ExecuteBerserkAbility();
    }
}