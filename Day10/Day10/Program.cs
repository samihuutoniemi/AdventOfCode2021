var lines = File.ReadAllLines("input.txt");
var starters = new[] { '(', '[', '{', '<' }.ToList();
var enders = new[] { ')', ']', '}', '>' }.ToList();
var corruptionPoints = new[] { 3, 57, 1197, 25137 }.ToList();
var completionPoints = new[] { 1, 2, 3, 4 }.ToList();

var corruptionScores = new List<int>();
var completionScores = new List<long>();

foreach (var line in lines)
{
    var stack = new Stack<char>();
    var isCorrupted = false;

    foreach (var c in line)
    {
        if (starters.Contains(c))
        {
            stack.Push(c);
        }

        else if (enders.Contains(c))
        {
            var peeked = stack.Peek();

            if (starters.IndexOf(peeked) == enders.IndexOf(c))
            {
                stack.Pop();
            }
            else
            {
                corruptionScores.Add(corruptionPoints[enders.IndexOf(c)]);
                isCorrupted = true;
                break;
            }
        }
    }

    if (isCorrupted)
    {
        continue;
    }

    if (stack.Count > 0)
    {
        long completionScore = 0;

        while (stack.Count > 0)
        {
            var correctCharacter = enders[starters.IndexOf(stack.Peek())];

            completionScore *= 5;
            completionScore += completionPoints[enders.IndexOf(correctCharacter)];

            stack.Pop();
        }

        completionScores.Add(completionScore);
    }

    corruptionScores.Add(0);
}

var task1Result = corruptionScores.Sum();
var task2Result = completionScores.OrderBy(x => x).ToArray()[completionScores.Count / 2];

return;