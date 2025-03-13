using System.Collections;
using UnityEngine;

[HelpURL("https://docs.google.com/document/d/1RMamVxE-yUpSfsPD_dEa4-Ak1qu6NTo83qY1O4XLxUY/edit?usp=sharing")]
public class DestroyModule : MonoBehaviour, IExecutable
{
    [SerializeField, Tooltip("Задержка перед уничтожением объекта (в секундах)")]
    private float destroyDelay = 1f;

    [SerializeField, Tooltip("Минимальное количество объектов, которые должны остаться")]
    private int minimalDestroyingObjectsCount = 1;

    private Transform myTransform;

    private void Awake()
    {
        myTransform = transform;
    }


    public void ActivateModule()
    {
        StartCoroutine(DestroyRandomChildObjectCoroutine());
    }

    private IEnumerator DestroyRandomChildObjectCoroutine()
    {
        while (myTransform.childCount > minimalDestroyingObjectsCount)
        {
            int index = Random.Range(0, myTransform.childCount);
            Destroy(myTransform.GetChild(index).gameObject);
            yield return new WaitForSeconds(destroyDelay);
        }
        Destroy(gameObject, Time.deltaTime);
    }
}
