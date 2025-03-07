using System;

[Serializable]
public class Book
{
    public int bookId;               // 图书编号
    public string title;             // 书名
    public string author;            // 作者
    public string category;          // 分类
    public int totalCopies;          // 总数量
    public int availableCopies;      // 可借数量
    public DateTime? publishedDate;  // 出版日期
    public string isbn;              // ISBN号
    public string location;          // 位置
    public BookStatus status;        // 状态
    public int? categoryId;          // 分类ID

    // 构造函数
    public Book(int bookId, string title, string author, string category, int totalCopies, int availableCopies,
                DateTime? publishedDate, string isbn, string location, BookStatus status, int? categoryId)
    {
        this.bookId = bookId;
        this.title = title;
        this.author = author;
        this.category = category;
        this.totalCopies = totalCopies;
        this.availableCopies = availableCopies;
        this.publishedDate = publishedDate;
        this.isbn = isbn;
        this.location = location;
        this.status = status;
        this.categoryId = categoryId;
    }

    // 默认构造函数
    public Book() { }
}

// 用于表示图书状态的枚举类型
public enum BookStatus
{
    Available,     // 可借
    Unavailable    // 不可借
}
