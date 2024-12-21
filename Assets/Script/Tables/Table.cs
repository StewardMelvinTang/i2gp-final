using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Table : MonoBehaviour {
    [Header("Default Settings")] [SerializeField]
    public GameObject foodObject;

    protected MeshRenderer tableRenderer;
    protected float originalMetallic;

    private AudioManager audioMamager;
    
    [FormerlySerializedAs("useCustomItemPositionOffset")] [Header("Custom Item Position")] 
    public bool useCustomItemTransformOffset = false;
    public Vector3 itemPositionOffset;
    public Vector3 itemRotationOffset;
    public bool maintainObjectTransformWhenPlaced = true;
    protected float initialItemPosY;
    protected Quaternion initialItemRotation;
    protected GameObject initialItemClass;

    protected virtual void Start() {
        audioMamager = FindObjectOfType<AudioManager>();
        tableRenderer = GetComponent<MeshRenderer>();
        if (tableRenderer != null)
        {
            originalMetallic = tableRenderer.material.GetFloat("_Metallic"); // Store original metallic value
        }
        if(foodObject != null){
            foodObject = Instantiate(foodObject);

            // Add ItemTransformData to foodObject if it doesn't already exist
            if (!foodObject.TryGetComponent(out ItemTransformData itemData))
            {
                itemData = foodObject.AddComponent<ItemTransformData>();
            }

            if (useCustomItemTransformOffset)
                foodObject.transform.position = new Vector3(transform.position.x + itemPositionOffset.x, 1f + itemPositionOffset.y, transform.position.z + itemPositionOffset.z);
            else
                foodObject.transform.position = new Vector3(transform.position.x, 1f , transform.position.z);

            if (useCustomItemTransformOffset)
                foodObject.transform.rotation *= Quaternion.Euler(itemRotationOffset);

            // Save initial transform data to the item
            itemData.SaveTransform(foodObject.transform, itemPositionOffset);
            initialItemClass = foodObject;
        }
    }


    void Update(){

    }

    public void SetHighlighted(bool highlighted)
    {
        if (tableRenderer != null)
        {
            tableRenderer.material.SetFloat("_Metallic", highlighted ? 0.8f : originalMetallic); // Adjust metallic value
        }
    }

    public virtual GameObject PutItem(GameObject gameObject){
        GameObject ret = foodObject;
        foodObject = gameObject;
        foodObject.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        //play sound effect
        if (audioMamager && audioMamager.putdownObjectSFX) audioMamager.PlayAudioOnce(audioMamager.putdownObjectSFX, 0.25f);
        return ret;
        
    }
    
    public virtual GameObject TakeItem(){
        GameObject returnItem = foodObject;
        foodObject = null;
        return returnItem;
    }
}
