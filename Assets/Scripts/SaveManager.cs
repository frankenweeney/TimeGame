using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public SaveData data;
    public static SaveManager instance;
    private string saveFilePath;
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(this);
            return;
        }

        instance = this;
        saveFilePath = $"{Application.persistentDataPath}/save/save.json";

        SaveToFile();
        LoadFromFile();

        Debug.Log(Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadFromFile()
    {
        if (!File.Exists(saveFilePath))
        {
            // do nothing
            return;
        }
        string json = File.ReadAllText(saveFilePath);
        JsonUtility.FromJsonOverwrite(json, data);
    }
    public void SaveToFile()
    {
        if (!File.Exists(saveFilePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(saveFilePath));
            File.CreateText(saveFilePath);
        }

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(saveFilePath, json);
    }
}
