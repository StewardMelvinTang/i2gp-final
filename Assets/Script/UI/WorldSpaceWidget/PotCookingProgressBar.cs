using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class PotCookingProgressBar : MonoBehaviour
{
    public Image progressBar;    // Reference to the progress bar Image
    private PotTable potTable;    // Reference to the parent PanTable script

    // private bool canStartCookingTimer = false;
    public CanvasGroup canvasGroup;

    void Start()
    {
        // progressBar = GetComponentInChildren<Image>();
        potTable = GetComponentInParent<PotTable>();
    }

    void Update()
    {
        if (canvasGroup != null && !potTable.startCounter) {
            canvasGroup.alpha = 0.0f;
        }
        if (potTable != null && potTable.startCounter && canvasGroup != null) {
            canvasGroup.alpha = 1.0f;
            // Debug.Log("PanTable Time Counter: " + panTable.timeCounter + "Pan Table Cooking Time: "+ panTable.cookingTime);
            float progress = Mathf.Clamp01(potTable.timeCounter / potTable.cookingTime);

            // Debug.Log("Progress : " + progress);
            progressBar.fillAmount = progress;
        }
    }
}