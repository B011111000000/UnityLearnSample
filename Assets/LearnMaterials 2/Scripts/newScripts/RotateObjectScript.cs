//RotateObjectScript
using UnityEngine;

public class RotateObjectScript : SampleScript
{
    public Vector3 targetRotation; // Угол поворота
    public float speed = 10f;      // Скорость вращения
    private bool isRotating = false; // Флаг для отслеживания вращения

    public override void Use()
    {
        isRotating = true;
    }

    void Update()
    {
        if (isRotating)
        {
            // Плавное вращение к целевому углу
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(targetRotation), speed * Time.deltaTime);

            // Остановка, если объект достиг целевого угла
            if (transform.rotation == Quaternion.Euler(targetRotation))
            {
                isRotating = false;
            }
        }
    }
}