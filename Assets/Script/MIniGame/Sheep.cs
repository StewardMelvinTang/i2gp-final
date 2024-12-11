using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float speed = 2.0f; // Initial movement speed
    public float jumpForce = 5.5f; // Force applied for jumping
    private Rigidbody rb;
    private bool isGrounded = true; // Check if the sheep is on the ground
    private bool isStopped = false; // Check if the sheep should stop moving
    public string runForwardAnimation = "run_forward";
    public string standtositAnimation = "stand_to_sit";

    private Animator animator; // Reference to the Animator component

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();

        // Get the Animator component
        animator = GetComponent<Animator>();

        // Freeze unnecessary axes to prevent falling or rotation
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    // Update is called once per frame
    void Update()
    {
        // Stop movement if the sheep has been stopped
        if (isStopped)
        {
            animator.Play(standtositAnimation);
            return;
        }

        // Keep the sheep facing to the right
        transform.rotation = Quaternion.Euler(0, 90, 0);

        // Check if the sheep's position exceeds 9.15 on the X-axis
        if (transform.position.x <= 9.15f)
        {
            // Move the sheep forward in the direction it's facing
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            // Play walking animation
            animator.Play(runForwardAnimation);

            // Check for jump input (e.g., spacebar or a specific key)
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }
        else
        {
            // Stop the sheep if it reaches the X-position limit
            isStopped = true;
        }
    }

    void Jump()
    {
        // Apply a vertical force for jumping
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        // Play jump animation
        animator.SetTrigger("Jump");

        // Set isGrounded to false since the sheep is now in the air
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the sheep lands back on the ground
        if (collision.gameObject.CompareTag("ground"))
        {
            isGrounded = true;
        }

        // Stop the sheep if it collides with a fence
        if (collision.gameObject.CompareTag("fence"))
        {
            isStopped = true;
        }
    }
}
