// Task 1

var result = (from line in File.ReadAllLines("input.txt")
              let tokens = line.Split(' ')
              let data = new { Direction = tokens[0], Distance = int.Parse(tokens[1]) }
              group data by data.Direction into grp
              let groupedData = new { Direction = grp.Key, Distance = grp.Sum(g => g.Distance) }
              group groupedData by 1 into grp2
              let partialResults = new
              {
                  Forward = grp2.FirstOrDefault(g => g.Direction == "forward").Distance,
                  Down = grp2.FirstOrDefault(g => g.Direction == "down").Distance,
                  Up = grp2.FirstOrDefault(g => g.Direction == "up").Distance,
              }
              select partialResults.Forward * (partialResults.Down - partialResults.Up)).FirstOrDefault();





var dict1 = (from line in File.ReadAllLines("input.txt")
             let tokens = line.Split(' ')
             let data = new { Direction = tokens[0], Distance = int.Parse(tokens[1]) }
             group data by data.Direction into grp
             select new { Direction = grp.Key, Distance = grp.Sum(g => g.Distance) })
            .ToDictionary(x => x.Direction, x => x.Distance);

var forward1 = dict1["forward"];
var depth1 = dict1["down"] - dict1["up"];

var result1 = forward1 * depth1;


// Task 2


int forward = 0, aim = 0, depth = 0;

foreach (var line in File.ReadAllLines("input.txt"))
{
    var tokens = line.Split(' ');
    var data = new { Direction = tokens[0], Distance = int.Parse(tokens[1]) };

    switch (data.Direction)
    {
        case "forward":
            forward += data.Distance;
            depth += aim * data.Distance;
            break;
        case "down":
            aim += data.Distance;
            break;
        case "up":
            aim -= data.Distance;
            break;
    }
}

var result2 = forward * depth;

return;