using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bernoulli
{
    public abstract class Row<T1, T2> : IEnumerable<(T1, T2)>
    {
        public IEnumerator<(T1, T2)> GetEnumerator() => Yielder().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Yielder().GetEnumerator();

        internal abstract IEnumerable<(T1 index, T2 value)> Yielder();

        public virtual T2 this[T1 t1] => Yielder().First(x => x.index.Equals(t1)).value;
        public virtual IEnumerable<(T1 index, T2 value)> this[T1 start, T1 end]
        {
            get
            {
                IEnumerator<(T1 index, T2 value)> indexer = Yielder().GetEnumerator();

                for (; !indexer.Current.index.Equals(start); indexer.MoveNext()) ;

                for (; !indexer.Current.index.Equals(end); indexer.MoveNext()) yield return (indexer.Current.index, indexer.Current.value);
            }
        }
    }

    public abstract class RARow<T1, T2> : Row<T1, T2>
    {
        internal abstract T2 Func(T1 index);
        internal abstract IEnumerable<T1> Indexer();

        internal override IEnumerable<(T1 index, T2 value)> Yielder() => Indexer().Select(index => (index, Func(index)));

        public override T2 this[T1 t1] => Func(t1);
        public override IEnumerable<(T1 index, T2 value)> this[T1 start, T1 end]
        {
            get
            {
                IEnumerator<T1> indexer = Indexer().GetEnumerator();

                for (; !indexer.Current.Equals(start); indexer.MoveNext()) ;

                for (; !indexer.Current.Equals(end); indexer.MoveNext()) yield return (indexer.Current, Func(indexer.Current));
            }
        }
    }
}
