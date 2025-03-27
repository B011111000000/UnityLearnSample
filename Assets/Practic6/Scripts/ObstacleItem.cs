using UnityEngine;
using UnityEngine.Events;

public class ObstacleItem : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float currentValue = 1f;
    [SerializeField] private UnityEvent onDestroyObstacle;

    private Renderer obstacleRenderer;
    private Color originalColor;

    /*private void Update()
    {
        GetDamage(0.1f);
    }*/
    private void Awake()
    {
        obstacleRenderer = GetComponent<Renderer>();
        if (obstacleRenderer == null)
        {
            obstacleRenderer = GetComponentInChildren<Renderer>();
        }

        originalColor = obstacleRenderer.material.color;
        UpdateColor();
    }

    public void GetDamage(float value)
    {
        if (currentValue <= 0) return; // Уже уничтожено

        currentValue -= value;
        currentValue = Mathf.Clamp01(currentValue);

        UpdateColor();

        if (currentValue <= 0)
        {
            onDestroyObstacle?.Invoke();
            Destroy(gameObject);
        }
    }

    private void UpdateColor()
    {
        if (obstacleRenderer != null)
        {
            obstacleRenderer.material.color = Color.Lerp(
                Color.red,
                originalColor,
                currentValue
            );
        }
    }

    // Для отладки в редакторе
    private void OnValidate()
    {
        if (obstacleRenderer == null)
        {
            obstacleRenderer = GetComponent<Renderer>();
            if (obstacleRenderer == null)
            {
                obstacleRenderer = GetComponentInChildren<Renderer>();
            }
        }

        if (obstacleRenderer != null)
        {
            originalColor = obstacleRenderer.sharedMaterial.color;
            UpdateColor();
        }
    }
}