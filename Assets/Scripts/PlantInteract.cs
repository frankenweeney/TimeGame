using UnityEngine;

public class PlantInteract : MonoBehaviour
{
    public GameObject wateringCan;
    public Vector3 tilted;
    public Vector3 upright;
    void Start()
    {
        tilted = new Vector3 (0, 0, 45);
        upright = new Vector3(0, 0, -45);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "WateringCan")
        {
            wateringCan.transform.Rotate(tilted);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "WateringCan")
        {
            wateringCan.transform.Rotate(upright);
        }
    }
}
