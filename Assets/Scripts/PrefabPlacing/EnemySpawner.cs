using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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
    [SerializeField]
    GameObject shop;
    [SerializeField]
    GameObject boss;
    bool enemiesCreated = false;

    private int level = 1;
    [SerializeField]
    private int maxLevel = 10;
    [SerializeField]
    private CorridorFirstDungeonGeneration corridorFirstDungeonGeneration;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    GameObject panel;
    [SerializeField]
    GameObject canvas;

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
            
            if (level == maxLevel)
            {
                StartCoroutine(End());
            }
            else
            {
                level++;
                enemiesCreated = false;

                StartCoroutine(NextLevel());
            }
            
            


            
        }
    }

    private IEnumerator NextLevel()
    {
        
        yield return new WaitForSeconds(5);

        canvas.GetComponent<UIDisplay>().updateLevelText(level);

        if (level % 5 == 0)
        {
            panel.SetActive(true);
            corridorFirstDungeonGeneration.SetCorridorValues(100, 0);
            corridorFirstDungeonGeneration.GenerateDungeonWithoutEnemies();
            player.transform.position = Vector3.zero;
            canvas.GetComponent<UIDisplay>().StartTimer();
            yield return new WaitForSeconds(30);
            panel.SetActive(false);
            corridorFirstDungeonGeneration.GenerateDungeon();
            player.transform.position = Vector3.zero;


            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().MultiplyValues(2f, 1.5f);
            }
            boss.GetComponent<Enemy>().MultiplyValues(1.5f, 1.5f);

            corridorFirstDungeonGeneration.ResetCorridorValues();

        }
        else
        {
            foreach(GameObject enemy in enemies)
            {
                 enemy.GetComponent<Enemy>().MultiplyValues(1.1f,1);
            }
            corridorFirstDungeonGeneration.GenerateDungeon();
            player.transform.position = Vector3.zero;
        }

        

        

    }

    private IEnumerator End()
    {
        enemiesCreated = false;
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(2);
    }

    public void EnemySpawning(HashSet<Vector2Int> rooms)
    {
        if(level % 5 == 0)
        {
            int numberEnemies = 1;

            List<Vector2Int> enemiesPositions = rooms.OrderBy(x => Guid.NewGuid()).Take(numberEnemies).ToList();

            Instantiate(boss, new Vector3(enemiesPositions[0].x, enemiesPositions[0].y, 0), Quaternion.identity);
        }
        else
        {
            int numberEnemies = Mathf.RoundToInt(rooms.Count * percentageEnemies);

            List<Vector2Int> enemiesPositions = rooms.OrderBy(x => Guid.NewGuid()).Take(numberEnemies).ToList();


            foreach (Vector2Int position in enemiesPositions)
            {
                Instantiate(enemies[Random.Range(0, enemies.Count)], new Vector3(position.x, position.y, 0), Quaternion.identity);
            }
        }
        

        enemiesCreated = true;

    }

    public void SpecialSpawning(HashSet<Vector2Int> rooms)
    {
        int numberEnemies = 1;

        List<Vector2Int> enemiesPositions = rooms.OrderBy(x => Guid.NewGuid()).Take(numberEnemies).ToList();


        foreach (Vector2Int position in enemiesPositions)
        {
           GameObject shopGO = Instantiate(shop, new Vector3(position.x, position.y, 0), Quaternion.identity);
            Destroy(shopGO,30);
        }

       

    }

    public void ResetEnemies()
    {
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().ResetValues();
        }
    }
}
