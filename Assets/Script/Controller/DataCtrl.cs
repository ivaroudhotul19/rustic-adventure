using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class DataCtrl : MonoBehaviour
{
    public static DataCtrl instance = null;
    public GameData data;     
    string dataFilePath; 
    BinaryFormatter bf;
    public bool devMode;                          

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        bf = new BinaryFormatter();

        dataFilePath = Application.persistentDataPath + "/game.dat";

        Debug.Log(dataFilePath);
    }

    public void RefreshData()
    {
        if(File.Exists(dataFilePath))
        {
            FileStream fs = new FileStream(dataFilePath, FileMode.Open);
            data = (GameData)bf.Deserialize(fs);
            fs.Close();

            Debug.Log("Data Refreshed");
        }
    }

    public bool isUnlocked(int levelNumber) {
        return data.levelData[levelNumber].isUnlocked;
    }

    public int getStars(int levelNumber) {
        return data.levelData[levelNumber].starsAwarded;
    }

    void OnEnable()
	{
        CheckDB();
	}

    void CheckDB()
    {
        if (!File.Exists(dataFilePath))
        {
            #if UNITY_ANDROID
            CopyDB();
            #endif
        }
        else
        {
            if(SystemInfo.deviceType == DeviceType.Desktop)
            {
                string destFile = Path.Combine(Application.streamingAssetsPath, "game.dat");
                File.Delete(destFile);
                File.Copy(dataFilePath, destFile);
            }

            if(devMode)
            {
                if (SystemInfo.deviceType == DeviceType.Handheld)
                {
                    File.Delete(dataFilePath);
                    CopyDB();
                } 
            }

            RefreshData();
        }
    }

    void CopyDB()
    {
        string srcFile = Path.Combine(Application.streamingAssetsPath, "game.dat");
        WWW downloader = new WWW(srcFile);
        while (!downloader.isDone)
        {
            // nothing to be done while downloader gets our db file
        }

        // then save to Application.persistentDataPath
        File.WriteAllBytes(dataFilePath, downloader.bytes);
        RefreshData();
    }
}
