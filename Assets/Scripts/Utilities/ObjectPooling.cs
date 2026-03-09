using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    private GameObject objectPrefab;
    private List<GameObject> objectPool = new();

    public void SetPrefab(GameObject prefab)
    {
        objectPrefab = prefab;
    }

    public GameObject Get()
    {
        if (objectPool == null || objectPool.Count <= 0)
        {
            var obj = Instantiate(objectPrefab);
            obj.SetActive(true);
            return obj;
        }

        objectPool[0].SetActive(true);
        return objectPool[0];
    }

    public void AddToPool(GameObject obj)
    {
        obj.SetActive(false);
        objectPool.Add(obj);
    }
}
