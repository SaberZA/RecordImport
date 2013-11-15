using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordImport.Common
{
    public class DuplicateKeyException : Exception
    {
        
        public DuplicateKeyException(string message)
            : base(message) { }
    }
}
