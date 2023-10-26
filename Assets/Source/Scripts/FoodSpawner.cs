using System.Collections;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _foodPrefab;
    [SerializeField] private float _spawnRate = 2.5f;

    private void Start()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void Spawn()
    {
        Instantiate(_foodPrefab, GetRandomSpawnPosition(), Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-25, 25);
        float z = Random.Range(-25, 25);

        return new Vector3(x, 0.5f, z);
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnRate);
            Spawn();
        }
    }
}
