namespace LudoAPI.Services
{
    public class IdGeneratorService<T> : IIdGeneratorService<T>
    {
        public int GetNewId(Dictionary<int, T> dictionary)
        {
            if (dictionary.Count == 0)
            {
                return 1;
            }

            return dictionary.Keys.Max() + 1;
        }
    }
}
