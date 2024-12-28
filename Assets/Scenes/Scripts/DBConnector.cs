using System;
using System.Data;
using MySql.Data.MySqlClient;


namespace Utils
{
    public class DBConnector
    {
        private string connectionString; // �洢 MySQL �����ַ���
        private MySqlConnection connection; // �洢 MySQL ����ʵ��

        // ���캯�������� MySQL ���Ӳ���
        public DBConnector(string server, string database, string uid, string password)
        {
            // ���� MySQL �����ַ���
            connectionString = $"Server={server};Database={database};Uid={uid};Pwd={password};";
        }

        // �� MySQL ����
        public bool OpenConnection()
        {
            try
            {
                // ���� MySQL ����ʵ��
                connection = new MySqlConnection(connectionString);
                // �� MySQL ����
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // ������Ӵ�����Ϣ
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // �ر� MySQL ����
        public bool CloseConnection()
        {
            // �ж������Ƿ�Ϊ��
            if (connection == null)
            {
                return false;
            }
            try
            {
                // �ر� MySQL ����
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // ���������Ϣ
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// ��ָ���� MySQL ���ݿⷢ�� SQL ��䲢���ؽ��
        /// </summary>
        /// <param name="query">����һ���ַ�����Ϊ������ʾ��ѯ</param>
        /// <returns></returns>
        public DataTable SelectQuery(string query)
        {
            // ���� empty DataTable���洢���صĽ��
            var dataTable = new DataTable();
            // ����һ����������Ա������ݿ���ִ�в�ѯ
            var command = new MySqlCommand(query, connection);
            // ִ�в�ѯ�����ؽ��
            var reader = command.ExecuteReader();
            // ����ѯ������ص� DataTable ��
            dataTable.Load(reader);
            // �ر����ݶ�ȡ������
            reader.Close();
            // ���ز�ѯ���
            return dataTable;
        }

        /// <summary>
        /// ִ�зǲ�ѯ��䣨�� Insert, Update, Delete��
        /// </summary>
        /// <param name="query">�ǲ�ѯ���</param>
        public void ExecuteNonQuery(string query)
        {
            // �����������
            var command = new MySqlCommand(query, connection);
            // ִ�зǲ�ѯ���
            command.ExecuteNonQuery();
        }
    }
}
