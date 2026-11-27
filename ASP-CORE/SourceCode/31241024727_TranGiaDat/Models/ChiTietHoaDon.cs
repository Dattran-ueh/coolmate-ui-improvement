using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatPaper.Models;

public class ChiTietHoaDon
{
    [Key]
    public int MaChiTietHoaDon { get; set; }

    public int MaHoaDon { get; set; }

    [ForeignKey(nameof(MaHoaDon))]
    public HoaDon HoaDonObj { get; set; }

    public int MaProduct { get; set; }

    [ForeignKey(nameof(MaProduct))]
    public Product ProductObj { get; set; }

    public int SoLuong { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal DonGia { get; set; }
}