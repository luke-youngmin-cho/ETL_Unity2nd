
using System.Collections;

namespace Collections
{
    // where 제한자 
    // 타입을 제한할때 사용하는 키워드 
    // where T : IComparable<T>  <- T 에 대입할 타입은 반드시 IComparable<T> 혹은 해당 타입을 상속받은 타입이어야한다.  (IComparable<T> 에 대해 공변성, 캐스팅 가능해야함)
    public struct KeyValuePair<T, K>
        where T : IComparable<T>
        where K : IComparable<K>
    {
        // 변수 선언시 변수 타입뒤에 오는 ? 는 Nullable 연산자로, 해당 변수에 null 값을 대입할 수 있다는명시임
        public T? Key;
        public K? Value;

        public KeyValuePair(T Key, K Value)
        {
            this.Key = Key;
            this.Value = Value;
        }
    }

    public class MyHashtable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
        where TKey : IComparable<TKey>
        where TValue : IComparable<TValue>  
    {
        public TValue this[TKey key]
        {
            get
            {
                var bucket = _buckets[Hash(key)];

                if (bucket == null)
                    throw new Exception($"[MyHashtable<{nameof(TKey)}, {nameof(TValue)}>] : Key {key.ToString()} has not been registered");

                for (var i = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key.CompareTo(key) == 0)
                    {
                        return bucket[i].Value;
                    }
                }

                throw new Exception($"[MyHashtable<{nameof(TKey)}, {nameof(TValue)}>] : Key {key.ToString()} has not been registered");
            }
            set
            {
                var bucket = _buckets[Hash(key)];

                if (bucket == null)
                    throw new Exception($"[MyHashtable<{nameof(TKey)}, {nameof(TValue)}>] : Key {key.ToString()} has not been registered");

                for (var i = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key.CompareTo(key) == 0)
                    {
                        bucket[i] = new KeyValuePair<TKey, TValue>(key, value);
                    }
                }

                throw new Exception($"[MyHashtable<{nameof(TKey)}, {nameof(TValue)}>] : Key {key.ToString()} has not been registered");
            }
        }

        public IEnumerable<TKey> Keys
        {
            get
            {
                MyDynamicArray<TKey> array = new MyDynamicArray<TKey>();
                for (int i = 0; i < _validIndexList.Count; i++)
                {
                    for (int j = 0; j < _buckets[_validIndexList[i]].Count; j++)
                    {
                        array.Add(_buckets[_validIndexList[i]][j].Key);
                    }
                }
                return array;
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                MyDynamicArray<TValue> array = new MyDynamicArray<TValue>();
                for (int i = 0; i < _validIndexList.Count; i++)
                {
                    for (int j = 0; j < _buckets[_validIndexList[i]].Count; j++)
                    {
                        array.Add(_buckets[_validIndexList[i]][j].Value);
                    }
                }
                return array;
            }
        }


        

        private const int DEFAULT_CAPACITY = 100;
        private MyDynamicArray<KeyValuePair<TKey, TValue>>[] _buckets = new MyDynamicArray<KeyValuePair<TKey, TValue>>[DEFAULT_CAPACITY];
        private MyDynamicArray<int> _validIndexList = new MyDynamicArray<int>();

        public void Add(TKey key, TValue value)
        {
            int index = Hash(key);
            var bucket = _buckets[index];

            // 아직 해당 키의 Bucket 이 없으면 만들어줌
            if (bucket == null)
            {
                _buckets[index] = new MyDynamicArray<KeyValuePair<TKey, TValue>>();
                _validIndexList.Add(index);
            }
            else
            {
                // Bucket 이 있으면 해당 Bucket 에 중복키가 있는지 확인
                for (int i = 0; i < _buckets[index].Count; i++)
                {
                    if (_buckets[index][i].Key.CompareTo(key) == 0)
                        throw new Exception($"[MyHashtable<{nameof(TKey)}, {nameof(TValue)}>] : Key {key.ToString()} has already been registered");
                }
            }

            _buckets[index].Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        public bool TryAdd(TKey key, TValue value)
        {
            int index = Hash(key);
            var bucket = _buckets[index];

            // 아직 해당 키의 Bucket 이 없으면 만들어줌
            if (bucket == null)
            {
                _buckets[index] = new MyDynamicArray<KeyValuePair<TKey, TValue>>();
                _validIndexList.Add(index);
            }
            else
            {
                // Bucket 이 있으면 해당 Bucket 에 중복키가 있는지 확인
                for (int i = 0; i < _buckets[index].Count; i++)
                {
                    if (_buckets[index][i].Key.CompareTo(key) == 0)
                        return false;
                }
            }

            _buckets[index].Add(new KeyValuePair<TKey, TValue>(key, value));
            return true;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            int index = Hash(key);
            var bucket = _buckets[index];

            if (bucket != null)
            {
                for (int i = 0; i < _buckets[index].Count; i++)
                {
                    if (_buckets[index][i].Key.CompareTo(key) == 0)
                    {
                        value = _buckets[index][i].Value;
                        return true;
                    }
                }
            }

            value = default;
            return false;
        }


        public bool Remove(TKey key)
        {
            int index = Hash(key);
            var bucket = _buckets[index];

            if (bucket != null)
            {
                for (int i = 0; i < bucket.Count; i++)
                {
                    if (bucket[i].Key.CompareTo(key) == 0)
                    {
                        bucket.RemoveAt(i);
                        return true;
                    }
                }
            }

            return false;
        }


        public int Hash(TKey key)
        {
            string keyName = key.ToString();
            int result = 0;
            for (int i = 0; i < keyName.Length; i++)
            {
                result += keyName[i];
            }
            result %= DEFAULT_CAPACITY;
            return result;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>
        {
            public object Current => _data[_index];

            KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current => _data[_index];

            private MyDynamicArray<KeyValuePair<TKey, TValue>> _data;
            private int _index;

            public Enumerator(MyHashtable<TKey, TValue> data)
            {
                _data = new MyDynamicArray<KeyValuePair<TKey, TValue>>();
                for (int i = 0; i < data._validIndexList.Count; i++)
                {
                    for (int j = 0; j < data._buckets[data._validIndexList[i]].Count; j++)
                    {
                        _data.Add(new KeyValuePair<TKey, TValue>(data._buckets[data._validIndexList[i]][j].Key,
                                                                 data._buckets[data._validIndexList[i]][j].Value));
                    }
                }

                _index = -1;
            }


            public void Dispose()
            {
            }

            public bool MoveNext()
            {
                _index++;
                return _index < _data.Count;
            }

            public void Reset()
            {
                _index = -1;
            }
        }
    }
}
