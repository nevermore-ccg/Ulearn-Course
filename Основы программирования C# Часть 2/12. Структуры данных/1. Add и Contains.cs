using System;

namespace BinaryTrees;

public class TreeNode<T>
{
    public T Value { get; }
    public TreeNode<T> Left { get; set; }
    public TreeNode<T> Right { get; set; }

    public TreeNode(T value) => Value = value;
}
public class BinaryTree<T> where T : IComparable
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
}