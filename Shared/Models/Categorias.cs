﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Models;

public class Categorias
{
    [Key]
    public int CategoriaId { get; set; }
    public string? Nombre {  get; set; }
}
