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

    void Start()
    {
        // progressBar = GetComponentInChildren<Image>();
        panTable = GetComponentInParent<PanTable>();
    }

    void Update()
    {
        if (canvasGroup != null && !panTable.startCounter) {
            canvasGroup.alpha = 0.0f;
        }
        if (panTable != null && panTable.startCounter && canvasGroup != null) {
            canvasGroup.alpha = 1.0f;
            // Debug.Log("PanTable Time Counter: " + panTable.timeCounter + "Pan Table Cooking Time: "+ panTable.cookingTime);
            float progress = Mathf.Clamp01(panTable.timeCounter / panTable.cookingTime);

            // Debug.Log("Progress : " + progress);
            progressBar.fillAmount = progress;
        }
    }
}