using UnityEngine;

public class SpawnManagerTarget : MonoBehaviour
{
    public GameObject itemPrefab;   // 생성할 아이템
    public int spawnCount = 10;     // 몇 개 스폰할지
    public Vector3 basePosition = new Vector3(0f, 1.2f, 0.2f); // 기준 위치
    public float range = 0.2f; // 랜덤 흔들림 범위 (±0.2)

    void Start()
    {
        SpawnRandomItems();
    }

    void SpawnRandomItems()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            // 기준 위치에서 ±range 만큼 랜덤 오프셋
            Vector3 randomPos = basePosition + new Vector3(
                Random.Range(-range, range),
                Random.Range(-range, range),
                Random.Range(-range, range)
            );

            Instantiate(itemPrefab, randomPos, Quaternion.identity);
        }
    }
}
