using UnityEngine;

public class Rotate2D : MonoBehaviour
{
    public float rotationSpeed = 0f; // ��ת�ٶ�

    void Update()
    {
        // ����ʱ��������ת�ٶ�
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        // ��Z����ת�������Լ���ת��
        transform.Rotate(0, 0, rotationThisFrame);

        // ���߿���ֱ������rotation����
        // transform.rotation *= Quaternion.Euler(0, 0, rotationThisFrame);
    }
}