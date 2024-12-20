﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class Ventas
{
    [Key]
    public int VentaId {  get; set; }
    public int ClienteId { get; set; }
    public int MetodoPagoId { get; set; }

    public int Cantidad { get; set; }
    public DateOnly Fecha { get; set; }
    public string? Descripcion { get; set; }
    public bool VentaDevuelta { get; set; }

}