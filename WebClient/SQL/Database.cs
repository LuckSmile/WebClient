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
        public Column[] Reader(string table, string column = null)
        {
            List<Column> answer = new List<Column>();
            using (SqlConnection connection = new SqlConnection(path))
            {
                SqlCommand command = new SqlCommand($"SELECT * FROM [{table}]", connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        List<string> search = new List<string>();
                        if (column != null)
                        {
                            search.Add(column);
                        }
                        else
                        {
                            search.AddRange(GetColumn(table));
                        }
                        while (reader.Read())
                        {
                            for (int index = 0; index < search.Count; index++)
                            {
                                string result = Convert.ToString(reader[search[index]]);
                                answer.Add(new Column(column, result, table));
                            }
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
