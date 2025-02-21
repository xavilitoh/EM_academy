using EM.Models;

namespace EM.Repositorio;

public interface IRepositorioBase<T> where T : class
{
    Task<T> Save(T modelo);
    Task<int> Update(T modelo);
    Task<T?> Get(int id);
    Task<List<T>> Get();
    Task<List<SelectListItem>> SelectList();
    Task<bool> CambiaEstado(int id, bool nuevoEstado = false);
}