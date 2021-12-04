// Variant 1

//var lines = File.ReadAllLines("input.txt")
//    .Select(i => int.Parse(i))
//    .ToArray();

//var result = lines
//    .Where((n, index) => index > 0 && lines[index] > lines[index - 1])
//    .Count();


//// Variant 2

//var count = 0;
//int? previous = null;
//foreach (var line in lines)
//{
//    if (previous == null)
//    {
//        previous = line;
//        continue;
//    }

//    if (line > previous)
//    {
//        count++;
//    }

//    previous = line;
//}




//var count3 = 0;
//File.ReadAllLines("input.txt")
//    .Select(i => int.Parse(i))
//    .Aggregate((i, j) => { count3 += j > i ? 1 : 0; return j; });

//return;


//Func<(int, int), int, (int, int)> func = (i, j) => (j, i.Item2 + (j > i.Item1 ? 1 : 0));

// Variant 3

//var result = File.ReadAllLines("input.txt").Select(i => int.Parse(i)).Aggregate((0, 0), (i, j) => (j, i.Item2 + (j > i.Item1 ? 1 : 0))).Item2 - 1;

//return;