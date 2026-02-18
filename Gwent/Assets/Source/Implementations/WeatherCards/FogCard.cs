using UnityEngine;
using Gwent.Cards.Core;
using Gwent.Cards.Interfaces.PropertyInterfaces;

namespace Gwent.Cards.Implementations.WeatherCards
{
    public class FogCard : Card, IWeatherCard
    {
        [Header("Weather Settings")]
        [SerializeField] private string weatherEffectName = "Густой туман";

        public string WeatherEffectName => weatherEffectName;
        public RowType AffectedRow => RowType.Archer;

        public void ApplyWeatherEffect()
        {
            Debug.Log($"ПОГОДА: {weatherEffectName} окутывает поле боя!");
            Debug.Log("Все дальнобойные отряды ослаблены до 1 силы!");

            OnWeatherApplied();
        }

        public void RemoveWeatherEffect()
        {
            Debug.Log($"Эффект {weatherEffectName} рассеивается");
            OnWeatherRemoved();
        }

        protected virtual void OnWeatherApplied() {}

        protected virtual void OnWeatherRemoved() {}

        public override void ApplyCardEffect()
        {
            ApplyWeatherEffect();
        }

        protected override void OnInitialized()
        {
            cardType = CardType.Weather;
            faction = Faction.Neutral;
            base.OnInitialized();
        }
    }
}