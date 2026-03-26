
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
    void Start()
    {
        timeMeter.fillAmount = 1;
        waterMeter.fillAmount = 1;
        sunMeter.fillAmount = 1;
    }

    void Update()
    {
        timeMeter.fillAmount -= Time.deltaTime / gameDuration;
        waterMeter.fillAmount -= Time.deltaTime / waterDuration;
        sunMeter.fillAmount -= Time .deltaTime / sunDuration;
    }
}
