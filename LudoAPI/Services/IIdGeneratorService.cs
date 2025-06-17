namespace LudoAPI.Services
{
    public interface IIdGeneratorService<T>
    {
        int GetNewId(Dictionary<int, T> dictionary);
    }
}
