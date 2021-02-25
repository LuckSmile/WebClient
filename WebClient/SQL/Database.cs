using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SQL
{
    public class Database
    {
        private readonly string path = "";
        public Database(string path)
        {
            this.path = path;
        }
        public Line[] Reader(string table)
        {
            List<Line> answer = new List<Line>();
            using (SqlConnection connection = new SqlConnection(path))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM [{table}]", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        List<string> search = new List<string>();
                        search.AddRange(GetColumn(table));
                        while (reader.Read())
                        {
                            List<Column> columns = new List<Column>();
                            for (int index = 0; index < search.Count; index++)
                            {
                                string title = search[index];
                                string result = Convert.ToString(reader[search[index]]);
                                columns.Add(new Column(title, result, table));
                            }
                            answer.Add(new Line(columns.ToArray()));
                        }
                    }
                }
            }
            return answer.ToArray();
        }
        public string[] GetColumn(string table)
        {
            List<string> answer = new List<string>();
            using (SqlConnection connection = new SqlConnection(path))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT * FROM [{table}]", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    for (int index = 0; index < reader.FieldCount; index++)
                        answer.Add(reader.GetName(index));
                }
            }
            return answer.ToArray();
        }
        public string[] GetTables()
        {
            string[] answer = new string[0];
            return answer;
        }
    }
}
