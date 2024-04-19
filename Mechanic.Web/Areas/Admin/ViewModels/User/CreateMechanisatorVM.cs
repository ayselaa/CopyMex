using System.ComponentModel.DataAnnotations;

namespace Mechanic.Web.Areas.Admin.ViewModels.User
{
    public class CreateMechanisatorVM
    {
        [Required(ErrorMessage = "Sürücülük vəsiqənizi qeyd edin")]
        public string DriverLicense { get; set; }
        [Required(ErrorMessage = "Telefon nömrəsini qeyd edin")]
        [MaxLength(12, ErrorMessage = "Telefon nömrəsinin formatını düzgün daxil edin.")]
        [MinLength(12, ErrorMessage = "Telefon nömrəsinin formatını düzgün daxil edin.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "FIN qeyd edin")]
        [MaxLength(7, ErrorMessage = "FIN  7 xarakterdən ibarət olmalıdır.")]
        [MinLength(7, ErrorMessage = "FIN  7 xarakterdən ibarət olmalıdır.")]
        public string FIN { get; set; }

        [Required(ErrorMessage = "Kategoriyanı seçin")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Verilmə tarixini seçin")]
        public DateTime IssueDate { get; set; }
        [Required(ErrorMessage = "Bitmə tarixini seçin")]
        public DateTime ExpirationDate { get; set; }
    }
}
