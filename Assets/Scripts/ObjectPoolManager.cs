using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject spherePrefab; // 구체 프리팹
    public int poolSize = 10;       // 오브젝트 풀 크기
    private List<GameObject> objectPool; // 오브젝트 풀 리스트

    [SerializeField] private Transform playerTransform; // 플레이어 트랜스폼

    private void Start()
    {
        InitializePool();
    }

    // 오브젝트 풀 초기화
    private void InitializePool()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(spherePrefab);
            obj.SetActive(false); // 시작 시 비활성화
            objectPool.Add(obj);
        }

        // 초기 위치 설정 및 활성화
        ActivateRandomObjects();
    }

    // 풀에서 비활성화된 오브젝트를 찾아 랜덤 위치에 활성화
    private void ActivateRandomObjects()
    {
        foreach (var obj in objectPool)
        {
            if (!obj.activeInHierarchy)
            {
                obj.transform.position = GetRandomPosition();
                obj.SetActive(true);
            }
        }
    }

    // 랜덤한 위치를 생성 (플레이어 기준 1~3m 거리)
    private Vector3 GetRandomPosition()
    {
        Vector3 randomDir = Random.insideUnitSphere.normalized;
        float randomDistance = Random.Range(1f, 3f);
        Vector3 randomPosition = playerTransform.position + randomDir * randomDistance;
        randomPosition.y = Mathf.Clamp(randomPosition.y, 0.5f, 2f); // y축 높이 제한
        return randomPosition;
    }

    // 오브젝트 비활성화 및 재활성화 시 처리
    public void DeactivateAndRespawn(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = GetRandomPosition();
        obj.SetActive(true);
    }
}
