using System.IO;
using UnityEngine;

public class GameDataLoader : MonoBehaviour
{
    private readonly string enemyDataPath = "Data/EnemyData";

    public LevelDataSO levelData;

    public void ImportData()
    {
        TextAsset textAsset = Resources.Load<TextAsset>(enemyDataPath);

        if (textAsset != null)
        {
            string data = textAsset.text;
        }
        else
        {
            ExportData(levelData);
            ImportData();
        }
    }

    void Start()
    {
        ExportData(levelData);
    }

    public void ExportData(Object dataClass)
    {
        string filePath = enemyDataPath + ".json";
        string directoryPath = Path.GetDirectoryName(filePath);

        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        string data = JsonUtility.ToJson(dataClass, true);
        File.WriteAllText(filePath, data);
    }
}
