using System.Diagnostics;

namespace Day13;

public record Coord(int x, int y);

public class Day13
{
    public static void Main()
    {
        var lines = File.ReadAllLines("input.txt").ToList();
        var foldLines = lines.Where(line => line.StartsWith("fold")).ToList();
        lines.RemoveAll(line => line.StartsWith("fold"));
        lines.RemoveAll(line => string.IsNullOrEmpty(line));

        var coords = lines.Select(line => line.Split(',').Select(token => int.Parse(token)).ToArray()).Select(line => new Coord(line[0], line[1])).ToList();

        foreach (var foldLine in foldLines)
        {
            var coord = int.Parse(foldLine.Split('=').Last());
            var axis = foldLine.Split('=').First().Last();

            if (axis == 'x')
            {
                coords = FoldX(coords, coord);
            }
            else
            {
                coords = FoldY(coords, coord);
            }
        }

        // Task 1 result is the number of coords after one execution of the foreach loop

        Draw(coords);   // Task 2 result

        return;
    }

    static List<Coord> FoldY(List<Coord> coords, int foldY)
    {
        var belowCoords = coords.Where(coord => coord.y > foldY).ToList();

        foreach (var coord in belowCoords)
        {
            var distance = coord.y - foldY;

            coords.Remove(coord);
            coords.Add(new Coord(coord.x, coord.y - 2 * distance));
        }

        return coords.Distinct().ToList();
    }

    static List<Coord> FoldX(List<Coord> coords, int foldX)
    {
        var rightCoords = coords.Where(coord => coord.x > foldX).ToList();

        foreach (var coord in rightCoords)
        {
            var distance = coord.x - foldX;

            coords.Remove(coord);
            coords.Add(new Coord(coord.x - 2 * distance, coord.y));
        }

        return coords.Distinct().ToList();
    }

    static void Draw(List<Coord> coords)
    {
        for (int y = 0; y <= coords.Select(c => c.y).Max(); y++)
        {
            for (int x = 0; x <= coords.Select(c => c.x).Max(); x++)
            {
                var character = coords.Any(c => c == new Coord(x, y)) ? "#" : ".";
                Debug.Write(character);
            }

            Debug.WriteLine("");
        }
    }
}