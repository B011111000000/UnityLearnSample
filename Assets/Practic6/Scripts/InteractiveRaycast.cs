using UnityEngine;

public class InteractiveRaycast : MonoBehaviour
{
    [SerializeField] private GameObject prefab; // Префаб кубика с InteractiveBox
    private Camera mainCamera;
    private InteractiveBox selectedBox; // Для хранения выбранного InteractiveBox

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
        if (Input.GetMouseButtonDown(0)) // Левый клик
        {
            HandleLeftClick();
        }
        else if (Input.GetMouseButtonDown(1)) // Правый клик
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
            // Проверяем, попали ли в InteractiveBox
            InteractiveBox hitBox = hit.collider.GetComponent<InteractiveBox>();

            if (hitBox != null)
            {
                if (selectedBox == null)
                {
                    // Первый выбор - запоминаем
                    selectedBox = hitBox;
                    Debug.Log("Selected box: " + selectedBox.name);
                }
                else if (selectedBox != hitBox)
                {
                    // Соединяем выбранный бокс с новым
                    selectedBox.AddNext(hitBox);
                    Debug.Log($"Connected {selectedBox.name} to {hitBox.name}");
                    selectedBox = null; // Сбрасываем выбор после соединения
                }
            }
            else if (hit.collider.CompareTag("InteractivePlane"))
            {
                // Создаем новый кубик на плоскости
                CreateBoxOnPlane(hit);

                // Если был выбран бокс, сбрасываем выбор
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

        // Учитываем размер объекта и нормаль поверхности
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
                // Удаляем выбранный бокс
                Destroy(hitBox.gameObject);
                Debug.Log("Deleted box: " + hitBox.name);

                // Если удаляемый бокс был выбран, сбрасываем выбор
                if (selectedBox == hitBox)
                {
                    selectedBox = null;
                }
            }
        }
    }
}