
namespace eNotification.DAL
{
    public class Enums
    {
        public enum DBProcedures
        {
            SelectActualEntries = 1,
            AddSchedule = 2,
            DeleteSchedule = 3,
            UpdateSchedule = 4,
            PartialScheduleUpdate = 5,
            AddDoctor = 6,
            DeleteDoctor = 7,
            UpdateDoctor = 8,
            GetAllDoctors = 9,
            GetAllSchedule = 10,
            GetScheduleByDoctorId = 11,
            AddUser = 12,
            GetUserByLogin = 13,
            GetTransactedSchedule = 14,
            DeleteObsoleteSchedule = 15
        }

        public enum Specialization
        {
            Dentist = 0,
            Dermatologist = 1,
            Masseur = 2,
            Neurologist = 3,
            Therapist = 4,
            Ultrasound = 5,
            ENT = 6,
            Gynecologist = 7,
            Surgeon = 8,
            Psychologist = 9,
            Urologist = 10,
            Traumatologist = 11
        }

        public enum SendStatus
        {
            Pending = 1,
            Approved = 2,
            Failed = 3
        }
    }
}
