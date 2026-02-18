using UnityEngine;
using Gwent.Cards.Core;
using Gwent.Cards.Interfaces.PropertyInterfaces;
using Gwent.Cards.Interfaces.AbilityInterfaces;

namespace Gwent.Cards.Implementations.SpecialCards
{
    public class ExecutionCard : Card, IHasSpecialAbility, IExecution
    {
        [Header("Execution Settings")]
        [SerializeField] private int targetsCount = 1;
        [SerializeField] private bool targetEnemyOnly = false; 

        public AbilityType AbilityType => AbilityType.Execution;
        public string AbilityDescription => "”ничтожает самую сильную карту на поле бо€";

        public int TargetsCount => targetsCount;
        public bool TargetEnemyOnly => targetEnemyOnly;

        public void ExecuteExecutionAbility()
        {
            Debug.Log($" ј«Ќ№! ”ничтожение {targetsCount} самых сильных карт!");
        }

        public void ApplyAbility()
        {
            ExecuteExecutionAbility();
        }

        public override void ApplyCardEffect()
        {
            Debug.Log($"—пециальна€ карта {cardName} разыграна");
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