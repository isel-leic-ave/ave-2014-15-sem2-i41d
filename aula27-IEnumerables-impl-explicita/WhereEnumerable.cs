using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WhereEnumerable<T> : IEnumerable<T>{
	
	private readonly Predicate<T> predicate;
	private readonly IEnumerable<T> source;

	public WhereEnumerable(IEnumerable<T> src, Predicate<T> p) {
		this.source = src;
		this.predicate = p;
	}

	public IEnumerator<T> GetEnumerator() {
		return new WhereEnumerator<T>(source.GetEnumerator(), predicate);
	}

	IEnumerator IEnumerable.GetEnumerator() {
		return new WhereEnumerator<T>(source.GetEnumerator(), predicate);
	}
}

public class WhereEnumerator<T> : IEnumerator<T>{
	private readonly IEnumerator<T> src;
	private readonly Predicate<T> predicate;
	private T current;

	public WhereEnumerator(IEnumerator<T> src, Predicate<T> predicate) {
		this.src = src;
		this.predicate = predicate;
		current = default(T);
	}

	public T Current {
		get { return current; }
	}

	public void Dispose() {
		src.Dispose();
	}

	object IEnumerator.Current {
		get { return current; }
	}

	public bool MoveNext() {
		while (src.MoveNext()){
            T aux = src.Current;
			if (predicate(aux)) { current = aux; return true; }
        }
		return false;
	}

	public void Reset() {
		src.Reset();
		current = default(T);
	}
}
