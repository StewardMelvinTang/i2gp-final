using System.Collections;
using UnityEngine;

public class RepairBench : Table
{
    private bool isInteracting = false; // Tracks if the player is interacting with the table
    private Item itemToRepair; // The item being repaired
    private Coroutine repairCoroutine; // The coroutine for repairing

    void Start()
    {
        base.Start();
    }

    private void FixedUpdate()
    {
        if (isHighlighted)
        {
            // Show prompt UI (Optional)
        }
    }

    public override GameObject PutItem(GameObject gameObject)
    {
        // Debug.Log("Game Object To Put in Repair Bench: " + gameObject.name);

        // Check if the item has an `Item` component
        if (gameObject.TryGetComponent<Item>(out itemToRepair) && itemToRepair.currentDurability < itemToRepair.maxDurability)
        {
            isInteracting = true;
            Debug.Log("Item is ready for repair.");
        }
        else
        {
            Debug.LogWarning("GameObject does not have an Item component! OR the durability is still high");
        }

        return gameObject;
    }

    void Update()
    {
        // Start repairing when the player holds the F key
        if (isInteracting && itemToRepair != null && (Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Comma)) && isHighlighted)
        {
            if (repairCoroutine == null) // Start repairing only if not already repairing
            {
                repairCoroutine = StartCoroutine(RepairItem());
            }
        }

        // Stop repairing when the F key is released
        if ((Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.Comma)) && repairCoroutine != null)
        {
            StopCoroutine(repairCoroutine);
            repairCoroutine = null;
        }
    }

    private IEnumerator RepairItem()
    {
        while (true)
        {
            // Repair the item (increase durability smoothly)
            itemToRepair.currentDurability = Mathf.Clamp(itemToRepair.currentDurability + 100 * Time.deltaTime, 0.0f, itemToRepair.maxDurability);

            // Log the current durability (optional for debugging)
            Debug.Log($"Current Durability: {itemToRepair.currentDurability}");

            // Wait for the next frame
            yield return null;
        }
    }
}
