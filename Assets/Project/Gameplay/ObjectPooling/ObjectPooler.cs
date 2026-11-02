using System;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField]private float timeToDeactivate;
    [SerializeField]private bool hasBeenInitialized;
    private ObjectPool objectPool;

    public void Initialize(ObjectPool objectPool)
    {
        this.objectPool = objectPool;
        hasBeenInitialized = true;
    }

    public void Deactivate()
    {
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    
        objectPool.AddObject(gameObject);
    }

    void OnEnable()
    {
        if (hasBeenInitialized)
        {
            if (timeToDeactivate > 0f)
            {
                Invoke(nameof(Deactivate), timeToDeactivate);
            }
        }
    }

    void OnDisable()
    {
        CancelInvoke(nameof(Deactivate));
    }
}
