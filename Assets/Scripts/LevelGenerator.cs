using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;
    [SerializeField] private GameObject[] platforms;
    [SerializeField] private float blockWidth = 0.5f;
    [SerializeField] private float blockHeight = 0.2f;

    [SerializeField] private int amountToSpawn = 100;

    private int _beginAmount = 0;

    public Vector3 lastPos;
    
    private List<GameObject> spawnedPlatforms = new List<GameObject>();

    [SerializeField] private GameObject[] players;
    public GameObject playerPrefab;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        if (playerPrefab == null)
        {
            playerPrefab = players[SaveManager.instance.CurrentlySelectedPlayer()];
        }
        
        InstantiateLevel();
    }

    void InstantiateLevel()
    {
        for (int i = _beginAmount; i < amountToSpawn; i++)
        {
            GameObject newPlatform;
            if (i == 0)
            {
                newPlatform = Instantiate(platforms[0]);
            }else if (i == amountToSpawn - 1)
            {
                newPlatform = Instantiate(platforms[2]);
                newPlatform.tag = "EndPlatform";
            }
            else
            {
                newPlatform = Instantiate(platforms[1]);
            }

            newPlatform.transform.parent = transform;
            spawnedPlatforms.Add(newPlatform);

            if (i == 0)
            {
                lastPos = newPlatform.transform.position;
                //instantiate the player
                Vector3 temp = lastPos;
                temp.y +=0.5f;
                Instantiate(playerPrefab, temp, Quaternion.identity);
                continue;
            }

            int left = Random.Range(0, 2);

            if (left == 0)
            {
                newPlatform.transform.position = new Vector3(lastPos.x - blockWidth, lastPos.y + blockHeight, lastPos.z);
            }
            else
            {
                newPlatform.transform.position = new Vector3(lastPos.x, lastPos.y + blockHeight, lastPos.z + blockWidth);
            }

            lastPos = newPlatform.transform.position;
            
            //Animation using DotTween
            if (i < 25)
            {
                float endPos = newPlatform.transform.position.y;
                
                newPlatform.transform.position = new Vector3(newPlatform.transform.position.x,newPlatform.transform.position.y - blockHeight * 3f,newPlatform.transform.position.z);
                newPlatform.transform.DOLocalMoveY(endPos,0.3f).SetDelay(i*0.1f);
            }
        }
    }
}
