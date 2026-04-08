using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockScript : MonoBehaviour
{
    public RectTransform hourHand;
    public RectTransform minuteHand;
    public RectTransform secondHand;

    // Update is called once per frame
    void Update()
    {
        System.DateTime time = System.DateTime.Now;
        float seconds = (time.Second);
        float minutes = (time.Minute + seconds / 60f);
        float hours = (time.Hour % 12 + minutes / 60f);
        secondHand.localRotation = Quaternion.Euler(0, 0f, -seconds * 6f);
        minuteHand.localRotation = Quaternion.Euler(0, 0f, -minutes * 6f);
        hourHand.localRotation = Quaternion.Euler(0, 0f, -hours * 30f);
    }
}