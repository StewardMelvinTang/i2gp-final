using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class panel_endgame : MonoBehaviour
{
    // Start is called before the first frame update
    public CanvasGroup parentPanel; // Reference to the parent panel's CanvasGroup
    public string sceneToLoad; // Name of the scene to load
    public float fadeDuration = 1f; // Duration of the fade

    private bool isFading = false;

    public void OnButtonPressed()
    {
        if (!isFading)
        {
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        isFading = true;

        // Fade out the parent panel
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            parentPanel.alpha = Mathf.Clamp01(1 - (elapsedTime / fadeDuration)); // Fade out alpha
            yield return null;
        }

        parentPanel.alpha = 0f; // Ensure it’s fully transparent

        // Load the next scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
