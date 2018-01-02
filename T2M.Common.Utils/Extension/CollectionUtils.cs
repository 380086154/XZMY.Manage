using System.Collections.Generic;
using System.Linq;
using T2M.Common.Utils.Models;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class CollectionUtils
    {

        public static CompareResult<T> Compare<T>(this IList<T> newarr, IList<T> oldarr) where T : IEquatable<T>
        {
            var res = new CompareResult<T>();
            res.AddedElements = newarr.Where(m => !m.IsIn(oldarr)).ToArray();
            res.RemovedElements = oldarr.Where(m => !m.IsIn(newarr)).ToArray();

            return res;
        }

        public static CompareResult<T> Compare<T, TAction>(this IList<T> newarr, IList<T> oldarr, Func<T, TAction> func) where T : IEquatable<T> where TAction : IEquatable<TAction>
        {
            var res = new CompareResult<T>();
            res.AddedElements = newarr.Where(m => !oldarr.Any(n => func(n).Equals(func(m)))).ToArray();
            res.RemovedElements = oldarr.Where(m => !newarr.Any(n => func(n).Equals(func(m)))).ToArray();

            return res;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrays"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> Join<T>(IEnumerable<IList<T>> arrays)
        {
            return Join(arrays.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrays"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> Join<T>(params IList<T>[] arrays)
        {
            var res = new List<T>();
            foreach (var array in arrays)
            {
                res.AddRange(array);
            }
            return res;
        }

        /// <summary>
        /// 验证列表中的元素是否为升序排序，元素类型必须实现IComparable接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Boolean ValidSortAsc<T>(this IList<T> list) where T : IComparable<T>
        {
            lock (list)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    Int32 res = list[i].CompareTo(list[i + 1]);
                    if (res > 0)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 验证列表中的元素是否按指定属性为升序排序，元素中的指定属性必须实现IComparable接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TC"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Boolean ValidSortAsc<T, TC>(this IList<T> list, Func<T, TC> func) where TC : IComparable<TC>
        {
            lock (list)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    Int32 res = func.Invoke(list[i]).CompareTo(func.Invoke(list[i + 1]));
                    if (res > 0)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 验证列表中的元素是否为升序排序，元素类型必须实现IComparable接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Boolean ValidSortDesc<T>(this IList<T> list) where T : IComparable<T>
        {
            lock (list)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    Int32 res = list[i].CompareTo(list[i + 1]);
                    if (res < 0)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 验证列表中的元素是否按指定属性为降序排序，元素中的指定属性必须实现IComparable接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TC"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Boolean ValidSortDesc<T, TC>(this IList<T> list, Func<T, TC> func) where TC : IComparable<TC>
        {
            lock (list)
            {
                for (int i = 0; i < list.Count - 1; i++)
                {
                    var res = func.Invoke(list[i]).CompareTo(func.Invoke(list[i + 1]));
                    if (res < 0)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 对集合中的所有元素依次执行操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        public static void Foreach<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (list == null)
                throw new NullReferenceException();

            lock (list)
            {
                foreach (var item in list)
                {
                    action.Invoke(item);
                }
            }
        }

        /// <summary>
        /// 对集合中的所有元素依次执行操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="actions"></param>
        public static void Foreach<T>(this IEnumerable<T> list, params Action<T>[] actions)
        {
            if (list == null)
                throw new NullReferenceException();

            lock (list)
            {
                foreach (var item in list)
                {
                    foreach (var action in actions)
                    {
                        action.Invoke(item);
                    }
                }
            }
        }

        /// <summary>
        /// 获取集合中基于给定对象的下一个对象，若不存在给定对象或给定对象位于集合尾部则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">集合</param>
        /// <param name="current">当前对象</param>
        /// <returns>集合中基于给定对象的下一个对象，若不存在给定对象或给定对象位于集合尾部则返回null</returns>
        public static T MoveNext<T>(this IList<T> list, T current) where T : class
        {

            var index = list.IndexOf(current);
            if (index < 0) return null;
            if (index + 1 >= list.Count) return null;
            return list[index + 1];
        }

        /// <summary>
        /// 获取集合中基于给定对象的下一个对象，若不存在给定对象或给定对象位于集合尾部则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">集合</param>
        /// <param name="current">当前对象</param>
        /// <returns>集合中基于给定对象的下一个对象，若不存在给定对象或给定对象位于集合尾部则返回null</returns>
        public static T MoveNext<T>(this IEnumerable<T> list, T current) where T : class
        {
            Boolean flage = false;

            foreach (var item in list)
            {
                if (flage) return item;

                if (item.Equals(current))
                    flage = true;
            }
            return null;
        }

        /// <summary>
        /// 把一个键值对集合中的数据批量添加到另一个集合中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="source"></param>
        /// <param name="other"></param>
        public static void AddRange<T, TV>(this IDictionary<T, TV> source, IDictionary<T, TV> other)
        {
            foreach (var item in other)
            {
                if (!source.ContainsKey(item.Key))
                    source.Add(item.Key, item.Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <exception cref="ArgumentException"></exception>
        public static void AddRange<T, TV>(this IDictionary<T, TV> source, IList<T> keys, IList<TV> values)
        {
            if (keys.Count != values.Count)
                throw new ArgumentException();
            for (int i = 0; i < keys.Count; i++)
            {
                if (!source.ContainsKey(keys[i]))
                    source.Add(keys[i], values[i]);
            }
        }

        /// <summary>
        /// 在键值对集合中根据值尝试查找键
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static TK FindKey<TK, TV>(this IDictionary<TK, TV> source, TV value)
        {
            var keyvaluepair = source.FirstOrDefault(kv => kv.Value.Equals(value));
            return keyvaluepair.Equals(default(KeyValuePair<TK, TV>)) ? default(TK) : keyvaluepair.Key;
        }

        /// <summary>
        /// 把有序列表中的元素顺序随机重新排列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        public static IList<T> Shuffle<T>(this IList<T> source)
        {
            if (source.Count <= 1)
                return source;

            var array = source.ToArray();
            Array.Sort(array, new ShuffleComparer<T>());

            return array.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class ShuffleComparer<T> : IComparer<T>
        {
            Random ran = new Random();
            public int Compare(T x, T y)
            {
                return ran.Next(1000) > 500 ? 1 : -1;
            }
        }

        /// <summary>
        /// 批量添加元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="list"></param>
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> list)
        {
            var templist = source as List<T>;
            if (templist != null)
            {
                templist.AddRange(list); return;
            }

            list.Foreach(source.Add);
        }

        /// <summary>
        /// 批量删除元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="itemsToRemove"></param>
        public static void RemoveRange<T>(this IList<T> source, IEnumerable<T> itemsToRemove)
        {
            var removingItems = new Dictionary<T, int>();

            foreach (var item in itemsToRemove)
            {
                if (removingItems.ContainsKey(item))
                {
                    removingItems[item]++;
                }
                else
                {
                    removingItems[item] = 1;
                }
            }

            var setIndex = 0;
            var count = source.Count;
            for (var getIndex = 0; getIndex < count; getIndex++)
            {
                var current = source[getIndex];
                if (removingItems.ContainsKey(current))
                {
                    removingItems[current]--;
                    if (removingItems[current] == 0)
                    {
                        removingItems.Remove(current);
                    }

                    continue;
                }

                source[setIndex++] = source[getIndex];
            }

            count = setIndex;

            for (int i = source.Count; i > count; i--)
            {
                source.RemoveAt(i - 1);
            }
        }

        /// <summary>
        /// 移除列表中第一个满足条件的元素，并返回它，如果列表中不存在满足条件的元素，则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="conf"></param>
        /// <returns></returns>
        public static T RemoveFirst<T>(this ICollection<T> source, Func<T, Boolean> conf)
        {
            var item = source.FirstOrDefault(conf);
            source.Remove(item); return item;
        }

        /// <summary>
        /// 批量删除元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="conf"></param>
        public static void RemoveAll<T>(this IList<T> source, Func<T, Boolean> conf)
        {
            var templist = source as List<T>;
            if (templist != null)
            {
                templist.RemoveAll(new Predicate<T>(conf)); return;
            }

            var list = source.Where(conf).ToList();

            source.RemoveRange(list);
        }

        /// <summary>
        /// 将集合中的指定属性相加，返回计算结果，指定属性必须支持加号操作符并且返回同一类型，否则将抛出异常。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <returns>计算结果</returns>
        /// <exception cref="MissingMethodException">传入参数类型不支持加号操作符</exception>
        /// <exception cref="InvalidCastException">传入参数类型相加后不返回同一类型</exception>
        /// <exception cref="OverflowException">计算结果超过范围</exception>
        public static TResult Sum<T, TResult>(this IEnumerable<T> source, Func<T, TResult> func)
        {
            dynamic result = default(TResult);
            foreach (var item in source)
            {
                if (result.Equals(default(TResult))) { result = func.Invoke(item); continue; }
                result = func.Invoke(item) + result;
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TAction"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, TAction>(this IEnumerable<T> list, Func<T, TAction> func) where TAction : IEquatable<TAction>
        {
            if (null == default(T))
            {
                var result = new List<TAction>();
                foreach (var item in list)
                {
                    var current = func.Invoke(item);
                    if (!result.Contains(current))
                    {
                        result.Add(current);
                        yield return item;
                    }
                }
            }
            else
            {
                var result = new List<TAction>();
                foreach (var item in list)
                {
                    var current = func.Invoke(item);
                    if (result.IndexOf(current) < 0)
                    {
                        result.Add(current);
                        yield return item;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> list, Func<T, Object>[] func) where T : class
        {
            var length = func.Length;
            var compareList = new List<Object[]>();

            foreach (var item in list)
            {
                var templist = new object[length];
                for (var i = 0; i < length; i++)
                {
                    var fun = func[i];
                    var current = fun.Invoke(item);
                    templist[i] = current;
                }
                var bl = compareList.Any(exists => exists.AllEquals(templist));
                if (bl) continue;
                compareList.Add(templist);
                yield return item;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="list"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Boolean AllEquals<T>(this IList<T> source, IList<T> list)
        {
            var c = 0;
            var length = source.Count;
            if (list.Count != length) return false;
            for (var i = 0; i < length; i++)
            {
                if (source[i].Equals(list[i]))
                    c += 1;
            }
            return c == length;
        }

        private static void Swap<T>(this IList<T> source, Int32 i1, Int32 i2)
        {
            T temp = source[i1];
            source[i1] = source[i2];
            source[i2] = temp;
        }
    }
}
