//SampleScriptManager

using System.Collections.Generic;
using UnityEngine;

public class SampleScriptManager : MonoBehaviour
{

    // Список всех SampleScript
    public List<SampleScript> scripts = new List<SampleScript>();


    // Метод для вызова Use() у всех скриптов
    [ContextMenu("Run All Scripts")]
    public void UseAll()
    {
        foreach (var script in scripts)
        {
            script.Use();
        }
    }
}