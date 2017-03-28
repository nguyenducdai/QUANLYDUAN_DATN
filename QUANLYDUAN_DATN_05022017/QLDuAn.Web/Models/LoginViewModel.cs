using System.ComponentModel.DataAnnotations;

namespace QLDuAn.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "vui lòng nhập tên tài khoản")]
        public string userName { set; get; }

        [Required(ErrorMessage = "vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string password { set; get; }

        public bool remember { set; get; }
    }
}