using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveData : MonoBehaviour
{
    [SerializeField] GameManager manager;
    public static List<GameManager> obj = new List<GameManager>();

    const string SUB_PATH = "/data";
    const string SUB_COUNT_PATH = "/data";

    private void Awake()
    {
        LoadGame();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SUB_PATH + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + SUB_COUNT_PATH + SceneManager.GetActiveScene().buildIndex;

        FileStream countStream = new FileStream(countPath, FileMode.Create);
        formatter.Serialize(countStream, obj.Count);
        countStream.Close();

        for (int i = 0; i < obj.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            GameData data = new GameData(obj[i]);

            formatter.Serialize(stream, data);
            stream.Close();
        }
    }

    public void LoadGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SUB_PATH + SceneManager.GetActiveScene().buildIndex;
        string countPath = Application.persistentDataPath + SUB_COUNT_PATH + SceneManager.GetActiveScene().buildIndex;
        int gameCount = 0;

        if (File.Exists(countPath))
        {
            FileStream pathStream = new FileStream(countPath, FileMode.Open);
            gameCount = (int)formatter.Deserialize(pathStream);
            pathStream.Close();
        }
        else
        {
            Debug.LogError("Path not found " + countPath);
        }

        for (int i = 0; i < gameCount; i++)
        {
            if(File.Exists(path + i))
            {
                FileStream stream = new FileStream(path + i, FileMode.Open);
                GameData data = formatter.Deserialize(stream) as GameData;

               
                stream.Close();
                manager.remainingCoins = data.remainingCoins;
                manager.cartNum = data.cartNum;
                manager.houseNum = data.houseNum;
            }
            else
            {
                Debug.LogError("Path not found " + countPath);
            }
        }

    }
}
