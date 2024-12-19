using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSplatterScript : MonoBehaviour
{
    [SerializeField] private float movementSpeedMultp = 0.5f; // Movement speed multiplier
    [SerializeField] private float lifeTime = 5f; // Lifetime of the slime splatter
    [SerializeField] private float fadeDuration = 1f; // Duration of the fade-out effect

    private Material slimeMaterial; // Material of the slime for fading
    private Color originalColor; // Original color of the material
    private bool isFading = false; // To track if the fade-out is in progress

    void Start()
    {
        // Get the material for fading if the object has a Renderer
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            slimeMaterial = renderer.material;
            originalColor = slimeMaterial.color;
        }

        // Start the life timer
        StartCoroutine(LifeTimer());
    }

    void Update()
    {
        // Optional: Add any additional logic if needed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovementController playerMovement = other.GetComponent<PlayerMovementController>();

            if (playerMovement != null)
            {
                playerMovement.movementSpeedMultiplier = movementSpeedMultp;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovementController playerMovement = other.GetComponent<PlayerMovementController>();

            if (playerMovement != null)
            {
                playerMovement.movementSpeedMultiplier = 1.0f;
            }
        }
    }

    private IEnumerator LifeTimer()
    {
        // Wait for the lifetime duration
        yield return new WaitForSeconds(lifeTime - fadeDuration);

        // Start fading out
        if (slimeMaterial != null)
        {
            isFading = true;
            yield return StartCoroutine(FadeOut());
        }

        // Destroy the object
        Destroy(gameObject);
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            // Update the material's color alpha value for transparency
            if (slimeMaterial != null)
            {
                Color newColor = originalColor;
                newColor.a = alpha;
                slimeMaterial.color = newColor;
            }

            yield return null;
        }
    }
}
