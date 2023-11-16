using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PointsSpawn : MonoBehaviour
{
    [SerializeField] private Transform[] _spawn;

    [SerializeField] private GameObject[] _pointPrefab;

    private GameObject[] _spawnedPoints;
    private void Awake()
    {
        //Invoke("Spawn", 0.5f);
        Spawn();
    }

    private void Spawn()
    {
        _spawnedPoints = new GameObject[_spawn.Length];
        
        for (var i = 0; i < _spawn.Length; i++)
        {
            var point = Instantiate(_pointPrefab[i]);
            point.transform.position = _spawn[i].transform.position;
            
            _spawnedPoints[i] = point;
        }
    }
    
    // Публичный метод для доступа к созданным объектам
    public GameObject[] GetSpawnedPoints()
    {
        return _spawnedPoints;
    }
}