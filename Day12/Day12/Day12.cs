using System.Text;

public class Day12
{
    public List<Node> Nodes { get; init; } = new List<Node>();
    public List<Edge> Edges { get; init; } = new List<Edge>();

    public void Init()
    {
        var lines = File.ReadAllLines("input.txt");

        foreach (var line in lines)
        {
            var edge = new Edge(line);

            if (!Edges.Contains(edge))
            {
                Edges.Add(edge);
            }

            if (!Nodes.Contains(edge.Node1))
            {
                Nodes.Add(edge.Node1);
            }

            if (!Nodes.Contains(edge.Node2))
            {
                Nodes.Add(edge.Node2);
            }
        }
    }

    public void CalculatePaths()
    {
        var smallCaves = Nodes.Where(n => n.IsSmall && n.Name != "start" && n.Name != "end").ToList();

        var totalResult = new List<List<Node>>();

        foreach (var cave in smallCaves)
        {
            var result = Traverse(null, null, cave);
            totalResult.AddRange(result);
        }

        var prettyResult = totalResult.Select(path => PrettyPath(path)).Distinct().ToList();    // Count here is the result
    }


    public List<List<Node>> Traverse(List<Node> path = null, Node node = null, Node mediumNode = null)
    {
        if (node == null)
        {
            node = Nodes.FirstOrDefault(n => n.Name == "start");
        }

        if (path == null)
        {
            path = new List<Node>();
        }

        path.Add(node);

        var result = new List<List<Node>>();

        if (node.Name == "end")
        {
            result.Add(path.ToList());
            return result;
        }

        var edges = Edges.Where(e => e.Node1 == node || e.Node2 == node).ToList();
        var possibleNodes = edges.Select(e => e.Other(node)).ToList();
        possibleNodes.RemoveAll(n => n.IsSmall && path.Contains(n) && n.Name != mediumNode.Name || n.Name == mediumNode.Name && path.Where(nd => nd.Name == n.Name).Count() > 1);

        foreach (var n in possibleNodes)
        {
            var results = Traverse(path.ToList(), n, mediumNode);
            result.AddRange(results);
        }

        return result;
    }

    // For debug
    public static string PrettyPath(List<Node> path)
    {
        if (path == null)
        {
            return "";
        }

        var sb = new StringBuilder();

        foreach (var node in path)
        {
            sb.Append($"{node.Name} ");
        }

        return sb.ToString().TrimEnd();
    }
}

public record Node
{
    public string Name { get; set; }
    public bool IsSmall => Name.ToLower() == Name;
    public bool IsMedium { get; set; }

    public Node(string name)
    {
        Name = name;
    }
}

public record Edge
{
    public Node Node1 { get; set; }
    public Node Node2 { get; set; }

    public Edge(string str)
    {
        var nodes = str.Split('-').Select(s => new Node(s)).ToArray();

        Node1 = nodes[0];
        Node2 = nodes[1];
    }

    public Node Other(Node node)
    {
        if (Node1 != node && Node2 != node)
        {
            throw new InvalidOperationException();
        }

        if (Node1 == node)
        {
            return Node2;
        }
        else
        {
            return Node1;
        }
    }
}
