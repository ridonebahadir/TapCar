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
        playerParent = transform.parent.parent.GetComponent<PlayerParent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag==("ParkArea"))
        {
            int index =other.transform.parent.GetSiblingIndex();
          
         
            if (index==id)
            {
                Debug.Log("YEAH");
            }
            else
            {
                Debug.Log("FAIL");
            }
        }
        if (other.tag== "Enemy")
        {
            DgKill();
            PlayerObj();
            Enemy(other.gameObject);
           
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
}
