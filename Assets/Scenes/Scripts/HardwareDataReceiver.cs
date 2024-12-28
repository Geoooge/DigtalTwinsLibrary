using UnityEngine;

public class HardwareDataReceiver : MonoBehaviour
{
    public TMWavecircle tmWavecircle;

    void Start()
    {
        if (tmWavecircle == null)
        {
            Debug.LogError("TMWavecircle δ���䣬���� Inspector �з��䡣");
        }
    }

    void Update()
    {
        // ��������ģ���Ӳ����ȡ������
        float hardwareValue = GetHardwareData(); // ����Ӵ�������ȡ��ֵ

        // ���� SetNo1Value ���½�����ֵ
        if (tmWavecircle != null)
        {
            tmWavecircle.SetNo1Value(hardwareValue);
        }
    }

    /// <summary>
    /// ģ���ȡӲ�����ݵķ���
    /// ʵ���Ͽ����滻Ϊ��ȡ���ڡ��������������Ӳ���ӿ�
    /// </summary>
    float GetHardwareData()
    {
        // ���������Ӳ�����ݻ�ȡ�߼�
        // ����ģ��һ�� 0 �� 100 �����ֵ
        return Random.Range(0f, 100f);
    }
}
