namespace Day5
{
    public static class Day5
    {
        public static void Main()
        {
            static Line ParseStringToLine(string s, int i)
            {
                var pointData = s.Split(new[] { ' ', '-', '>', ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.Parse(x)).ToArray();

                return new Line(new Point(pointData[0], pointData[1]), new Point(pointData[2], pointData[3]));
            }

            var task1 = File.ReadAllLines("input.txt")
                .Select(ParseStringToLine)
                .Where(line => line.IsHorizontal || line.IsVertical)
                .SelectMany(line => line.AllPoints)
                .GroupBy(point => (point.X, point.Y))
                .Select(grp => new { Point = grp.Key, Amount = grp.Count() })
                .Where(grp => grp.Amount >= 2)
                .ToList();  // 7674

            var task2 = File.ReadAllLines("input.txt")
                .Select(ParseStringToLine)
                .SelectMany(line => line.AllPoints)
                .OrderBy(p => p.X).ThenBy(p => p.Y)
                .GroupBy(point => (point.X, point.Y))
                .Select(grp => new { Point = grp.Key, Amount = grp.Count() })
                .Where(grp => grp.Amount >= 2)
                .ToList();  // 20898

            return;

        }
    }

    public record Point(int X, int Y);

    public class Line
    {
        public Point P1 { get; set; }
        public Point P2 { get; set; }

        public Line(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public bool IsHorizontal => P1.Y == P2.Y;
        public bool IsVertical => P1.X == P2.X;
        public Point[] AllPoints
        {
            get
            {
                if (IsHorizontal)
                {
                    var minX = Math.Min(P1.X, P2.X);
                    var maxX = Math.Max(P1.X, P2.X);

                    return Enumerable.Range(minX, maxX - minX + 1).Select(x => new Point(x, P1.Y)).ToArray();
                }

                else if (IsVertical)
                {
                    var minY = Math.Min(P1.Y, P2.Y);
                    var maxY = Math.Max(P1.Y, P2.Y);

                    return Enumerable.Range(minY, maxY - minY + 1).Select(y => new Point(P1.X, y)).ToArray();
                }

                else // IsDiagonal
                {
                    var minX = Math.Min(P1.X, P2.X);
                    var maxX = Math.Max(P1.X, P2.X);

                    var length = maxX - minX + 1;

                    var minXPoint = P1.X == minX ? P1 : P2;
                    var maxXPoint = P1.X == maxX ? P1 : P2;

                    var coefficient = maxXPoint.Y > minXPoint.Y ? 1 : -1;

                    var result = new List<Point>();

                    for (int i = 0; i < length; i++)
                    {
                        int x = minX + i;
                        int y = minXPoint.Y + coefficient * i;

                        result.Add(new Point(x, y));
                    }

                    return result.ToArray();
                }
            }
        }
    }
}