using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Data
{
    public float[] scores;

}
public class DataController : MonoBehaviour
{
    #region Singleton

    private static DataController insctance;
    public static DataController Instance => insctance;

    private void Awake()
    {
        if (insctance != null)
        {
            Destroy(gameObject);
        }
        else
        { insctance = this; }


        data = new Data();
        DontDestroyOnLoad(this.gameObject);
        LoadData();

        scores = data.scores;
        // volume = data.volume;

        //   FindObjectOfType<Slider>().value = volume;
    }
    #endregion


    Data data;
    string str;

    [HideInInspector] public float volume;

    public float[] scores;

    public float getLevelScore(int index)
    { return scores[index]; }

    public void LoadData()
    {
        if (PlayerPrefs.HasKey("results"))
        {
            str = PlayerPrefs.GetString("results");
            data = JsonUtility.FromJson<Data>(str);
            if (data.scores.Length < 8)
            {
                float[] newDataScores = new float[8];
                for (int i = 0; i < 5; i++)
                {

                    newDataScores[i] = data.scores[i];

                }

                newDataScores[5] = 0;
                newDataScores[6] = 0;
                newDataScores[7] = 0;
                data.scores = newDataScores;
            }
        }
        else
        {
            data.scores = new float[] { 0, 0, 0, 0, 0, 0, 0, 0 };
            // data.volume = 1f;

        }
    }

    public void SetScore(float score, int levelIndex)
    {
        if (score > data.scores[levelIndex])
        {
            scores[levelIndex] = score;
        }
    }


    public void SaveData()
    {

        data.scores = scores;
        //  data.volume = volume;

        str = JsonUtility.ToJson(data);

        PlayerPrefs.SetString("results", str);
        PlayerPrefs.Save();
    }
    public void Auth()
    {

    }
}
