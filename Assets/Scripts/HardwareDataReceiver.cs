using UnityEngine;

public class HardwareDataReceiver : MonoBehaviour
{
    public TMWavecircle tmWavecircle;

    void Start()
    {
        if (tmWavecircle == null)
        {
            Debug.LogError("TMWavecircle 未分配，请在 Inspector 中分配。");
        }
    }

    void Update()
    {
        // 假设这里模拟从硬件获取的数据
        float hardwareValue = GetHardwareData(); // 例如从传感器获取的值

        // 调用 SetNo1Value 更新进度条值
        if (tmWavecircle != null)
        {
            tmWavecircle.SetNo1Value(hardwareValue);
        }
    }

    /// <summary>
    /// 模拟获取硬件数据的方法
    /// 实际上可以替换为读取串口、网络请求或其他硬件接口
    /// </summary>
    float GetHardwareData()
    {
        // 这里可以是硬件数据获取逻辑
        // 例如模拟一个 0 到 100 的随机值
        return Random.Range(0f, 100f);
    }
}
