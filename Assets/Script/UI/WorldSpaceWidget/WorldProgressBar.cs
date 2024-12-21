using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.UI;
using UnityEngine;

public class WorldProgressBar : MonoBehaviour
{
    public Image progressBar;    // Reference to the progress bar Image
    private GameManager gameManager;

    // private bool canStartCookingTimer = false;
    // public CanvasGroup canvasGroup;

    void Start()
    {
        // progressBar = GetComponentInChildren<Image>();
        // potTable = GetComponentInParent<PotTable>();
        gameManager = FindObjectOfType<GameManager>();
        Debug.Log("is the timer here");
    }

    void Update()
    {
        // if (canvasGroup != null) {
            // canvasGroup.alpha = 1.0f;
            // Debug.Log("PanTable Time Counter: " + panTable.timeCounter + "Pan Table Cooking Time: "+ panTable.cookingTime);
            float progress = Mathf.Clamp01(gameManager.GetCurrentTime() / gameManager.GetTotalGameTime());
            Debug.Log(progress);

            // Debug.Log("Progress : " + progress);
            progressBar.fillAmount = progress;
        // }
    }

    void HideProgressBar() {
        // canvasGroup.alpha = 0.0f;
    }
}