using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float Record;
    public float Balance;
    public string[] PurchasedSkins;
    public string ActiveSkinId;
}

public static class SaveManager
{
    private const string SaveKey = "SaveData";
    public static void SaveProgress(float record, float balance, string[] purchasedSkins, string activeSkinId)
    {
        var data = new SaveData
        {
            Record = record,
            Balance = balance,
            PurchasedSkins = purchasedSkins,
            ActiveSkinId = activeSkinId
        };
        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }
    public static void SaveProgressOnlyStats(float record, float balance)
    {
        var data = LoadProgress();
        data.Record = record;
        data.Balance = balance;
        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }

    public static void SaveProgressOnlySkins(string[] purchasedSkins, string activeSkinId)
    {
        var data = LoadProgress();
        data.PurchasedSkins = purchasedSkins;
        data.ActiveSkinId = activeSkinId;
        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }

    public static SaveData LoadProgress()
    {
        if (PlayerPrefs.HasKey(SaveKey))
        {
            var data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SaveKey));

            // safety check: вдруг список пуст (обновление старого сейва)
            if (data.PurchasedSkins == null || data.PurchasedSkins.Length == 0)
            {
                data.PurchasedSkins = new string[] { "0" };
                data.ActiveSkinId = "0";
                PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(data));
                PlayerPrefs.Save();
            }
            return data;
        }

        // ѕервый запуск Ч выдаЄм первый скин
        var newData = new SaveData
        {
            Record = 0,
            Balance = 0,
            PurchasedSkins = new string[] { "0" },
            ActiveSkinId = "0"
        };
        PlayerPrefs.SetString(SaveKey, JsonUtility.ToJson(newData));
        PlayerPrefs.Save();
        return newData;
    }
    public static void ResetAllProgress()
    {
        PlayerPrefs.DeleteKey(SaveKey);
        PlayerPrefs.Save();
    }
}
