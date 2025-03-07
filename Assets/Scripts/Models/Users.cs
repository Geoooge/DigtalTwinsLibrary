using System;

[Serializable]
public class Users
{
    public int userId;              // �û�ID
    public string userName;         // �û���
    public string password;         // ����
    public String  identity;   // ���
    public string email;            // ����
    public string phoneNumber;      // �绰����
    public DateTime? registrationDate; // ע��ʱ��
    public DateTime? lastLogin;     // ����¼ʱ��
    public UserStatus status;       // ״̬

    // ���캯��
    public Users(int userId, string userName, string password, String identity,
                string email, string phoneNumber, DateTime? registrationDate,
                DateTime? lastLogin, UserStatus status)
    {
        this.userId = userId;
        this.userName = userName;
        this.password = password;
        this.identity = identity;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.registrationDate = registrationDate;
        this.lastLogin = lastLogin;
        this.status = status;
    }

    // Ĭ�Ϲ��캯��
    public Users() { }
}

// ���ڱ�ʾ�û���ݵ�ö������
public enum UserIdentity
{
    ����Ա,   // Admin
    ѧ��,     // Student
    ��ʦ,     // Teacher
    �ÿ�      // Guest
}

// ���ڱ�ʾ�û�״̬��ö������
public enum UserStatus
{
    Active,   // ����
    Inactive  // �Ǽ���
}
