using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class Proovedores
{
    [Key]
    public int ProovedorId { get; set; }
  
    public string Nombre { get; set; } = string.Empty;

    public string Direccion { get; set; } = string.Empty;
  
    public string? Email { get; set; }

    public string Celular { get; set; } = string.Empty;
}
