using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnerPositions;
    public int difficulty = 2;
    
    public GameObject[] enemies;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("IncreaseDifficulty");
        StartCoroutine("SpawnEnemies");

        //enemie = Resources.Load("")

    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator IncreaseDifficulty()
    {
        while (difficulty < 8)
        {
            difficulty++;
            //Debug.Log(difficulty);
            yield return new WaitForSeconds(40); 
            
        }
    }

    private IEnumerator SpawnEnemies()
    {
        for (int i=0 ; i < difficulty ; i++)
        {
            if (Random.Range(0, 10) < difficulty+1)
            {
                int index = Random.Range(0, enemies.Length);
                Instantiate(enemies[index], spawnerPositions[i].position, Quaternion.identity);
                //Debug.Log(i);
            }

        }
        yield return new WaitForSeconds(4); 
        StartCoroutine("SpawnEnemies");

    }
}
