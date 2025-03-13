//MoveObjectScript
using UnityEngine;

public class MoveObjectScript : SampleScript
{
    public Vector3 targetPosition; // Целевая точка
    public float speed = 1f;       // Скорость перемещения
    private bool isMoving = false; // Флаг для отслеживания движения

    public override void Use()
    {
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            // Плавное перемещение к целевой точке
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Остановка, если объект достиг цели
            if (transform.position == targetPosition)
            {
                isMoving = false;
            }
        }
    }
}