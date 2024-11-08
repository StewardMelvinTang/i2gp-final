using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2.5f;
    [SerializeField] private float raycastRange = 1f;
    private KitchenTable lastHitTable;

    void Start()
    {
        
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

        // Normalize moveVector if there's any input, then move the player
        if (moveVector != Vector3.zero)
        {
            transform.position += moveVector.normalized * movementSpeed * Time.deltaTime;
            transform.rotation = Quaternion.LookRotation(moveVector); // Rotate to face movement direction
        }
    }

    private void RayCastObject()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            KitchenTable table = hit.collider.GetComponent<KitchenTable>();

            if (table != null)
            {
                table.SetHighlighted(true);
                if (lastHitTable != null && lastHitTable != table)
                {
                    lastHitTable.SetHighlighted(false);
                }
                lastHitTable = table;
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