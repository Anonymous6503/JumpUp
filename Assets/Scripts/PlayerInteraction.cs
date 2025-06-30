using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerInteraction : MonoBehaviour
{
    public static PlayerInteraction instance;
    private Rigidbody rb;
    private bool playerDied;
    private CameraFollow _cameraFollow;

    private Vector3 lastPosOfPlayer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        
        rb = GetComponent<Rigidbody>();
        _cameraFollow = Camera.main.GetComponent<CameraFollow>();
    }

    public void SetPlayerDeadState(bool playerDead)
    {
        playerDied = playerDead;
        gameObject.SetActive(true);
    }

    public void SetPlayerPosition()
    {
        transform.position = lastPosOfPlayer;
        _cameraFollow.CanFollow = true;
    }

    private void Update()
    {
        if (!playerDied)
        {
            if (rb.linearVelocity.sqrMagnitude > 65)
            {
                playerDied = true;
                _cameraFollow.CanFollow = false;
                //Play Die Sound
                SoundManager.instance.PlaySound(3);
                //Restart Game
                GameManager.instance.RestartGame();
                //Display Score
                //GameManager.instance.IncrementScore();
            }
        }
    }

    private void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.CompareTag("Coin"))
        {
            target.gameObject.SetActive(false);
            //Increment Score
            GameManager.instance.IncrementScore();
            SaveManager.instance.SaveCoins(1);
            //Play Coin Sound
            SoundManager.instance.PlaySound(1);
        }

        if (target.gameObject.CompareTag("Spike"))
        {
            _cameraFollow.CanFollow = false;
            gameObject.SetActive(false);//Deactivate Player
            //Play Game End sound
            SoundManager.instance.PlaySound(3);
            //Restart Game
            GameManager.instance.RestartGame();
            Destroy(target.gameObject,0.5f);
            //target.gameObject.SetActive(false);
            //Display Score
        }
    }

    private void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.CompareTag("EndPlatform"))
        {
            //Play Game Win Sound
            SoundManager.instance.PlaySound(0);
            //Display Score
            //Restart Game
            GameManager.instance.NextLevel();
            SaveManager.instance.SaveLevels(1);
            Debug.Log("You Win!");
        }

        if (target.gameObject.CompareTag("Platform"))
        {
            int score = Random.Range(4, 7);
            ScoreSystem.instance.currentScore += score;
            SaveManager.instance.SetCurrentScoreFromPreviousLevel(ScoreSystem.instance.currentScore);
            lastPosOfPlayer = transform.position;
            SoundManager.instance.PlaySound(4);
        }
    }
}
