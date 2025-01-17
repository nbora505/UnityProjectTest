using UnityEngine;

public class RaycastClickHandler : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ��
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log($"Ŭ���� ������Ʈ: {hit.collider.gameObject.name}");

                // Ŭ���� ������Ʈ�� ���� �߰�
                if (hit.collider.gameObject.CompareTag("Clickable"))
                {
                    Debug.Log($"{hit.collider.gameObject.name}�� Ŭ���߽��ϴ�!");
                }
            }
        }
    }
}
