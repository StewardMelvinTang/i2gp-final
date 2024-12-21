using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] public float rotationSpeed = 10.0f;
    [SerializeField] public float movementSpeedMultiplier = 1.0f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed = 24.0f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCooldown = 1.0f;
    [SerializeField] private float doubleTapTimeWindow = 0.3f;

    [Header("Change Keybind")]
    [SerializeField] private int playerMovementType;

    [Header("Footstep Settings")]
    // [SerializeField] private AudioClip[] footstepClips; // Array for random footstep sounds
    [SerializeField] private float footstepInterval = 0.3f; // Time between footstep sounds
    // private AudioSource audioSource; // AudioSource for footstep sounds

    private Rigidbody rb;
    private Vector3 moveDirection;
    private bool canDash = true;
    private bool isDashing = false;
    private bool isMoving = false; // To track movement for footstep sounds

    private float lastStepTime; // Time since last footstep sound
    private float lastTapTime;
    private KeyCode lastKeyPressed;

    private Animator animator;
    private float currentSpeed;

    private KeyCode m_Up;
    private KeyCode m_Down;
    private KeyCode m_Left;
    private KeyCode m_Right;

    private AudioManager audioManager;
    // [SerializeField] private AudioClip dashSoundEffect;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        if(playerMovementType == 1) {
            m_Up = KeyCode.W;
            m_Down = KeyCode.S;
            m_Left = KeyCode.A;
            m_Right = KeyCode.D;
        }
        else  if(playerMovementType == 2) {
            m_Up = KeyCode.UpArrow;
            m_Down = KeyCode.DownArrow;
            m_Left = KeyCode.LeftArrow;
            m_Right = KeyCode.RightArrow;
        }
        
    }

    void Update()
    {
        if (isDashing) return;

        HandleInput();
        UpdateAnimations();
        PlayFootstepSounds();
    }

    void FixedUpdate()
    {
        if (isDashing) return;
        UpdatePlayerMovement();
    }

    void HandleInput()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        if (Input.GetKey(m_Up)) moveVertical += 1.0f;
        if (Input.GetKey(m_Down)) moveVertical -= 1.0f;
        if (Input.GetKey(m_Left)) moveHorizontal -= 1.0f;
        if (Input.GetKey(m_Right)) moveHorizontal += 1.0f;

        moveDirection = new Vector3(moveHorizontal, 0, moveVertical).normalized;

        isMoving = moveDirection.magnitude > 0;

        if (Input.GetKeyDown(m_Up)) TryDash(m_Up);
        if (Input.GetKeyDown(m_Down)) TryDash(m_Down);
        if (Input.GetKeyDown(m_Left)) TryDash(m_Left);
        if (Input.GetKeyDown(m_Right)) TryDash(m_Right);
    }

    private void TryDash(KeyCode key)
    {
        if (canDash && key == lastKeyPressed && Time.time - lastTapTime < doubleTapTimeWindow)
        {
            StartCoroutine(DashCoroutine());
            if (audioManager && audioManager.dashSoundEffect) audioManager.PlayAudioOnce(audioManager.dashSoundEffect);
        }

        
        lastKeyPressed = key;
        lastTapTime = Time.time;
    }

    private IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;

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
            Vector3 moveVector = moveDirection * (movementSpeed * movementSpeedMultiplier);
            rb.velocity = new Vector3(moveVector.x, rb.velocity.y, moveVector.z);

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void UpdateAnimations()
    {
        if (animator == null) return;
        float targetSpeed = moveDirection.magnitude > 0 ? 1.0f : 0.0f;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, Time.deltaTime * 10f);
        animator.SetFloat("Speed", currentSpeed);
    }

    private void PlayFootstepSounds() {
        // if () return;
        if (!audioManager || !isMoving || audioManager.footstepClips.Length == 0) return;

        if (Time.time - lastStepTime > footstepInterval)
        {
            AudioClip footstepClip = audioManager.footstepClips[Random.Range(0, audioManager.footstepClips.Length)];
            audioManager.PlayAudioOnce(footstepClip);
            lastStepTime = Time.time;
        }
    }
}
