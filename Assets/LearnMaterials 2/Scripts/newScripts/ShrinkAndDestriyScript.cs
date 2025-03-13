//ShrinkAndDestroyScript
using UnityEngine;

public class ShrinkAndDestroyScript : SampleScript
{
    public Transform target; // Целевой объект, чьи дочерние объекты будут удалены
    public float shrinkSpeed = 1f; // Скорость сжатия
    private bool isShrinking = false; // Флаг для отслеживания сжатия

    public override void Use()
    {
        isShrinking = true;
    }

    void Update()
    {
        if (isShrinking && target != null)
        {
            foreach (Transform child in target)
            {
                // Плавное сжатие
                child.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;

                // Удаление, если объект стал слишком маленьким
                if (child.localScale.x <= 0.1f)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}