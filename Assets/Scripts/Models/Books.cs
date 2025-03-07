using System;

[Serializable]
public class Book
{
    public int bookId;               // ͼ����
    public string title;             // ����
    public string author;            // ����
    public string category;          // ����
    public int totalCopies;          // ������
    public int availableCopies;      // �ɽ�����
    public DateTime? publishedDate;  // ��������
    public string isbn;              // ISBN��
    public string location;          // λ��
    public BookStatus status;        // ״̬
    public int? categoryId;          // ����ID

    // ���캯��
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

    // Ĭ�Ϲ��캯��
    public Book() { }
}

// ���ڱ�ʾͼ��״̬��ö������
public enum BookStatus
{
    Available,     // �ɽ�
    Unavailable    // ���ɽ�
}
