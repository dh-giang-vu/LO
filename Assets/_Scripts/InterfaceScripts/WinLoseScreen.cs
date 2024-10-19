using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Add this for scene management

public class GameOverManager : MonoBehaviour
{
    public TextMeshProUGUI gameOverText;  // Main "Game Over" text
    public TextMeshProUGUI glowText;      // Glow layer behind the text
    public Button restartButton;           // Restart button reference
    public float fadeDuration = 2f;       // Duration for fade-in effect
    public float totalDuration = 4f;      // Total duration for fade in and color change

    [SerializeField] private bool testGameOver = false;  // Serialize boolean to trigger game over

    private void Start()
    {
        // Initially hide both texts and the restart button
        SetTextAlpha(gameOverText, 0f);
        SetTextAlpha(glowText, 0f);
        SetButtonAlpha(restartButton, 0f); // Hide the entire button

        // Disable both texts and the button at start
        gameOverText.gameObject.SetActive(false);
        glowText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);


    }

    private void Update()
    {
        // Check if the testGameOver boolean is true and trigger Game Over
        if (testGameOver)
        {
            TriggerGameOver();
            testGameOver = false;  // Reset the boolean to prevent multiple triggers
        }
    }

    // Trigger the game over screen
    public void TriggerGameOver()
    {
        gameOverText.gameObject.SetActive(true);
        glowText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        StartCoroutine(FadeInTextAndGlow());
    }

    // Coroutine to fade in the text, glow effect, and change font size/color
    IEnumerator FadeInTextAndGlow()
    {
        float elapsedTime = 0f;
        float initialGlowSize = glowText.fontSize; // Store the initial glow size
        float targetGlowSize = 32f; // Target font size for glow text
        Color initialColorGameOver = gameOverText.color; // Store the initial color of game over text
        Color targetColor = Color.white; // Target color for both texts

        // First phase: Fade in
        while (elapsedTime < fadeDuration)
        {
            // Calculate the current alpha value for fade
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            SetTextAlpha(gameOverText, alpha);
            SetTextAlpha(glowText, alpha);

            // Lerp font size for glow text
            glowText.fontSize = Mathf.Lerp(initialGlowSize, targetGlowSize, elapsedTime / fadeDuration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure both texts are fully visible and glow text is resized
        SetTextAlpha(gameOverText, 1f);
        SetTextAlpha(glowText, 1f);
        glowText.fontSize = targetGlowSize;

        // Second phase: Change color to white over the remaining time
        elapsedTime = 0f; // Reset elapsed time for color change
        while (elapsedTime < (totalDuration - fadeDuration)) // Remaining time
        {
            // Lerp color for both texts to white while maintaining the alpha
            float alpha = 1f; // Keep alpha at 1

            gameOverText.color = Color.Lerp(initialColorGameOver, targetColor, elapsedTime / (totalDuration - fadeDuration));
            glowText.color = Color.Lerp(glowText.color, targetColor, elapsedTime / (totalDuration - fadeDuration));

            // Set alpha to 1 to avoid flashing
            SetTextAlpha(gameOverText, alpha);
            SetTextAlpha(glowText, alpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure both texts are set to white at the end
        gameOverText.color = targetColor;
        glowText.color = targetColor;

        // Fade in the restart button after the texts have turned white
        elapsedTime = 0f; // Reset elapsed time for button fade-in
        while (elapsedTime < fadeDuration) // Fade in duration for button
        {
            float buttonAlpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            SetButtonAlpha(restartButton, buttonAlpha); // Fade in the entire button (image and text)

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the button is fully visible at the end
        SetButtonAlpha(restartButton, 1f);
    }

 

    // Utility function to set the alpha of the text
    private void SetTextAlpha(TextMeshProUGUI text, float alpha)
    {
        Color color = text.color;
        color.a = alpha;
        text.color = color;
    }

    // Utility function to set the alpha of the button (image and text)
    private void SetButtonAlpha(Button button, float alpha)
    {
        // Set the button image alpha
        Image buttonImage = button.GetComponent<Image>();
        Color imageColor = buttonImage.color;
        imageColor.a = alpha;
        buttonImage.color = imageColor;

        // Set the button text alpha
        TextMeshProUGUI buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
        Color textColor = buttonText.color;
        textColor.a = alpha;
        buttonText.color = textColor;
    }
}
