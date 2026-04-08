using UnityEngine;

public class PulseEffect : MonoBehaviour
{
    public float pulseSpeed = 2f;

    [Tooltip("Maximum scale increase from the original size.")]
    public float pulseAmount = 0.2f;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        float scaleOffset = Mathf.PingPong(Time.time * pulseSpeed, pulseAmount);

      
        transform.localScale = originalScale * (1f + scaleOffset);
    }
}

