using UnityEngine;

public class Rotate2D : MonoBehaviour
{
    public float rotationSpeed = 0f; // 旋转速度

    void Update()
    {
        // 根据时间缩放旋转速度
        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        // 绕Z轴旋转（即绕自己旋转）
        transform.Rotate(0, 0, rotationThisFrame);

        // 或者可以直接设置rotation属性
        // transform.rotation *= Quaternion.Euler(0, 0, rotationThisFrame);
    }
}