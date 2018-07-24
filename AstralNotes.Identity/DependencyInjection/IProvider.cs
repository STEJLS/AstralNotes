namespace AstralNotes.Identity.DependencyInjection
{
    /// <summary>
    /// Провадйер объектов
    /// </summary>
    /// <typeparam name="T"> Тип объекта </typeparam>
    public interface IProvider<out T>
    {
        /// <summary>
        /// Получение объекта
        /// </summary>
        /// <returns> Объект </returns>
        T Get();
    }
}