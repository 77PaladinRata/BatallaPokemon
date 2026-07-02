using UnityEngine;
using System.Collections.Generic;

public class PoolManager : MonoBehaviour
{
    private Dictionary<GameObject, Pool> pools = new Dictionary<GameObject, Pool>();
    public void RegisterPrefab(GameObject prefab)
    {
        if (!pools.ContainsKey(prefab))
        {
            Pool newPool = new GameObject(prefab. name + " Pool").AddComponent<Pool>();
            newPool.prefab = prefab;
            newPool. transform.parent = transform;
            pools.Add(prefab, newPool);
        }
    }
    public GameObject GetObject(GameObject prefab, Vector3 position)
    {
       if (pools.TryGetValue(prefab, out Pool pool))
        {
            return pool.InstantiateObject(position);
        }
        else
        {
            RegisterPrefab(prefab);
            return pools[prefab]. InstantiateObject(position);
        }
    }
}
