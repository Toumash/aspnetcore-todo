using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetcore_todo.Models
{
    public class ResetPasswordViewModel
    {
        [DataType(DataType.Password), Required(ErrorMessage = "Old Password Required")]
        public string oldPassword { get; set; }

        [DataType(DataType.Password), Required(ErrorMessage = "New Password Required")]
        public string newPassword { get; set; }

        public string UserId { get; set; }
    }
}
