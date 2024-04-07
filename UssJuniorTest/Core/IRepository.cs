using UssJuniorTest.Core.Models;

namespace UssJuniorTest.Core;

/// <summary>
/// Репозиторий для операций с данными.
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IRepository<T> where T : Model
{
    /// <summary>
    /// Получить объект.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> Get(long id);

    /// <summary>
    /// Получить все объекты.
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<T>> GetAll();
}