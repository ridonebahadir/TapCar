using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;
using DG.Tweening;
using System;
using UnityEngine.UI;


[System.Serializable]
public class ColorTone 
{
   
    public Color[] color;
}

public class PlayerParent : MonoBehaviour
{
    List<int> numbersToChooseFrom = new List<int>();
    List<GameObject> cars = new List<GameObject>();
    public PathCreator pathCreator;
    public ColorTone[] colorTones;
    bool oneTime;
    public Material[] material;
    
    
    [Space(50)]
    [Header("Final")]
    public ParticleSystem confetti;
    public Text completedCarText;
    public int completedCar;
 
    void Start()
    {
        
        int rand = UnityEngine.Random.Range(0,colorTones.Length);
        for (int i = 0; i < transform.childCount; i++)
        {
            numbersToChooseFrom.Add(i);
        }
       
        transform.position = pathCreator.bezierPath.points[0];
        //transform.position = pathCreator.path.localPoints[0];


        for (int i = 0; i < transform.childCount; i++)
        {
            cars.Add(transform.GetChild(i).gameObject);
           

            int randomNumber = numbersToChooseFrom[UnityEngine.Random.Range(0, numbersToChooseFrom.Count)];
            cars[i].gameObject.transform.GetChild(0).GetComponent<Player>().id = randomNumber;
            numbersToChooseFrom.Remove(randomNumber);
            material[i].color = colorTones[rand].color[randomNumber];
            
        }
    }
   
    void Update()
    {
       
        if (Input.GetMouseButton(0))
        {
            if (!oneTime)
            {
                OnRoad();
                cars[0].transform.GetChild(0).transform.parent.GetComponent<PathFollower>().enabled = true;
                oneTime = true;
                
            }

        }
        else
        {
            
            if (oneTime)
            {
                OffRoad();
                oneTime = false;
                cars[0].transform.GetChild(0).transform.parent.GetComponent<PathFollower>().enabled = false;
               
               
                

            }
        }
          
           
    }
    public Sequence mySequence;
    public Sequence mySequenceR;


    public Sequence mySequenceO;
    public Sequence mySequenceOR;
    private void OnRoad()
    {
        mySequence = DOTween.Sequence();
        mySequenceR = DOTween.Sequence();
        GameObject obj = cars[0].transform.GetChild(0).gameObject;
        mySequence.Append(obj.transform.DOLocalMove(Vector3.zero, 1f).OnComplete(() => StartCoroutine(CarMoveForward())));
        mySequenceR.Append(obj.transform.DOLocalRotate(new Vector3(60, 0, 90), 0.5f).OnComplete(() => obj.transform.DOLocalRotate(new Vector3(90, -90, 0), 0.5f)));

       
    }
    void OffRoad()
    {
        mySequenceO = DOTween.Sequence();
        mySequenceOR = DOTween.Sequence();
        GameObject obj = cars[0].transform.GetChild(0).gameObject;
        mySequenceO.Append(obj.transform.DOLocalMove(new Vector3(0, 3f, 1f), 1f).OnComplete(() => cars.RemoveAt(0)));
        mySequenceOR.Append(obj.transform.DOLocalRotate(new Vector3(0, 0, 90), 1f));
    }
    IEnumerator CarMoveForward()
    {

        for (int i = 1; i < cars.Count; i++)
        {
            GameObject car = cars[i].transform.gameObject;
            cars[i].transform.DOLocalMoveZ(cars[i].transform.localPosition.z + 3, 0.3f);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
