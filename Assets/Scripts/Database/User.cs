using System;
using Utils;

public class User
{
    private DBConnector dbConnector; // 数据库连接器

    // 构造函数，传入用于连接数据库的连接器
    public User(DBConnector dbConn)
    {
        dbConnector = dbConn; // 保存连接器对象
    }

    // 用户注册
    public int Register(string user_id, string user_name, string password, string identity,string confirmPassword)
    {
        // 查询用户名的数量
        var query1 = $"SELECT COUNT(*) FROM users WHERE user_name = '{user_name}'";
        // 插入一条新用户记录
        var query2 = $"INSERT INTO users (user_id, user_name, password, identity) VALUES ('{user_id}', '{user_name}', '{password}', '{identity}')";

        // 尝试与数据库建立连接
        if (dbConnector.OpenConnection() == true)
        {
            try
            {
                // 执行查询用户名数量的语句
                var dataTable = dbConnector.SelectQuery(query1);
                // 从查询结果中获取数量
                int count = int.Parse(dataTable.Rows[0][0].ToString());
                

                if (count > 0) // 如果已存在同名用户
                {
                    dbConnector.CloseConnection(); // 关闭连接
                    return 2; // 用户名已存在
                }
                if (password != confirmPassword)
                {
                    dbConnector.CloseConnection(); // 关闭连接
                    return 5;
                }

                dbConnector.ExecuteNonQuery(query2); // 执行插入语句插入新用户记录
                dbConnector.CloseConnection(); // 关闭连接
                return 1; // 注册成功
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                dbConnector.CloseConnection();
                return 4; // 插入数据失败
            }
        }
        else
        {
            return 3; // 连接错误
        }
    }

    // 用户登录
    public int Login(string userId, string password,string identity)
    {
        // 查询指定用户名的记录
        var query = $"SELECT * FROM users WHERE user_id = '{userId}' LIMIT 1";

        // 尝试与数据库建立连接
        if (dbConnector.OpenConnection() == true)
        {
            // 执行查询指定用户名记录的语句
            var dataTable = dbConnector.SelectQuery(query);

            // 查询用户输入的用户名是否存在于数据库中
            if (dataTable.Rows.Count == 1)
            {
                // 获取查询结果中的用户密码
                string storedPassword = dataTable.Rows[0]["password"].ToString();
                string stroedIdentity = dataTable.Rows[0]["identity"].ToString();
                // 如果密码与输入密码匹配
                if (storedPassword == password && stroedIdentity == identity)
                {
                    dbConnector.CloseConnection(); // 关闭连接
                    return 1; // 登录成功
                }
                else if (storedPassword != password && stroedIdentity == identity)
                {
                    dbConnector.CloseConnection(); // 关闭连接
                    return 2; // 密码错误
                }
                else if (storedPassword == password && stroedIdentity != identity)
                {
                    dbConnector.CloseConnection(); // 关闭连接
                    return 5;
                }
                else
                {
                    dbConnector.CloseConnection(); // 关闭连接
                    return 2;
                }
            }
            else
            {
                dbConnector.CloseConnection(); // 关闭连接
                return 3; // 用户名不存在
            }
        }
        else
        {
            return 4; // 连接错误
        }
    }
}
    namespace Static
{
    public class StaticData
    {
        public enum RegisterCode
        {
            RegisterSuccess = 1, // 注册成功
            UsernameDoesExist = 2, // 用户名已存在
            ConnectionError = 3, // 连接错误
            InsertDataError = 4, // 插入数据失败
            PasswordsNotMatch = 5,//密码输入不一致
        };
        public enum LoginCode
        {
            LoginSuccess = 1, // 登录成功
            IncorrectPassword = 2, // 密码不正确
            UsernameDoesNotExist = 3, // 用户名不存在
            ConnectionError = 4, //连接错误
            IdentityNotMatch = 5,//身份不匹配
        };
    }
}

