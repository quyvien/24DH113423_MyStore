using System.ComponentModel.DataAnnotations;

namespace _24DH113423_MyStore.Models.ViewModel
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Tên đăng nhập là bắt buộc.")]
        [Display(Name = "Tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu là bắt buộc.")]
        [DataType(DataType.Password)]
        [Display(Name = "Mật khẩu")]
        [StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự.", MinimumLength = 6)]  // Đảm bảo độ dài mật khẩu
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Xác nhận mật khẩu")]
        [Compare("Password", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Tên khách hàng là bắt buộc.")]
        [Display(Name = "Họ tên")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không được quá 100 ký tự.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [Display(Name = "Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(15, ErrorMessage = "Số điện thoại không được quá 15 ký tự.")]
        public string CustomerPhone { get; set; }

        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "Địa chỉ email không được quá 100 ký tự.")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [Display(Name = "Địa chỉ")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được quá 200 ký tự.")]
        public string CustomerAddress { get; set; }
    }
}
