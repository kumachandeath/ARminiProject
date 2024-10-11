using System.Collections.Generic;
using UnityEngine;

public class ARInteractionManager : MonoBehaviour
{
    public ObjectPoolManager objectPoolManager; // 오브젝트 풀 매니저
    [SerializeField] Camera arCamera; // AR 카메라

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                HandleTouch(touch.position);
            }
        }
    }

    // 터치된 위치에 있는 오브젝트를 감지하고 반응 처리
    private void HandleTouch(Vector2 touchPosition)
    {
        Ray ray = arCamera.ScreenPointToRay(touchPosition);
        RaycastHit hit;

        // 터치된 곳에 오브젝트가 있는지 확인
        if (Physics.Raycast(ray, out hit))
        {
            GameObject touchedObject = hit.collider.gameObject;

            // 오브젝트가 활성화된 구체인지 확인
            if (touchedObject.CompareTag("Sphere"))
            {
                // 구체 오브젝트 비활성화 후 재배치
                objectPoolManager.DeactivateAndRespawn(touchedObject);
            }
        }
    }
}
