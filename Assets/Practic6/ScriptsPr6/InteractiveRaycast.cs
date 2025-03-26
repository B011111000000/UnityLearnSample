using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    public GameObject prefab;
    private InteractiveBox selectedBox;

    private void Update()
    {
        HandleMouseClicks();
    }

    private void HandleMouseClicks()
    {
        if (Input.GetMouseButtonDown(0)) // Left Click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("InteractivePlane"))
                {
                    Instantiate(prefab, hit.point + hit.normal * prefab.transform.localScale.y / 2,
                        Quaternion.identity);
                }
                else
                {
                    InteractiveBox box = hit.collider.GetComponent<InteractiveBox>();
                    if (box != null)
                    {
                        if (selectedBox == null)
                        {
                            selectedBox = box;
                        }
                        else
                        {
                            selectedBox.AddNext(box);
                            selectedBox = null;
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1)) // Right Click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                InteractiveBox box = hit.collider.GetComponent<InteractiveBox>();
                if (box != null) Destroy(box.gameObject);
            }
        }
    }
}