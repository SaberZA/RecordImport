using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordImport.BinarySearchTree
{
    public class TreeNode<E>
    {
        public E Element { get; private set; }
        public TreeNode<E> Left { get; set; }
        public TreeNode<E> Right { get; set; } 

        public TreeNode(E element)
        {
            Element = element;
        }
    }
}
