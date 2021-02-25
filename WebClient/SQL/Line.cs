using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL
{
    public class Line
    {
        public Dictionary<string,Column> Columns { get; private set; }
        public Line(Column[] columns)
        {
            Columns = new Dictionary<string, Column>();
            for(int index = 0; index < columns.Length; index++)
            {
                Column column = columns[index];
                Columns.Add(column.Title, column);
            }
        }
    }
}
