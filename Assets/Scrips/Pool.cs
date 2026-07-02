using UnityEngine;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{
    private Stack<GameObject> poolStack = new Stack<GameObject>();
    private readonly HashSet<GameObject> activeObjects = new HashSet<GameObject>();
    public IReadOnlyCollection<GameObject> ActiveObjects => activeObjects;
    public GameObject prefab;
    public GameObject InstantiateObject(Vector3 position)
    {
        GameObject currentObject;
        if (poolStack.Count > 0)
        {
            currentObject = poolStack.Pop();
            currentObject.SetActive(true);
            currentObject.transform.position = position;
            currentObject.transform.rotation = Quaternion.identity;
        }
        else
        {
            currentObject = Instantiate(prefab, position, Quaternion.identity);
            currentObject.AddComponent<PoolObject>().Pool = this;
        }
        activeObjects.Add(currentObject);
        return currentObject;
    }
    public GameObject InstantiateObject(Transform parent)
    {
        return InstantiateObject(parent.position);   
    }
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
        poolStack.Push(obj);
        activeObjects.Remove(obj);
    }
    public void DeactivateAllObjects()
    {
        foreach (var obj in activeObjects)
        {
            obj.SetActive(false);
            poolStack.Push(obj);
        }
        activeObjects.Clear();
    }
}
