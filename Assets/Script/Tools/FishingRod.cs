using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingRod : Item
{
    [Header("Fishing Settings")]
    [SerializeField] private float progressDecayRate = 0.5f; // How fast the progress decreases
    [SerializeField] private float progressIncreaseAmount = 0.2f; // How much progress is added per button press
    [SerializeField] private float catchThreshold = 0.95f; // Progress needed to catch fish

    private float fishingProgress = 0f;
    private bool isFishing = false;
    private TextMesh progressText;
    private GameObject textObject;

    public GameObject dropItem;
    public GameObject dropItemPrefab;

    private GameObject pond;

    void Start()
    {
        // Create 3D text object
        textObject = new GameObject("FishingProgressText");
        progressText = textObject.AddComponent<TextMesh>();
        progressText.characterSize = 0.1f;
        progressText.fontSize = 50;
        progressText.alignment = TextAlignment.Center;
        progressText.anchor = TextAnchor.MiddleCenter;
        textObject.SetActive(false);

        pond = GameObject.FindGameObjectWithTag("pond");
    }

    public override GameObject Use()
    {
        // Check if player is close to a pond
        if (Vector3.Distance(transform.position, pond.transform.position) > 10.0f)
        {
            return null;
        }

        if (!isFishing)
        {
            StartFishing();
        }
        else
        {
            // Increase progress when player presses the use button
            fishingProgress = Mathf.Min(fishingProgress + progressIncreaseAmount, 1f);
            
            // Check if fish is caught
            if (fishingProgress >= catchThreshold)
            {
                CatchFish();
            }
        }
        return null;
    }

    void StartFishing()
    {
        isFishing = true;
        fishingProgress = 0f;
        
        // Position the text above the player
        textObject.transform.position = transform.position + new Vector3(0f, 2f, 0f);
        // Make text face the camera
        textObject.transform.rotation = Camera.main.transform.rotation;
        
        textObject.SetActive(true);
    }

    void CatchFish()
    {
        GameObject obj = Instantiate(dropItemPrefab);
        DropItem dropItemScript = obj.GetComponent<DropItem>();
        dropItemScript.InitObject(dropItem);
        dropItemScript.transform.position = transform.position;

        StopFishing();
    }

    void StopFishing()
    {
        isFishing = false;
        fishingProgress = 0f;
        textObject.SetActive(false);
    }

    void Update()
    {
        if (isFishing)
        {
            // Decrease progress over time
            fishingProgress = Mathf.Max(fishingProgress - (progressDecayRate * Time.deltaTime), 0f);

            // Update progress text
            progressText.text = $"{(fishingProgress * 100):F0}%";
            
            // Make text always face the camera
            textObject.transform.rotation = Camera.main.transform.rotation;
            textObject.transform.position = transform.position + new Vector3(0f, 2f, 0f);

            // Optional: Allow player to cancel fishing by pressing another key
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopFishing();
            }
        }
    }

    void OnDestroy()
    {
        // Clean up the text object when the fishing rod is destroyed
        if (textObject != null) Destroy(textObject);
    }
}