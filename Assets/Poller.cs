using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poller : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    [SerializeField] private List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> PoolDictionary;
    
    
    public static Poller Instance;

    private void Awake()
    {
        Instance = this;
    }


    private void OnEnable()
    {
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            
            PoolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {

        if (!PoolDictionary.ContainsKey(tag))
        {
            Debug.Log("pool with tag doesnt exist: " + tag);
            return null;
        }

        GameObject objToSpawn = PoolDictionary[tag].Dequeue();
        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;
        PoolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }
}
