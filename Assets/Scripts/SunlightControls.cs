using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class SunlightControls : MonoBehaviour
{
  
    public SpriteRenderer sky; 
    [Header("Fade Settings")]
    public float fadeDuration = 5f;        // Time to fade in/out
    public float dayTime = 20f;            // Time to hold fully visible sprite
    public float nightTime = 30f;

    public SpriteRenderer curtain;
    public Sprite closed;
    public Sprite open;
    private bool curtainOpen;
    private Color originalColor;

    public bool inSunlight;
    public Light2D sunbeam;
    public Light2D globalLight;
    public Light2D lamp;
    public float sunDuration = 30f;
    public Image sunMeter;
    public float sunProgression = 0f;
    public float startTime;
    public float timer;
    public GameObject flower;

    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    void Awake()
    {
        originalPosition = flower.transform.localPosition;
    }
    void Start()
    {
        inSunlight = true;
        sunProgression = 0f;
        timer = 0f;
        globalLight.intensity = 1;
        globalLight.color = Color.white;
        lamp.intensity = 0;
        sunbeam.intensity = 1.5f;
        sunbeam.color = Color.softYellow;

        curtainOpen = true;
        curtain.sprite = open;
        curtain = GetComponent<SpriteRenderer>();
        if (curtain != null)
        {
            originalColor = curtain.material.color;
        }

        if (sky == null)
        {
            Debug.LogError("SpriteRenderer is not assigned!");
            enabled = false;
            return;
        }

        StartCoroutine(CycleSprite());
    }

    void Update()
    {
        timer += Time.deltaTime;
        sunProgression = timer * 0.5f;
        sunMeter.fillAmount = Mathf.Clamp(sunMeter.fillAmount, 0, 1);

        if (inSunlight == true)
        {
            sunMeter.fillAmount += sunProgression / sunDuration;
        }
        if (inSunlight == false)
        {
            sunMeter.fillAmount -= sunProgression / sunDuration;
        }

        if (sunMeter.fillAmount < 0.2)
        {
            StartShake(0.5f, 0.05f);
        }
    }
    IEnumerator CycleSprite()
    {
        while (true)
        {
            // Fade in
            yield return StartCoroutine(FadeSprite(0f, 1f, fadeDuration));
            DaytimeEvents();
            
            yield return new WaitForSeconds(dayTime);

            // Fade out
            yield return StartCoroutine(FadeSprite(1f, 0f, fadeDuration));
            NighttimeEvents();
            yield return new WaitForSeconds(nightTime);
        }
    }

    IEnumerator FadeSprite(float startAlpha, float endAlpha, float duration)
    {
        Color color = sky.color;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            color.a = Mathf.Lerp(startAlpha, endAlpha, t);
            sky.color = color;
            yield return null;
        }

        // Ensure final alpha is exact
        color.a = endAlpha;
        sky.color = color;
    }

    private void OnMouseOver2D()
    {
        if (curtain != null)
        {
            curtain.material.color = Color.white;
        }
    }
    public void OnMouseDown2D()
    {
        if (curtainOpen == true)
        {
            curtain.sprite = closed;
            curtainOpen = false;
        }
        if (curtainOpen == false)
        {
            curtain.sprite = open;
            curtainOpen = true;
        }
    }

    private void OnMouseExit2D()
    {
        if (curtain != null)
        {
            curtain.material.color = originalColor;
        }
    }

    public void StartShake(float duration, float magnitude)
    {
        // Stop any ongoing shake before starting a new one
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            flower.transform.localPosition = originalPosition;
        }

        shakeCoroutine = StartCoroutine(Shake(duration, magnitude));
    }

    private IEnumerator Shake(float duration, float magnitude)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;

            flower.transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset position
        flower.transform.localPosition = originalPosition;
        shakeCoroutine = null;
    }
    void DaytimeEvents()
    {
        inSunlight = true;
        timer = 0;
        sunbeam.color = Color.softYellow;
        globalLight.intensity = 1;
        globalLight.color = Color.white;
        lamp.intensity = 0;
    }
    void NighttimeEvents()
    {
        inSunlight = false;
        timer = 0;
        sunbeam.color = Color.mediumBlue;
        globalLight.intensity = 0.8f;
        globalLight.color = Color.midnightBlue;
        lamp.intensity = 1;
    }
}