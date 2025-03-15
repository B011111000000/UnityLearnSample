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

        // Находим все объекты, которые реализуют интерфейс IExecutable
        MonoBehaviour[] allMonoBehaviours = FindObjectsOfType<MonoBehaviour>();
        List<IExecutable> executables = new List<IExecutable>();

        foreach (var mb in allMonoBehaviours)
        {
            if (mb is IExecutable executable)
            {
                executables.Add(executable);
            }
        }

        // Вызываем метод Use() у каждого скрипта
        foreach (var script in scripts)
        {
            script.Use();
        }

        // Вызываем метод ActivateModule() у каждого объекта, реализующего IExecutable
        foreach (var executable in executables)
        {
            executable.ActivateModule();
        }

        Debug.Log($"Ran {scripts.Length} scripts and {executables.Count} executables.");
    }
}