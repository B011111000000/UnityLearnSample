//SpawnObjectScript

using UnityEngine;

public class SpawnObjectsScript : SampleScript
{
    public GameObject prefab; // Префаб для создания копий
    public int count = 5;     // Количество копий
    public float step = 1f;   // Шаг между копиями

    public override void Use()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = transform.position + new Vector3(i * step, 0, 0);
            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}