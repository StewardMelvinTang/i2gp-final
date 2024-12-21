using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Networking;
using Unity.Netcode;
using UnityEngine;

public class PanTable : Table {

    public float timeCounter;
    public bool startCounter { get; set; }

    public float cookingTime { get; set; }
    
    public AudioSource audioSource;
    private AudioManager audioManager;
    
    //custom action eventp
    public event Action<GameObject> OnItemPlaced;
    public bool isCooking = false;

    void Update(){
        // Debug.Log("Start Counter Bool: " + startCounter + " Time Counter Float: " + timeCounter);
        if(startCounter){
            timeCounter -= Time.deltaTime;
            isCooking = true;
            
            if(timeCounter < 0.0f){
                startCounter = false;
                isCooking = false;
                Destroy(foodObject);
                foodObject = Instantiate(foodObject.GetComponent<Item>().objAfterPan);
                foodObject.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            
                
                if (foodObject.GetComponent<Item>().canPan) {
                    isCooking = false;
                    startCounter = true;
                    timeCounter = foodObject.GetComponent<Item>().panTime;
                    cookingTime = timeCounter;
                }
            }
        }
    }

    public override GameObject PutItem(GameObject targetObject) {

        // Debug.Log("PUT ITEMM!" + targetObject.name);
        OnItemPlaced?.Invoke(targetObject);
        Item targetItem = null;
        Item currentItem = null;

        if (audioManager == null) audioManager = FindObjectOfType<AudioManager>();
        
        if (targetObject != null) {
            targetItem = targetObject.GetComponent<Item>();
        }

        if (foodObject != null) {
            currentItem = foodObject.GetComponent<Item>();
        }

        if (targetObject != null && targetItem.canPan) {
            if (audioManager && audioSource) {
                audioManager.PlayAudioOnce(audioManager.panCookingSFX, 0.35f, audioSource);
            }
            GameObject returnItem = foodObject;
            startCounter = true;
            timeCounter = targetItem.panTime;
            cookingTime = timeCounter;
            foodObject = targetObject;
            // isCooking = true;
            foodObject.transform.position = new Vector3(transform.position.x, 1.2f, transform.position.z);
            return returnItem;
        }
        else {
            return targetObject;
        }
        
    }

    public override GameObject TakeItem(){
        GameObject returnItem = foodObject;
        foodObject = null;
        startCounter = false;
        
        return returnItem;
    }
    
    
}
