using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Utils
{
    public class DBConnector
    {
        private string connectionString; // 存储 MySQL 连接字符串
        private MySqlConnection connection; // 存储 MySQL 连接实例

        // 构造函数，用于初始化 MySQL 连接字符串
        public DBConnector(string server, string database, string uid, string password)
        {
            // 设置 MySQL 连接字符串
            connectionString = $"Server={server};Database={database};Uid={uid};Pwd={password};";
        }

        // 打开 MySQL 连接
        public bool OpenConnection()
        {
            try
            {
                // 创建 MySQL 连接实例
                connection = new MySqlConnection(connectionString);
                // 打开 MySQL 连接
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                // 捕获连接错误并输出错误信息
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        // 关闭 MySQL 连接
        public bool CloseConnection()
        {
            // 判断连接是否为空
            if (connection == null)
            {
                return false;
            }
            try
            {
                // 关闭 MySQL 连接
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                // 捕获关闭连接时的错误并输出错误信息
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 执行查询类 SQL 语句（如 SELECT 语句），并返回查询结果
        /// </summary>
        /// <param name="query">查询语句</param>
        /// <returns>返回查询结果的 DataTable</returns>
        public DataTable SelectQuery(string query)
        {
            // 创建一个空的 DataTable 来存储查询结果
            var dataTable = new DataTable();
            // 创建 MySQL 命令对象并设置查询语句
            var command = new MySqlCommand(query, connection);
            // 执行查询并返回读取器
            var reader = command.ExecuteReader();
            // 将查询结果加载到 DataTable 中
            dataTable.Load(reader);
            // 关闭数据读取器
            reader.Close();
            // 返回查询结果
            return dataTable;
        }

        /// <summary>
        /// 执行非查询 SQL 语句（如 Insert, Update, Delete 等）
        /// </summary>
        /// <param name="query">SQL 语句</param>
        public void ExecuteNonQuery(string query)
        {
            // 创建 MySQL 命令对象并设置 SQL 语句
            var command = new MySqlCommand(query, connection);
            // 执行非查询 SQL 语句
            command.ExecuteNonQuery();
        }
    }
}
