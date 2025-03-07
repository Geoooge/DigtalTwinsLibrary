using System.Collections.Generic;
using UnityEngine;
using MySql.Data.MySqlClient;

public class BookController : MonoBehaviour
{
    private string connectionString = "server=localhost;database=gamedb;user=root;password=123456;";

    // 添加图书
    public void AddBook(string title, string author, int categoryId, int quantity)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "INSERT INTO books (title, author, category_id, quantity) VALUES (@title, @author, @categoryId, @quantity)";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@title", title);
                cmd.Parameters.AddWithValue("@author", author);
                cmd.Parameters.AddWithValue("@categoryId", categoryId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.ExecuteNonQuery();
            }
        }
        Debug.Log("Book added successfully!");
    }

    // 查询所有图书
    public List<Book> GetAllBooks()
    {
        List<Book> books = new List<Book>();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM books";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    books.Add(new Book
                    {
                        bookId = reader.GetInt32("book_id"),
                        title = reader.GetString("title"),
                        author = reader.GetString("author"),
                        categoryId = reader.GetInt32("category_id"),
                        availableCopies = reader.GetInt32("available_copies")
                    });
                }
            }
        }
        return books;
    }

    // 根据图书ID查询单本书
    public Book GetBookById(int bookId)
    {
        Book book = null;
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT * FROM books WHERE book_id = @bookId";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@bookId", bookId);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read()) // 如果查询到了数据
                    {
                        book = new Book
                        {
                            bookId = reader.GetInt32("book_id"),
                            title = reader.GetString("title"),
                            author = reader.GetString("author"),
                            categoryId = reader.GetInt32("category_id"),
                            availableCopies = reader.GetInt32("available_copies")
                        };
                    }
                }
            }
        }
        return book;
    }

    // 更新图书信息
    public void UpdateBook(int bookId, string newTitle, string newAuthor, int newCategoryId, int newQuantity)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "UPDATE books SET title=@newTitle, author=@newAuthor, category_id=@newCategoryId, quantity=@newQuantity WHERE book_id=@bookId";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@newTitle", newTitle);
                cmd.Parameters.AddWithValue("@newAuthor", newAuthor);
                cmd.Parameters.AddWithValue("@newCategoryId", newCategoryId);
                cmd.Parameters.AddWithValue("@newQuantity", newQuantity);
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.ExecuteNonQuery();
            }
        }
        Debug.Log("Book updated successfully!");
    }

    // 删除图书
    public void DeleteBook(int bookId)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            string query = "DELETE FROM books WHERE book_id=@bookId";
            using (MySqlCommand cmd = new MySqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@bookId", bookId);
                cmd.ExecuteNonQuery();
            }
        }
        Debug.Log("Book deleted successfully!");
    }
}
