using eNotification.Domain;
using eNotification.Extensions;
using System.Data.SQLite;
using static eNotification.DAL.Enums;

namespace eNotification.DAL
{
    public class SqlUserProvider : SqlProvider
    {
        #region Methods

        public void AddUser(User user)
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                var command = new SQLiteCommand(GetProcedureByType(DBProcedures.AddUser), dbConnection);

                command.Parameters.AddWithValue("@Login", user.Login);
                command.Parameters.AddWithValue("@Password", user.Password.Encrypt());

                command.ExecuteNonQuery();
            }
        }

        public User GetUserByLogin(string login)
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                var command = new SQLiteCommand(GetProcedureByType(DBProcedures.GetUserByLogin), dbConnection);

                command.Parameters.AddWithValue("@Login", login);

                using (var reader = command.ExecuteReader())
                {
                    var loginIndex = reader.GetOrdinal("Login");
                    var passwordIndex = reader.GetOrdinal("Password");

                    if (reader.Read())
                    {
                        return new User(
                            reader.GetString(loginIndex), 
                            reader.GetString(passwordIndex));
                    }
                }
                return new User();
            }
        }

        #endregion

    }
}
