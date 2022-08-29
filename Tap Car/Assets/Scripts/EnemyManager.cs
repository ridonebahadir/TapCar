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

        CloneEnemy();
        StartCoroutine(GroupEnemy());
       
    }
    IEnumerator GroupEnemy()
    {
        while (true)
        {
            int random = Random.Range(2, 6);
            yield return new WaitForSeconds(random);
            int r = Random.Range(0, 4);
            for (int i = 0; i < r; i++)
            {
               
                if (enemies.Count>0)
                {
                    enemies[0].gameObject.GetComponent<PathFollower>().enabled = true;
                    yield return new WaitForSeconds(0.75f);
                    enemies.Remove(enemies[0]);

                }
                else
                {
                    CloneEnemy();
                }

            }
        }
       
    }

    void CloneEnemy()
    {
        for (int i = 0; i < adet; i++)
        {
            GameObject obj = Instantiate(enemy, transform);
            int randomEnemy = Random.Range(0, obj.transform.GetChild(0).childCount);
            obj.transform.GetChild(0).GetChild(randomEnemy).gameObject.SetActive(true);
            obj.GetComponent<PathFollower>().pathCreator = pathCreator;
            enemies.Add(obj);
        }
    }

}
