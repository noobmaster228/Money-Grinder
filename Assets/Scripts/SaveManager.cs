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
            return JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString(SaveKey));
        return new SaveData();
    }
}
