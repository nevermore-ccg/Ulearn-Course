using System;
using System.Collections.Generic;

namespace DiskTree;

public class TreeNode
{
    public int Level { get; private set; }
    public SortedDictionary<string, TreeNode> Nodes = new(StringComparer.Ordinal);

    public TreeNode(int level) => Level = level;
}
public static class DiskTreeTask
{
    public static List<string> Solve(List<string> input)
    {
        var tree = CreateTree(input);
        return ConvertTreeToList(tree);
    }

    private static List<string> ConvertTreeToList(TreeNode tree)
    {
        var result = new List<string>();
        if (tree.Nodes.Count == 0)
            return result;
        foreach (var node in tree.Nodes)
        {
            result.Add(new string(' ', node.Value.Level) + node.Key);
            result.AddRange(ConvertTreeToList(node.Value));
        }
        return result;
    }

    private static TreeNode CreateTree(List<string> input)
    {
        var tree = new TreeNode(-1);
        for (var i = 0; i < input.Count; i++)
        {
            var folders = input[i].Split(@"\", StringSplitOptions.RemoveEmptyEntries);
            var treeNode = tree;
            for (var j = 0; j < folders.Length; j++)
            {
                if (!treeNode.Nodes.ContainsKey(folders[j]))
                    treeNode.Nodes[folders[j]] = new TreeNode(j);
                treeNode = treeNode.Nodes[folders[j]];
            }
        }
        return tree;
    }
}