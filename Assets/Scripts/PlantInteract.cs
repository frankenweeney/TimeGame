using System.Collections;
using UnityEngine;
using UnityEngine.UI;
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

    public float gameDuration = 300f;
    public Image timeMeter;
    public float waterDuration = 20f;
    public Image waterMeter;
    public float hungerDuration = 45;
    public Image HungerMeter;
    public SpriteRenderer plant;
    public Sprite plant1;
    public Sprite plant2;
    public Sprite plant3;
    public Sprite plant4;
    public Sprite plant5;
    public float waterProgression = 0f;
    public float hungerProgression = 0f;
    public SpriteRenderer stomach;
    public Color color;

    public Transform bottleTransform;
    public Transform plantDirection;
    public float rotationOffset = 0f;

    public GameObject plantFood;
    

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
        HungerMeter.fillAmount= 1;
        
        waterProgression = 0f;
        hungerProgression= 0f;
        isBeingWatered = false;
        color.a = 0f;
        stomach.enabled = false;

        startTime = Time.time;
        timer = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        waterProgression = isBeingWatered ? -2 * Time.deltaTime : Time.deltaTime ;
        timeMeter.fillAmount -= Time.deltaTime / gameDuration;
        waterMeter.fillAmount -= waterProgression / waterDuration;
        waterMeter.fillAmount = Mathf.Clamp(waterMeter.fillAmount, 0, 1);
        timer += Time.deltaTime;
        hungerProgression = Time.deltaTime;
        HungerMeter.fillAmount -= hungerProgression / hungerDuration;

        if (HungerMeter.fillAmount >= 0.5f)
        {
            color.a = 0f;
        }
        if (HungerMeter.fillAmount < 0.5 && HungerMeter.fillAmount > 0.2)
        {
            color.a = 0.5f;
            stomach.enabled = true;
        }
        if (HungerMeter.fillAmount <= 0.2)
        {
            color.a = 1f;
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
        if (other.gameObject.name == "plantFood")
        {
            Destroy(plantFood);
            GameObject clone= Instantiate(plantFood, new Vector2(-4.27f, 6f), Quaternion.identity);
            hungerProgression = 0;
            HungerMeter.fillAmount = 1;
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

