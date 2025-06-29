using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.ViewModel.Admin.Subscribes
{
    public class SubscribeVM
    {
        //public string Email { get; set; } = null!;
        //public DateTime SubscribedDate { get; set; }



        public int Id { get; set; }

        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
