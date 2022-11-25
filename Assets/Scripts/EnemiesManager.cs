using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public List<GameObject> enemyPrefabs = new List<GameObject>();

    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int nbEnemies = 55;
    [SerializeField] private int nbLine = 5;
    [SerializeField] private Vector3 spawnEnemyZero = new Vector3(-8, 5, 0);
    [SerializeField] private float enemiesStep = 0.5f;
    [SerializeField] private float spacingHorizontal = 1.2f;
    [SerializeField] private float spacingVertical = 1.2f;
    
    private List<Enemy> enemies;
    private bool moveRight = true;
    private float timer = 0;

    [Header("_____DEBUG____")]
    //[SerializeField] private bool startWave = false;
    public bool startWave = false;

    // Start is called before the first frame update
    void Awake()
    {
        enemies = new List<Enemy>(nbEnemies);
        SpawnEnemies();
    }

    void OnEnable()
    {
        MenuManager.OnPlay += StartWave;
        PlayerManager.OnDeath += EndWave;
    }


    void OnDisable()
    {
        MenuManager.OnPlay -= StartWave;
        PlayerManager.OnDeath -= EndWave;
    }

    private void StartWave()
    {
        startWave = true;
        foreach(Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(true);
        }
    }

    private void EndWave()
    {
        startWave = false;
        foreach (Enemy enemy in enemies)
        {
            enemy.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!startWave)
        { 
            return;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!startWave) startWave = true;
            SpawnEnemies();
        }

       
            HandleEnemiesMove();
        
    }

    void HandleEnemiesMove()
    {
        if (enemies.Count == 0) return;

        bool canMove = true;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (moveRight && enemies[i].transform.position.x + enemiesStep >= Mathf.Abs(spawnEnemyZero.x))
                canMove = false;
            else if (!moveRight && enemies[i].transform.position.x - enemiesStep <= spawnEnemyZero.x)
                canMove = false;
                
        }

        if (canMove)
        {
            switch (moveRight)
            {
                case true:
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        Vector3 pos = enemies[i].transform.position;
                        enemies[i].transform.position = new Vector3(pos.x + enemiesStep, pos.y, pos.z);
                    }
                    break;
                case false:
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        Vector3 pos = enemies[i].transform.position;
                        enemies[i].transform.position = new Vector3(pos.x - enemiesStep, pos.y, pos.z);
                    }
                    break;
            }
        }
        else
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                Vector3 pos = enemies[i].transform.position;
                enemies[i].transform.position = new Vector3(pos.x, pos.y - 0.35f, pos.z);
            }
            moveRight = !moveRight;
        }
    }

    void SpawnEnemies()
    {
        if (enemies == null) return;

        if (enemies.Count > 0)
        {
            int index = 0;
            for (int i = 0; i < nbLine; i++)
            {
                for (int j = 0; j < nbEnemies / nbLine; j++)
                {
                    Enemy enemy = enemies[index++];
                    enemy.Init(new Vector3(spawnEnemyZero.x + 0.5f + (spacingHorizontal * j), spawnEnemyZero.y + (spacingVertical * i), spawnEnemyZero.z));
                    enemy.gameObject.SetActive(true);   
                }
            }
            return;
        }

        for (int i = 0; i < nbLine; i++)
        {
            for (int j = 0; j < nbEnemies / nbLine; j++)
            {
                int rng = UnityEngine.Random.Range(0, enemyPrefabs.Count);
                GameObject enemyObject = Instantiate(enemyPrefabs[rng], transform);
                Enemy enemy = enemyObject.GetComponent<Enemy>();
                enemy.Init(new Vector3(spawnEnemyZero.x + 0.5f + (spacingHorizontal * j), spawnEnemyZero.y + (spacingVertical * i), spawnEnemyZero.z));
                enemies.Add(enemy);

                if (!startWave)
                    enemy.gameObject.SetActive(false);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(spawnEnemyZero, 0.5f);
    }
}
