using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatPaper.Models;

public class HoaDon
{
    [Key]
    public int MaHoaDon { get; set; }

    public IList<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        = new List<ChiTietHoaDon>();

    [Column(TypeName = "decimal(18,2)")]
    public decimal TongTien { get; set; }

    public DateTime NgayLap { get; set; }

    public int? MaKhachHang { get; set; }
}