namespace Day9;

record Coord(int x, int y);

class Day9
{
    static void Main()
    {
        // Task 1:

        var heightmap = File.ReadAllLines("input.txt")
            .Select(line => line.ToArray().Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

        var adjacents = new int[heightmap[0].Length, heightmap.Length][];

        for (int y = 0; y < heightmap.Length; y++)
        {
            for (int x = 0; x < heightmap[0].Length; x++)
            {
                adjacents[x, y] = GetAdjacents(heightmap, new Coord(x, y));
            }
        }

        var riskLevelSum = 0;

        for (int y = 0; y < heightmap.Length; y++)
        {
            for (int x = 0; x < heightmap[0].Length; x++)
            {
                var isLowPoint = IsLowPoint(heightmap, adjacents[x, y], x, y);

                if (isLowPoint)
                {
                    var riskLevel = heightmap[y][x] + 1;
                    riskLevelSum += riskLevel;
                }
            }
        }

        // Task 2:

        var basins = new List<Coord[]>();

        for (int y = 0; y < heightmap.Length; y++)
        {
            for (int x = 0; x < heightmap[0].Length; x++)
            {
                if (basins.Any(b => b.Any(c => c == new Coord(x, y))))
                {
                    continue;
                }

                var basin = GetBasin(heightmap, x, y);

                if (basin != null && !basins.Any(b => b.Any(coord => coord == basin.First())))
                {
                    basins.Add(basin);
                }
            }
        }

        var threeLargestBasins = basins.OrderByDescending(b => b.Length).ToArray();
        var multipliedSizes = threeLargestBasins[0].Length * threeLargestBasins[1].Length * threeLargestBasins[2].Length;

        return;
    }

    private static int[] GetAdjacents(int[][] heightmap, Coord c)
    {
        var result = new List<int>();

        // Above
        if (c.y > 0)
        {
            result.Add(heightmap[c.y - 1][c.x]);
        }

        // Right
        if (c.x < heightmap[0].Length - 1)
        {
            result.Add(heightmap[c.y][c.x + 1]);
        }

        // Below
        if (c.y < heightmap.Length - 1)
        {
            result.Add(heightmap[c.y + 1][c.x]);
        }

        // Left
        if (c.x > 0)
        {
            result.Add(heightmap[c.y][c.x - 1]);
        }

        return result.ToArray();
    }

    private static Coord[] GetAdjacentCoordsNotNine(int[][] heightmap, Coord c)
    {
        var result = new List<Coord>();

        // Above
        if (c.y > 0)
        {
            var height = heightmap[c.y - 1][c.x];
            if (height < 9)
            {
                result.Add(new Coord(c.x, c.y - 1));
            }
        }

        // Right
        if (c.x < heightmap[0].Length - 1)
        {
            var height = heightmap[c.y][c.x + 1];
            if (height < 9)
            {
                result.Add(new Coord(c.x + 1, c.y));
            }
        }

        // Below
        if (c.y < heightmap.Length - 1)
        {
            var height = heightmap[c.y + 1][c.x];
            if (height < 9)
            {
                result.Add(new Coord(c.x, c.y + 1));
            }
        }

        // Left
        if (c.x > 0)
        {
            var height = heightmap[c.y][c.x - 1];
            if (height < 9)
            {
                result.Add(new Coord(c.x - 1, c.y));
            }
        }

        return result.ToArray();
    }

    private static bool IsLowPoint(int[][] heightmap, int[] adjacents, int x, int y)
    {
        var height = heightmap[y][x];

        var isLowPoint = adjacents.All(i => i > height);

        return isLowPoint;
    }

    private static Coord[]? GetBasin(int[][] heightmap, int x, int y)
    {
        var result = new List<Coord>();

        var height = heightmap[y][x];
        if (height == 9)
        {
            return null;
        }

        result.Add(new Coord(x, y));
        var missingAdjacents = new List<Coord> { new Coord(x, y) };

        while (missingAdjacents.Any())
        {
            var allAdjacents = result.Select(c => GetAdjacentCoordsNotNine(heightmap, c)).SelectMany(x => x).Distinct().ToArray();
            missingAdjacents = allAdjacents.Except(result).ToList();

            result.AddRange(missingAdjacents);
        }

        return result.ToArray();
    }
}
