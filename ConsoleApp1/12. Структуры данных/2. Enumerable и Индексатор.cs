using System;
using System.Collections;
using System.Collections.Generic;

namespace BinaryTrees;

public class TreeNode<T>
{
    public T Value { get; }
    public TreeNode<T> Left { get; set; }
    public TreeNode<T> Right { get; set; }
    public int Size = 1;

    public TreeNode(T value) => Value = value;
}
public class BinaryTree<T> : IEnumerable<T>
    where T : IComparable
{
    private TreeNode<T> _node;

    public void Add(T key)
    {
        if (_node == null)
        {
            _node = new TreeNode<T>(key);
            return;
        }
        var binaryTree = _node;
        while (true)
        {
            binaryTree.Size++;
            var comparison = key.CompareTo(binaryTree.Value);
            var sideNode = comparison < 0 ? binaryTree.Left : binaryTree.Right;
            if (sideNode == null)
            {
                if (comparison < 0)
                    binaryTree.Left = new TreeNode<T>(key);
                else
                    binaryTree.Right = new TreeNode<T>(key);
                break;
            }
            binaryTree = sideNode;
        }
    }

    public bool Contains(T key)
    {
        var binaryTree = _node;
        while (true)
        {
            if (binaryTree == null) return false;
            switch (key.CompareTo(binaryTree.Value))
            {
                case 0:
                    return true;
                case 1:
                    binaryTree = binaryTree.Right;
                    break;
                case -1:
                    binaryTree = binaryTree.Left;
                    break;
            }
        }
    }

    public T this[int i]
    {
        get
        {
            var binaryTree = _node;
            while (true)
            {
                if (binaryTree == null) continue;
                var size = binaryTree.Left?.Size ?? 0;
                if (i == size)
                    return binaryTree.Value;
                if (i < size)
                    binaryTree = binaryTree.Left;
                else
                {
                    binaryTree = binaryTree.Right;
                    i -= size + 1;
                }
            }
        }
    }

    public IEnumerator<T> GetEnumerator() => EnumerateValues(_node).GetEnumerator();

    private static IEnumerable<T> EnumerateValues(TreeNode<T> node)
    {
        if (node == null) yield break;
        foreach (var value in EnumerateValues(node.Left))
            yield return value;
        yield return node.Value;
        foreach (var value in EnumerateValues(node.Right))
            yield return value;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}