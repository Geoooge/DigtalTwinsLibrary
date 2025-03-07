using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class UserController : MonoBehaviour
{
    private string connectionString = "server=localhost;database=gamedb;user=root;password=123456;";

    // 添加用户
    public void AddUser(string userName, string password, string identity, string email, string phoneNumber)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO users (user_name, password, identity, email, phone_number) VALUES (@userName, @password, @identity, @email, @phoneNumber)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userName", userName);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@identity", identity);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phoneNumber", phoneNumber);
                cmd.ExecuteNonQuery();
            }
        }
        Debug.Log("User added successfully!");
    }

    // 查询所有用户
    public List<Users> GetAllUsers()
    {
        List<Users> users = new List<Users>();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM users";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    users.Add(new Users
                    {
                        userId = reader.GetInt32("user_id"),
                        userName = reader.GetString("user_name"),
                        password = reader.GetString("password"),
                        identity = reader.GetString("identity"),
                        email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                        phoneNumber = reader.IsDBNull(reader.GetOrdinal("phone_number")) ? null : reader.GetString("phone_number")
                    });
                }
            }
        }
        return users;
    }
    public Users GetUserById(int userId)
    {
        Users users = null;
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM users WHERE user_id = @userId";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // 如果查询到了数据
                    {
                        users = new Users
                        {
                            userId = reader.GetInt32("user_id"),
                            userName = reader.GetString("user_name"),
                            password = reader.GetString("password"),
                            identity = reader.GetString("identity"),
                            email = reader.IsDBNull(reader.GetOrdinal("email")) ? null : reader.GetString("email"),
                            phoneNumber = reader.IsDBNull(reader.GetOrdinal("phone_number")) ? null : reader.GetString("phone_number")
                        };
                    }
                }
            }
        }
        return users;
    }

    // 更新用户信息
    public void UpdateUser(int userId, string newUserName, string newPassword, string newIdentity, string newEmail, string newPhoneNumber)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE users SET user_name=@newUserName, password=@newPassword, identity=@newIdentity, email=@newEmail, phone_number=@newPhoneNumber WHERE user_id=@userId";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@newUserName", newUserName);
                cmd.Parameters.AddWithValue("@newPassword", newPassword);
                cmd.Parameters.AddWithValue("@newIdentity", newIdentity);
                cmd.Parameters.AddWithValue("@newEmail", newEmail);
                cmd.Parameters.AddWithValue("@newPhoneNumber", newPhoneNumber);
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
            }
        }
        Debug.Log("User updated successfully!");
    }

    // 删除用户
    public void DeleteUser(int userId)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "DELETE FROM users WHERE user_id=@userId";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.ExecuteNonQuery();
            }
        }
        Debug.Log("User deleted successfully!");
    }
}
