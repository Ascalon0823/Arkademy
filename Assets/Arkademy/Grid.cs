namespace Arkademy
{
    public class Grid<T>
    {
        private readonly T[,] data;
        
        public Grid(int x, int y)
        {
            data = new T[x, y];
        }

        public Grid(int x, int y, T val)
        {
            data = new T[x, y];
            Fill(val);
        }

        public Grid(T[,] d)
        {
            data = d;
        }

        public int Width()
        {
            return data.GetLength(0);
        }

        public int Height()
        {
            return data.GetLength(1);
        }
        
        public T this[int x,int y]
        {
            get => data[x, y];
            set => data[x, y] = value;
        }


        public void Fill(T val)
        {
            Iterate((i, j) => data[i, j] = val);
        }
        
        public void Iterate(System.Action<int, int> action)
        {
            if (data == null)
            {
                return;
            }

            for (var j = 0; j < data.GetLength(1); j++)
            {
                for (var i = 0; i < data.GetLength(0); i++)
                {
                    action?.Invoke(i, j);
                }
            }
        }
        public override string ToString()
        {
            if (data == null)
            {
                return "null";
            }

            var print = "";
            for (var j = 0; j < data.GetLength(1); j++)
            {
                print += "[";
                for (var i = 0; i < data.GetLength(0); i++)
                {
                    print += $" {data[i, j].ToString()} ";
                }

                print += "]\n";
            }

            return print;
        }
    }
}