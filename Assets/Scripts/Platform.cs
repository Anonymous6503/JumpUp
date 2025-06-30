using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    [SerializeField] private Transform[] spikes;

    [SerializeField] private GameObject coin;

    [SerializeField]private bool _fallDown;
    private int lastValue;
    //private Rigidbody _rigidbody;
    private void Start()
    {
        lastValue = Random.Range(0, 10);
        ActivatePlatform();
        //_rigidbody = GetComponent<Rigidbody>();
    }

    void ActivateSpike()
    {
        int index = Random.Range(0, spikes.Length);
        spikes[index].gameObject.SetActive(true);
        spikes[index].DOLocalMoveY(0.7f, 1.3f).SetLoops(-1, LoopType.Yoyo).SetDelay(Random.Range(4f, 5.5f));
    }

    void AddCoin()
    {
        GameObject c = Instantiate(coin,transform.position,Quaternion.Euler(90,0,0),transform);
        //GameObject c = Instantiate(coin, transform);
        //c.transform.position = transform.position;
        c.transform.DOLocalMoveY(1f, 0f);
    }
    
    private int UniqueRandomInt(int min, int max)
    {
        int val = Random.Range(min, max);
        while(lastValue == 0 || lastValue == 6 || lastValue == 9)
        {
            val = Random.Range(min, max);
            lastValue = val;
        }
        
        return val;
    }


    void ActivatePlatform()
    {
        int chance = Random.Range(0, 100);
        if (chance > 65) // (100-chance) percent of time something will be spawned
        {
            //int type = Random.Range(0, 10);
            int type = UniqueRandomInt(0, 10);
            switch (type)
            {
                case 0: ActivateSpike();
                    break;
                case 1: AddCoin();
                    break;
                case 2: _fallDown = true;
                    break;
                case 3:
                    break;
                case 4: AddCoin();
                    break;
                case 5:
                    break;
                case 6: ActivateSpike();
                    break;
                case 7: AddCoin();
                    break;
                case 8: 
                    break;
                case 9: ActivateSpike();
                    break;
            }
        }
    }

    void InvokeFalling()
    {
        gameObject.AddComponent<Rigidbody>();
        //_rigidbody = gameObject.AddComponent<Rigidbody>();
        Destroy(gameObject,5f);
        //Destroy(this,5f);
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.CompareTag("Player"))
        {
            if (_fallDown)
            {
                _fallDown = false;
                Invoke("InvokeFalling",2.15f);
            }
        }
    }

    private void OnBecameInvisible()
    {
        enabled = false;
    }

    private void OnBecameVisible()
    {
        enabled = true;
    }
}
