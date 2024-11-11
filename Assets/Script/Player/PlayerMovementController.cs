using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] public float rotationSpeed = 10.0f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 24.0f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1.0f; // Cooldown in seconds
    [SerializeField] private float doubleTapTimeWindow = 0.3f; // Time window to detect double tap

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool canDash = true;
    private bool isDashing = false;

    // For tracking double-tap
    private float lastTapTime;
    private KeyCode lastKeyPressed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing) return;
        HandleInput();
    }

    void FixedUpdate() {
        if (isDashing) return;
        UpdatePlayerMovement();
    }

    void HandleInput() {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKey(KeyCode.W)) moveVertical += 1.0f;
        if (Input.GetKey(KeyCode.S)) moveVertical -= 1.0f;
        if (Input.GetKey(KeyCode.A)) moveHorizontal -= 1.0f;
        if (Input.GetKey(KeyCode.D)) moveHorizontal += 1.0f;

        // Create a normalized move direction vector
        moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        // Check for double-tap dash
        if (Input.GetKeyDown(KeyCode.W)) TryDash(KeyCode.W);
        if (Input.GetKeyDown(KeyCode.S)) TryDash(KeyCode.S);
        if (Input.GetKeyDown(KeyCode.A)) TryDash(KeyCode.A);
        if (Input.GetKeyDown(KeyCode.D)) TryDash(KeyCode.D);
    }

    private void TryDash(KeyCode key)
    {
        if (canDash && key == lastKeyPressed && Time.time - lastTapTime < doubleTapTimeWindow)
        {
            StartCoroutine(DashCoroutine());
        }

        // Update double-tap tracking
        lastKeyPressed = key;
        lastTapTime = Time.time;
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;

        // Apply dash velocity
        rb.velocity = moveDirection * dashSpeed;

        yield return new WaitForSeconds(dashTime);

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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
