using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] public float rotationSpeed = 10.0f;

    // for movement controller
    private Rigidbody rb;
    private Vector3 moveDirection; // Store input direction here

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKey(KeyCode.W)) moveVertical += 1.0f;
        if (Input.GetKey(KeyCode.S)) moveVertical -= 1.0f;
        if (Input.GetKey(KeyCode.A)) moveHorizontal -= 1.0f;
        if (Input.GetKey(KeyCode.D)) moveHorizontal += 1.0f;

        // Create a normalized move direction vector
        moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;
    }

    void FixedUpdate() {
        UpdatePlayerMovement();
    }

    private void UpdatePlayerMovement()
    {
        if (moveDirection != Vector3.zero)
        {
            // Calculate movement vector and apply to Rigidbody
            Vector3 moveVector = moveDirection * movementSpeed;
            rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);

            // Smoothly rotate the player towards the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            // Stop horizontal movement if no input is detected
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}
