using System;

[Serializable]
public class Users
{
    public int userId;              // 用户ID
    public string userName;         // 用户名
    public string password;         // 密码
    public String  identity;   // 身份
    public string email;            // 邮箱
    public string phoneNumber;      // 电话号码
    public DateTime? registrationDate; // 注册时间
    public DateTime? lastLogin;     // 最后登录时间
    public UserStatus status;       // 状态

    // 构造函数
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

    // 默认构造函数
    public Users() { }
}

// 用于表示用户身份的枚举类型
public enum UserIdentity
{
    管理员,   // Admin
    学生,     // Student
    教师,     // Teacher
    访客      // Guest
}

// 用于表示用户状态的枚举类型
public enum UserStatus
{
    Active,   // 激活
    Inactive  // 非激活
}
