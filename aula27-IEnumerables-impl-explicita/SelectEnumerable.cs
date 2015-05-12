using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class SelectEnumerator<T, R> : IEnumerator<R>
{

    private readonly Func<T, R> selectFunc;
    private readonly IEnumerator<T> selected;

    public SelectEnumerator(IEnumerator<T> selected, Func<T, R> selectFunc)
    {
        this.selected = selected;
        this.selectFunc = selectFunc;
    }

    public R Current { get { return selectFunc(selected.Current); } }

    public bool MoveNext() { return selected.MoveNext(); }

    object IEnumerator.Current
    {
        get { return Current; }
    }

    public void Dispose()
    {
        selected.Dispose();
    }

    public void Reset()
    {
        selected.Reset();
    }
}
    
class SelectEnumerable<T,R> : IEnumerable<R>
{
    private readonly Func<T, R> selectFunc;
    private readonly IEnumerable<T> selected;

    public SelectEnumerable(IEnumerable<T> selected, Func<T, R> selectFunc)
    {
        this.selectFunc = selectFunc;
        this.selected = selected;
    }

    public IEnumerator<R> GetEnumerator()
    {
        return new SelectEnumerator<T, R>(selected.GetEnumerator(), selectFunc);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
