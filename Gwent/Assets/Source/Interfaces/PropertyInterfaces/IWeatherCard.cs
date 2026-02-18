using Gwent.Cards.Core;

namespace Gwent.Cards.Interfaces.PropertyInterfaces
{
    public interface IWeatherCard
    {
        string WeatherEffectName { get; }
        RowType AffectedRow { get; }

        void ApplyWeatherEffect();
        void RemoveWeatherEffect();
    }
}