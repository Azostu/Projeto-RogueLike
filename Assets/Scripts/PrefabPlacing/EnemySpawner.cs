using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    float percentageEnemies = 0.01f;
    [SerializeField]
    List<GameObject> enemies;
    bool enemiesCreated = false;

    private void Update()
    {
        if(enemiesCreated)
        {

            VerifyNumberOfEnemies();
        }
    }

    private void VerifyNumberOfEnemies()
    {
        int numberEnemies = GameObject.FindGameObjectsWithTag("Enemy").Count();
        if(numberEnemies == 0) {
            SceneManager.LoadScene(2);
        }
    }

    public void EnemySpawning(HashSet<Vector2Int> rooms)
    {
        int numberEnemies = Mathf.RoundToInt(rooms.Count * percentageEnemies);

        List<Vector2Int> enemiesPositions = rooms.OrderBy(x => Guid.NewGuid()).Take(numberEnemies).ToList();
        

        foreach(Vector2Int position in enemiesPositions)
        {
            Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(position.x, position.y, 0), Quaternion.identity);
        }

        enemiesCreated = true;

    } 
}
