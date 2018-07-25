namespace AstralNotes.Domain.Abstractions
{
    /// <summary>
    /// Сервис, генерирующий уникальные картинки
    /// </summary>
    public interface IUniqueImageService
    {
        /// <summary>
        /// Генерирует уникальную картинку
        /// </summary>
        /// <param name="seed">Значение по которому генерируется картинка</param>
        /// <returns>Массив байт, который представляет картинку</returns>
        byte[] Get(string seed);
    }
}
