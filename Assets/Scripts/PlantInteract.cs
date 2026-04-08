using System;
using System.Collections;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class PlantInteract : MonoBehaviour
{
    public GameObject wateringCan;
    public Vector3 tilted;
    public Vector3 upright;
    public ParticleSystem water;
    public float startTime;
    public float timer;
    private bool isBeingWatered;
    private bool inSunlight;

    public float gameDuration = 300f;
    public Image timeMeter;
    public float waterDuration = 20f;
    public Image waterMeter;
    public float sunDuration = 30f;
    public Image sunMeter;
    public SpriteRenderer plant;
    public Sprite plant1;
    public Sprite plant2;
    public Sprite plant3;
    public Sprite plant4;
    public Sprite plant5;
    public float waterProgression = 0f;
    public float sunProgression = 0f;

    private Vector3 originalPosition;
    private Coroutine shakeCoroutine;

    public Transform bottleTransform;
    public Transform plantDirection;
    public float rotationOffset = 0f;

    void Awake()
    {
        // Store the starting position
        originalPosition = transform.localPosition;
    }

    void Start()
    {
        GetComponent<ParticleSystem>();
        tilted = new Vector3 (0, 0, 45);
        upright = new Vector3(0, 0, -45);
        water.Stop();

        GetComponent<SpriteRenderer>();
        plant.sprite = plant1;
        timeMeter.fillAmount = 1;
        waterMeter.fillAmount = 1;
        sunMeter.fillAmount = 1;
        waterProgression = 0f;
        sunProgression = 0f;
        isBeingWatered = false;
        inSunlight = false;

        startTime = Time.time;
        timer = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        waterProgression = isBeingWatered ? -2 * Time.deltaTime : Time.deltaTime ;
        sunProgression = inSunlight ? -2 * Time.deltaTime : Time.deltaTime;
        timeMeter.fillAmount -= Time.deltaTime / gameDuration;
        waterMeter.fillAmount -= waterProgression / waterDuration;
        sunMeter.fillAmount -= sunProgression / sunDuration;
        waterMeter.fillAmount = Mathf.Clamp(waterMeter.fillAmount, 0, 1);
        timer += Time.deltaTime;

        if (waterMeter.fillAmount < 0.2)
        {
            StartShake(0.5f, 0.05f);
        }
        if (waterMeter.fillAmount < 0.5 && waterMeter.fillAmount > 0.2)
        {

        }
        if (waterMeter.fillAmount > 0.5)
        {

        }

        if (sunMeter.fillAmount < 0.2)
        {

        }
        if (sunMeter.fillAmount < 0.5 && waterMeter.fillAmount > 0.2)
        {

        }
        if (sunMeter.fillAmount > 0.5)
        {

        }

        if (timer >= 240)
        {
            plant.sprite = plant5;
        }
        if (timer < 240 && gameDuration >= 180)
        {
            plant.sprite = plant4;
        }
        if (timer < 180 && gameDuration >= 120)
        {
            plant.sprite = plant3;
        }
        if (timer < 120 && gameDuration >= 60)
        {
            plant.sprite = plant2;
        }
        if (timer < 60)
        {
            plant.sprite = plant1;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "WateringCan")
        {
            PointTowards();

            water.Play();
            isBeingWatered = true;

            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "WateringCan")
        {
            water.Stop();
      
            isBeingWatered = false;
        }
    }

    public void StartShake(float duration, float magnitude)
    {
        // Stop any ongoing shake before starting a new one
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            transform.localPosition = originalPosition;
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

            transform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0f);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset position
        transform.localPosition = originalPosition;
        shakeCoroutine = null;
    }

    public void PointTowards()
    {
        if (plantDirection == null)
        {
            Debug.LogWarning("Target not assigned in " + gameObject.name);
            return;
        }

        // Calculate direction from this object to the target
        Vector2 direction = (Vector2)plantDirection.position - (Vector2)bottleTransform.position;

        if (direction.sqrMagnitude < Mathf.Epsilon)
            return; // Avoid NaN errors if objects are at the same position

        // Calculate angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply rotation (Z-axis rotation for 2D)
        bottleTransform.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + rotationOffset));
    }
}

