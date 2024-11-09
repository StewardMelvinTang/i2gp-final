using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed = 2.5f;
    [SerializeField] private float raycastRange = 1f;
    [SerializeField] private float holdDistance = 1.0f;

    private KitchenTable lastHitTable;
    private GameObject holdItem;

    /*
        MOVE        : WASD
        INTERACT    : F
    */

    void Start()
    {
        holdItem = null;
    }

    void Update()
    {
        UpdatePlayerMovement();
        RayCastObject();
    }

    private void UpdatePlayerMovement()
    {
        Vector3 moveVector = new Vector3(0.0f, 0.0f, 0.0f);

        if (Input.GetKey(KeyCode.W)) moveVector.z += 1.0f;
        if (Input.GetKey(KeyCode.S)) moveVector.z -= 1.0f;
        if (Input.GetKey(KeyCode.A)) moveVector.x -= 1.0f;
        if (Input.GetKey(KeyCode.D)) moveVector.x += 1.0f;

        if (moveVector != Vector3.zero)
        {
            transform.position += moveVector.normalized * movementSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(moveVector);
        }
    }

    private void ObjectInteract(KitchenTable table)
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameObject item = null;
            if (holdItem)
            {
                holdItem.transform.SetParent(null);
                item = table.PutItem(holdItem);
                holdItem = item;
                if (item) Debug.Log("Object Interaction returns an output (it failed putting) : " + item.name);
            }
            else
            {
                item = table.TakeItem();
            }
            if (item != null)
            {
                //Debug.Log("Holding Item : " + item.name);
                holdItem = item;
                holdItem.transform.SetParent(transform); 
                holdItem.transform.localPosition = item.GetComponent<Item>().holdPosition;
            }
        }
    }

    /*
        #==================================================#
        #========= You can ignore below functions =========#
        #==================================================#
    */
    private void RayCastObject()
    {
        Ray ray = new Ray(transform.position, transform.forward + Vector3.down * 0.5f);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            KitchenTable table = hit.collider.GetComponent<KitchenTable>();

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
                ObjectInteract(table);
            }
            else if (lastHitTable != null)
            {
                lastHitTable.SetHighlighted(false);
                lastHitTable = null;
            }
        }
        else if (lastHitTable != null)
        {
            lastHitTable.SetHighlighted(false);
            lastHitTable = null;
        }
    }
}