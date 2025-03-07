using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening; // ʹ�� DOTween �����⴦����Ч��

public class UIManager : MonoBehaviour
{
    public GameObject panel1, panel2, panel3;
    public Button button1, button2, button3;

    private Button currentButton;
    private GameObject currentPanel;

    void Start()
    {
        // ���ð�ť�������ͣЧ��
        SetupButton(button1, panel1);
        SetupButton(button2, panel2);
        SetupButton(button3, panel3);

        // Ĭ����ʾ��һ�����
        ShowPanel(panel1);
    }
    /// <summary>
    /// Ϊ��ť��ӵ������ͣЧ��
    /// </summary>
    void SetupButton(Button button, GameObject targetPanel)
    {
        // �����ťʱ�л�����Ӧ���
        button.onClick.AddListener(() => SwitchPanel(targetPanel));

        // ��������ͣЧ��
        AddHoverEffect(button);
    }

    /// <summary>
    /// ��ʾĿ����岢�����������
    /// </summary>
    void ShowPanel(GameObject targetPanel)
    {
        // ��ʾĿ����壬�����������
        panel1.SetActive(targetPanel == panel1);
        panel2.SetActive(targetPanel == panel3);
        panel3.SetActive(targetPanel == panel2);

        // ���µ�ǰ��ʾ�����
        currentPanel = targetPanel;
    }

    /// <summary>
    /// �л���Ŀ�����
    /// </summary>
    void SwitchPanel(GameObject targetPanel)
    {
        if (currentPanel == targetPanel)
        {
            return; // ��������������ǵ�ǰ��壬�򲻽����κβ���
        }

        // �л����µ����
        ShowPanel(targetPanel);

        Debug.Log($"�л������: {targetPanel.name}");
    }

    /// <summary>
    /// ��������ͣЧ��
    /// </summary>
    void AddHoverEffect(Button button)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        // �����ͣʱִ��
        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((data) => OnButtonHoverEnter(button));

        // ����뿪ʱִ��
        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((data) => OnButtonHoverExit(button));

        trigger.triggers.Add(pointerEnter);
        trigger.triggers.Add(pointerExit);
    }

    /// <summary>
    /// �����ͣ�ڰ�ť��ʱ�Ĵ����߼�
    /// </summary>
    void OnButtonHoverEnter(Button button)
    {
        // ֹͣ��ť���ж�������ֹ�ظ�������ͻ
        button.transform.DOKill();

        // �Ŵ����תЧ��
        button.transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack);
        button.transform.DORotate(Vector3.forward * 360f, 1f, RotateMode.FastBeyond360)
            .SetLoops(-1).SetEase(Ease.Linear);
    }

    /// <summary>
    /// ����뿪��ťʱ�Ĵ����߼�
    /// </summary>
    void OnButtonHoverExit(Button button)
    {
        // ֹͣ��ť����ת����
        button.transform.DOKill();

        // �ָ���ťԭʼ��С����ת
        button.transform.DOScale(1f, 0.2f).SetEase(Ease.InBack);
        button.transform.rotation = Quaternion.identity; // ������ת
    }
}
