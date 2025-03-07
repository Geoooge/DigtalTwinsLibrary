using System;

[Serializable]
public class BorrowRecord
{
    public int recordId;             // 记录ID
    public int userId;               // 用户ID
    public int bookId;               // 图书ID
    public DateTime? borrowDate;     // 借阅日期
    public DateTime? dueDate;        // 到期日期
    public DateTime? returnDate;     // 归还日期
    public BorrowStatus status;      // 借阅状态

    // 构造函数
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

    // 默认构造函数
    public BorrowRecord() { }
}

// 用于表示借阅状态的枚举类型
public enum BorrowStatus
{
    Borrowing,          // 借阅中
    Returned,           // 已归还
    OverdueNotReturned  // 逾期未还
}
