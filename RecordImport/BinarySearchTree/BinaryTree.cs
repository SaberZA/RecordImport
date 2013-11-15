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
        where E : IComparable<E>
    {
        public TreeNode<E> root;
        protected int size = 0;

        public BinaryTree(){} 
        public BinaryTree(E[] objects)
        {
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
            if (root == null)
                root = createNewNode(e);
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
                        if (person == null)
                            return false;

                        throw new DuplicateKeyException(string.Format("A key with Surname: {0}, FirstName:{1} and Age:{2} already exists",
                            person.GetValueByProperty(PersonElements.Surname),
                            person.GetValueByProperty(PersonElements.FirstName),
                            person.GetValueByProperty(PersonElements.Age)));
                    }
                }

                if (e.CompareTo(parent.Element) < 0)
                    parent.Left = createNewNode(e);
                else
                    parent.Right = createNewNode(e);
                
            }
            size++;
            return true;
        }

        private TreeNode<E> createNewNode(E e)
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

        class InOrderIterator<E> : IEnumerator<E> where E : IComparable<E>
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

            public E Current { get; private set; }

            object IEnumerator.Current
            {
                get { return list[current++]; }
            }
        }
    }

    
}
