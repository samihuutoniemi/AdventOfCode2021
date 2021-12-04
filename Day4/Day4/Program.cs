class Day4Task1
{
    public static void Main()
    {

        var lines = File.ReadAllLines("input.txt");

        var callNumbers = lines.First().Split(',').Select(s => int.Parse(s)).ToList();
        var boardNumbers = lines.Skip(1).Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray()).ToList();

        var boards = boardNumbers.Chunk(5).Select((chunk, index) =>
            new BingoBoard { Index = index, Numbers = chunk.ToArray(), Transpose = GetTranspose(chunk) }).ToList();

        var drawnNumbers = new List<int>();
        var completedBoards = new List<int>();

        foreach (var number in callNumbers)
        {
            drawnNumbers.Add(number);

            var winningBingoBoards = boards.Select(board => board.HasWon(drawnNumbers.ToArray())).Where(i => i >= 0).ToList();

            if (winningBingoBoards.Any())
            {
                var score = boards[winningBingoBoards.First()].Score(drawnNumbers); // Task 1 result

                completedBoards.AddRange(winningBingoBoards.Where(w => !completedBoards.Contains(w)));
            }

            if (completedBoards.Count == boards.Count)
            {
                var lastBoardToWin = completedBoards.Last();
                var score = boards[lastBoardToWin].Score(drawnNumbers);
            }
        }
    }

    private static int[][] GetTranspose(int[][] numbers)
    {
        return Enumerable.Range(0, 5)
           .Select(i => numbers.Select(l => l[i]).ToArray()).ToArray();
    }
}

public class BingoBoard
{
    public int Index { get; init; }
    public int[][] Numbers { get; init; }
    public int[][] Transpose { get; init; }

    public int HasWon(IEnumerable<int> drawnNumbers)
    {
        var rowWinners = Enumerable.Range(0, 5).Select(i => RowIsWinner(i, drawnNumbers));
        var colWinners = Enumerable.Range(0, 5).Select(i => ColIsWinner(i, drawnNumbers));

        return (rowWinners.Any(x => x) || colWinners.Any(x => x)) ? Index : -1;
    }

    public bool RowIsWinner(int row, IEnumerable<int> drawnNumbers) => Numbers[row].All(i => drawnNumbers.Contains(i));

    public bool ColIsWinner(int col, IEnumerable<int> drawnNumbers) => Transpose[col].All(i => drawnNumbers.Contains(i));

    public int Score(IEnumerable<int> drawnNumbers)
    {
        var allUncalledNumbers = Numbers.SelectMany(x => x)
            .Where(i => !drawnNumbers.Contains(i))
            .Sum();


        return allUncalledNumbers * drawnNumbers.Last();
    }
}

