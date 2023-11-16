using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPatrolling : MonoBehaviour
{
    [SerializeField] private float _whaitTime = 3f;

    [SerializeField] private float _speed = 2f;

    private int _index;
    private float _currentTime;

    private GameObject[] _spawnedPoints;
    private Transform _destination;

    // Start is called before the first frame update
    private void Start()
    {
        // Находим объект, содержащий PointsSpawn скрипт
        GameObject pointsSpawnObject = GameObject.Find("Floor");

        // Получаем ссылку на PointsSpawn скрипт
        PointsSpawn pointsSpawnScript = pointsSpawnObject.GetComponent<PointsSpawn>();

        // Получаем созданные объекты
        _spawnedPoints = pointsSpawnScript.GetSpawnedPoints();

        // Вызываем метод патрулирования
        Patrolling();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_currentTime < _whaitTime)
        {
            _currentTime += Time.deltaTime;
            return;
        }

        var travelDistance = _speed * Time.deltaTime;
        transform.LookAt(_destination);
        var newPosition = Vector3.MoveTowards(transform.position, _destination.position, travelDistance);
        transform.position = newPosition;

        if (Vector3.Distance(transform.position, _destination.position) < 0.5)
        {
            _index++;
            Patrolling();
            _currentTime = 0f;
        }
    }

    private void Patrolling()
    {
        if (_index > _spawnedPoints.Length-1)
        {
            _index = 0;
        }

        _destination = _spawnedPoints[_index].transform;
    }
}