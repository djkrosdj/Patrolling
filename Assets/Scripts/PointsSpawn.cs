using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Класс спавна контрольных точек
/// </summary>
public class PointsSpawn : MonoBehaviour
{
    // Ссылки на места, где будут появляться объекты
    [SerializeField] private Transform[] _spawn;

    // Префабы объектов, которые будут создаваться
    [SerializeField] private GameObject[] _pointPrefab;

    // Массив для хранения созданных объектов
    private GameObject[] _spawnedPoints;

    // Вызывается при запуске сцены перед любыми вызовами метода Start
    private void Awake()
    {
        // Вызывает метод Spawn для создания объектов при запуске сцены
        Spawn();
    }

    // Метод для создания и размещения объектов в указанных местах
    private void Spawn()
    {
        // Инициализация массива для хранения созданных объектов
        _spawnedPoints = new GameObject[_spawn.Length];

        // Цикл для создания объектов для каждого указанного места
        for (var i = 0; i < _spawn.Length; i++)
        {
            // Создание объекта из префаба
            var point = Instantiate(_pointPrefab[i]);

            // Установка позиции созданного объекта в соответствии с указанным местом
            point.transform.position = _spawn[i].transform.position;

            // Добавление созданного объекта в массив для последующего доступа
            _spawnedPoints[i] = point;
        }
    }

    // Публичный метод для доступа к созданным объектам из других скриптов
    public GameObject[] GetSpawnedPoints()
    {
        return _spawnedPoints;
    }
}