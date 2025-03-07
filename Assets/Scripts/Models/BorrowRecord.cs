using System;

[Serializable]
public class BorrowRecord
{
    public int recordId;             // ��¼ID
    public int userId;               // �û�ID
    public int bookId;               // ͼ��ID
    public DateTime? borrowDate;     // ��������
    public DateTime? dueDate;        // ��������
    public DateTime? returnDate;     // �黹����
    public BorrowStatus status;      // ����״̬

    // ���캯��
    public BorrowRecord(int recordId, int userId, int bookId, DateTime? borrowDate, DateTime? dueDate,
                        DateTime? returnDate, BorrowStatus status)
    {
        this.recordId = recordId;
        this.userId = userId;
        this.bookId = bookId;
        this.borrowDate = borrowDate;
        this.dueDate = dueDate;
        this.returnDate = returnDate;
        this.status = status;
    }

    // Ĭ�Ϲ��캯��
    public BorrowRecord() { }
}

// ���ڱ�ʾ����״̬��ö������
public enum BorrowStatus
{
    Borrowing,          // ������
    Returned,           // �ѹ黹
    OverdueNotReturned  // ����δ��
}
