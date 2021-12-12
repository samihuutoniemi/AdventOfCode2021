using System.Diagnostics;

namespace Day11;

public class Day11
{
    static void Main()
    {
        var input = File.ReadAllLines("input.txt")
            .Select(line => line.ToArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

        var grid = new Octopus[10, 10];

        for (int y = 0; y < input.Length; y++)
        {
            for (int x = 0; x < input[0].Length; x++)
            {
                grid[x, y] = new Octopus(x, y, input[y][x]);
            }
        }

        for (int i = 0; i < 2000; i++)
        {
            DoStep(grid);

            var isSimulFlash = true;
            foreach (var octopus in grid)
            {
                if (octopus.Value > 0)
                {
                    isSimulFlash = false;
                    break;
                }
            }

            if (isSimulFlash)
            {
                var simulFlashStep = i + 1; // Task 2 result
            }

            foreach (var octopus in grid)
            {
                octopus.HasFlashed = false;
            }
        }

        int totalFlashes = 0;
        foreach (var octopus in grid)
        {
            totalFlashes += octopus.NumFlashes; // Task 1 result with iMax = 100
        }

        return;
    }

    static void DoStep(Octopus[,] grid)
    {
        foreach (var o in grid)
        {
            o.Progress(grid);
        }
    }

    public static void Draw(Octopus[,] grid)
    {
        for (int y = 0; y < grid.GetLength(0); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                Debug.Write(grid[x, y].Value);
            }

            Debug.WriteLine("");
        }
    }
}

public record Octopus
{
    public int X { get; init; }
    public int Y { get; init; }
    public int Value { get; set; }
    public bool HasFlashed { get; set; }
    public int NumFlashes { get; set; }

    public Octopus(int x, int y, int value)
    {
        X = x;
        Y = y;
        Value = value;
    }

    public void Progress(Octopus[,] grid)
    {
        if (!HasFlashed)
        {
            Value++;

            if (Value > 9)
            {
                Flash(grid);
            }
        }
    }

    public void Flash(Octopus[,] grid)
    {
        Value = 0;
        HasFlashed = true;
        NumFlashes++;

        var adjacents = GetAdjacent(grid, this);

        foreach (var adjacent in adjacents)
        {
            adjacent.Progress(grid);
        }
    }

    static Octopus[] GetAdjacent(Octopus[,] grid, Octopus octopus)
    {
        var test = grid.Where(o => Distance(octopus, o) < 1.5).Where(o => o != octopus).ToArray();
        return test;
    }

    public static double Distance(Octopus a, Octopus b)
    {
        var x = Math.Abs(a.X - b.X);
        var y = Math.Abs(a.Y - b.Y);

        var result = Math.Sqrt(x * x + y * y);
        return result;
    }
};

public static class LinqExtensions
{
    public static IEnumerable<T> Where<T>(this T[,] source, Func<T, bool> predicate)
    {
        if (source == null) throw new ArgumentNullException("source");
        if (predicate == null) throw new ArgumentNullException("predicate");
        return WhereImpl(source, predicate);
    }

    private static IEnumerable<T> WhereImpl<T>(this T[,] source, Func<T, bool> predicate)
    {
        for (int i = 0; i < source.GetLength(0); ++i)
        {
            T[] values = new T[source.GetLength(1)];
            for (int j = 0; j < values.Length; ++j)
            {
                if (predicate(source[i, j]))
                {
                    yield return source[i, j];
                }
            }
        }
    }
}