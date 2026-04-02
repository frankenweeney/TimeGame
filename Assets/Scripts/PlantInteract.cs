using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlantInteract : MonoBehaviour
{
    public GameObject wateringCan;
    public Vector3 tilted;
    public Vector3 upright;
    public ParticleSystem water;
    public float startTime;
    public float timer;
    public float holdTime;
    private bool isBeingWatered;


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
    public float lastWaterRefill;
    public float lastSunRefill;

    void Start()
    {
        GetComponent<ParticleSystem>();
        tilted = new Vector3 (0, 0, 45);
        upright = new Vector3(0, 0, -45);
        water.Stop();

        GetComponent<SpriteRenderer>();
        timeMeter.fillAmount = 1;
        waterMeter.fillAmount = 1;
        sunMeter.fillAmount = 1;
        waterProgression = 0f;
        sunProgression = 0f;
        lastWaterRefill = 0;
        lastSunRefill = 0;
        isBeingWatered = false;
    }

    // Update is called once per frame
    void Update()
    {
        waterProgression = isBeingWatered ? -2 * Time.deltaTime : Time.deltaTime ;
        sunProgression = Time.deltaTime;
        timeMeter.fillAmount -= Time.deltaTime / gameDuration;
        waterMeter.fillAmount -= waterProgression / waterDuration;
        sunMeter.fillAmount -= sunProgression / sunDuration;
        waterMeter.fillAmount = Mathf.Clamp(waterMeter.fillAmount, 0, 1);

        if (waterMeter.fillAmount < 0.2)
        {

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

        if (Time.deltaTime >= 240)
        {
            plant.sprite = plant5;
        }
        if (Time.deltaTime < 240 && gameDuration >= 180)
        {
            plant.sprite = plant4;
        }
        if (Time.deltaTime < 180 && gameDuration >= 120)
        {
            plant.sprite = plant3;
        }
        if (Time.deltaTime < 120 && gameDuration >= 60)
        {
            plant.sprite = plant2;
        }
        if (Time.deltaTime < 60)
        {
            plant.sprite = plant1;
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "WateringCan")
        {
            wateringCan.transform.Rotate(tilted);
            water.Play();
            isBeingWatered = true;

            
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "WateringCan")
        {
            wateringCan.transform.Rotate(upright);
            water.Stop();
      
            isBeingWatered = false;
        }
    }
}

