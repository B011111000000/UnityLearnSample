using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenManager : MonoBehaviour
{
    [ContextMenu("Run All Scripts")]
    public void RunAllScripts()
    {
        // Находим все объекты с компонентами SampleScript
        SampleScript[] scripts = FindObjectsOfType<SampleScript>();
        IExecutable[] s = FindObjectsOfType<IExecutable>();

        // Вызываем метод Use() у каждого скрипта
        foreach (var script in scripts)
        {
            script.Use();
        }
        foreach (var script in s)
        {
            script.ActivateModule();
        }

        Debug.Log($"Ran {scripts.Length} scripts.");
    }
}
