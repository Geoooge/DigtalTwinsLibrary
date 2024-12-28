using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TMWavecircle : MonoBehaviour
{
    [Range(0, 100)]
    public float no1;

    public Transform wave;
    public Transform s, e;

    //public Text theText;
    public TextMeshProUGUI theText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
        UpdatePercent(no1);
       
    }

    void UpdatePercent(float f)
    {
        wave.position = s.position + (e.position - s.position) * f / 100;

        theText.text = Mathf.RoundToInt(f) + "%";
    }

    /// <summary>
    /// 提供给外部的接口方法，用于设置 no1 的值
    /// </summary>
    /// <param name="value">新的 no1 值，范围 0-100</param>
    public void SetNo1Value(float value)
    {
        no1 = Mathf.Clamp(value, 0, 100); // 将值限制在 0 到 100 之间
        Debug.Log($"no1 值已更新为: {no1}");
    }
}
