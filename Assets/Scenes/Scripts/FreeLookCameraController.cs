using Cinemachine;
using UnityEngine;

public class FreeLookCameraController : MonoBehaviour
{

    public CinemachineFreeLook freeLookCamera;
    public float rotateSpeed = 1.0f; // 控制旋转速度
    public float zoomSpeed = 2.0f;   // 控制缩放速度
    public float minZoom = 2.0f;     // 最小缩放距离
    public float maxZoom = 10.0f;    // 最大缩放距离
    public float autoRotateSpeed = 10.0f; // 自动旋转速度
    public bool isAutoRotateEnabled = false; // 自动旋转开关

    private float targetYAxisValue;
    private float yAxisSmoothTime = 0.2f; // Y轴平滑时间
    private float yAxisVelocity = 0.0f;   // 用于平滑计算的临时变量

    private void Start()
    {
        if (freeLookCamera == null)
        {
            freeLookCamera = GetComponent<CinemachineFreeLook>();
        }

        targetYAxisValue = freeLookCamera.m_YAxis.Value;
    }

    private void Update()
    {
        // 检查鼠标右键是否按下以进行旋转
        if (Input.GetMouseButton(0)) // 鼠标左键
        {
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

            // 修改 X 轴旋转值
            freeLookCamera.m_XAxis.Value += horizontal;

            // 更新 Y 轴目标值
            targetYAxisValue = Mathf.Clamp(freeLookCamera.m_YAxis.Value - vertical, 0.0f, 1.0f);
        }
        else if (isAutoRotateEnabled)
        {
            // 自动旋转时只影响 X 轴
            freeLookCamera.m_XAxis.Value += autoRotateSpeed * Time.deltaTime;
        }

        // 平滑地将 Y 轴插值到目标值
        freeLookCamera.m_YAxis.Value = Mathf.SmoothDamp(freeLookCamera.m_YAxis.Value, targetYAxisValue, ref yAxisVelocity, yAxisSmoothTime);

        // 鼠标滚轮控制缩放
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll != 0.0f)
        {
            for (int i = 0; i < 3; i++)
            {
                freeLookCamera.m_Orbits[i].m_Radius = Mathf.Clamp(
                    freeLookCamera.m_Orbits[i].m_Radius - scroll * zoomSpeed, minZoom, maxZoom
                );
            }
        }
    }

    public void ToggleAutoRotate()
    {
        isAutoRotateEnabled = !isAutoRotateEnabled;
    }
}
