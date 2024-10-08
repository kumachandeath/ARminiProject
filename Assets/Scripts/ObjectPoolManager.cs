using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject spherePrefab; // ��ü ������
    public int poolSize = 10;       // ������Ʈ Ǯ ũ��
    private List<GameObject> objectPool; // ������Ʈ Ǯ ����Ʈ

    [SerializeField] private Transform playerTransform; // �÷��̾� Ʈ������

    private void Start()
    {
        InitializePool();
    }

    // ������Ʈ Ǯ �ʱ�ȭ
    private void InitializePool()
    {
        objectPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(spherePrefab);
            obj.SetActive(false); // ���� �� ��Ȱ��ȭ
            objectPool.Add(obj);
        }

        // �ʱ� ��ġ ���� �� Ȱ��ȭ
        ActivateRandomObjects();
    }

    // Ǯ���� ��Ȱ��ȭ�� ������Ʈ�� ã�� ���� ��ġ�� Ȱ��ȭ
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

    // ������ ��ġ�� ���� (�÷��̾� ���� 1~3m �Ÿ�)
    private Vector3 GetRandomPosition()
    {
        Vector3 randomDir = Random.insideUnitSphere.normalized;
        float randomDistance = Random.Range(1f, 3f);
        Vector3 randomPosition = playerTransform.position + randomDir * randomDistance;
        randomPosition.y = Mathf.Clamp(randomPosition.y, 0.5f, 2f); // y�� ���� ����
        return randomPosition;
    }

    // ������Ʈ ��Ȱ��ȭ �� ��Ȱ��ȭ �� ó��
    public void DeactivateAndRespawn(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.position = GetRandomPosition();
        obj.SetActive(true);
    }
}
