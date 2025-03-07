using System;

[Serializable]
public class Categories
{
    public int categoryId;          // 分类ID
    public string categoryName;     // 分类名称

    // 构造函数
    public Categories(int categoryId, string categoryName)
    {
        this.categoryId = categoryId;
        this.categoryName = categoryName;
    }

    // 默认构造函数
    public Categories() { }
}
