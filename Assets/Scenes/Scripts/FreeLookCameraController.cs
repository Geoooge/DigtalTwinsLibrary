using Cinemachine;
using UnityEngine;

public class FreeLookCameraController : MonoBehaviour
{

    public CinemachineFreeLook freeLookCamera;
    public float rotateSpeed = 1.0f; // ������ת�ٶ�
    public float zoomSpeed = 2.0f;   // ���������ٶ�
    public float minZoom = 2.0f;     // ��С���ž���
    public float maxZoom = 10.0f;    // ������ž���
    public float autoRotateSpeed = 10.0f; // �Զ���ת�ٶ�
    public bool isAutoRotateEnabled = false; // �Զ���ת����

    private float targetYAxisValue;
    private float yAxisSmoothTime = 0.2f; // Y��ƽ��ʱ��
    private float yAxisVelocity = 0.0f;   // ����ƽ���������ʱ����

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
        // �������Ҽ��Ƿ����Խ�����ת
        if (Input.GetMouseButton(0)) // ������
        {
            float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
            float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

            // �޸� X ����תֵ
            freeLookCamera.m_XAxis.Value += horizontal;

            // ���� Y ��Ŀ��ֵ
            targetYAxisValue = Mathf.Clamp(freeLookCamera.m_YAxis.Value - vertical, 0.0f, 1.0f);
        }
        else if (isAutoRotateEnabled)
        {
            // �Զ���תʱֻӰ�� X ��
            freeLookCamera.m_XAxis.Value += autoRotateSpeed * Time.deltaTime;
        }

        // ƽ���ؽ� Y ���ֵ��Ŀ��ֵ
        freeLookCamera.m_YAxis.Value = Mathf.SmoothDamp(freeLookCamera.m_YAxis.Value, targetYAxisValue, ref yAxisVelocity, yAxisSmoothTime);

        // �����ֿ�������
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
