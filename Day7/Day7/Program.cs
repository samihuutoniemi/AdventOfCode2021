//var initialPositions = File.ReadAllText("input.txt")
//    .Split(new[] { ',' }).Select(x => int.Parse(x)).ToArray();

//var dict = Enumerable.Range(initialPositions.Min(), initialPositions.Max() - initialPositions.Min() + 1)
//    .ToDictionary(x => x, x => 0);

//foreach (var key in dict.Keys)
//{
//    dict[key] = GetFuelCostForPosition(initialPositions, key);
//}

//var min = dict.Min(x => x.Value); // Task 1

//static int GetFuelCostForPosition(int[] positions, int target)
//{
//    return positions.Select(x => Math.Abs(x-target)).Sum();
//}

var initialPositions = File.ReadAllText("input.txt")
    .Split(new[] { ',' }).Select(x => int.Parse(x)).ToArray();

var dict = Enumerable.Range(initialPositions.Min(), initialPositions.Max() - initialPositions.Min() + 1)
    .ToDictionary(x => x, x => 0);

foreach (var key in dict.Keys)
{
    dict[key] = GetTotalFuelCostForAllCrabs(initialPositions, key);
}

var min = dict.Min(x => x.Value); // Result

static int GetFuelCostForCrab(int position, int target)
{
    var distance = Math.Abs(target - position);
    var cost = Enumerable.Range(1, distance).Sum();

    return cost;
}

static int GetTotalFuelCostForAllCrabs(int[] positions, int target)
{
    return positions.Select(x => GetFuelCostForCrab(x, target)).Sum();
}

return;