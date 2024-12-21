using UnityEngine;
using TMPro; // Required for TextMeshPro

public class ScorePopup : MonoBehaviour
{
    public float lifetime = 1.5f; // How long the popup will last
    public Vector3 moveDirection = new Vector3(0, 1, 0); // Direction of movement
    public float fadeSpeed = 2f; // Speed of fading

    private TextMeshProUGUI textMeshPro;
    private CanvasGroup canvasGroup;

    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        Destroy(gameObject, lifetime); // Destroy the popup after its lifetime
    }

    void Update()
    {
        // Move the popup upwards
        transform.position += moveDirection * Time.deltaTime;

        // Fade out the popup
        if (canvasGroup != null)
        {
            canvasGroup.alpha -= fadeSpeed * Time.deltaTime;
        }
    }

    // Set the text displayed in the popup
    public void SetScoreText(string text)
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }
        textMeshPro.text = text;
    }
}