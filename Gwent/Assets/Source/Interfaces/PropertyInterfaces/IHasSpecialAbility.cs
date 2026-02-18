using Gwent.Cards.Core;

namespace Gwent.Cards.Interfaces.PropertyInterfaces
{
    public interface IHasSpecialAbility
    {
        AbilityType AbilityType { get; }
        string AbilityDescription { get; }

        void ApplyAbility();
    }
}