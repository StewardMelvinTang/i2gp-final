using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public GameObject dropItem;
    public GameObject dropItemPrefab;

    public int health = 1;
    public float moveSpeed = 2f;
    public float rotationSpeed = 5f;
    public float minStopTime = 2f;
    public float maxStopTime = 5f;
    public float minMoveTime = 3f;
    public float maxMoveTime = 8f;
    public float panicSpeedMultiplier = 3f;
    public float minPanicTime = 2f;
    public float maxPanicTime = 4f;

    private Vector3 moveDirection;
    private bool isMoving = false;
    private bool isPanicked = false;
    private float stateTimer = 0f;
    private float currentStateDuration = 0f;
    private Quaternion targetRotation;
    private Animator animator;

    private Color originalColor;
    private Color redOverlay = new Color(1f, 0f, 0f, 0.8f);
    private float hitDuration = 0.0f;

    private enum AnimalState
    {
        Moving,
        Stopping,
        Panicking,
        Dying
    }

    private AnimalState currentState;

    void Start()
    {
        Renderer childRenderer = GetComponentInChildren<Renderer>();
        if (childRenderer != null) {
            originalColor = childRenderer.material.color;
        }
        animator = GetComponent<Animator>();
        ChangeToNewState(AnimalState.Moving);
    }

    public GameObject TakeDamage(int damage)
    {
        health -= damage;
        hitDuration = 1.0f;
        Renderer childRenderer = GetComponentInChildren<Renderer>();
        childRenderer.material.color = originalColor + redOverlay;

        if (health <= 0)
        {
            // Destroy(gameObject);
            currentState = AnimalState.Dying;
            return dropItem;
        }

        stateTimer = 0.0f;

        // Enter panic mode
        ChangeToNewState(AnimalState.Panicking);
        return null;
    }

    void Update()
    {
        stateTimer += Time.deltaTime;

        if (currentState == AnimalState.Dying) {
            if(hitDuration <= 0.0f){
                GameObject obj = Instantiate(dropItemPrefab);
                DropItem dropItemScript = obj.GetComponent<DropItem>();
                dropItemScript.InitObject(dropItem);
                dropItemScript.transform.position = transform.position;

                Destroy(gameObject);
            }
            else{
                hitDuration -= Time.deltaTime;
                transform.Rotate(0f, 0f, 100f * Time.deltaTime);
            }
            return;
        }

        // State duration check
        if (stateTimer >= currentStateDuration)
        {
            if (currentState == AnimalState.Panicking)
            {
                // After panic ends, go to either moving or stopping
                ChangeToNewState(Random.value > 0.5f ? AnimalState.Moving : AnimalState.Stopping);
            }
            else if (currentState == AnimalState.Moving)
            {
                ChangeToNewState(AnimalState.Stopping);
            }
            else if (currentState == AnimalState.Stopping)
            {
                ChangeToNewState(AnimalState.Moving);
            }
        }

        // Movement and animation updates
        UpdateMovementAndAnimation();

        // Update enemy hit red overlay
        if(hitDuration > 0.0f){
            hitDuration -= Time.deltaTime;
            if(hitDuration <= 0.0f){
                Renderer childRenderer = GetComponentInChildren<Renderer>();
                childRenderer.material.color = originalColor;
            }
        }
    }

    private void UpdateMovementAndAnimation()
    {
        float currentSpeed = 0f;
        bool shouldMove = false;
        bool shouldEat = false;

        switch (currentState)
        {
            case AnimalState.Moving:
                currentSpeed = moveSpeed;
                shouldMove = true;
                break;

            case AnimalState.Panicking:
                currentSpeed = moveSpeed * panicSpeedMultiplier;
                shouldMove = true;
                break;

            case AnimalState.Stopping:
                shouldEat = true;
                break;
        }

        if (shouldMove)
        {
            // Rotate towards target direction
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            // Move forward
            transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        }

        // Update animator
        animator.SetFloat("Speed_f", currentSpeed);
        animator.SetBool("Eat_b", shouldEat);
    }

    private void ChangeToNewState(AnimalState newState)
    {
        currentState = newState;
        stateTimer = 0f;

        switch (newState)
        {
            case AnimalState.Moving:
                currentStateDuration = Random.Range(minMoveTime, maxMoveTime);
                PickNewDirection();
                break;

            case AnimalState.Stopping:
                currentStateDuration = Random.Range(minStopTime, maxStopTime);
                break;

            case AnimalState.Panicking:
                currentStateDuration = Random.Range(minPanicTime, maxPanicTime);
                PickNewDirection();
                break;
        }
    }

    private void PickNewDirection()
    {
        float randomAngle = Random.Range(0f, 2f * Mathf.PI);
        moveDirection = new Vector3(Mathf.Cos(randomAngle), 0f, Mathf.Sin(randomAngle)).normalized;
        targetRotation = Quaternion.LookRotation(moveDirection);
    }
}