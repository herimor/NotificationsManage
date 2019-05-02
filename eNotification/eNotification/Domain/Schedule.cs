using eNotification.Extensions;
using System;
using System.ComponentModel.DataAnnotations;
using static eNotification.DAL.Enums;

namespace eNotification.Domain
{
    public class Schedule
    {
        #region Fields

        private int _id;
        private string _phoneNumber;
        private DateTime _appointmentDate;
        private Doctor _doctor;
        private int _sendingStatus;
        private int? _transactionId;
        private int _doctorId;

        #endregion

        #region Properties

        public Doctor Doctor
        {
            get { return _doctor; }
            set { _doctor = value; }
        }

        [Required(ErrorMessage = "Номер телефона обязателен")]
        [RegularExpression(@"^\(?\d{3}\)? *\d{3}-? *-?\d{2}-?\d{2}", ErrorMessage = "Неправильный формат номера. Пример (988)123-45-67")]
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        [DateRangeValidation(AddMonthCount = 1)]
        public DateTime AppointmentDate
        {
            get { return _appointmentDate; }
            set { _appointmentDate = value; }
        }

        public int SendingStatus
        {
            get { return _sendingStatus; }
            set { _sendingStatus = value; }
        }

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int? TransactionId
        {
            get { return _transactionId; }
            set { _transactionId = value; }
        }

        public bool IsSendingApproved
        {
            get { return this.SendingStatus == (int)SendStatus.Approved; }
        }

        [Required(ErrorMessage = "Выберите специалиста")]
        public int DoctorId
        {
            get { return _doctorId; }
            set { _doctorId = value; }
        }

        #endregion

        #region Constructors

        public Schedule()
        {
            SendingStatus = (int) SendStatus.Pending;
        }

        public Schedule(int id, Doctor doctor, string phoneNumber, DateTime appointmentDate, int sendingStatus = (int) SendStatus.Pending, int? transactionId = null)
        {
            _id = id;
            _doctor = doctor;
            _phoneNumber = phoneNumber;
            _appointmentDate = appointmentDate;
            _sendingStatus = sendingStatus;
            _transactionId = transactionId;
        }

        #endregion
    }
}
