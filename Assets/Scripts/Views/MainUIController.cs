using System.Collections;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;

public class MainUIController : MonoBehaviour
{
  
    // 创建两个 TextMeshProUGUI 的类，名为 TimeText01，TimeText02
    public TextMeshProUGUI TimeText01;
    public TextMeshProUGUI TimeText02;
    // 创建 DateTime 类型的时间对象 NowSystemTime
    public DateTime NowSystemTime;
    // 创建一个当前时间字符串变量 NowTime_Str

    public Image imgWeather;    //天气图片
    public Text textWeather;    //天气
    //public Text textTemperature;//温度
    public TextMeshProUGUI textTemperature;
    public Text textCity;       //城市

    private void Start()
    {
        StartCoroutine(GetRuntimeWeather());
    }

    // 实时获取当前系统时间 并转化为字符串格式
    void System_Time_Get()
    {
        // 获取当前时间
        NowSystemTime = DateTime.Now.ToLocalTime();
    }

    void Update()
    {
        System_Time_Get();
        // TimeText01.text 
        TimeText01.text = NowSystemTime.Year + "." + NowSystemTime.Month + "." + NowSystemTime.Day;
        TimeText02.text = NowSystemTime.Hour + ":" + NowSystemTime.Minute + ":" + NowSystemTime.Second;
    }
    IEnumerator GetRuntimeWeather()
    {
        //1.获取本地公网IP
        //https://ip.qaros.com/   https://icanhazip.com/  可查询当前公网
        UnityWebRequest wwwWebIp = UnityWebRequest.Get(@"https://ip.qaros.com/");
        yield return wwwWebIp.SendWebRequest();
        if (wwwWebIp.isNetworkError || wwwWebIp.isHttpError)
        {
            yield break;
        }
        else
        {
            Debug.Log("Network No Error");
        }
        Debug.Log(wwwWebIp.downloadHandler.text);
        //2.根据IP查询城市（心知天气提供接口，需要申请key）
        //string urlQueryCity = "https://api.seniverse.com/v3/location/search.json?key=SNqQSvqfouBhq64LG&q=WSKMS3KBE2JM";
        // wwwWebIp.downloadHandler.text所指向的公网ip在该api下查询不到归属地,所以用城市编码替代
        string urlQueryCity = "https://api.seniverse.com/v3/location/search.json?key=SNqQSvqfouBhq64LG&q=" +wwwWebIp.downloadHandler.text;
        UnityWebRequest wwwQueryCity = UnityWebRequest.Get(urlQueryCity);
        Debug.Log(urlQueryCity);
        yield return wwwQueryCity.SendWebRequest();
        if (wwwQueryCity.isNetworkError || wwwQueryCity.isHttpError)
        {
            yield break;
        }
        Debug.Log("City Query Response: " + wwwQueryCity.downloadHandler.text);

        JObject cityData = JsonConvert.DeserializeObject<JObject>(wwwQueryCity.downloadHandler.text);
        string cityId = cityData["results"][0]["id"].ToString();
        textCity.text = cityData["results"][0]["path"].ToString(); //城市

        //3.根据城市查询天气（心知天气提供接口，需要申请key）
        string urlWeather = string.Format("https://api.seniverse.com/v3/weather/now.json?key=SNqQSvqfouBhq64LG&location={0}&language=zh-Hans&unit=c", cityId);
        UnityWebRequest wwwWeather = UnityWebRequest.Get(urlWeather);
        yield return wwwWeather.SendWebRequest();

        if (wwwWeather.isNetworkError || wwwWeather.isHttpError)
        {
          Debug.Log(wwwWeather.error);
        }
        Debug.Log(wwwWeather.downloadHandler.text);
        //4.解析天气

        if (cityData["results"] != null && cityData["results"] != null)
        {
            Debug.Log(cityData["results"][0]["name"].ToString());
        }
        else
        {
            Debug.Log("City data is not in the expected format");
            yield break;
        }
        //yield return wwwWebIp.SendWebRequest();
        if (wwwQueryCity.isNetworkError || wwwQueryCity.isHttpError)
        {
            Debug.Log("Error getting IP: " + wwwWebIp.error);
            yield break;
        }
        else { Debug.Log("1"); }



        try
        {
            JObject weatherData = JsonConvert.DeserializeObject<JObject>(wwwWeather.downloadHandler.text);
            string spriteName = string.Format("UI/black/{0}@2x", weatherData["results"][0]["now"]["code"].ToString());
            Debug.Log(weatherData);

            //天气文字
            textWeather.text = weatherData["results"][0]["now"]["text"].ToString();


            //图片，可以在心知天气上下载
            imgWeather.sprite = Resources.Load<Sprite>(spriteName);
            //Debug.Log(spriteName);

            //温度
            textTemperature.text = string.Format("{0} °C", weatherData["results"][0]["now"]["temperature"].ToString());
            

            if (textCity == null || textWeather == null || imgWeather == null || textTemperature == null)
            {
                Debug.Log("One or more UI elements are not assigned.");
                yield break;
            }

        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }


}
