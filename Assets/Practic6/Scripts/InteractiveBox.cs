using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    [SerializeField] private InteractiveBox next;
    private LineRenderer lineRenderer;
    private ObstacleItem currentObstacle;

    private void Awake()
    {
        // Создаем LineRenderer для визуализации луча
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Unlit/Color")) { color = Color.green };
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if (next != null)
        {
            // Обновляем позиции луча
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, next.transform.position);
            lineRenderer.enabled = true;

            // Проверяем попадание луча в препятствия
            CheckRaycast();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public void AddNext(InteractiveBox box)
    {
        // Удаляем старую связь, если она была
        if (next != null)
        {
            next = null;
        }

        // Устанавливаем новую связь
        next = box;
        Debug.Log($"Added next box: {box.name}");
    }

    private void CheckRaycast()
    {
        Vector3 direction = next.transform.position - transform.position;
        float distance = Vector3.Distance(transform.position, next.transform.position);

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        // Рисуем луч в окне Scene (для Debug)
        Debug.DrawRay(transform.position, direction, Color.green);

        if (Physics.Raycast(ray, out hit, distance))
        {
            ObstacleItem obstacle = hit.collider.GetComponent<ObstacleItem>();
            if (obstacle != null)
            {
                // Наносим урон препятствию
                obstacle.GetDamage(Time.deltaTime);
                currentObstacle = obstacle;
            }
            else if (currentObstacle != null)
            {
                currentObstacle = null;
            }
        }
        else if (currentObstacle != null)
        {
            currentObstacle = null;
        }
    }

    // Для визуализации в редакторе
    private void OnDrawGizmos()
    {
        if (next != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, next.transform.position);
        }
    }
}