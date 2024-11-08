using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float forwardSpeed = 5f;
    public float jumpForce = 5f;
    public float laneDistance = 3f;
    private CharacterController controller;
    private Vector3 moveDirection;
    private int currentLane = 1;

    private Vector2 startTouchPosition, endTouchPosition;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Forward movement
        moveDirection.z = forwardSpeed;

        // Handle touch inputs for lane switching
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                HandleSwipe();
            }
        }

        // Jumping
        if (controller.isGrounded)
        {
            moveDirection.y = -1f;
            if (Input.touchCount > 0 && Input.GetTouch(0).tapCount == 2) // Double-tap to jump
            {
                moveDirection.y = jumpForce;
            }
        }
        else
        {
            moveDirection.y += Physics.gravity.y * Time.deltaTime;
        }

        controller.Move(moveDirection * Time.deltaTime);
    }

    void HandleSwipe()
    {
        Vector2 swipeDelta = endTouchPosition - startTouchPosition;

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y)) // Horizontal swipe
        {
            if (swipeDelta.x > 0 && currentLane < 2) // Swipe right
            {
                currentLane++;
            }
            else if (swipeDelta.x < 0 && currentLane > 0) // Swipe left
            {
                currentLane--;
            }
        }

        // Move player to the target lane
        Vector3 targetPosition = transform.position;
        targetPosition.x = (currentLane - 1) * laneDistance;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 10);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Trigger a game over
        if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Hit an obstacle!");
            
        }
    }
}
