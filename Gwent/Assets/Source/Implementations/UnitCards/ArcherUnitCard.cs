using UnityEngine;
using Gwent.Cards.Core;
using Gwent.Cards.Interfaces.RowInterfaces;
using Gwent.Cards.Interfaces.PropertyInterfaces;

namespace Gwent.Cards.Implementations.UnitCards
{
    public class ArcherUnitCard : Card, IArcher, IHasStrength, IHasSpecialAbility
    {
        [Header("Unit Settings")]
        [SerializeField] private int baseStrength = 4;
        [SerializeField] private AbilityType abilityType = AbilityType.None;
        [SerializeField] private string abilityDescription = "";

        private int _currentStrength;
        private int _strengthModifier;

        public int BaseStrength => baseStrength;
        public int CurrentStrength => _currentStrength;

        public AbilityType AbilityType => abilityType;
        public string AbilityDescription => abilityDescription;

        public void OnPlacedInArcherRow()
        {
            Debug.Log($"Лучник {cardName} размещен в ряду дальнего боя");
            ApplyStrength();
        }

        public void OnRemovedFromArcherRow()
        {
            Debug.Log($"Лучник {cardName} удален из ряда дальнего боя");
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

        public void ApplyAbility()
        {
            if (abilityType != AbilityType.None)
            {
                Debug.Log($"Применена способность {abilityType}: {abilityDescription}");
            }
        }

        public override void ApplyCardEffect()
        {
            ApplyStrength();
            ApplyAbility();
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
            OnPlacedInArcherRow();
        }

        public override void OnRemovedFromBoard()
        {
            base.OnRemovedFromBoard();
            OnRemovedFromArcherRow();
        }
    }
}