using eNotification.Domain;
using System.Collections.Generic;
using System.Data.SQLite;
using static eNotification.DAL.Enums;

namespace eNotification.DAL
{
    public class SqlDoctorProvider : SqlProvider
    {
        #region Methods

        public void AddDoctor(Doctor doctor)
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();


                var command = new SQLiteCommand(GetProcedureByType(DBProcedures.AddDoctor), dbConnection);

                command.Parameters.AddWithValue("@FirstName", doctor.FirstName);
                command.Parameters.AddWithValue("@SecondName", doctor.SecondName);
                command.Parameters.AddWithValue("@Patronymic", doctor.Patronymic);
                command.Parameters.AddWithValue("@SpecializationID", doctor.SpecializationId);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteDoctor(int doctorId)
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                var command = new SQLiteCommand(GetProcedureByType(DBProcedures.DeleteDoctor), dbConnection);

                command.Parameters.AddWithValue("@DoctorID", doctorId);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateDoctor(Doctor doctor)
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                var command = new SQLiteCommand(GetProcedureByType(DBProcedures.UpdateDoctor), dbConnection);

                command.Parameters.AddWithValue("@DoctorID", doctor.Id);
                command.Parameters.AddWithValue("@FirstName", doctor.FirstName);
                command.Parameters.AddWithValue("@SecondName", doctor.SecondName);
                command.Parameters.AddWithValue("@Patronymic", doctor.Patronymic);
                command.Parameters.AddWithValue("@SpecializationID", doctor.SpecializationId);

                command.ExecuteNonQuery();
            }
        }

        public List<Doctor> GetAllDoctors()
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                var command = new SQLiteCommand(GetProcedureByType(DBProcedures.GetAllDoctors), dbConnection);

                using (var reader = command.ExecuteReader())
                {
                    var idIndex = reader.GetOrdinal("ID");
                    var firstNameIndex = reader.GetOrdinal("FirstName");
                    var secondNameIndex = reader.GetOrdinal("SecondName");
                    var patronymicIndex = reader.GetOrdinal("Patronymic");
                    var specializationIndex = reader.GetOrdinal("Specialization_id");

                    var doctorList = new List<Doctor>();

                    while (reader.Read())
                    {
                        var doctor = new Doctor(
                            reader.GetInt32(idIndex),
                            reader.GetString(firstNameIndex),
                            reader.GetString(secondNameIndex),
                            reader.GetString(patronymicIndex),
                            reader.GetInt32(specializationIndex)
                            );

                        doctorList.Add(doctor);
                    }
                    return doctorList;
                }
            }
        }

        #endregion
    }
}
