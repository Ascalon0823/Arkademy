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
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    data[i, j] = val;
                }
            }
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

        public bool TryGet(int x, int y, out T val)
        {
            val = default;
            if (data == null)
            {
                return false;
            }

            if (x < 0 || x >= data.GetLength(0) || y < 0 || y >= data.GetLength(1))
            {
                return false;
            }

            val = data[x, y];
            return true;
        }
        
        public void Set(int x, int y, T val)
        {
            if (data == null)
            {
                return;
            }

            if (x < 0 || x >= data.GetLength(0) || y < 0 || y >= data.GetLength(1))
            {
                return;
            }

            data[x, y] = val;
        }

        public void Fill(T val)
        {
            if (data == null)
            {
                return;
            }

            for (var i = 0; i < data.GetLength(0); i++)
            {
                for (var j = 0; j < data.GetLength(1); j++)
                {
                    data[i, j] = val;
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