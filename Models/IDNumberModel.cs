using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IDNoValidator.Models
{
  public class IDNumberModel
  {
    [Key]
    public int ID { get; set; }

    [Required]
    public string IDNumber { get; set; }

    [Required]
    public bool IsValid { get; set; }

    public string Details { get; set; }
  }
}
