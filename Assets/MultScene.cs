using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    void Start()
    {
        // Загружаем первую сцену
        SceneManager.LoadScene("Ignat_scene", LoadSceneMode.Additive);

        // Загружаем вторую сцену
        SceneManager.LoadScene("Artem_scene", LoadSceneMode.Additive);

        // Загружаем третью сцену
        SceneManager.LoadScene("Mikhail_scene", LoadSceneMode.Additive);

        // Загружаем четвертую сцену
        SceneManager.LoadScene("Tanchur_scene", LoadSceneMode.Additive);
    }
}