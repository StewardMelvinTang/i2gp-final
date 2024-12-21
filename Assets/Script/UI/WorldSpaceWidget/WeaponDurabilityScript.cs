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
    }
    

    // Update is called once per frame
    void Update()
    {
        
        // Debug.Log(player.holdItem.name);
        // Check if the player is holding an item
        if (!playerItemHolding)
        {

            canvasGroup.alpha = 0.0f; // Hide the UI if no item is held
            
            // Try to get the Item component from the player's held item
            if (player.holdItem && player.holdItem.TryGetComponent<Item>(out playerItemHolding))
            {
                // Update the weapon name if the item is valid
                if (weaponNameText)
                {
                    weaponNameText.SetText(playerItemHolding.itemName);
                }
            }
            else
            {
                return; // Exit early if no valid item is found
            }
        }

        player.holdItem.TryGetComponent<Item>(out playerItemHolding);

        // Ensure the player is holding a valid item
        if (player.holdItem && playerItemHolding && playerItemHolding.enableDurabilitySystem && playerItemHolding.isTool)
        {
            
            // Enable or disable the UI based on the durability system flag
            canvasGroup.alpha = 1.0f;

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
