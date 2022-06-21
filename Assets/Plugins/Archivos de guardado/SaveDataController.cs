using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SaveDataController : MonoBehaviour
{
    static SaveData saveData;
    static string fileName = "SaveData.json";
    static bool loaded;
    public static void Initialize()
    {
        saveData = new SaveData();
        SaveToFile();
        loaded = true;
    }
    public static SaveData GetSaveData()
    {
        CheckInitialized();
        return saveData;
    }
    public static void CheckInitialized()
    {
        if (!File.Exists(Application.persistentDataPath + "/" + fileName))
        {
            Initialize();
        }
        else
        {
            if (saveData == null )
            {
                saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(Application.persistentDataPath + "/" + fileName));
            }
        }
    }

    static void SaveToFile()
    {
        string saveDataJSON = JsonUtility.ToJson(saveData);
        //string encodedString = EncryptDecrypt(saveDataJSON);
        File.WriteAllText(Application.persistentDataPath + "/" + fileName, saveDataJSON);
    }
    
    public static string EncryptDecrypt(string textToEncrypt)
    {
        int key = 129;
        StringBuilder inSb = new StringBuilder(textToEncrypt);
        StringBuilder outSb = new StringBuilder(textToEncrypt.Length);
        char c;
        for (int i = 0; i < textToEncrypt.Length; i++)
        {
            c = inSb[i];
            c = (char)(c ^ key);
            outSb.Append(c);
        }
        return outSb.ToString();
    }

    #region Currency

    public static int SoftCoins
    {
        get 
        {
            CheckInitialized();
            return saveData.softCoins;
        }
        set
        {
            CheckInitialized();
            saveData.softCoins = value;
            SaveToFile();
        }
    }

    public static int HardCoins
    {
        get 
        {
            CheckInitialized();
            return saveData.hardCoins;
        }
        set
        {
            CheckInitialized();
            saveData.hardCoins = value;
            SaveToFile();
        }
    }

    public static int Energy
    {
        get 
        {
            CheckInitialized();
            return saveData.energy;
        }
        set
        {
            CheckInitialized();
            saveData.energy = value;
            SaveToFile();
        }
    }

    public static int Experience
    {
        get 
        {
            CheckInitialized();
            return saveData.experience;
        }
        set
        {
            CheckInitialized();
            saveData.experience = value;
            SaveToFile();
        }
    }

    #endregion


    public static string LastConnection
    {
        get
        {
            CheckInitialized();
            return saveData.lastConnection;
        }
        set
        {
            CheckInitialized();
            saveData.lastConnection = value;
            SaveToFile();
        }
    }

    

    #region Array of talents

    public static int[] TalentsList
    {
        get
        {
            CheckInitialized();
            return saveData.talentsList;
        }
        set
        {
            CheckInitialized();
            saveData.talentsList = value;
            SaveToFile();
        }
    }

    public static string Inventory
    {
        get
        {
            CheckInitialized();
            return saveData.inventory;
        }
        set
        {
            CheckInitialized();
            saveData.inventory = value;
            SaveToFile();
        }
    }

    public static string Equipment
    {
        get
        {
            CheckInitialized();
            return saveData.equipment;
        }
        set
        {
            CheckInitialized();
            saveData.equipment = value;
            SaveToFile();
        }
    }

    #endregion

    #region Progress

    public static int CurrentWorld
    {
        get 
        {
            CheckInitialized();
            return saveData.currentWorld;
        }
        set
        {
            CheckInitialized();
            saveData.currentWorld = value;
            SaveToFile();
        }
    }

    public static int[] HighestStageReached
    {
        get 
        {
            CheckInitialized();
            return saveData.highestStageReached;
        }
        set
        {
            CheckInitialized();
            saveData.highestStageReached = value;
            SaveToFile();
        }
    }

    #endregion

    public static int CurrentDailyLoginReward
    {
        get 
        {
            CheckInitialized();
            return saveData.currentDailyLoginReward;
        }
        set
        {
            CheckInitialized();
            saveData.currentDailyLoginReward = value;
            SaveToFile();
        }
    }

    public static string Achievements
    {
        get
        {
            CheckInitialized();
            return saveData.achievements;
        }

        set
        {
            CheckInitialized();
            saveData.achievements = value;
            SaveToFile();
        }
    }

    public static bool[] CompletedDailyQuests
    {
        get
        {
            CheckInitialized();
            return saveData.completedDailyQuests;
        }

        set
        {
            CheckInitialized();
            saveData.completedDailyQuests = value;
            SaveToFile();
        }
    }

    public static bool[] ReclaimedDailyQuests
    {
        get
        {
            CheckInitialized();
            return saveData.reclaimedDailyQuests;
        }

        set
        {
            CheckInitialized();
            saveData.reclaimedDailyQuests = value;
            SaveToFile();
        }
    }
}
