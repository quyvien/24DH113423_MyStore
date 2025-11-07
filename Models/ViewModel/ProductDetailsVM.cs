using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _24DH113423_MyStore.Models.ViewModel
{
    public class ProductDetailsVM
    {
        //Sản phẩm hiện tại
    public Product Product { get; set; }

    //Số lượng người dùng chọn
    public int quantity { get; set; } = 1;

    //Giá trị tạm tính = quantity * ProductPrice
    public decimal estimatedValue { get; set; }

    //Search
    public string SearchTerm { get; set; }

    //Phân trang
    public int PageNumber { get; set; }
    public int PageSize { get; set; } = 3;

    //Danh sách 8 sản phẩm cùng danh mục
    public IPagedList<Product> RelatedProducts { get; set; }

     //Danh sách 8 sản phẩm bán chạy nhất cùng danh mục (TopProduct)
    public IPagedList<Product> TopProducts { get; set; }
    }
}