namespace Day14;

public static class DictionaryExtensions
{
    public static void AddOrIncrement<TKey>(this Dictionary<TKey, long> dict, TKey key, long value)
    {
        if (dict.ContainsKey(key))
        {
            dict[key] += value;
        }
        else
        {
            dict.Add(key, value);
        }
    }
}
