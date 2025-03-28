using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // ������ ������ � InteractiveBox
    private Camera mainCamera;
    private InteractiveBox selectedBox; // ��� �������� ���������� InteractiveBox

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        HandleMouseClicks();
    }

    private void HandleMouseClicks()
    {
        if (Input.GetMouseButtonDown(0)) // ����� ����
        {
            HandleLeftClick();
        }
        else if (Input.GetMouseButtonDown(1)) // ������ ����
        {
            HandleRightClick();
        }
    }

    private void HandleLeftClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // ���������, ������ �� � InteractiveBox
            InteractiveBox hitBox = hit.collider.GetComponent<InteractiveBox>();

            if (hitBox != null)
            {
                if (selectedBox == null)
                {
                    // ������ ����� - ����������
                    selectedBox = hitBox;
                    Debug.Log("Selected box: " + selectedBox.name);
                }
                else if (selectedBox != hitBox)
                {
                    // ��������� ��������� ���� � �����
                    selectedBox.AddNext(hitBox);
                    Debug.Log($"Connected {selectedBox.name} to {hitBox.name}");
                    selectedBox = null; // ���������� ����� ����� ����������
                }
            }
            else if (hit.collider.CompareTag("InteractivePlane"))
            {
                // ������� ����� ����� �� ���������
                CreateBoxOnPlane(hit);

                // ���� ��� ������ ����, ���������� �����
                if (selectedBox != null)
                {
                    selectedBox = null;
                }
            }
        }
    }

    private void CreateBoxOnPlane(RaycastHit hit)
    {
        if (prefab == null)
        {
            Debug.LogError("Prefab is not assigned!");
            return;
        }

        // ��������� ������ ������� � ������� �����������
        Vector3 position = hit.point + hit.normal * (prefab.transform.localScale.y / 2);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

        GameObject newBox = Instantiate(prefab, position, rotation);
        newBox.name = "InteractiveBox_" + Time.time;
        Debug.Log("Created new box: " + newBox.name);
    }

    private void HandleRightClick()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            InteractiveBox hitBox = hit.collider.GetComponent<InteractiveBox>();
            if (hitBox != null)
            {
                // ������� ��������� ����
                Destroy(hitBox.gameObject);
                Debug.Log("Deleted box: " + hitBox.name);

                // ���� ��������� ���� ��� ������, ���������� �����
                if (selectedBox == hitBox)
                {
                    selectedBox = null;
                }
            }
        }
    }
}