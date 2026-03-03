using UnityEngine;
using System.Collections.Generic;

public class EnemySpawn : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public GameObject enemyPrefab;
        public float spawnTimer;
        public float spawnInterval;
        public int enemiesPerWave;
        public int spawnedEnemyCount;
    }

    public List<Wave> waves;
    public int waveNumber;
    public Transform minPos;
    public Transform maxPos;

    void Update()
    {
        if (PlayerMovement.Instance.gameObject.activeSelf)
        {


            // zabezpieczenie przed wyjœciem poza listê
            if (waveNumber >= waves.Count) return;

            Wave currentWave = waves[waveNumber];

            currentWave.spawnTimer += Time.deltaTime;

            // spawn tylko jeœli nie przekroczono limitu przeciwników
            if (currentWave.spawnTimer >= currentWave.spawnInterval &&
                currentWave.spawnedEnemyCount < currentWave.enemiesPerWave)
            {
                currentWave.spawnTimer = 0;
                SpawnEnemy(currentWave);
            }


            // przejœcie do kolejnej fali
            if (currentWave.spawnedEnemyCount >= currentWave.enemiesPerWave)
            {
                currentWave.spawnedEnemyCount = 0; // resetowanie licznika przeciwników dla tej fali
                if
                    (currentWave.spawnInterval > 0.3)
                    currentWave.spawnInterval *= 0.9f;
                waveNumber++;
            }

            if (waveNumber >= waves.Count)
            {
                waveNumber = 0; // resetowanie fal, jeœli chcesz, aby cykl siê powtarza³
            }
        }
    }

    private void SpawnEnemy(Wave wave)
    {
        Instantiate(wave.enemyPrefab, RandomSpawnPoint(), transform.rotation);
        wave.spawnedEnemyCount++;
    }

    private Vector2 RandomSpawnPoint()
    {
        Vector2 spawnPoint = Vector2.zero;

        // losujemy czy spawn ma byæ na górze/dole czy na lewej/prawej krawêdzi
        if (Random.Range(0f, 1f) > 0.5f)
        {
            // górna lub dolna krawêdŸ
            spawnPoint.x = Random.Range(minPos.position.x, maxPos.position.x);
            spawnPoint.y = Random.Range(0f, 1f) > 0.5f ? minPos.position.y : maxPos.position.y;
        }
        else
        {
            // lewa lub prawa krawêdŸ
            spawnPoint.y = Random.Range(minPos.position.y, maxPos.position.y);
            spawnPoint.x = Random.Range(0f, 1f) > 0.5f ? minPos.position.x : maxPos.position.x;
        }

        return spawnPoint;
    }

}
