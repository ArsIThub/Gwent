using UnityEngine;
using Gwent.Cards.Core;
using Gwent.Cards.Interfaces.RowInterfaces;
using Gwent.Cards.Interfaces.PropertyInterfaces;

namespace Gwent.Cards.Implementations.UnitCards
{
    public class MeleeUnitCard : Card, IMeleeFighter, IHasStrength
    {
        [Header("Unit Settings")]
        [SerializeField] private int baseStrength = 5;

        private int _currentStrength;
        private int _strengthModifier;

        public int BaseStrength => baseStrength;
        public int CurrentStrength => _currentStrength;

        public void OnPlacedInMeleeRow()
        {
            Debug.Log($"Боец {cardName} размещен в ряду ближнего боя");
            ApplyStrength();
        }

        public void OnRemovedFromMeleeRow()
        {
            Debug.Log($"Боец {cardName} удален из ряда ближнего боя");
        }

        public void ApplyStrength()
        {
            _currentStrength = baseStrength + _strengthModifier;
            Debug.Log($"Сила {cardName}: {_currentStrength}");
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