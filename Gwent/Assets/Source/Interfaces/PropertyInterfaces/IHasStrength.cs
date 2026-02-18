namespace Gwent.Cards.Interfaces.PropertyInterfaces
{
    public interface IHasStrength
    {
        int BaseStrength { get; }
        int CurrentStrength { get; }

        void ApplyStrength();
        void ModifyStrength(int modifier);
        void ResetStrength();
    }
}