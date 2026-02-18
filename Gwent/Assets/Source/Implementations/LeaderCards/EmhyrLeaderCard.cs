using UnityEngine;
using Gwent.Cards.Core;
using Gwent.Cards.Interfaces.PropertyInterfaces;

namespace Gwent.Cards.Implementations.LeaderCards
{
    public class EmhyrLeaderCard : Card, ILeader, IRareCard
    {
        [Header("Leader Settings")]
        [SerializeField] private string leaderAbilityName = "Императорский взгляд";
        [SerializeField][TextArea(2, 5)] private string leaderAbilityDesc = "Посмотрите 3 карты из руки противника, верните одну в колоду";

        private bool _ultimateReady = true;

        public string LeaderAbilityName => leaderAbilityName;
        public string LeaderAbilityDescription => leaderAbilityDesc;
        public bool IsUltimateReady => _ultimateReady;

        public bool IsRare => true;
        public string RarityDescription => "Легендарный император Нильфгаарда";

        public void ActivateUltimate()
        {
            if (!_ultimateReady)
            {
                Debug.LogWarning($"Ульта {cardName} уже использована!");
                return;
            }

            Debug.Log($"АКТИВАЦИЯ УЛЬТЫ:{cardName} - {leaderAbilityName}!");
            Debug.Log("Просмотр карт противника...");

            OnUltimateActivated();

            _ultimateReady = false;
        }

        public void ResetUltimate()
        {
            _ultimateReady = true;
            Debug.Log($"Ульта {cardName} восстановлена");
        }

        protected virtual void OnUltimateActivated() {}

        public override void ApplyCardEffect()
        {
            Debug.Log($"Лидер {cardName}: пассивный эффект лидера активен");
        }

        protected override void OnInitialized()
        {
            cardType = CardType.Leader;
            faction = Faction.Nilfgaard;
            base.OnInitialized();
        }
    }
}