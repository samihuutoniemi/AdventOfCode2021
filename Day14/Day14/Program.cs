namespace Day14;

public class Day14
{
    public static void Main()
    {
        var lines = File.ReadAllLines("input.txt");
        var template = lines.First();
        var rules = lines.Skip(2).Select(line => line.Split(new[] { ' ', '-', '>' }, StringSplitOptions.RemoveEmptyEntries).ToArray()).ToDictionary(x => x[0], x => x[1]);

        var initialPairs = template.ToArray().Take(template.Length - 1).Select((c, i) => $"{c}{template[i + 1]}").Select(s => new Pair(s)).ToArray();

        var dict = initialPairs
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => (long)x.Count());

        for (int i = 0; i < 40; i++)
        {
            var newDict = new Dictionary<Pair, long>();

            foreach (var item in dict)
            {
                var pair = item.Key;
                var insert = rules[pair.String];
                var child1 = new Pair($"{pair.Char1}{insert}");
                var child2 = new Pair($"{insert}{pair.Char2}");

                newDict.AddOrIncrement(child1, item.Value);
                newDict.AddOrIncrement(child2, item.Value);
            }

            dict = newDict;
        }

        var distinctLetters = dict.Select(d => d.Key.Char1).Distinct().ToArray();
        var sums = distinctLetters.Select(dl => new { dl, Sum = dict.Where(d => d.Key.Char1 == dl).Select(d => d.Value).Sum() }).OrderBy(x => x.Sum).ToArray();

        var result = sums.Last().Sum - sums.First().Sum - 1;
    }
}

public record Pair
{
    public string String { get; set; }
    public char Char1 => String[0];
    public char Char2 => String[1];

    public Pair(string pair)
    {
        String = pair;
    }
}
