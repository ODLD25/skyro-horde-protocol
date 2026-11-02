using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]private GameObject prefab;
    [SerializeField]private int startingAmount;
    [SerializeField]private bool keepItFilled;
    private bool queueInitialzied;
    private int poolSize;
    private Queue<GameObject> objectPool = new Queue<GameObject>();

    void Start()
    {
        if (!queueInitialzied)
        {
            SpawnObjects(startingAmount);
            queueInitialzied = true;
        }
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
        if (objectPool.Count == 0 && !queueInitialzied)
        {
            SpawnObjects(startingAmount);
            queueInitialzied = true;
        }
        if (objectPool.Count <= 1 && keepItFilled)
        {
            SpawnObjects(5);
        }
        else if (objectPool.Count == 0) {Debug.LogWarning("Pool is empty even though something is trying to acces it."); return null;}

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
