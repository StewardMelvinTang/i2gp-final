using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour {
    [Header("Default Settings")] [SerializeField]
    public GameObject foodObject;

    protected MeshRenderer tableRenderer;
    protected float originalMetallic;

    private AudioManager audioMamager;

    protected virtual void Start() {
        audioMamager = FindObjectOfType<AudioManager>();
        tableRenderer = GetComponent<MeshRenderer>();
        if (tableRenderer != null)
        {
            originalMetallic = tableRenderer.material.GetFloat("_Metallic"); // Store original metallic value
        }
        if(foodObject != null){
            foodObject = Instantiate(foodObject);
            foodObject.transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
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
