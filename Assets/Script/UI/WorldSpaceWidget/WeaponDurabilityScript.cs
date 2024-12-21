using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDurabilityScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Image durabilityProgressBar;
    public TMP_Text weaponNameText;
    public PlayerController player;

    public float currentFill = 1.0f;
    public float maxFill = 1.0f;

    private Item playerItemHolding;

    public CanvasGroup canvasGroup;
    
    void Start()
    {
        // StartCoroutine(CheckPlayerItemRoutine());]

        // canvasGroup = GetComponent<CanvasGroup>();
        
        // play bgm
        
    }
    

    // Update is called once per frame
    void Update() {

        if (player.holdItem) player.holdItem.TryGetComponent<Item>(out playerItemHolding);
        else canvasGroup.alpha = 0.0f;
        
        // Ensure the player is holding a valid item
        if (player.holdItem && playerItemHolding && playerItemHolding.enableDurabilitySystem && playerItemHolding.isTool)
        {
            
            // Enable or disable the UI based on the durability system flag
            canvasGroup.alpha = 1.0f;
            weaponNameText.SetText(playerItemHolding.itemName);
 
            // Update the durability progress bar
            if (durabilityProgressBar && playerItemHolding.enableDurabilitySystem)
            {
                durabilityProgressBar.fillAmount = Mathf.Clamp01(playerItemHolding.currentDurability / playerItemHolding.maxDurability);
            }
        }
        else {
            canvasGroup.alpha = 0.0f;
        }
    }
}
