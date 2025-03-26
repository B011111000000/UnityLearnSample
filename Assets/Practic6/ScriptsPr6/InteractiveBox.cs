using UnityEngine;

public class InteractiveBox : MonoBehaviour
{
    private InteractiveBox next;
    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
    }

    public void AddNext(InteractiveBox box)
    {
        next = box;
        UpdateRay();
    }

    private void Update()
    {
        if (next != null)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, next.transform.position);

            RaycastHit[] hits = Physics.RaycastAll(transform.position,
                next.transform.position - transform.position,
                Vector3.Distance(transform.position, next.transform.position));

            foreach (var hit in hits)
            {
                ObstacleItem obstacle = hit.collider.GetComponent<ObstacleItem>();
                if (obstacle != null)
                {
                    obstacle.GetDamage(Time.deltaTime);
                }
            }
        }
    }

    private void UpdateRay()
    {
        lineRenderer.enabled = next != null;
    }
}