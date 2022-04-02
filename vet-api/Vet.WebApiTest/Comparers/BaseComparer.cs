using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Vet.WebApiTest.Comparers
{
    public abstract class BaseComparer<T> : IComparer where T : class
    {
        public int Compare(object x,object y)
        {
            var equals = this.AreEqual(x as T, y as T);

            return equals ? 0 : 1;
        }

        protected abstract bool AreEqual(T expected, T actual);
    }
}