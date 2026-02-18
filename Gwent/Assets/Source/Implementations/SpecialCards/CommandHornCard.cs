using UnityEngine;
using Gwent.Cards.Core;
using Gwent.Cards.Interfaces.PropertyInterfaces;
using Gwent.Cards.Interfaces.AbilityInterfaces;

namespace Gwent.Cards.Implementations.SpecialCards
{
    public class CommandHornCard : Card, IHasSpecialAbility, IConnection
    {
        [Header("Command Horn Settings")]
        [SerializeField] private int strengthBonus = 2;

        public AbilityType AbilityType => AbilityType.Connection;
        public string AbilityDescription => "Удваивает силу всех отрядов в ряду";

        public int StrengthBonusPerConnection => strengthBonus;

        public void ExecuteConnectionAbility()
        {
            Debug.Log($"СПОСОБНОСТЬ: Командный рог удваивает силу ряда!");
        }

        public void ApplyAbility()
        {
            ExecuteConnectionAbility();
        }

        public override void ApplyCardEffect()
        {
            Debug.Log($"Специальная карта {cardName} разыграна");
            ApplyAbility();
        }

        protected override void OnInitialized()
        {
            cardType = CardType.Special;
            faction = Faction.Neutral;
            base.OnInitialized();
        }
    }
}