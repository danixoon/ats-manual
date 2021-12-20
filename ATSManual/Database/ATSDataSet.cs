using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ATSManual.Database
{


    partial class ATSDataSet
    {
        partial class SubscriberDataTable
        {
        }

        public static SqlConnection connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(Properties.Settings.Default.ATSManualDBConnectionString);
                    _connection.Open();
                }
                return _connection;
            }
        }
        private static SqlConnection _connection;
        public static string JoinParams(params string[] array)
        {
            var query = $",{string.Join(",", array)},";
            return query;
        }

        public static string Quote(string value)
        {
            return $"'{value}'";
        }

        public static string QuoteValues(IEnumerable<string> values)
        {
            return $"{string.Join(", ", values.Select(v => $"'{v}'"))}";
        }

        public static string JoinValues(IEnumerable<string> values)
        {
            return $"{string.Join(", ", values.Select(v => $"{v}"))}";
        }

        public static string QuoteValues(params object[] values)
        {
            return $"{string.Join(", ", values.Select(v => $"'{v}'"))}";
        }



        public static void ReadRows(SqlDataReader reader, Action<SqlDataReader> cb)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cb(reader);
                }
            }
            reader.Close();
        }

        public static IEnumerable<SqlDataReader> GetRows(SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    yield return reader;
                }
            }
            reader.Close();
        }


        public static IEnumerable<int> ReadRowsIds(SqlDataReader reader)
        {
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    yield return reader.GetInt32(0);
                }
            }
            reader.Close();
        }

        public static SqlCommand Command(string query)
        {
            var command = new SqlCommand(query, connection);
            return command;
        }
    }
}

namespace ATSManual.Database.ATSDataSetTableAdapters
{
    partial class SubscriberDataTableAdapter
    {

    }

    partial class SubscriberTableAdapter
    {

    }

    partial class DataTableAdapter
    {
    }

    public partial class DepartmentTableAdapter
    {
    }
}
