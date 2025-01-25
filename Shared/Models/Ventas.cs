using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class Ventas
{
    [Key]
    public int VentaId {  get; set; }
    public int MetodoPagoId { get; set; }
    public int ClienteId { get; set; }

    [ForeignKey(nameof(ClienteId))]
    public Clientes? Cliente { get; set; }

    [ForeignKey("MetodoPagoId")]
    public MetodosPagos? MetodoPago { get; set; }

    public int Cantidad { get; set; }
    public DateTime Fecha { get; set; }
    public string? Descripcion { get; set; }
    public bool VentaDevuelta { get; set; }
    [ForeignKey("VentaId")]
    public ICollection<VentasDetalle> VentaDetalles { get; set; } = new List<VentasDetalle>();



}
