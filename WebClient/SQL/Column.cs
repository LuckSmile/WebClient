using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQL
{
    public class Column
    {
        public string Title { get; private set; }
        public string Value { get; private set; }
        public string Table { get; private set; }
        public Column(string title, string value, string table)
        {
            this.Title = title;
            this.Value = value;
            this.Table = table;
        }
    }
}
