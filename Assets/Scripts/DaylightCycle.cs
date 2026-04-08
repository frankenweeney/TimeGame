using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaylightCycle : MonoBehaviour
{
    [Header("Sprite Settings")]
    public List<Sprite> sprites;           // List of sprites to cycle through
    public SpriteRenderer spriteRenderer;  // The SpriteRenderer to display them

    [Header("Fade Settings")]
    public float fadeDuration = 5f;        // Time to fade in/out
    public float holdTime = 20f;            // Time to hold fully visible sprite

    private int currentIndex = 0;

    void Start()
    {
        // Validate inputs
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer is not assigned!");
            enabled = false;
            return;
        }
        if (sprites == null || sprites.Count == 0)
        {
            Debug.LogError("No sprites assigned to cycle!");
            enabled = false;
            return;
        }

        // Start the fade cycle
        spriteRenderer.sprite = sprites[currentIndex];
        StartCoroutine(CycleSprites());
    }

    IEnumerator CycleSprites()
    {
        while (true)
        {
            // Fade in
            yield return StartCoroutine(FadeSprite(0f, 1f, fadeDuration));

            // Hold fully visible
            yield return new WaitForSeconds(holdTime);

            // Fade out
            yield return StartCoroutine(FadeSprite(1f, 0f, fadeDuration));

            // Move to next sprite
            currentIndex = (currentIndex + 1) % sprites.Count;
            spriteRenderer.sprite = sprites[currentIndex];
        }
    }

    IEnumerator FadeSprite(float startAlpha, float endAlpha, float duration)
    {
        Color color = spriteRenderer.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            spriteRenderer.color = color;
            yield return null;
        }

        // Ensure final alpha is exact
        color.a = endAlpha;
        spriteRenderer.color = color;
    }
}