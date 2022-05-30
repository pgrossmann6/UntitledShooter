using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnerPositions;
    public int difficulty = 2;

    [SerializeField] private int EnemiesActives;
    
    public GameObject[] enemies;

    private int maxIndex;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("IncreaseDifficulty");
        StartCoroutine("SpawnEnemies");

        EnemiesActives = 0;
        EnemyAI.Died += RemoveEnemy;
        maxIndex = 1;

        //enemie = Resources.Load("")

    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnDisable()
    {
        EnemyAI.Died -= RemoveEnemy;
    }

    private IEnumerator IncreaseDifficulty()
    {
        while (difficulty < 8)
        {
            difficulty++;
            if (difficulty == 4) maxIndex = 2;
            if (difficulty == 5) maxIndex = 3;
            if (difficulty == 6) maxIndex = 4;
            if (difficulty == 7) maxIndex = 5;
            //Debug.Log(difficulty);
            yield return new WaitForSeconds(60); 
            
        }
        

    }

    private IEnumerator SpawnEnemies()
    {
        //if (EnemiesActives > difficulty+1)
        for (int i=0 ; i < difficulty ; i++)
        {
            if (Random.Range(0, 10) < difficulty+1 && (EnemiesActives < difficulty+1))
            {
                //int index = Random.Range(0, enemies.Length);
                int index = Random.Range(0, maxIndex);

                Instantiate(enemies[index], spawnerPositions[i].position, Quaternion.identity);
                //Debug.Log(i);
                EnemiesActives++;
            }

        }
        yield return new WaitForSeconds(4); 
        StartCoroutine("SpawnEnemies");

    }

    public void RemoveEnemy()
    {
        EnemiesActives--;
    }
}
