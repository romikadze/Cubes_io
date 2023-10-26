using System.Collections;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private float _mapSizeXZ = 20;
    [SerializeField] private int _maxEnemies = 3;

    //Game starts with player and 3 enemies
    private void Start()
    {
        SpawnEnemy();
        SpawnEnemy();
        SpawnEnemy();
        SpawnPlayer();
        StartCoroutine(SpawnCoroutine());
    }

    private void SpawnPlayer()
    {
        GameObject cube = Instantiate(_cubePrefab, GetRandomSpawnPosition(), Quaternion.identity);

        cube.AddComponent<CameraMovement>();
        cube.AddComponent<ChangeEnemyColors>();
        cube.GetComponent<MeshRenderer>().material.color = new Color(0.5f, 0.25f, 0.5f);
        cube.AddComponent<KeyboardMovement>();
    }

    private void SpawnEnemy()
    {
        GameObject cube = Instantiate(_cubePrefab, GetRandomSpawnPosition(), Quaternion.identity);
        cube.AddComponent<EnemyAI>();
    }

    private Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(-_mapSizeXZ, _mapSizeXZ);
        float z = Random.Range(-_mapSizeXZ, _mapSizeXZ);

        return new Vector3(x, 0.5f, z);
    }

    //Every 10 seconds checks for player and enemies. Spawn if someone dies
    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);

            if (FindObjectsOfType<KeyboardMovement>().Length == 0)
                SpawnPlayer();
            if (FindObjectsOfType<EnemyAI>().Length < _maxEnemies)
                SpawnEnemy();
        }
    }
}
