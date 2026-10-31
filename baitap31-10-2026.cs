using System;
using System.Data;
using System.Data.SqlClient;

public class BookDAL
{
    private string connectionString = "Server=YOUR_SERVER;Database=YOUR_DB;Integrated Security=True;";

    // Lấy danh sách tất cả các đầu sách
    public DataTable GetAllBooks()
    {
        DataTable dt = new DataTable();
        string query = "SELECT MaSach, TenSach, TheLoai, TacGia, NhaXuatBan, GiaSach, NamXuatBan, SoBanSao FROM Book";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }
        return dt;
    }

    // Thêm đầu sách mới ("Thêm đầu sách")
    public bool InsertBook(string maSach, string tenSach, string tacGia, string nhaXuatBan, string theLoai, decimal giaSach, int namXuatBan, int soBanSao)
    {
        string query = "INSERT INTO Book (MaSach, TenSach, TacGia, NhaXuatBan, TheLoai, GiaSach, NamXuatBan, SoBanSao) " +
                       "VALUES (@MaSach, @TenSach, @TacGia, @NhaXuatBan, @TheLoai, @GiaSach, @NamXuatBan, @SoBanSao)";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                cmd.Parameters.AddWithValue("@TenSach", tenSach);
                cmd.Parameters.AddWithValue("@TacGia", tacGia);
                cmd.Parameters.AddWithValue("@NhaXuatBan", nhaXuatBan);
                cmd.Parameters.AddWithValue("@TheLoai", theLoai);
                cmd.Parameters.AddWithValue("@GiaSach", giaSach);
                cmd.Parameters.AddWithValue("@NamXuatBan", namXuatBan);
                cmd.Parameters.AddWithValue("@SoBanSao", soBanSao);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    // Cập nhật thông tin đầu sách ("Lưu chỉnh sửa")
    public bool UpdateBook(string maSach, string tenSach, string tacGia, string nhaXuatBan, string theLoai, decimal giaSach, int namXuatBan)
    {
        string query = "UPDATE Book SET TenSach = @TenSach, TacGia = @TacGia, NhaXuatBan = @NhaXuatBan, " +
                       "TheLoai = @TheLoai, GiaSach = @GiaSach, NamXuatBan = @NamXuatBan WHERE MaSach = @MaSach";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                cmd.Parameters.AddWithValue("@TenSach", tenSach);
                cmd.Parameters.AddWithValue("@TacGia", tacGia);
                cmd.Parameters.AddWithValue("@NhaXuatBan", nhaXuatBan);
                cmd.Parameters.AddWithValue("@TheLoai", theLoai);
                cmd.Parameters.AddWithValue("@GiaSach", giaSach);
                cmd.Parameters.AddWithValue("@NamXuatBan", namXuatBan);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    // Xóa đầu sách ("Xóa đầu sách")
    public bool DeleteBook(string maSach)
    {
        string query = "DELETE FROM Book WHERE MaSach = @MaSach";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    // Tìm kiếm đầu sách theo từ khóa và thể loại ("Tìm kiếm")
    public DataTable SearchBooks(string keyword, string category)
    {
        DataTable dt = new DataTable();
        string query = "SELECT MaSach, TenSach, TheLoai, TacGia, NhaXuatBan, GiaSach, NamXuatBan, SoBanSao " +
                       "FROM Book WHERE (MaSach LIKE @Keyword OR TenSach LIKE @Keyword OR TacGia LIKE @Keyword) ";

        if (!string.IsNullOrEmpty(category) && category != "Tất cả thể loại")
        {
            query += "AND TheLoai = @Category";
        }

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                if (!string.IsNullOrEmpty(category) && category != "Tất cả thể loại")
                {
                    cmd.Parameters.AddWithValue("@Category", category);
                }

                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }
        return dt;
    }
}
