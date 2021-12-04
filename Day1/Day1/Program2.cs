var lines = File.ReadAllLines("input.txt")
    .Select(i => int.Parse(i))
    .ToArray();

var threeSums = Enumerable.Range(0, lines.Length - 2)
    .Select(i => lines[i..(i + 3)].Sum())
    .ToList();

var result = threeSums
    .Where((n, index) => index > 0 && threeSums[index] > threeSums[index - 1])
    .Count();

return; 