using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawnManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemyBoss;
    private List<GameObject> enemies = new List<GameObject>();
    public GameObject[] powerupPrefabs;
    [SerializeField] float creationRange = 7;

    private int numberInWave = 3;
    bool animStart;

    private void Start()
    {
        enemies.Add(enemy1);
        GenerateEnemyWave();
    }
    private void Update()
    {
        int enemyNumber = GameObject.FindGameObjectsWithTag("Enemy").Length;
        if (enemyNumber <= 0 && !animStart)
        {
            if (numberInWave <= 3)
            {
                if (numberInWave >= 3)
                    enemies.Add(enemy2);                
                animStart = true;
                numberInWave++;
                gameManager.StartAnimation();
                Invoke(nameof(GenerateEnemyWave), 2);
                gameManager.UpdateScore(numberInWave);

            }
            else GenerateBoss();
        }
    }

    private void GenerateBoss()
    {
        Instantiate(enemyBoss, GenerateSpawnPos(transform.position.y), Quaternion.identity);
    }

    Vector3 GenerateSpawnPos(float posY)
    {
        Vector3 pos = new(Random.Range(-creationRange, creationRange), posY, Random.Range(-creationRange, creationRange));
        return pos;
    }

    void GenerateEnemyWave()
    {
        int randomEnemy;
        int randomPower = Random.Range(0, 3);

        for (int i = 0; i < numberInWave; i++)
        {
            randomEnemy = Random.Range(0, enemies.Count);
            Instantiate(enemies[randomEnemy], GenerateSpawnPos(transform.position.y), Quaternion.identity).transform.parent = transform;
        }
        Instantiate(powerupPrefabs[randomPower], GenerateSpawnPos(0), Quaternion.identity).transform.parent = transform;
        gameManager.StopAnimation();
        animStart= false;
    }
}