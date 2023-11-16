using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс потрулирования объекта по точкам
/// </summary>
public class PlayerPatrolling : MonoBehaviour
{
    // Время ожидания между движениями к следующей точке
    [SerializeField] private float _whaitTime = 3f;

    // Скорость движения персонажа
    [SerializeField] private float _speed = 2f;

    // Индекс текущей точки, к которой движется персонаж
    private int _index;

    // Текущее время ожидания
    private float _currentTime;

    // Массив созданных точек для патрулирования
    private GameObject[] _spawnedPoints;

    // Точка, к которой движется персонаж
    private Transform _destination;

    // Вызывается при старте
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

    // Вызывается каждый кадр
    private void Update()
    {
        // Если еще не прошло нужное время ожидания
        if (_currentTime < _whaitTime)
        {
            // Увеличиваем текущее время ожидания
            _currentTime += Time.deltaTime;
            return; // Прерываем выполнение метода Update
        }

        // Вычисляем расстояние, которое нужно пройти за текущий кадр
        var travelDistance = _speed * Time.deltaTime;

        // Поворачиваем персонажа в сторону точки, к которой движемся
        transform.LookAt(_destination);

        // Вычисляем новую позицию персонажа
        var newPosition = Vector3.MoveTowards(transform.position, _destination.position, travelDistance);

        // Устанавливаем новую позицию персонажа
        transform.position = newPosition;

        // Если персонаж достиг точки назначения
        if (Vector3.Distance(transform.position, _destination.position) < 0.5)
        {
            // Увеличиваем индекс для движения к следующей точке
            _index++;

            // Вызываем метод патрулирования для движения к следующей точке
            Patrolling();

            // Сбрасываем текущее время ожидания
            _currentTime = 0f;
        }
    }

    // Метод для выбора точки назначения для патрулирования
    private void Patrolling()
    {
        // Если индекс больше, чем количество точек, сбрасываем его
        if (_index > _spawnedPoints.Length - 1)
        {
            _index = 0;
        }

        // Устанавливаем точку назначения
        _destination = _spawnedPoints[_index].transform;
    }
}
