using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebsiteSellingSuitcases.Models
{
    [MetadataTypeAttribute(typeof(tDanhMucSPMetaData))]
    public partial class tDanhMucSP
    {
        internal sealed class tDanhMucSPMetaData
        {
            [Display(Name = "Mã sản phẩm")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            public string MaSP { get; set; }
            [Display(Name = "Tên sản phẩm")]
            [Required(ErrorMessage = "Vui lòng nhập dữ liệu cho trường này")]
            public string TenSP { get; set; }
            [Display(Name = "Ngăn Laptop")]
            public string NganLapTop { get; set; }

            [Display(Name = "Màu sắc")]
            public string MauSac { get; set; }

            [Display(Name = "Cân nặng")]
            public Nullable<double> CanNang { get; set; }
            [Display(Name = "Độ nới")]
            public Nullable<double> DoNoi { get; set; }
            [Display(Name = "Thời gian bảo hành")]
            public Nullable<double> ThoiGianBaoHanh { get; set; }
            [Display(Name = "Giới thiệu sản phẩm")]
            public string GioiThieuSP { get; set; }
            [Display(Name = "Giá Bán")]
            public Nullable<double> GiaBan { get; set; }
            [Display(Name = "Chiết khấu")]
            public Nullable<double> ChietKhau { get; set; }
            [Display(Name = "Ảnh sản phẩm")]
            public string Anh { get; set; }
      
        }
    }
}