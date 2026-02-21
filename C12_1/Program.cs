namespace C12_1
{
    public interface IMatrix<T>
    {
        public T[,] matrix { get; set; }
        public int Count { get; set; }
        public void Add(T item);
        public void Clear();
        public bool Contains(T item);
        int IndexOf(T item);
        public void Insert(int index, T item);
        public void RemoveAt(int index);
        public bool Remove(T item);
        public T[,] ToMatrix();
        public void FillRandom(int count);
        public void Sort();
        public void Reverse();
        public void ShiftRight();
    }
    public class Matrix<T> : IMatrix<T>
    {
        public T[,] matrix { get; set; }
        public int Count { get; set; }
        private int rows;
        private int cols;
        public Matrix(int r, int c)
        {
            rows = r;
            cols = c;
            matrix = new T[rows, cols];
            Count = 0;
        }
        public void Add(T item)
        {
            if (Count >= rows * cols)
                return;
            int row = Count / cols;
            int col = Count % cols;
            matrix[row, col] = item;
            Count++;
        }
        public void Clear()
        {
            matrix = new T[rows, cols];
            Count = 0;
        }
        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (GetByIndex(i).Equals(item))
                    return true;
            }
            return false;
        }
        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (GetByIndex(i).Equals(item))
                    return i;
            }
            return -1;
        }
        public void Insert(int index, T item)
        {
            if (index < 0 || index >= rows * cols)
                return;
            for (int i = Count; i > index; i--)
            {
                SetByIndex(i, GetByIndex(i - 1));
            }
            SetByIndex(index, item);
            Count++;
        }
        public void RemoveAt(int index)
        {
            for (int i = index; i < Count - 1; i++)
            {
                SetByIndex(i, GetByIndex(i + 1));
            }
            Count--;
        }
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }
        public T[,] ToMatrix()
        {
            return matrix;
        }
        public void FillRandom(int count)
        {
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                object value = rnd.Next(0, 100);
                Add((T)value);
            }
        }
        public void Sort()
        {
            for (int i = 0; i < Count - 1; i++)
            {
                for (int j = 0; j < Count - i - 1; j++)
                {
                    dynamic a = GetByIndex(j);
                    dynamic b = GetByIndex(j + 1);
                    if (a > b)
                    {
                        SetByIndex(j, b);
                        SetByIndex(j + 1, a);
                    }
                }
            }
        }
        public void Reverse()
        {
            for (int i = 0; i < Count / 2; i++)
            {
                T temp = GetByIndex(i);
                SetByIndex(i, GetByIndex(Count - 1 - i));
                SetByIndex(Count - 1 - i, temp);
            }
        }
        public void ShiftRight()
        {
            if (Count == 0)
                return;
            T last = GetByIndex(Count - 1);
            for (int i = Count - 1; i > 0; i--)
            {
                SetByIndex(i, GetByIndex(i - 1));
            }
            SetByIndex(0, last);
        }
        private T GetByIndex(int index)
        {
            int row = index / cols;
            int col = index % cols;
            return matrix[row, col];
        }
        private void SetByIndex(int index, T value)
        {
            int row = index / cols;
            int col = index % cols;
            matrix[row, col] = value;
        }
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    result += matrix[i, j] + "\t";
                }
                result += "\n";
            }
            return result;
        }
    }
        public interface IArray<T>
    {
        public T[] arr {  get; set; }
        public int Count { get; set; }
        public void Add(T item);
        public void Clear();
        public bool Contains(T item);
        int IndexOf(T item);
        public void Insert(int index, T item);
        public void RemoveAt(int index);
        public bool Remove(T item);
        public T[] ToArray();
        public void FillRandom(int count);
    }
    public class Array<T> : IArray<T>
    {
        public T[] arr { get; set; }
        public int Count { get; set; }
        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < Count)
                {
                    return arr[index];
                }
                throw new IndexOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < Count)
                {
                    arr[index] = value;
                }
                throw new IndexOutOfRangeException();
            }
        }
        public Array()
        {
            Count = 0;
            arr = new T[0];
        }
        public void Add(T item)
        {
            T[] newArr = new T[Count + 1];
            for (int i = 0; i < Count; i++)
                newArr[i] = arr[i];
            newArr[Count] = item;
            arr = newArr;
            Count++;
        }

        public void Clear()
        {
            arr = new T[0];
            Count = 0;
        }

        public bool Contains(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (arr[i].Equals(item))
                    return true;
            }
            return false;
        }

        public void FillRandom(int count)
        {
            Random rnd = new Random();
            for (int i = 0; i < count; i++)
            {
                object value = rnd.Next(0, 100);
                Add((T)value);
            }
        }

        public int IndexOf(T item)
        {
            for (int i = 0; i < Count; i++)
            {
                if (arr[i].Equals(item))
                    return i;
            }
            return -1;
        }

        public void Insert(int index, T item)
        {
            if (index < 0 || index > Count)
                throw new IndexOutOfRangeException();
            T[] newArr = new T[Count + 1];
            for (int i = 0; i < index; i++)
                newArr[i] = arr[i];
            newArr[index] = item;
            for (int i = index; i < Count; i++)
                newArr[i + 1] = arr[i];
            arr = newArr;
            Count++;
        }

        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1)
                return false;
            RemoveAt(index);
            return true;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 || index >= Count)
                throw new IndexOutOfRangeException();
            T[] newArr = new T[Count - 1];
            for (int i = 0; i < index; i++)
                newArr[i] = arr[i];
            for (int i = index + 1; i < Count; i++)
                newArr[i - 1] = arr[i];
            arr = newArr;
            Count--;
        }

        public T[] ToArray()
        {
            T[] newArr = new T[Count];
            for (int i = 0; i < Count; i++)
            {
                newArr[i] = arr[i];
            }
            return newArr;
        }
        public override string ToString()
        {
            string result = "";
            for (int i = 0; i < Count; i++)
            {
                result += arr[i] + " ";
            }
            return result;
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            //Array<int> a = new Array<int>();
            //a.Add(5);
            //a.Add(2);
            //a.Add(9);
            //a.Add(15);
            //a.Add(10);
            //a.Add(20);
            //Console.WriteLine("Array: " + a);
            //a.Insert(1, 7);
            //Console.WriteLine("Insert: "+ a);
            //a.Remove(10);
            //Console.WriteLine("Remove: " + a);

            Matrix<int> m = new Matrix<int>(2, 3);
            m.FillRandom(6);
            Console.WriteLine(m);
            m.Sort();
            Console.WriteLine("Sorted:");
            Console.WriteLine(m);
            m.Reverse();
            Console.WriteLine("Reversed:");
            Console.WriteLine(m);
            m.ShiftRight();
            Console.WriteLine("Shifted Right:");
            Console.WriteLine(m);

            Console.ReadKey();
        }
    }
}
