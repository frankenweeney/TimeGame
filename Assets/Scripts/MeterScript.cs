
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class TimerScript : MonoBehaviour
{
    public float gameDuration = 600f;
    public Image timeMeter;
    public float waterDuration = 20f;
    public Image waterMeter;
    public float sunDuration = 20f;
    public Image sunMeter;
    public SpriteRenderer plant;
    void Start()
    {
        plant = GetComponent<SpriteRenderer>();
        timeMeter.fillAmount = 1;
        waterMeter.fillAmount = 1;
        sunMeter.fillAmount = 1;
    }

    void Update()
    {
        timeMeter.fillAmount -= Time.deltaTime / gameDuration;
        waterMeter.fillAmount -= Time.deltaTime / waterDuration;
        sunMeter.fillAmount -= Time .deltaTime / sunDuration;

        if (waterMeter.fillAmount < 0.2)
        {
            plant.color = Color.brown;
        }
        if (waterMeter.fillAmount < 0.5 && waterMeter.fillAmount > 0.2)
        {
            plant.color = Color.yellow;
        }
        if (waterMeter.fillAmount > 0.5)
        {
            plant.color = Color.white;
        }

        if (sunMeter.fillAmount < 0.2)
        {
            plant.color = Color.brown;
        }
        if (sunMeter.fillAmount < 0.5 && waterMeter.fillAmount > 0.2)
        {
            plant.color = Color.yellow;
        }
        if (sunMeter.fillAmount > 0.5)
        {
            plant.color = Color.white;
        }
    }

   
}
