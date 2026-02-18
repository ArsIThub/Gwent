using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Gwent.Cards.Core;
using Gwent.Data;

namespace Gwent.Pooling
{
    public class CardObjectPool : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform poolParent;
        [SerializeField] private int preloadCount = 2;

        private CardDatabase _database;
        private Dictionary<string, Queue<Card>> _pools;
        private List<Card> _activeCards;
        private bool _isInitialized;

        public static CardObjectPool Instance { get; private set; }
        public bool IsInitialized => _isInitialized;

        private void Awake()
        {
            if (Instance != null) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Initialize(CardDatabase database)
        {
            if (_isInitialized)
            {
                Debug.LogWarning("Уже инициализирован!");
                return;
            }

            if (database == null)
            {
                Debug.LogError("Database is null!");
                return;
            }

            _database = database;
            _pools = new Dictionary<string, Queue<Card>>();
            _activeCards = new List<Card>();

            var prefabs = _database.GetAllCardPrefabs().ToList();

            if (prefabs.Count == 0)
            {
                Debug.LogError("В базе данных нет карт!");
                return;
            }

            Debug.Log($"Инициализация из {prefabs.Count} карт...");

            foreach (var prefab in prefabs)
            {
                InitializePoolForCard(prefab);
            }

            _isInitialized = true;
            Debug.Log($"Инициализация завершена. Всего пулов: {_pools.Count}");
        }

        private void InitializePoolForCard(Card prefab)
        {
            string id = prefab.CardId;

            if (string.IsNullOrEmpty(id))
            {
                Debug.LogError($"У префаба {prefab.name} не заполнен CardId!");
                return;
            }

            if (_pools.ContainsKey(id))
            {
                Debug.LogWarning($"Дубликат ID '{id}' пропущен!");
                return;
            }

            _pools[id] = new Queue<Card>();

            for (int i = 0; i < preloadCount; i++)
            {
                var instance = CreateInstance(prefab);
                ReturnToPool(instance);
            }

            Debug.Log($"Пул '{id}' ({prefab.CardName}): {preloadCount} экземпляров");
        }

        private Card CreateInstance(Card prefab)
        {
            var instance = Instantiate(prefab, poolParent);
            instance.name = $"{prefab.CardId}_instance";
            instance.gameObject.SetActive(false);
            instance.Initialize();
            return instance;
        }

        public Card Get(string cardId)
        {
            if (!_isInitialized)
            {
                Debug.LogError("Пул не инициализирован!");
                return null;
            }

            if (!_pools.TryGetValue(cardId, out var pool) || pool.Count == 0)
            {
                return CreateNewInstance(cardId);
            }

            var card = pool.Dequeue();
            Activate(card);
            return card;
        }

        private Card CreateNewInstance(string cardId)
        {
            var prefab = _database.GetPrefabById(cardId);
            if (prefab == null)
            {
                Debug.LogError($"Префаб для '{cardId}' не найден в базе!");
                return null;
            }

            var instance = CreateInstance(prefab);
            Activate(instance);
            return instance;
        }

        private void Activate(Card card)
        {
            card.OnSpawnFromPool();
            _activeCards.Add(card);
        }

        public void ReturnToPool(Card card)
        {
            if (card == null) return;

            card.ReturnToPool();
            _activeCards.Remove(card);

            if (!_pools.ContainsKey(card.CardId))
                _pools[card.CardId] = new Queue<Card>();

            _pools[card.CardId].Enqueue(card);
        }

        public void ReturnAllCards()
        {
            var cardsToReturn = new List<Card>(_activeCards);
            foreach (var card in cardsToReturn)
            {
                ReturnToPool(card);
            }
        }

        public int GetTotalPoolSize()
        {
            return _pools?.Values.Sum(q => q.Count) ?? 0;
        }

        public int GetActiveCount()
        {
            return _activeCards?.Count ?? 0;
        }
    }
}