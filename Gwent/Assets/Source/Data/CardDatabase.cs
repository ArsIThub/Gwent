using Gwent.Cards.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gwent.Data
{
    [CreateAssetMenu(fileName = "CardDatabase", menuName = "Gwent/Card Database")]
    public class CardDatabase : ScriptableObject
    {
        [SerializeField] private List<CardData> cards = new();

        public IReadOnlyList<CardData> AllCards => cards;

        public IEnumerable<Card> GetAllCardPrefabs()
        {
            return cards.Where(c => c?.CardPrefab != null).Select(c => c.CardPrefab.GetComponent<Card>()).Where(c => c != null);
        }

        public Card GetPrefabById(string cardId)
        {
            var data = cards.FirstOrDefault(c =>
            {
                if (c?.CardPrefab == null) return false;
                var card = c.CardPrefab.GetComponent<Card>();
                return card != null && card.CardId == cardId;
            });

            return data?.CardPrefab?.GetComponent<Card>();
        }

        public IEnumerable<Card> GetPrefabsByType(CardType type)
        {
            return GetAllCardPrefabs().Where(c => c.Type == type);
        }

        public IEnumerable<Card> GetPrefabsByFaction(Faction faction)
        {
            return GetAllCardPrefabs().Where(c => c.CardFaction == faction);
        }
    }
}