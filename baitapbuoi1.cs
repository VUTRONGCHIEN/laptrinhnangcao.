using System;
using System.Collections.Generic;

public interface IHinh
{
    double GetDienTich();
    double GetChuVi();
    void Nhap();
    void HienThi();
}

public class HinhTron : IHinh
{
    private double r;
    public double R
    {
        get => r;
        set => r = value > 0 ? value : throw new Exception("Bán kính phải > 0");
    }

    public HinhTron() { }
    public HinhTron(double r) => R = r;

    public double GetDienTich() => Math.PI * r * r;
    public double GetChuVi() => 2 * Math.PI * r;

    public void Nhap()
    {
        Console.Write("Nhập bán kính: ");
        R = double.Parse(Console.ReadLine());
    }

    public void HienThi() => Console.WriteLine($"[Hình Tròn] R: {r:F2} | Chu vi: {GetChuVi():F2} | Diện tích: {GetDienTich():F2}");
}

public class HinhChuNhat : IHinh
{
    private double dai, rong;
    public double Dai { get => dai; set => dai = value > 0 ? value : throw new Exception("Dài phải > 0"); }
    public double Rong { get => rong; set => rong = value > 0 ? value : throw new Exception("Rộng phải > 0"); }

    public HinhChuNhat() { }
    public HinhChuNhat(double d, double r) { Dai = d; Rong = r; }

    public double GetDienTich() => dai * rong;
    public double GetChuVi() => 2 * (dai + rong);

    public void Nhap()
    {
        Console.Write("Nhập chiều dài: "); Dai = double.Parse(Console.ReadLine());
        Console.Write("Nhập chiều rộng: "); Rong = double.Parse(Console.ReadLine());
    }

    public void HienThi() => Console.WriteLine($"[HCN] Dài: {dai:F2}, Rộng: {rong:F2} | Chu vi: {GetChuVi():F2} | Diện tích: {GetDienTich():F2}");
}

public class HinhTamGiac : IHinh
{
    private double a, b, c;
    public double A { get => a; set => a = value > 0 ? value : throw new Exception("Cạnh > 0"); }
    public double B { get => b; set => b = value > 0 ? value : throw new Exception("Cạnh > 0"); }
    public double C { get => c; set => c = value > 0 ? value : throw new Exception("Cạnh > 0"); }

    public HinhTamGiac() { }
    public HinhTamGiac(double a, double b, double c)
    {
        if (!IsTamGiac(a, b, c)) throw new Exception("Không phải tam giác");
        A = a; B = b; C = c;
    }

    public static bool IsTamGiac(double a, double b, double c) => a > 0 && b > 0 && c > 0 && (a + b > c) && (a + c > b) && (b + c > a);

    public double GetChuVi() => a + b + c;
    public double GetDienTich()
    {
        double p = GetChuVi() / 2;
        return Math.Sqrt(p * (p - a) * (p - b) * (p - c));
    }

    public void Nhap()
    {
        Console.Write("Nhập 3 cạnh a, b, c: ");
        string[] s = Console.ReadLine().Split();
        double ta = double.Parse(s[0]), tb = double.Parse(s[1]), tc = double.Parse(s[2]);
        if (!IsTamGiac(ta, tb, tc)) throw new Exception("Không thỏa mãn tam giác!");
        A = ta; B = tb; C = tc;
    }

    public void HienThi() => Console.WriteLine($"[Tam Giác] Cạnh: {a:F2}, {b:F2}, {c:F2} | Chu vi: {GetChuVi():F2} | Diện tích: {GetDienTich():F2}");
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        List<IHinh> ds = new List<IHinh> { new HinhTron(), new HinhChuNhat(), new HinhTamGiac() };

        foreach (var h in ds)
        {
            h.Nhap();
        }

        Console.WriteLine("\n--- KẾT QUẢ ---");
        foreach (var h in ds)
        {
            h.HienThi();
        }
    }
}
