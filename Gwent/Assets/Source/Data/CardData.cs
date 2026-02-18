using Gwent.Cards.Core;
using UnityEngine;

namespace Gwent.Data
{
    [CreateAssetMenu(fileName = "NewCard", menuName = "Gwent/Card Data")]
    public class CardData : ScriptableObject
    {
        public GameObject CardPrefab;

        private void OnValidate()
        {
            if (CardPrefab == null) return;

            var card = CardPrefab.GetComponent<Card>();
            if (card == null)
            {
                Debug.LogError("[CardData] На префабе нет компонента Card!", this);
                return;
            }

            if (string.IsNullOrEmpty(card.CardId))
            {
                Debug.LogError($"[CardData] У карты {CardPrefab.name} не заполнен CardId!", this);
            }
        }
    }
}