namespace DynamicConsole.IO.Formatting
{
    using System.Collections.Generic;
    using System.Linq;

    public class TabularOutput
    {

        public int Width { get; set; }

        public TabularOutput(int width)
        {
            this.Width = width;
            this.Data = new List<List<string>>();
            this.maxWidths = new List<int>();
        }

        private List<int> maxWidths { get; set; }

        public List<List<string>> Data { get; set; }

        public void AddRow(params string[] values)
        {
            int rowNumber = this.Data.Count;

            for (int i = 0; i < values.Length; i++)
            {
                var fresh = values[i].Length;

                if (i < this.maxWidths.Count)
                {
                    this.maxWidths[i] = this.maxWidths[i] < fresh ? fresh : this.maxWidths[i];
                }
                else
                {
                    this.maxWidths.Add(values[i].Length);
                }


            }

            this.Data.Add(values.ToList());
        }

        public int GetColumnWidth(int cellIndex)
        {
            return this.maxWidths[cellIndex];
        }

        public int GetColumnStart(int cellIndex)
        {
            return this.maxWidths.Take(cellIndex).Sum();
        }

        public int GetTableWidth()
        {
            return this.maxWidths.Sum();
        }
    }
}
