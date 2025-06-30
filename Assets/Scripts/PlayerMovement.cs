using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    
    private float movementForce = 0.5f;
    private float jumpForce = 0.15f;
    private float jumpTime = 0.15f;

  //  private SwipeDirection _swipeDirection;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
//        SwipeDetector.OnSwipe += SwipeDetector_OnSwipe;
    }
    
//    private void SwipeDetector_OnSwipe(SwipeData data)
//    {
//        //Debug.Log("Swipe in Direction: " + data.Direction);
//        _swipeDirection = data.Direction;
//    }

    private void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.A) ||
            SwipeInput.swipedLeft)
        {
            Jump(true);
        }
        if (Input.GetKeyDown(KeyCode.D) ||
            SwipeInput.swipedRight)
        {
            Jump(false);
        }
    }

    void Jump(bool left)
    {
        //Jump Sound
        SoundManager.instance.PlaySound(2);
        if (left)
        {
            transform.DORotate(new Vector3(0f, 90f, 0f), 0f);

            rb.DOJump(new Vector3(transform.position.x - movementForce,
                transform.position.y + jumpForce,
                transform.position.z), 0.5f, 1, jumpTime);
        }
        else
        {
            transform.DORotate(new Vector3(0f, -180f, 0f), 0f);
            
            
            rb.DOJump(new Vector3(transform.position.x,
                transform.position.y + jumpForce,
                transform.position.z + movementForce), 0.5f, 1, jumpTime);
        }
    }
}
