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
  
    // �������� TextMeshProUGUI ���࣬��Ϊ TimeText01��TimeText02
    public TextMeshProUGUI TimeText01;
    public TextMeshProUGUI TimeText02;
    // ���� DateTime ���͵�ʱ����� NowSystemTime
    public DateTime NowSystemTime;
    // ����һ����ǰʱ���ַ������� NowTime_Str

    public Image imgWeather;    //����ͼƬ
    public Text textWeather;    //����
    //public Text textTemperature;//�¶�
    public TextMeshProUGUI textTemperature;
    public Text textCity;       //����

    private void Start()
    {
        StartCoroutine(GetRuntimeWeather());
    }

    // ʵʱ��ȡ��ǰϵͳʱ�� ��ת��Ϊ�ַ�����ʽ
    void System_Time_Get()
    {
        // ��ȡ��ǰʱ��
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
        //1.��ȡ���ع���IP
        //https://ip.qaros.com/   https://icanhazip.com/  �ɲ�ѯ��ǰ����
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
        //2.����IP��ѯ���У���֪�����ṩ�ӿڣ���Ҫ����key��
        //string urlQueryCity = "https://api.seniverse.com/v3/location/search.json?key=SNqQSvqfouBhq64LG&q=WSKMS3KBE2JM";
        // wwwWebIp.downloadHandler.text��ָ��Ĺ���ip�ڸ�api�²�ѯ����������,�����ó��б������
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
        textCity.text = cityData["results"][0]["path"].ToString(); //����

        //3.���ݳ��в�ѯ��������֪�����ṩ�ӿڣ���Ҫ����key��
        string urlWeather = string.Format("https://api.seniverse.com/v3/weather/now.json?key=SNqQSvqfouBhq64LG&location={0}&language=zh-Hans&unit=c", cityId);
        UnityWebRequest wwwWeather = UnityWebRequest.Get(urlWeather);
        yield return wwwWeather.SendWebRequest();

        if (wwwWeather.isNetworkError || wwwWeather.isHttpError)
        {
          Debug.Log(wwwWeather.error);
        }
        Debug.Log(wwwWeather.downloadHandler.text);
        //4.��������

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

            //��������
            textWeather.text = weatherData["results"][0]["now"]["text"].ToString();


            //ͼƬ����������֪����������
            imgWeather.sprite = Resources.Load<Sprite>(spriteName);
            //Debug.Log(spriteName);

            //�¶�
            textTemperature.text = string.Format("{0} ��C", weatherData["results"][0]["now"]["temperature"].ToString());
            

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
