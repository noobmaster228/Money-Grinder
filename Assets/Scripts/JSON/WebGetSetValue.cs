using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using System.Collections;





public class WebGetSetValue : MonoBehaviour
{

    [SerializeField] string Name;
    [SerializeField] string type;
    [SerializeField] string attack1;
    [SerializeField] string spriteURL;

    public RawImage pokeRawImage;

    //  Value value = new Value();
    public float points;
    public float endPosX;
    public float endPosY;
    public float endPosZ;
    public float speed;
    public float moneybag1;
    public float moneybag2;
    public float money1;
    public float money2;
    public float multiplier;
    public Text nameText;
    public MainMenu MM;

    


    DataController dataController;

    private static WebGetSetValue insctance;
    public static WebGetSetValue Instance => insctance;
    private void Awake()
    {
        if (insctance != null)
        {
            Destroy(gameObject);
        }
        else
        { insctance = this; }

        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        MM = FindObjectOfType<MainMenu>();
        dataController = FindObjectOfType<DataController>();
        // GetLevelSettings();
    }
    //string name;
    /*public void SetUserName()
    {
        name = nameText.text;
        SendResult(name, "123");
    }*/

    public void GetAllData()
    {
        StartCoroutine(SendRequest());
    }


    public JSONNode requestText;
    public IEnumerator SendRequest()
    {
        string url = "https://money-grinder-default-rtdb.europe-west1.firebasedatabase.app/.json";
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request.error);
            yield break;
        }

        requestText = JSON.Parse(request.downloadHandler.text);  //переводит Json в JSONNode, который можно прочитать как string
                                                                 // можно выделить отдельный JSONNode из общего JSONNode

    }

    public void SetUpLevelSetting(int num)
    {

        points = requestText["levels"][num]["PointsPerSecond"];
        endPosX = requestText["levels"][num]["endX"];
        endPosY = requestText["levels"][num]["endY"];
        endPosZ = requestText["levels"][num]["endZ"];
        speed = requestText["levels"][num]["speed"];
        multiplier = requestText["levels"][num]["multiply"];
        moneybag1 = requestText["common"]["moneyBagValue"];
        moneybag2 = requestText["common"]["BadmoneyBagValue"];
        money1 = requestText["common"]["moneyValue"];
        money2 = requestText["common"]["badMoneyValue"];



        /*Moving mv = FindObjectOfType<Moving>();
        mv.PointsRate = points;
        mv.endPos = new Vector3(endPosX, endPosY, endPosZ);
        mv.multiply = multiplier;
        mv.moneybagValue = moneybag1;
        mv.badMoneybagValue = moneybag2;
        mv.moneyValue = money1;
        mv.badMoneyValue = money2;
        mv.speed = speed;*/

    }

    public void SendResult(string imya, string password)
    {        MM = FindObjectOfType<MainMenu>();
        StartCoroutine(SendResultsCr(imya, password));

    }
    private IEnumerator SendResultsCr(string name, string password)
    {
        int j = 0;
        while (requestText["Users"][j] != null)
        {
            j++;
        }

        bool isExist=false;
        for (int i = 0; i < j; i++)
        {
            if (requestText["Users"][i]["UserName"] == name)
            {
                isExist = true;
                if (requestText["Users"][i]["Password"] == password)
                {

                    //изменить значение
                    for (int k = 0; k < 8; k++)
                    {
                        requestText["results"][name][k] = dataController.getLevelScore(k);
                    }
                    //StartCoroutine(MM.ResultsSaved());
                }
                else
                {
                    //StartCoroutine(MM.WrongPassword());
                    /*MM.WrongPasssword.SetActive(true);
                    MM.t3.SetActive(true);
                    MM.SendRes.SetActive(false);
                    MM.ImportRes.SetActive(false);
                    MM.BackButton.SetActive(false);
                    MM.cancel.SetActive(false);
                    MM.sendbut.SetActive(false);
                    MM.impbut.SetActive(false);
                    MM.passfield.SetActive(false);
                    MM.namefield.SetActive(false);
                    MM.Scores.SetActive(false);
                    yield return new WaitForSeconds(1);
                    MM.t3.SetActive(false);
                    MM.t2.SetActive(true);
                    yield return new WaitForSeconds(1);
                    MM.t2.SetActive(false);
                    MM.t1.SetActive(true);
                    yield return new WaitForSeconds(1);
                    MM.WrongPasssword.SetActive(false);
                    MM.t1.SetActive(false);
                    MM.SendRes.SetActive(true);
                    MM.ImportRes.SetActive(true);
                    MM.BackButton.SetActive(true);
                    MM.Scores.SetActive(true);*/
                    //говорим, что пароль не совпал
                    break;
                }
            }
        }

        if (!isExist)
        {
            requestText["Users"][j]["UserName"] = name;
            requestText["Users"][j]["Password"] = password;

            //изменить значение
            for (int k = 0; k < 8; k++)
            {
                requestText["results"][name][k] = dataController.getLevelScore(k);
            }
        }


        UnityWebRequest request2 = UnityWebRequest.Put("https://money-grinder-default-rtdb.europe-west1.firebasedatabase.app/.json", requestText.ToString());

        yield return request2.SendWebRequest();
        if (request2.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.LogError(request2.error);
            yield break;
        }

    }



}

