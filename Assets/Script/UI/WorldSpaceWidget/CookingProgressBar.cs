using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class CookingProgressBar : MonoBehaviour
{
    public Image progressBar;    // Reference to the progress bar Image
    private PanTable panTable;    // Reference to the parent PanTable script

    // private bool canStartCookingTimer = false;
    public CanvasGroup canvasGroup;
    
    private KitchenFire kitchenFireComp;
    public Image dangerImage;
    
    // public RectTransform targetImage; // The RectTransform of your image
    public float scaleUpSize = 1.5f;  // Maximum scale size
    public float scaleDownSize = 1.0f; // Minimum scale size
    public float animationDuration = 1.5f; // Full cycle duration

    void Start()
    {
        // progressBar = GetComponentInChildren<Image>();
        panTable = GetComponentInParent<PanTable>();
        kitchenFireComp = GetComponentInParent<KitchenFire>();
    }

    void Update()
    {
        HandleProgressBar();
        HandleFireState();
        

    }

    /// <summary>
    /// Handles the visibility and progress of the progress bar.
    /// </summary>
    private void HandleProgressBar()
    {
        if (panTable != null && panTable.startCounter && canvasGroup != null)
        {
            progressBar.enabled = true;
            canvasGroup.alpha = 1.0f;

            float progress = Mathf.Clamp01(panTable.timeCounter / panTable.cookingTime);
            progressBar.fillAmount = progress;
        }
        else
        {
            progressBar.enabled = false;
        }
    }

    /// <summary>
    /// Handles the visibility and behavior of the danger warning when the kitchen is on fire.
    /// </summary>
    private void HandleFireState()
    {
        var progressBarCanvasGroup = progressBar.GetComponent<CanvasGroup>();

        if (kitchenFireComp != null && kitchenFireComp.isOnFire)
        {
            // Show danger warning
            canvasGroup.alpha = 1.0f;
            dangerImage.enabled = true;

            // Disable the progress bar and ensure its canvas group remains visible
            progressBar.enabled = false;
            if (progressBarCanvasGroup != null)
            {
                progressBarCanvasGroup.alpha = 1.0f;
            }
            
            // Calculate the scale factor using PingPong for smooth scaling up/down
            float scale = Mathf.Lerp(scaleDownSize, scaleUpSize, Mathf.PingPong(Time.time / animationDuration, 1));

            // Apply the scale to the image (uniform scaling on X, Y, Z axes)
            var targetImage = dangerImage.rectTransform;
            targetImage.localScale = new Vector3(scale, scale, scale);

        }
        else
        {
            // Hide danger warning
            dangerImage.enabled = false;

            // Reset canvas alpha if no fire and no active cooking
            if (panTable == null || !panTable.startCounter)
            {
                canvasGroup.alpha = 0.0f;
            }
        }
    }

}