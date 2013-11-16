using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordImport.BinarySearchTree
{
    public class InOrderIterator<E> : IEnumerator<E> where E : IComparable<E>, ISortable
    {
        private List<E> list = new List<E>();
        private int current = 0;
        private BinaryTree<E> Parent { get; set; }

        public InOrderIterator(BinaryTree<E> binaryTree)
        {
            this.Parent = binaryTree;
            inOrder();
        }

        private void inOrder()
        {
            inOrder(Parent.root);
        }

        private void inOrder(TreeNode<E> root)
        {
            if (root == null)
                return;
            inOrder(root.Left);
            list.Add(root.Element);
            inOrder(root.Right);
        }

        public void Dispose()
        {

        }

        public bool MoveNext()
        {
            if (current < list.Count)
                return true;

            return false;
        }

        public void Reset()
        {
            list = new List<E>();
            current = 0;
        }

        public E Current
        {
            get
            {
                return list[current++];
            }

            private set { list[current++] = value; }
        }

        object IEnumerator.Current
        {
            get { return list[current++]; }
        }


    }
}
