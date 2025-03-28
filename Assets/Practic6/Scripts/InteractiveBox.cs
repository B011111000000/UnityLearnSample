using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    [SerializeField] private InteractiveBox next;
    private LineRenderer lineRenderer;
    private ObstacleItem currentObstacle;

    private void Awake()
    {
        // ������� LineRenderer ��� ������������ ����
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
            // ��������� ������� ����
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, next.transform.position);
            lineRenderer.enabled = true;

            // ��������� ��������� ���� � �����������
            CheckRaycast();
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    public void AddNext(InteractiveBox box)
    {
        // ������� ������ �����, ���� ��� ����
        if (next != null)
        {
            next = null;
        }

        // ������������� ����� �����
        next = box;
        Debug.Log($"Added next box: {box.name}");
    }

    private void CheckRaycast()
    {
        Vector3 direction = next.transform.position - transform.position;
        float distance = Vector3.Distance(transform.position, next.transform.position);

        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        // ������ ��� � ���� Scene (��� Debug)
        Debug.DrawRay(transform.position, direction, Color.green);

        if (Physics.Raycast(ray, out hit, distance))
        {
            ObstacleItem obstacle = hit.collider.GetComponent<ObstacleItem>();
            if (obstacle != null)
            {
                // ������� ���� �����������
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

    // ��� ������������ � ���������
    private void OnDrawGizmos()
    {
        if (next != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, next.transform.position);
        }
    }
}