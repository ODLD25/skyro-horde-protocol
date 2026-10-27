using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]private GameObject prefab;
    [SerializeField]private int startingAmount;
    [SerializeField, ReadOnly]private int poolSize;
    private Queue<GameObject> objectPool = new Queue<GameObject>();

    void Start()
    {
        SpawnObjects(startingAmount);
    }

    private void SpawnObjects(int amount)
    {
        if (amount <= 0) return;

        for (int i = 0; i < amount; i++)
        {
            GameObject spawnedObject = Instantiate(prefab);
            spawnedObject.GetComponent<ObjectPooler>().Initialize(this);
            spawnedObject.SetActive(false);
            objectPool.Enqueue(spawnedObject);

            poolSize = objectPool.Count;
        }
    }

    public GameObject GetObject()
    {
        if (objectPool.Count <= 1)
        {
            SpawnObjects(5);
        }

        GameObject objectToDequeue = objectPool.Dequeue();
        objectToDequeue.SetActive(true);
        
        poolSize = objectPool.Count;
        return objectToDequeue;
    }

    public void AddObject(GameObject objectToAdd)
    {
        objectToAdd.SetActive(false);
        objectPool.Enqueue(objectToAdd);
        poolSize = objectPool.Count;
    }
}
