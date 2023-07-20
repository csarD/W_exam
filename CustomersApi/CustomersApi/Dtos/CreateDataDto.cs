using System.ComponentModel.DataAnnotations;

namespace CustomersApi.Dtos
{
    public class CreateDataDto
    {
        [Required (ErrorMessage ="La data debe ser especificada")]
        public int[] Data { get; set; }
        

    }
}
