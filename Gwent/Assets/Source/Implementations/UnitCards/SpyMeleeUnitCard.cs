using UnityEngine;
using Gwent.Cards.Core;
using Gwent.Cards.Interfaces.RowInterfaces;
using Gwent.Cards.Interfaces.PropertyInterfaces;
using Gwent.Cards.Interfaces.AbilityInterfaces;

namespace Gwent.Cards.Implementations.UnitCards
{
    public class SpyMeleeUnitCard : Card, IMeleeFighter, IHasStrength, IHasSpecialAbility, ISpy
    {
        [Header("Unit Settings")]
        [SerializeField] private int baseStrength = 0; 
        [SerializeField] private int cardsToDraw = 2;

        private int _currentStrength;
        private int _strengthModifier;

        public int BaseStrength => baseStrength;
        public int CurrentStrength => _currentStrength;

        public AbilityType AbilityType => AbilityType.Spy;
        public string AbilityDescription => $"Шпион: разместите на поле противника и возьмите {cardsToDraw} карты";
        public int CardsToDraw => cardsToDraw;

        public void OnPlacedInMeleeRow()
        {
            Debug.Log($"Шпион {cardName} размещен в ряду ближнего боя ПРОТИВНИКА");
            ApplyStrength();
            ExecuteSpyAbility(); 
        }

        public void OnRemovedFromMeleeRow()
        {
            Debug.Log($"Шпион {cardName} удален из ряда ближнего боя");
        }

        public void ExecuteSpyAbility()
        {
            Debug.Log($"ШПИОНАЖ: {cardName} передает разведданные!");
            Debug.Log($"Игрок берет {cardsToDraw} карты из колоды");

            OnSpyAbilityExecuted();
        }

        public void ApplyStrength()
        {
            _currentStrength = baseStrength + _strengthModifier;
        }

        public void ModifyStrength(int modifier)
        {
            _strengthModifier += modifier;
            ApplyStrength();
        }

        public void ResetStrength()
        {
            _strengthModifier = 0;
            ApplyStrength();
        }

        public void ApplyAbility()
        {
            ExecuteSpyAbility();
        }

        protected virtual void OnSpyAbilityExecuted() {}

        public override void ApplyCardEffect()
        {
            ApplyStrength();
        }

        protected override void OnInitialized()
        {
            cardType = CardType.Unit;
            _currentStrength = baseStrength;
            base.OnInitialized();
        }

        public override void OnPlacedOnBoard()
        {
            base.OnPlacedOnBoard();
            OnPlacedInMeleeRow();
        }

        public override void OnRemovedFromBoard()
        {
            base.OnRemovedFromBoard();
            OnRemovedFromMeleeRow();
        }
    }
}