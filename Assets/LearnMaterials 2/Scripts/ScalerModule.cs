using System.Collections;
using UnityEngine;

/// <summary>
/// Модуль для изменения масштаба объекта с заданной скоростью.
/// </summary>
[HelpURL("https://docs.google.com/document/d/1rdTEVSrCcYOjqTJcFCHj46RvnbdJhmQUb3gHMDhVftI/edit?usp=sharing")]
public class ScalerModule : MonoBehaviour
{
    [SerializeField, Tooltip("Целевой масштаб объекта.")]
    private Vector3 targetScale = new Vector3(2, 2, 2);

    [SerializeField, Tooltip("Скорость изменения масштаба."), Range(0.1f, 10f)]
    private float changeSpeed = 1f;

    private Vector3 defaultScale;
    private Transform myTransform;
    private bool toDefault;

    private void Start()
    {
        myTransform = transform;
        defaultScale = myTransform.localScale;
        toDefault = false;
        ActivateModule();
    }

    public void ActivateModule()
    {
        Vector3 target = toDefault ? defaultScale : targetScale;
        StopAllCoroutines();
        StartCoroutine(ScaleCoroutine(target));
        toDefault = !toDefault;
    }

    public void ReturnToDefaultState()
    {
        toDefault = true;
        ActivateModule();
    }

    private IEnumerator ScaleCoroutine(Vector3 target)
    {
        Vector3 start = myTransform.localScale; // Изменено на localScale для корректного масштабирования
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * changeSpeed;
            myTransform.localScale = Vector3.Lerp(start, target, t);
            yield return null;
        }
        myTransform.localScale = target;
    }
}
