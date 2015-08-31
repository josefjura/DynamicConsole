namespace DynamicConsole.IO
{
    using System.Collections.Generic;
    using System.Linq;

    public class TabularOutput
    {

        public int Width { get; set; }

        public TabularOutput(int width)
        {
            Width = width;
            Data = new List<List<string>>();
            maxWidths = new List<int>();
        }

        private List<int> maxWidths { get; set; }

        public List<List<string>> Data { get; set; }

        public void AddRow(params string[] values)
        {
            int rowNumber = Data.Count;

            for (int i = 0; i < values.Length; i++)
            {
                var fresh = values[i].Length;

                if (i < maxWidths.Count)
                {
                    maxWidths[i] = maxWidths[i] < fresh ? fresh : maxWidths[i];
                }
                else
                {
                    maxWidths.Add(values[i].Length);
                }


            }

            Data.Add(values.ToList());
        }

        public int GetColumnWidth(int cellIndex)
        {
            return maxWidths[cellIndex];
        }
    }
}
