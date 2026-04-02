using UnityEngine;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    public Vector3 position;
    public string playerName;
    public int currentLevel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[System.Serializable]
public class PlayerData
{
    public string name;
    public int level;
}