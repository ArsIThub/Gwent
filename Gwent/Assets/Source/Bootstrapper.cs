using UnityEngine;
using Gwent.Pooling;
using Gwent.Data;

namespace Gwent.Bootstrap
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CardDatabase database;

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (database == null)
            {
                Debug.LogError("CardDatabase не назначена!");
                return;
            }

            var pool = CardObjectPool.Instance;
            if (pool == null)
            {
                Debug.LogError("CardObjectPool не найден!");
                return;
            }

            pool.Initialize(database);

            Debug.Log("Инициализация завершена");
            LogPoolStatus(pool);
        }

        private void LogPoolStatus(CardObjectPool pool)
        {
            Debug.Log($"СТАТУС ПУЛА");
            Debug.Log($"В пуле: {pool.GetTotalPoolSize()}");
            Debug.Log($"Активных: {pool.GetActiveCount()}");
        }
    }
}