using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float raycastRange = 1f;
    [SerializeField] private float holdDistance = 1.0f;

    private Table lastHitTable;
    private GameObject holdItem;
    private StackFood backpack;
    private float backpackDelay;


    [Header("Attachment Settings")] [SerializeField]
    private GameObject handAttachment;


    private Animator animator;
    private bool canHoldAnimation = true;

    private AudioManager audioManager;
    
    /*
        MOVE        : WASD
        INTERACT    : F
    */

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        holdItem = null;

        GameObject backpackObject = new GameObject("Backpack");
        animator = GetComponentInChildren<Animator>();
        backpackObject.transform.SetParent(transform);
        backpack = backpackObject.AddComponent<StackFood>();
        backpack.transform.localPosition = new Vector3(0, 0, -1);
        backpackDelay = 0.0f;
    }

    void Update()
    {
        /* Timer */
        if (backpackDelay > 0.0f)
        {
            backpackDelay -= Time.deltaTime;
        }

        /* Update Function */
        RayCastObject();
        UseItem();

        if (canHoldAnimation)
        {
            // Smoothly blend the weight of layer 1
            
            float targetWeight = holdItem ? 1.0f : 0.0f;
            float currentWeight = animator.GetLayerWeight(1);
            float newWeight = Mathf.MoveTowards(currentWeight, targetWeight, Time.deltaTime * 5f); // Adjust 5f for faster/slower blending
            animator.SetLayerWeight(1, newWeight);

            // Set the pickingUpItem parameter
            animator.SetBool("pickingUpItem", holdItem);
        }
        else {
            float targetWeight = 0.0f;
            float currentWeight = animator.GetLayerWeight(1);
            float newWeight = Mathf.MoveTowards(currentWeight, targetWeight, Time.deltaTime * 5f); // Adjust 5f for faster/slower blending
            animator.SetLayerWeight(1, newWeight);

            // Set the pickingUpItem parameter
            animator.SetBool("pickingUpItem", false);
        }
    }


    // void FixedUpdate() {
    //     UpdatePlayerMovement();
    // }

    // private void UpdatePlayerMovement()
    // {
    //     Vector3 moveVector = new Vector3(0.0f, 0.0f, 0.0f);

    //     if (Input.GetKey(KeyCode.W)) moveVector.z += 1.0f;
    //     if (Input.GetKey(KeyCode.S)) moveVector.z -= 1.0f;
    //     if (Input.GetKey(KeyCode.A)) moveVector.x -= 1.0f;
    //     if (Input.GetKey(KeyCode.D)) moveVector.x += 1.0f;

    //     if (moveVector != Vector3.zero)
    //     {
    //         transform.position += moveVector.normalized * movementSpeed * Time.deltaTime;
    //         Quaternion targetRotation = Quaternion.LookRotation(moveVector);
    //         transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    //     }
    //     // Vector3 moveVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

    //     // if (moveVector.magnitude > 0.1f)
    //     // {
    //     //     moveVector = moveVector.normalized;
    //     //     transform.position += moveVector * movementSpeed * Time.deltaTime;

    //     //     Quaternion targetRotation = Quaternion.LookRotation(moveVector);
    //     //     transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
    //     // }
    // }



    private void RefillCrate(FiniteCrate crate){
        if (Input.GetKey(KeyCode.F) && backpack.foodStack.Count > 0){
            if(backpackDelay <= 0.0f){
                GameObject obj = backpack.Pop();
                crate.PutItem(obj);
                backpackDelay = 0.1f;
            }
        }
        else{
            ObjectInteract(crate);
        }
    }

    private void ObjectInteract(Table table)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject item = null;
            
            if (holdItem)
            {
                holdItem.transform.SetParent(null);
                item = table.PutItem(holdItem);
                holdItem = item;

                if (holdItem == null) canHoldAnimation = false;
                Debug.Log("Replacing Holding Item With " + holdItem.name);

                Item itemRef;
                if (item.TryGetComponent<Item>(out itemRef))
                {
                    Debug.Log("Item Ref is " + itemRef.isTool);
                    canHoldAnimation = !itemRef.isTool;
                }


            }
            else
            {
                item = table.TakeItem();
            }

            if (item != null)
            {
                holdItem = item;
                // play sound effect
                if(audioManager && audioManager.pickupObjectSFX) audioManager.PlayAudioOnce(audioManager.pickupObjectSFX, 0.25f);
                
                var itemRef = item.GetComponent<Item>();
                
                if (itemRef.isTool) canHoldAnimation = false;
                else canHoldAnimation = true;
                //if is tool, don't play the item picking up animation, instead use the original idle animation (since it will have the attach to bone feature)
                
                if (itemRef.attachToBone == true && handAttachment != null) {
                
                    holdItem.transform.SetParent(handAttachment.transform);
                    holdItem.transform.localPosition = Vector3.zero;
                    holdItem.transform.localRotation = Quaternion.identity;
                    return;
                }
                
                holdItem.transform.SetParent(transform); 
                holdItem.transform.localPosition = item.GetComponent<Item>().getHoldPosition();
                holdItem.transform.localPosition = new Vector3(holdItem.transform.localPosition.x, holdItem.transform.localPosition.y + 1, holdItem.transform.localPosition.z);
                
            }
        }
    }

    private void UseItem() {
        Item item = null;

        if (holdItem) {
            item = holdItem.GetComponent<Item>();
        }

        if (Input.GetKeyDown(KeyCode.E) && item && item.isTool) {
            // Attacking
            if (animator) animator.SetTrigger("AttackTrigger");
            GameObject droppedObj = holdItem.GetComponent<Item>().Use();
            if (droppedObj) {
                GameObject obj = Instantiate(droppedObj);
                // obj.transform.SetParent(gameObject.transform);
                backpack.InsertFood(obj);
            }
        }
    }

    void OnCollisionEnter (Collision collision) {
        if(collision.gameObject.tag == "dropItem") {
            DropItem item = collision.gameObject.GetComponent<DropItem>();
            GameObject obj = item.TakeItem();
            backpack.InsertFood(obj);
        }
    }

    /*
        #==================================================#
        #========= You can ignore below functions =========#
        #==================================================#
    */
    private void RayCastObject()
    {
        // Define the ray starting point and direction
        Vector3 rayOrigin = transform.position + Vector3.up * 0.5f; // Adjust origin if necessary
        Vector3 rayDirection = transform.forward + Vector3.up * 0.2f; // Adjust direction if necessary

        // Draw a visible debug line in the Scene view
        Debug.DrawRay(rayOrigin, rayDirection * raycastRange, Color.red);

        Ray ray = new Ray(rayOrigin, rayDirection * raycastRange);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            // Debug.Log("Raycast Hit Object : " + hit.collider.name);

            Table table = hit.collider.GetComponent<Table>();
            FiniteCrate finiteCrate = hit.collider.GetComponent<FiniteCrate>();

            if (table != null)
            {
                /* Highlighted */
                table.SetHighlighted(true);
                if (lastHitTable != null && lastHitTable != table)
                {
                    lastHitTable.SetHighlighted(false);
                }
                lastHitTable = table;

                /* Interact */
                if (finiteCrate)
                {
                    RefillCrate(finiteCrate);
                }
                else
                {
                    ObjectInteract(table);
                }
            }
            else
            {
                // Debug.LogWarning("Raycast hit an object, but it doesn't have a Table component.");
                if (lastHitTable != null)
                {
                    lastHitTable.SetHighlighted(false);
                    lastHitTable = null;
                }
            }
        }
        else if (lastHitTable != null)
        {
            // Debug.Log("No hit table found");
            lastHitTable.SetHighlighted(false);
            lastHitTable = null;
        }
    }

}