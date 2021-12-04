public class Day3
{
    public static void Main()
    {
        // Task 1

        var lines = File.ReadAllLines("input.txt")
            .Select(x => x.ToArray().Select(c => int.Parse(c.ToString())).ToArray()).ToList();

        var transpose = GetTranspose(lines);

        var counts = transpose.Select(x => (NumZeros: x.Count(n => n == 0), NumOnes: x.Count(n => n == 1))).ToList();

        var mostCommons = counts.Select(x => x.NumOnes > x.NumZeros ? 1 : 0).ToList();
        var mostUncommons = counts.Select(x => x.NumOnes < x.NumZeros ? 1 : 0).ToList();

        var gammaRate = Convert.ToInt32(string.Join("", mostCommons), 2);
        var epsilonRate = Convert.ToInt32(string.Join("", mostUncommons), 2);

        var result = gammaRate * epsilonRate;   // 1131506


        // Task 2

        var oxygenGeneratorRating = Task2Solver(lines, 0, true);
        var Co2ScrubberRating = Task2Solver(lines, 0, false);

        var result2 = oxygenGeneratorRating * Co2ScrubberRating; // 7863147

        return;
    }

    private static int Task2Solver(List<int[]> bits, int index, bool findCommon)
    {
        var transPose = GetTranspose(bits);
        var array = transPose[index];
        var mostCommonBit = MostCommonBit(array);

        if (findCommon == false)
        {
            mostCommonBit = mostCommonBit == 0 ? 1 : 0;
        }

        var continueWith = bits.Where(x => x[index] == mostCommonBit).ToList();

        if (continueWith.Count == 1)
        {
            return Convert.ToInt32(string.Join("", continueWith[0]), 2);
        }

        return Task2Solver(continueWith, index + 1, findCommon);
    }

    private static List<int[]> GetTranspose(List<int[]> lines)
    {
        return Enumerable.Range(0, 12)
            .Select(i => lines.Select(l => l[i]).ToArray()).ToList();
    }

    private static int MostCommonBit(int[] bits)
    {
        var numZeroes = bits.Count(x => x == 0);
        var numOnes = bits.Count(x => x == 1);

        if (numZeroes == numOnes)
        {
            return 1;
        }

        return numOnes > numZeroes ? 1 : 0;
    }

}