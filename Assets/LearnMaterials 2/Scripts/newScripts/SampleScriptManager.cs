//SampleScriptManager

using System.Collections.Generic;
using UnityEngine;

public class SampleScriptManager : MonoBehaviour
{
    void Start()
    {
        UseAll();
    }

    // Список всех SampleScript
    public List<SampleScript> scripts = new List<SampleScript>();

    // Метод для вызова Use() у всех скриптов
    public void UseAll()
    {
        foreach (var script in scripts)
        {
            script.Use();
        }
    }
}