using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using PathCreation.Examples;
using DG.Tweening;
public class Player : MonoBehaviour
{
    public int id;
    PlayerParent playerParent;
    PathFollower pathFollower;
    
    private void Start()
    {
        
        pathFollower = transform.parent.GetComponent<PathFollower>();
        pathFollower.pathCreator = pathFollower.transform.parent.GetComponent<PathCreator>();
        playerParent = transform.parent.parent.GetComponent<PlayerParent>();
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        playerParent.completedCarText.transform.DOScale(new Vector3(1, 1, 1), 3).OnComplete(()=>
    //        playerParent.completedCarText.transform.DOScale(new Vector3(0, 0, 0), 1)
    //        ).SetEase(Ease.OutElastic);
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag==("ParkArea"))
        {
            int index =other.transform.parent.GetSiblingIndex();
            other.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(EditPos(other.transform));
         
            if (index==id)
            {
                playerParent.parkTrue = true;
                playerParent.confetti.Play();
                playerParent.completedCar++;
                DgText();
                playerParent.completedCarText.text = playerParent.completedCar.ToString() + " / " + playerParent.transform.childCount;
               
               

            }
            else
            {
                playerParent.Fail();
                playerParent.parkTrue = false;
            }
        }
        if (other.tag== "Enemy")
        {
            DgKill();
            PlayerObj();
            Enemy(other.gameObject);
            playerParent.Fail();
           
        }
    }
    void DgKill()
    {
        playerParent.mySequence.Kill();
        playerParent.mySequenceR.Kill();
        playerParent.mySequenceO.Kill();
        playerParent.mySequenceOR.Kill();
    }
    void PlayerObj()
    {
        playerParent.enabled = false;
        pathFollower.enabled = false;
        transform.DOBlendableLocalRotateBy(new Vector3(0, 360, 0), 0.5f, RotateMode.FastBeyond360);
        transform.DOLocalMove(new Vector3(0, -3, 3), 0.5f);
    }

    void Enemy(GameObject other)
    {
        GameObject obj = other.gameObject;
        obj.transform.parent.GetComponent<PathFollower>().enabled = false;
        obj.transform.DOBlendableLocalRotateBy(new Vector3(0, 360, 0), 0.5f, RotateMode.FastBeyond360);
        obj.transform.DOLocalMove(new Vector3(0, 3, -3), 0.5f);
    }

    void DgText()
    {
        playerParent.run = false;
        playerParent.completedCarText.transform.DOScale(new Vector3(1, 1, 1), 3).OnComplete(() =>
        playerParent.completedCarText.transform.DOScale(new Vector3(0, 0, 0), 1).OnComplete(() => {
            if (playerParent.cars.Count > 0)
            {
                playerParent.run = true;
            }
            else
            {
                Debug.Log("NEW LEVEL");
            }

        })).SetEase(Ease.OutElastic);
    }
    IEnumerator EditPos(Transform other)
    {
        yield return new WaitForSeconds(2);
        transform.parent = other.parent;
        transform.DOLocalMove(new Vector3(0,0,1), 2f);
    }
}
