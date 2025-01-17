using UnityEngine;

public class RaycastClickHandler : MonoBehaviour
{
    public Camera mainCamera;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                Debug.Log($"클릭한 오브젝트: {hit.collider.gameObject.name}");

                // 클릭한 오브젝트에 동작 추가
                if (hit.collider.gameObject.CompareTag("Clickable"))
                {
                    Debug.Log($"{hit.collider.gameObject.name}을 클릭했습니다!");
                }
            }
        }
    }
}
