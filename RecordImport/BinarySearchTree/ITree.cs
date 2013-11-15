using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordImport.BinarySearchTree
{
    public interface ITree<E>
        
    {
        bool search(E e);
        bool insert(E e);
        //bool delete(E e);
        //void preOrder();
        //void postOrder();
        void inOrder();
        int getSize();
        bool isEmpty();
        IEnumerator<E> iterator();
        void clear();
    }
}
