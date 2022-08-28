using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;
using PathCreation;

public class EnemyManager : MonoBehaviour
{
    public PathCreator pathCreator;
    public GameObject enemy;
    public int adet;
    public List<GameObject> enemies = new List<GameObject>();
    void Start()
    {
        
        for (int i = 0; i < adet; i++)
        {
            GameObject obj = Instantiate(enemy, transform);
            obj.GetComponent<PathFollower>().pathCreator = pathCreator;
            enemies.Add(obj);
        }
        StartCoroutine(GroupEnemy());
       
    }
    IEnumerator GroupEnemy()
    {
        while (true)
        {
            int random = Random.Range(0, 3);
            yield return new WaitForSeconds(random);
            int r = Random.Range(0, 4);
            for (int i = 0; i < r; i++)
            {
                enemies[0].gameObject.GetComponent<PathFollower>().enabled = true;
                yield return new WaitForSeconds(0.75f);
                enemies.Remove(enemies[0]);
            }
        }
       
    }



}
