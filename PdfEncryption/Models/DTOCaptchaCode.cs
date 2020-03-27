using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PdfEncryption.Models
{
    public class DTOCaptchaCode
    {
        [Required]
        [StringLength(4)]
        public string CaptchaCode { get; set; }
    }
}
