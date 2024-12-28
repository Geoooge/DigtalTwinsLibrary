using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening; // 使用 DOTween 动画库处理动画效果

public class UIManager : MonoBehaviour
{
    public GameObject panel1, panel2, panel3;
    public Button button1, button2, button3;

    private Button currentButton;
    private GameObject currentPanel;

    void Start()
    {
        // 设置按钮点击和悬停效果
        SetupButton(button1, panel1);
        SetupButton(button2, panel2);
        SetupButton(button3, panel3);

        // 默认显示第一个面板
        ShowPanel(panel1);
    }
    /// <summary>
    /// 为按钮添加点击和悬停效果
    /// </summary>
    void SetupButton(Button button, GameObject targetPanel)
    {
        // 点击按钮时切换到对应面板
        button.onClick.AddListener(() => SwitchPanel(targetPanel));

        // 添加鼠标悬停效果
        AddHoverEffect(button);
    }

    /// <summary>
    /// 显示目标面板并隐藏其他面板
    /// </summary>
    void ShowPanel(GameObject targetPanel)
    {
        // 显示目标面板，隐藏其他面板
        panel1.SetActive(targetPanel == panel1);
        panel2.SetActive(targetPanel == panel3);
        panel3.SetActive(targetPanel == panel2);

        // 更新当前显示的面板
        currentPanel = targetPanel;
    }

    /// <summary>
    /// 切换到目标面板
    /// </summary>
    void SwitchPanel(GameObject targetPanel)
    {
        if (currentPanel == targetPanel)
        {
            return; // 如果点击的面板已是当前面板，则不进行任何操作
        }

        // 切换到新的面板
        ShowPanel(targetPanel);

        Debug.Log($"切换到面板: {targetPanel.name}");
    }

    /// <summary>
    /// 添加鼠标悬停效果
    /// </summary>
    void AddHoverEffect(Button button)
    {
        EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        // 鼠标悬停时执行
        EventTrigger.Entry pointerEnter = new EventTrigger.Entry();
        pointerEnter.eventID = EventTriggerType.PointerEnter;
        pointerEnter.callback.AddListener((data) => OnButtonHoverEnter(button));

        // 鼠标离开时执行
        EventTrigger.Entry pointerExit = new EventTrigger.Entry();
        pointerExit.eventID = EventTriggerType.PointerExit;
        pointerExit.callback.AddListener((data) => OnButtonHoverExit(button));

        trigger.triggers.Add(pointerEnter);
        trigger.triggers.Add(pointerExit);
    }

    /// <summary>
    /// 鼠标悬停在按钮上时的处理逻辑
    /// </summary>
    void OnButtonHoverEnter(Button button)
    {
        // 停止按钮已有动画，防止重复动画冲突
        button.transform.DOKill();

        // 放大和旋转效果
        button.transform.DOScale(1.2f, 0.2f).SetEase(Ease.OutBack);
        button.transform.DORotate(Vector3.forward * 360f, 1f, RotateMode.FastBeyond360)
            .SetLoops(-1).SetEase(Ease.Linear);
    }

    /// <summary>
    /// 鼠标离开按钮时的处理逻辑
    /// </summary>
    void OnButtonHoverExit(Button button)
    {
        // 停止按钮的旋转动画
        button.transform.DOKill();

        // 恢复按钮原始大小和旋转
        button.transform.DOScale(1f, 0.2f).SetEase(Ease.InBack);
        button.transform.rotation = Quaternion.identity; // 重置旋转
    }
}
