using eNotification.Extensions;
using System.Configuration;
using System.Data.SQLite;
using System.Text;
using static eNotification.DAL.Enums;

namespace eNotification.DAL
{
    public class SqlProvider
    {
        #region Fields

        protected readonly string _connectionString = string.Empty;

        #endregion

        #region Constants

        private const string SELECT_PROCEDURE = "SELECT ProcedureText FROM Procedures WHERE ID = ";
        private const int PASSWORD_LENGTH = 9; // 9 symbols it's 'Password=' string length

        #endregion

        #region Constructors

        public SqlProvider()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["SQLiteConnection"].ConnectionString;

            var passwordEndIndex = _connectionString.IndexOf("Password=") + PASSWORD_LENGTH;
            var passwordHash = _connectionString.Substring(passwordEndIndex).TrimEnd(';');

            var stringBuilder = new StringBuilder(_connectionString);

            _connectionString = stringBuilder.Replace(passwordHash, passwordHash.Decrypt()).ToString();
        }

        #endregion

        #region Methods

        public string GetProcedureByType(DBProcedures procedureType)
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                var selectProcedure = SELECT_PROCEDURE + (int)procedureType;

                var command = new SQLiteCommand(selectProcedure, dbConnection);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return reader["ProcedureText"].ToString();
                    }
                }

                return string.Empty;
            }
        }

        #endregion
    }
}
