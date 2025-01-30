using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLlavesWebAPI.Abstractions.Interfaces;

public interface ICuadresService
{
    public Task<List<Cuadres>> GetAsync();
    public Task<bool> AddAsync(Cuadres cuadres);
    public Task<bool> PutAsync(Cuadres cuadres);
    public Task<Cuadres?> DeleteAsync(int cuadresId);
    public Task<Cuadres> GetByIdAsync(int cuadresId);
}
