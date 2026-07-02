using UnityEngine;

public class PoolObject : MonoBehaviour
{
    private Pool pool;
    public Pool Pool
    {
        set { pool = value; }
    }
    private void OnDisable()
    {
        if (pool != null)
        {
            pool.ReturnToPool(gameObject);
        }
    }
}
