using UnityEngine;
using UnityEngine.Events;

public class ObstacleItem : MonoBehaviour
{
    [SerializeField] private float currentValue = 1f;
    [SerializeField] private UnityEvent onDestroyObstacle;
    private Renderer rend;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend == null)
        {
            Debug.LogError("Renderer not found on " + gameObject.name);
        }
        UpdateColor();
    }

    public void GetDamage(float value)
    {
        Debug.Log($"Damage received: {value}, Current HP: {currentValue}");
        currentValue = Mathf.Clamp01(currentValue - value);
        UpdateColor();

        if (currentValue <= 0)
        {
            Debug.Log("Obstacle destroyed!");
            onDestroyObstacle.Invoke();
            Destroy(gameObject);
        }
    }

    private void UpdateColor()
    {
        rend.material.color = Color.Lerp(Color.red, Color.white, currentValue);
    }
}