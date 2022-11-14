using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private int nbEnemies = 55;
    [SerializeField] private int nbLine = 5;
    [SerializeField] private Vector3 spawnEnemyZero = new Vector3(-8, 5, 0);
    [SerializeField] private float enemiesStep = 0.5f;
    
    private List<Enemy> enemies;
    private bool moveRight = true;
    private float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<Enemy>(nbEnemies);
        for (int i = 0; i < nbLine; i++)
        {
            for (int j = 0; j < nbEnemies / nbLine; j++)
            {
                Enemy enemy = Instantiate(enemyPrefab);
                enemy.Init(new Vector3(spawnEnemyZero.x + 0.5f + (1.2f * j), spawnEnemyZero.y + (1.2f * i), spawnEnemyZero.z));
                enemies.Add(enemy);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 0.5f)
        {
            timer -= 0.5f;
            HandleEnemiesMove();
        }
    }

    void HandleEnemiesMove()
    {
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
                enemies[i].transform.position = new Vector3(pos.x, pos.y - enemiesStep, pos.z);
            }
            moveRight = !moveRight;
        }
    }
}
