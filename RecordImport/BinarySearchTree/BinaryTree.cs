using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using RecordImport.Common;

namespace RecordImport.BinarySearchTree
{
    public class BinaryTree<E> : ITree<E>
        where E : IComparable<E>, ISortable
    {
        public TreeNode<E> root;
        protected int size = 0;
        public List<string> SortingProperties { get; set; }

        public BinaryTree(){} 
        public BinaryTree(E[] objects)
        {
            SortingProperties = new List<string>() { PersonElements.Surname, PersonElements.FirstName, PersonElements.Age };
            foreach (E obj in objects)
            {
                insert(obj);
            }
        }

        public bool search(E e)
        {
            TreeNode<E> current = root;

            while (current != null)
            {
                if (e.CompareTo(current.Element) < 0)
                {
                    current = current.Left;
                }
                else if (e.CompareTo(current.Element) > 0)
                {
                    current = current.Right;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        public bool insert(E e)
        {
            e.SetSortingProperties(SortingProperties);
            if (root == null)
                root = CreateNewNode(e);
            else
            {
                TreeNode<E> parent = null;
                TreeNode<E> current = root;

                E currentPerson = e;
                
                while (current!=null)
                {
                    if (e.CompareTo(current.Element) < 0)
                    {
                        parent = current;
                        current = current.Left;
                    }
                    else if (e.CompareTo(current.Element) > 0)
                    {
                        parent = current;
                        current = current.Right;
                    }
                    else
                    {
                        var person = e as Person;
                        var exceptionStringBuilder = new StringBuilder();
                        exceptionStringBuilder.Append("A key with ");
                        foreach (var sortingProperty in SortingProperties)
                        {
                            if (person != null)
                                exceptionStringBuilder.Append(string.Format("{0}: {1}, ", sortingProperty,
                                                                            person.GetValueByProperty(sortingProperty)));
                        }
                        exceptionStringBuilder.Append("already exists.");
                        throw new DuplicateKeyException(exceptionStringBuilder.ToString());
                    }
                }
                
                if (e.CompareTo(parent.Element) < 0)
                    parent.Left = CreateNewNode(e);
                else
                    parent.Right = CreateNewNode(e);
                
            }
            size++;
            return true;
        }

        private TreeNode<E> CreateNewNode(E e)
        {
            return new TreeNode<E>(e);
        }

        //public bool delete(E e)
        //{
            
        //}

        //public void preOrder()
        //{
            
        //}

        //public void postOrder()
        //{
            
        //}

        public void inOrder()
        {
            inOrder(root);
        }

        private void inOrder(TreeNode<E> root)
        {
            if (root == null)
                return;

            inOrder(root.Left);
            Debug.Write(root.Element + " ");
            inOrder(root.Right);
        }


        public int getSize()
        {
            return this.size;
        }

        public bool isEmpty()
        {
            return getSize() == 0;
        }

        public IEnumerator<E> iterator()
        {
            return inOrderIterator();
        }

        private IEnumerator<E> inOrderIterator()
        {
            return new InOrderIterator<E>(this);

        }

        public TreeNode<E> getRoot()
        {
            return root;
        }

        public void clear()
        {
            root = null;
            size = 0;
        }

        public BinaryTree<E> Sort()
        {
            var sortedTree = new BinaryTree<E>();
            sortedTree.SortingProperties = this.SortingProperties;
            var treeIterator = iterator();
            while (treeIterator.MoveNext())
            {
                var item = treeIterator.Current;
                if (item.Equals(null))
                    continue;
                sortedTree.insert(treeIterator.Current);
            }
            return sortedTree;
        }

        

        
    }

    public interface ISortable
    {
        void SetSortingProperties(List<string> sortingProperties);
    }
}
