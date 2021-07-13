namespace Arkademy
{
    public class Grid<T>
    {
        private readonly T[,] _data;
        
        public Grid(int x, int y)
        {
            _data = new T[x, y];
        }

        public Grid(int x, int y, T val)
        {
            _data = new T[x, y];
            Fill(val);
        }

        public Grid(T[,] d)
        {
            _data = d;
        }

        public int Width()
        {
            return _data.GetLength(0);
        }

        public int Height()
        {
            return _data.GetLength(1);
        }
        
        public T this[int x,int y]
        {
            get => _data[x, y];
            set => _data[x, y] = value;
        }


        public void Fill(T val)
        {
            Iterate((i, j) => _data[i, j] = val);
        }
        
        public void Iterate(System.Action<int, int> action)
        {
            if (_data == null)
            {
                return;
            }

            for (var j = 0; j < _data.GetLength(1); j++)
            {
                for (var i = 0; i < _data.GetLength(0); i++)
                {
                    action?.Invoke(i, j);
                }
            }
        }
        public override string ToString()
        {
            if (_data == null)
            {
                return "null";
            }

            var print = "";
            for (var j = 0; j < _data.GetLength(1); j++)
            {
                print += "[";
                for (var i = 0; i < _data.GetLength(0); i++)
                {
                    print += $" {_data[i, j].ToString()} ";
                }

                print += "]\n";
            }

            return print;
        }
    }
}