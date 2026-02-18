using UnityEngine;

namespace Gwent.Cards.Core
{
    public abstract class Card : MonoBehaviour
    {
        [Header("Card Info")]
        [SerializeField] protected string cardId;
        [SerializeField] protected string cardName;
        [SerializeField] protected Sprite cardIcon;
        [SerializeField][TextArea(3, 10)] protected string description;
        [SerializeField] protected CardType cardType;
        [SerializeField] protected Faction faction;

        public string CardId => cardId;
        public string CardName => cardName;
        public Sprite CardIcon => cardIcon;
        public string Description => description;
        public CardType Type => cardType;
        public Faction CardFaction => faction;

        public bool IsActive { get; protected set; }
        public bool IsOnBoard { get; protected set; }

        public virtual void Initialize()
        {
            if (string.IsNullOrEmpty(cardId))
            {
                Debug.LogError($"{GetType().Name} CardId не назначен в инспекторе!");
                cardId = GetType().Name.ToLower();
            }

            OnInitialized();
        }

        public abstract void ApplyCardEffect();

        public virtual void OnPlacedOnBoard()
        {
            IsOnBoard = true;
            Debug.Log($"Карта {cardName} размещена на поле боя");
        }

        public virtual void OnRemovedFromBoard()
        {
            IsOnBoard = false;
            Debug.Log($"Карта {cardName} удалена с поля боя");
        }

        public virtual void ReturnToPool()
        {
            IsActive = false;
            IsOnBoard = false;
            gameObject.SetActive(false);
        }

        public virtual void OnSpawnFromPool()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        protected virtual void OnInitialized()
        {
            LogCardCreation();
        }

        protected virtual string GetCardFeatures()
        {
            var features = new System.Text.StringBuilder();
            var interfaces = GetType().GetInterfaces();

            foreach (var iface in interfaces)
            {
                if (iface.Name.StartsWith("I") && iface.Namespace?.Contains("Interfaces") == true)
                {
                    if (iface.Name == "IComponent" || iface.Name == "IComponentConnector")
                        continue;

                    features.Append($"{iface.Name.Replace("I", "")}, ");
                }
            }

            var result = features.ToString().TrimEnd(',', ' ');
            return string.IsNullOrEmpty(result) ? "нет особых свойств" : result;
        }

        private void LogCardCreation()
        {
            string placementType = GetPlacementType();
            string specialAbility = GetSpecialAbilityInfo();
            string strengthInfo = GetStrengthInfo();

            string logMessage = $"Создана карта {cardName}; " +
                              $"тип размещения: {placementType}; " +
                              $"тип карты: {cardType}; " +
                              $"фракция: {faction}; " +
                              $"специальная способность: {specialAbility}; " +
                              $"очки силы: {strengthInfo}; " +
                              $"особенности: {GetCardFeatures()}";


            Debug.Log(logMessage);
        }

        protected virtual string GetPlacementType()
        {
            if (this is Interfaces.RowInterfaces.IMeleeFighter) return "ближний бой";
            if (this is Interfaces.RowInterfaces.IArcher) return "дальний бой";
            if (this is Interfaces.RowInterfaces.ISiegeWeapon) return "осадный ряд";

            return "не требует размещения";
        }

        protected virtual string GetSpecialAbilityInfo()
        {
            if (this is Interfaces.PropertyInterfaces.IHasSpecialAbility ability)
            {
                return $"{ability.AbilityType} ({ability.AbilityDescription})";
            }

            return "отсутствует";
        }

        protected virtual string GetStrengthInfo()
        {
            if (this is Interfaces.PropertyInterfaces.IHasStrength strength)
            {
                return $"{strength.BaseStrength}";
            }

            return "N/A";
        }

        public override string ToString()
        {
            return $"{cardName} ({cardType})";
        }
    }
}