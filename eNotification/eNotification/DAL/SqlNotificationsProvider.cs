using eNotification.Domain;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using static eNotification.DAL.Enums;

namespace eNotification.DAL
{
    public class SqlNotificationsProvider : SqlProvider
    {
        #region Constants

        private const string SELECT_PROCEDURE = "SELECT ProcedureText FROM Procedures WHERE ID = ";

        #endregion

        #region Methods

        public void AddSchedule(Schedule schedule)
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                var command = new SQLiteCommand(GetProcedureByType(DBProcedures.AddSchedule), dbConnection);

                command.Parameters.AddWithValue("@DoctorId", schedule.Doctor.Id);
                command.Parameters.AddWithValue("@PhoneNumber", schedule.PhoneNumber);
                command.Parameters.AddWithValue("@AppointmentDate", schedule.AppointmentDate.ToString("yyyy-MM-dd-HH:mm:ss"));
                command.Parameters.AddWithValue("@SendingStatus", schedule.SendingStatus);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteSchedule(int scheduleId)
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                var command = new SQLiteCommand(GetProcedureByType(DBProcedures.DeleteSchedule), dbConnection);

                command.Parameters.AddWithValue("@ScheduleID", scheduleId);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateScheduleByProcedure(Schedule schedule, DBProcedures procedure)
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                var command = new SQLiteCommand(GetProcedureByType(procedure), dbConnection);

                command.Parameters.AddWithValue("@ScheduleID", schedule.Id);
                command.Parameters.AddWithValue("@DoctorId", schedule.Doctor.Id);
                command.Parameters.AddWithValue("@PhoneNumber", schedule.PhoneNumber);
                command.Parameters.AddWithValue("@SendingStatus", schedule.SendingStatus);
                command.Parameters.AddWithValue("@TransactionId", schedule.TransactionId);
                command.Parameters.AddWithValue("@AppointmentDate", schedule.AppointmentDate.ToString("yyyy-MM-dd-HH:mm:ss"));

                command.ExecuteNonQuery();
            }
        }

        public List<Schedule> GetScheduleByDate(DateTime searchDate)
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                var command = new SQLiteCommand(GetSelectProcedure(searchDate), dbConnection);

                using (var reader = command.ExecuteReader())
                {
                    var scheduleIdIndex = reader.GetOrdinal("ID");
                    var doctorIdIndex = reader.GetOrdinal("Doctor_id");
                    var doctorFirstNameIndex = reader.GetOrdinal("FirstName");
                    var doctorSecondNameIndex = reader.GetOrdinal("SecondName");
                    var doctorPatronymicIndex = reader.GetOrdinal("Patronymic");
                    var doctorSpecializationIndex = reader.GetOrdinal("Specialization_id");
                    var phoneNumberIndex = reader.GetOrdinal("PhoneNumber");
                    var appointmentDateIndex = reader.GetOrdinal("AppointmentDate");
                    var sendingStatusIndex = reader.GetOrdinal("SendingStatus");

                    var scheduleList = new List<Schedule>();

                    while (reader.Read())
                    {
                        var appointmentDateString = reader.GetString(appointmentDateIndex);
                        var appointmentDate = DateTime.ParseExact(appointmentDateString, "yyyy-MM-dd-HH:mm:ss",
                                CultureInfo.InvariantCulture);

                        var doctor = new Doctor(
                            reader.GetInt32(doctorIdIndex),
                            reader.GetString(doctorFirstNameIndex),
                            reader.GetString(doctorSecondNameIndex),
                            reader.GetString(doctorPatronymicIndex),
                            reader.GetInt32(doctorSpecializationIndex)
                            );

                        scheduleList.Add(new Schedule(
                            reader.GetInt32(scheduleIdIndex),
                            doctor,
                            reader.GetString(phoneNumberIndex),
                            appointmentDate,
                            reader.GetInt32(sendingStatusIndex)));
                    }
                    return scheduleList;
                }
            }
        }

        public List<Schedule> GetScheduleByProcedureType(DBProcedures procedureType, int doctorId = -1)
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                var command = new SQLiteCommand(GetProcedureByType(procedureType), dbConnection);

                if (doctorId != -1)
                    command.Parameters.AddWithValue("@DoctorID", doctorId);

                using (var reader = command.ExecuteReader())
                {
                    var scheduleIdIndex = reader.GetOrdinal("ID");
                    var doctorIdIndex = reader.GetOrdinal("Doctor_id");
                    var doctorFirstNameIndex = reader.GetOrdinal("FirstName");
                    var doctorSecondNameIndex = reader.GetOrdinal("SecondName");
                    var doctorPatronymicIndex = reader.GetOrdinal("Patronymic");
                    var doctorSpecializationIndex = reader.GetOrdinal("Specialization_id");
                    var phoneNumberIndex = reader.GetOrdinal("PhoneNumber");
                    var appointmentDateIndex = reader.GetOrdinal("AppointmentDate");
                    var sendingStatusIndex = reader.GetOrdinal("SendingStatus");
                    var transactionIdIndex = reader.GetOrdinal("TransactionId");

                    var scheduleList = new List<Schedule>();

                    while (reader.Read())
                    {
                        var appointmentDateString = reader.GetString(appointmentDateIndex);
                        var appointmentDate = DateTime.ParseExact(appointmentDateString, "yyyy-MM-dd-HH:mm:ss",
                                CultureInfo.InvariantCulture);

                        var doctor = new Doctor(
                            reader.GetInt32(doctorIdIndex),
                            reader.GetString(doctorFirstNameIndex),
                            reader.GetString(doctorSecondNameIndex),
                            reader.GetString(doctorPatronymicIndex),
                            reader.GetInt32(doctorSpecializationIndex)
                            );

                        scheduleList.Add(new Schedule(
                            reader.GetInt32(scheduleIdIndex),
                            doctor,
                            reader.GetString(phoneNumberIndex),
                            appointmentDate,
                            reader.GetInt32(sendingStatusIndex),
                            reader.IsDBNull(transactionIdIndex) ? (int?)null : reader.GetInt32(transactionIdIndex)));
                    }
                    return scheduleList;
                }
            }
        }

        public void DeleteObsoleteSchedule()
        {
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                // Delete all entries with Appointment date < now time
                var command = new SQLiteCommand(GetSelectProcedure(DateTime.Now, DBProcedures.DeleteObsoleteSchedule), dbConnection);

                command.ExecuteNonQuery();
            }
        }

        public string GetSelectProcedure(DateTime searchDate, DBProcedures procedure = DBProcedures.SelectActualEntries)
        {
            // used instead of GetProcedureByType, because of date formats converting
            using (var dbConnection = new SQLiteConnection(_connectionString))
            {
                dbConnection.Open();

                var selectProcedure = SELECT_PROCEDURE + (int) procedure;

                var command = new SQLiteCommand(selectProcedure, dbConnection);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return string.Format(reader["ProcedureText"].ToString(), searchDate.ToString("yyyy-MM-dd-HH:mm:ss"));
                    }
                }
                return string.Empty;
            }
        }

        #endregion
    }
}
