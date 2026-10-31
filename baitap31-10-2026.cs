using System;
using System.Data;
using System.Data.SqlClient;

public static class DatabaseHelper
{
    public static string ConnectionString = "Server=.\\SQLEXPRESS;Database=QuanLyThuVien;Integrated Security=True;";

    public static SqlConnection GetConnection()
    {
        return new SqlConnection(ConnectionString);
    }
}

public class CategoryDAL
{
    public DataTable GetAllCategories()
    {
        DataTable dt = new DataTable();
        string query = "SELECT MaTheLoai, TenTheLoai FROM Category";
        
        using (SqlConnection conn = DatabaseHelper.GetConnection())
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
}

public class BookDAL
{
    public DataTable GetAllBooks()
    {
        DataTable dt = new DataTable();
        string query = "SELECT MaSach, TenSach, TheLoai, TacGia, NhaXuatBan, GiaSach, NamXuatBan, SoBanSao FROM Book";

        using (SqlConnection conn = DatabaseHelper.GetConnection())
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

    public bool InsertBook(string maSach, string tenSach, string tacGia, string nhaXuatBan, string theLoai, decimal giaSach, int namXuatBan, int soBanSao)
    {
        string query = "INSERT INTO Book (MaSach, TenSach, TacGia, NhaXuatBan, TheLoai, GiaSach, NamXuatBan, SoBanSao) " +
                       "VALUES (@MaSach, @TenSach, @TacGia, @NhaXuatBan, @TheLoai, @GiaSach, @NamXuatBan, @SoBanSao)";

        using (SqlConnection conn = DatabaseHelper.GetConnection())
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

    public bool UpdateBook(string maSach, string tenSach, string tacGia, string nhaXuatBan, string theLoai, decimal giaSach, int namXuatBan)
    {
        string query = "UPDATE Book SET TenSach = @TenSach, TacGia = @TacGia, NhaXuatBan = @NhaXuatBan, " +
                       "TheLoai = @TheLoai, GiaSach = @GiaSach, NamXuatBan = @NamXuatBan WHERE MaSach = @MaSach";

        using (SqlConnection conn = DatabaseHelper.GetConnection())
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

    public bool DeleteBook(string maSach)
    {
        string query = "DELETE FROM Book WHERE MaSach = @MaSach";

        using (SqlConnection conn = DatabaseHelper.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    public DataTable SearchBooks(string keyword, string category)
    {
        DataTable dt = new DataTable();
        string query = "SELECT MaSach, TenSach, TheLoai, TacGia, NhaXuatBan, GiaSach, NamXuatBan, SoBanSao " +
                       "FROM Book WHERE (MaSach LIKE @Keyword OR TenSach LIKE @Keyword OR TacGia LIKE @Keyword) ";

        if (!string.IsNullOrEmpty(category) && category != "-- Tất cả thể loại --")
        {
            query += "AND TheLoai = @Category";
        }

        using (SqlConnection conn = DatabaseHelper.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");
                if (!string.IsNullOrEmpty(category) && category != "-- Tất cả thể loại --")
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

public class BookCopyDAL
{
    public DataTable GetCopiesByBookId(string maSach)
    {
        DataTable dt = new DataTable();
        string query = "SELECT MaBanSao, TrangThai, TinhTrangVatLy FROM BookCopy WHERE MaSach = @MaSach";

        using (SqlConnection conn = DatabaseHelper.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
            }
        }
        return dt;
    }

    public bool InsertBookCopy(string maBanSao, string maSach, string trangThai, string tinhTrangVatLy)
    {
        string query = "INSERT INTO BookCopy (MaBanSao, MaSach, TrangThai, TinhTrangVatLy) " +
                       "VALUES (@MaBanSao, @MaSach, @TrangThai, @TinhTrangVatLy)";

        using (SqlConnection conn = DatabaseHelper.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaBanSao", maBanSao);
                cmd.Parameters.AddWithValue("@MaSach", maSach);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                cmd.Parameters.AddWithValue("@TinhTrangVatLy", tinhTrangVatLy);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    public bool UpdateBookCopy(string maBanSao, string trangThai, string tinhTrangVatLy)
    {
        string query = "UPDATE BookCopy SET TrangThai = @TrangThai, TinhTrangVatLy = @TinhTrangVatLy WHERE MaBanSao = @MaBanSao";

        using (SqlConnection conn = DatabaseHelper.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaBanSao", maBanSao);
                cmd.Parameters.AddWithValue("@TrangThai", trangThai);
                cmd.Parameters.AddWithValue("@TinhTrangVatLy", tinhTrangVatLy);

                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }

    public bool DeleteBookCopy(string maBanSao)
    {
        string query = "DELETE FROM BookCopy WHERE MaBanSao = @MaBanSao";

        using (SqlConnection conn = DatabaseHelper.GetConnection())
        {
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@MaBanSao", maBanSao);
                conn.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
