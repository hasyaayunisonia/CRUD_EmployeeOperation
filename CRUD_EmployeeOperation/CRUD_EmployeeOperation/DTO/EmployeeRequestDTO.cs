using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRUD_EmployeeOperation.DTO
{
    public class EmployeeRequestDTO
    {
        [Required(ErrorMessage = "FullName is required.")]
        [DefaultValue("Zeri")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "BirthDate is required.")]
        [RegularExpression(@"\d{2}-\d{2}-\d{4}", ErrorMessage = "Invalid date format. Please use dd-MM-yyyy.")]
        [DefaultValue("24-11-2002")]
        public string BirthDate { get; set; }
        //format DD-MM-YYYY
    }
}
