using System;

[Serializable]
public class Categories
{
    public int categoryId;          // ����ID
    public string categoryName;     // ��������

    // ���캯��
    public Categories(int categoryId, string categoryName)
    {
        this.categoryId = categoryId;
        this.categoryName = categoryName;
    }

    // Ĭ�Ϲ��캯��
    public Categories() { }
}
