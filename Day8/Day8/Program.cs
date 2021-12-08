namespace Day8;

static class Day8
{
    public static void Main()
    {
        var lines = File.ReadAllLines("input.txt");

        var tokenziedLines = lines.Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray().Where(x => x != "|").Distinct().ToArray()).ToList();
        var groups = tokenziedLines.Select(line => line.GroupBy(x => x.Length).Select(x => new { Key = x.Key, Count = x.Count() }).ToList());
        var numbersUniqueSegments = groups.SelectMany(x => x).Where(x => x.Count == 1).Select(x => x.Key).Distinct().ToList();  // 2, 3, 4, 7

        var outputs = lines.Select(line => line.Split('|').Last().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray()).ToList();

        // Task 1 result:
        var digitsUsingUniqueNumberOfSequences = outputs.SelectMany(x => x).Where(x => numbersUniqueSegments.Contains(x.Length)).Count(); // 26/521

        // Task 2 begins here:

        var totalValue = 0;
        foreach (var line in lines)
        {
            var tokenizedline = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Where(x => x != "|").Distinct().Select(s => AlphabetizeWord(s)).Distinct().OrderBy(x => x).ToArray();
            var dict = tokenizedline.ToDictionary(x => x, x => (int?)null);
            AssignValues(dict);

            var lastWords = line.Split('|').Last().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            var values = lastWords.Select(word => dict[AlphabetizeWord(word)].Value).ToArray();
            var word = string.Join("", values);

            totalValue += int.Parse(word);
        }

        return;
    }

    static string AlphabetizeWord(string s) => string.Join("", s.ToArray().OrderBy(c => c));

    static void AssignValues(Dictionary<string, int?> dict)
    {
        // 1

        var key = dict.FirstOrDefault(d => d.Key.Length == 2).Key;
        dict[key] = 1;

        // 7 

        key = dict.FirstOrDefault(d => d.Key.Length == 3).Key;
        dict[key] = 7;

        // 4

        key = dict.FirstOrDefault(d => d.Key.Length == 4).Key;
        dict[key] = 4;

        // 8 

        key = dict.FirstOrDefault(d => d.Key.Length == 7).Key;
        dict[key] = 8;

        //2,3,5

        var keys235 = dict.Where(d => d.Key.Length == 5).Select(x => x.Key).ToArray();
        var uniqueLetters = keys235.SelectMany(key => key.ToArray()).GroupBy(x => x).Where(x => x.Count() == 1).Select(x => x.Key).ToList();
        var key3 = keys235.FirstOrDefault(key => !key.Any(c => uniqueLetters.Any(ul => c == ul)));

        dict[key3] = 3;

        keys235 = keys235.Where(key => key != key3).ToArray();

        var key5 = keys235.FirstOrDefault(key => key.Where(c => dict.First(d => d.Value == 4).Key.Contains(c)).Count() == 3);
        dict[key5] = 5;
        keys235 = keys235.Where(key => key != key5).ToArray();

        dict[keys235.First()] = 2;


        // 0,6,9

        var keys069 = dict.Where(d => d.Key.Length == 6).Select(x => x.Key).ToArray();

        var key9 = keys069.FirstOrDefault(k => key5.All(k5 => k.Contains(k5)) && dict.FirstOrDefault(d => d.Key.Length == 3).Key.All(k7 => k.Contains(k7)));
        dict[key9] = 9;

        var rest = dict.Where(d => d.Value is null).Select(d => d.Key).ToList();

        var key0 = rest.FirstOrDefault(k => dict.FirstOrDefault(d => d.Key.Length == 3).Key.All(k7 => k.Contains(k7)));

        dict[key0] = 0;

        var key6 = rest.Where(key => key != key0).FirstOrDefault();

        dict[key6] = 6;
    }
}
