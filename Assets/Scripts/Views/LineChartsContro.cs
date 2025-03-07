using UnityEngine;
using XCharts.Runtime;

public class LineChartsContro : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        linecharts();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    void linecharts()  
    {
        
        var charts = gameObject.GetComponent<LineChart>();
    //    charts.ClearData();

   //     for (int i = 4;i < 12;i++) 
    //    {
     //       charts.AddXAxisData(i*2+":00",0);
    //        
         //   Debug.Log(i);
      //      charts.AddData(0, Random.Range(30,120));
      //  }
    }
}   
