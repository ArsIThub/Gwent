using UnityEngine;
using Gwent.Cards.Core;
using Gwent.Cards.Interfaces.RowInterfaces;
using Gwent.Cards.Interfaces.PropertyInterfaces;

namespace Gwent.Cards.Implementations.UnitCards
{
    public class SiegeUnitCard : Card, ISiegeWeapon, IHasStrength
    {
        [Header("Unit Settings")]
        [SerializeField] private int baseStrength = 6;

        private int _currentStrength;
        private int _strengthModifier;

        public int BaseStrength => baseStrength;
        public int CurrentStrength => _currentStrength;

        public void OnPlacedInSiegeRow()
        {
            Debug.Log($"Осадное орудие {cardName} размещено в осадном ряду");
            ApplyStrength();
        }

        public void OnRemovedFromSiegeRow()
        {
            Debug.Log($"Осадное орудие {cardName} удалено из осадного ряда");
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
            OnPlacedInSiegeRow();
        }

        public override void OnRemovedFromBoard()
        {
            base.OnRemovedFromBoard();
            OnRemovedFromSiegeRow();
        }
    }
}