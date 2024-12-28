using System;
using Utils;

public class User
{
    private DBConnector dbConnector; // ���ݿ�������

    // ���캯�������������������ݿ��������
    public User(DBConnector dbConn)
    {
        dbConnector = dbConn; // ��������������
    }

    // �û�ע��
    public int Register(string user_id, string user_name, string password, string identity,string confirmPassword)
    {
        // ��ѯ�û���������
        var query1 = $"SELECT COUNT(*) FROM users WHERE user_name = '{user_name}'";
        // ����һ�����û���¼
        var query2 = $"INSERT INTO users (user_id, user_name, password, identity) VALUES ('{user_id}', '{user_name}', '{password}', '{identity}')";

        // ���������ݿ⽨������
        if (dbConnector.OpenConnection() == true)
        {
            try
            {
                // ִ�в�ѯ�û������������
                var dataTable = dbConnector.SelectQuery(query1);
                // �Ӳ�ѯ����л�ȡ����
                int count = int.Parse(dataTable.Rows[0][0].ToString());
                

                if (count > 0) // ����Ѵ���ͬ���û�
                {
                    dbConnector.CloseConnection(); // �ر�����
                    return 2; // �û����Ѵ���
                }
                if (password != confirmPassword)
                {
                    dbConnector.CloseConnection(); // �ر�����
                    return 5;
                }

                dbConnector.ExecuteNonQuery(query2); // ִ�в������������û���¼
                dbConnector.CloseConnection(); // �ر�����
                return 1; // ע��ɹ�
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                dbConnector.CloseConnection();
                return 4; // ��������ʧ��
            }
        }
        else
        {
            return 3; // ���Ӵ���
        }
    }

    // �û���¼
    public int Login(string userId, string password,string identity)
    {
        // ��ѯָ���û����ļ�¼
        var query = $"SELECT * FROM users WHERE user_id = '{userId}' LIMIT 1";

        // ���������ݿ⽨������
        if (dbConnector.OpenConnection() == true)
        {
            // ִ�в�ѯָ���û�����¼�����
            var dataTable = dbConnector.SelectQuery(query);

            // ��ѯ�û�������û����Ƿ���������ݿ���
            if (dataTable.Rows.Count == 1)
            {
                // ��ȡ��ѯ����е��û�����
                string storedPassword = dataTable.Rows[0]["password"].ToString();
                string stroedIdentity = dataTable.Rows[0]["identity"].ToString();
                // �����������������ƥ��
                if (storedPassword == password && stroedIdentity == identity)
                {
                    dbConnector.CloseConnection(); // �ر�����
                    return 1; // ��¼�ɹ�
                }
                else if (storedPassword != password && stroedIdentity == identity)
                {
                    dbConnector.CloseConnection(); // �ر�����
                    return 2; // �������
                }
                else if (storedPassword == password && stroedIdentity != identity)
                {
                    dbConnector.CloseConnection(); // �ر�����
                    return 5;
                }
                else
                {
                    dbConnector.CloseConnection(); // �ر�����
                    return 2;
                }
            }
            else
            {
                dbConnector.CloseConnection(); // �ر�����
                return 3; // �û���������
            }
        }
        else
        {
            return 4; // ���Ӵ���
        }
    }
}
    namespace Static
{
    public class StaticData
    {
        public enum RegisterCode
        {
            RegisterSuccess = 1, // ע��ɹ�
            UsernameDoesExist = 2, // �û����Ѵ���
            ConnectionError = 3, // ���Ӵ���
            InsertDataError = 4, // ��������ʧ��
            PasswordsNotMatch = 5,//�������벻һ��
        };
        public enum LoginCode
        {
            LoginSuccess = 1, // ��¼�ɹ�
            IncorrectPassword = 2, // ���벻��ȷ
            UsernameDoesNotExist = 3, // �û���������
            ConnectionError = 4, //���Ӵ���
            IdentityNotMatch = 5,//��ݲ�ƥ��
        };
    }
}

