namespace Day6;
public static class Day6
{
    public static void Main()
    {
        var dict = File.ReadAllText("input.txt")
            .Split(',')
            .Select(x => int.Parse(x))
            .GroupBy(x => x).ToDictionary(x => x.Key, x => (long)x.Count());

        for (int i = 0; i < 256; i++)
        {
            dict = AgeDictionary(dict);
        }

        var sum = dict.GetTotalPop(); // Answer
    }

    private static Dictionary<int, long> AgeDictionary(Dictionary<int, long> dict)
    {
        var newDict = new Dictionary<int, long>();

        for (int i = 0; i < 8; i++)
        {
            newDict[i] = dict.GetValueOrZero(i + 1);
        }

        newDict[6] = newDict.GetValueOrZero(6) + dict.GetValueOrZero(0);
        newDict[8] = dict.GetValueOrZero(0);

        return newDict;
    }

    public static long GetValueOrZero(this Dictionary<int, long> dict, int key)
    {
        if (dict.ContainsKey(key))
        {
            return dict[key];
        }

        return 0;
    }

    public static long GetTotalPop(this Dictionary<int, long> dict)
    {
        return Enumerable.Range(0, 9).Select(i => dict.GetValueOrZero(i)).Sum();
    }
}