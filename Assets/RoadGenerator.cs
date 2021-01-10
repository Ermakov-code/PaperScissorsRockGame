using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float startCountRoad = 5;
    [SerializeField] private float startCountPSR = 10f;

    private List<GameObject> objectMass;
    
    
    private Transform _lastRoad;
    private Transform _lastPSR;

    private int _indexOfRandObj;

    private Random Ram;
    

    Poller _poller;
    

    void Start()
    {
        Ram = new Random();
        objectMass = new List<GameObject>();
        _poller = Poller.Instance;
        
        for (int i = 0; i < startCountRoad; i++)
        {
            GameObject spawnedRoad = _poller.SpawnFromPool("Road", target.transform.position + new Vector3(0, -1.13f, i * 30), Quaternion.identity);
            _lastRoad = spawnedRoad.transform;
        }

        for (int i = 0; i < startCountPSR; i++)
        {
            objectMass.Add(_poller.SpawnFromPool("Paper",
                transform.position, Quaternion.identity));
            objectMass.Add(_poller.SpawnFromPool("Rock", transform.position, Quaternion.identity));
            objectMass.Add(_poller.SpawnFromPool("Scissors", transform.position, Quaternion.identity));
            
            for (int j = 0; j < 3; j++)
            {
                _indexOfRandObj = Ram.Next(objectMass.Count);
                GameObject randObj = objectMass[_indexOfRandObj];
                randObj.transform.position = target.position + new Vector3(((j * 4) - 4), 0, (i * 20) + 30);
                _lastPSR = objectMass[_indexOfRandObj].transform;
                objectMass.RemoveAt(_indexOfRandObj);
            }
            objectMass.Clear();
        }
    }
    
    

   
    void LateUpdate()
    {
        if ((target.transform.position - _lastRoad.position).magnitude < 115f)
        {
            GameObject spawnedRoad =
                _poller.SpawnFromPool("Road", _lastRoad.position + new Vector3(0, 0, 30), Quaternion.identity);
            _lastRoad = spawnedRoad.transform;
        }

        if ((target.transform.position - _lastPSR.position).magnitude < 115f)
        {
            GameObject objForSpawn = _poller.SpawnFromPool("Paper",
                transform.position, Quaternion.identity);
            objectMass.Add(objForSpawn);
            objectMass.Add(_poller.SpawnFromPool("Rock", transform.position, Quaternion.identity));
            objectMass.Add(_poller.SpawnFromPool("Scissors", transform.position, Quaternion.identity));
            
            for (int j = 0; j < 3; j++)
            {
                _indexOfRandObj = Ram.Next(objectMass.Count);
                GameObject randObj = objectMass[_indexOfRandObj];
                randObj.transform.position = new Vector3(((j * 4) - 4), _lastPSR.position.y, (_lastPSR.position.z) + 20);
                objectMass.RemoveAt(_indexOfRandObj);
            }
            _lastPSR = objForSpawn.transform;
            objectMass.Clear();
        }
    }
}
