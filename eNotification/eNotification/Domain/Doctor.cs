using System.ComponentModel.DataAnnotations;
using static eNotification.DAL.Enums;

namespace eNotification.Domain
{
    public class Doctor
    {
        #region Fields

        private int _id;
        private string _firstName;
        private string _secondName;
        private string _patronymic;
        private int _specializationId;
        private string _specializationName;

        #endregion

        #region Properties

        [Required(ErrorMessage = "Укажите специалиста")]
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        [Required(ErrorMessage = "Укажите имя")]
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        [Required(ErrorMessage = "Укажите фамилию")]
        public string SecondName
        {
            get { return _secondName; }
            set { _secondName = value; }
        }

        [Required(ErrorMessage = "Укажите отчество")]
        public string Patronymic
        {
            get { return _patronymic; }
            set { _patronymic = value; }
        }

        [Required(ErrorMessage = "Укажите специализацию")]
        public int SpecializationId
        {
            get { return _specializationId; }
            set { _specializationId = value; }
        }

        public string SpecializationName
        {
            get { return _specializationName; }
            set { _specializationName = value; }
        }

        #endregion

        #region Constructors

        public Doctor() { }

        public Doctor(int id, string firstName, string secondName, string patronymic, int specializationId)
        {
            _id = id;
            _firstName = firstName;
            _secondName = secondName;
            _patronymic = patronymic;
            _specializationId = specializationId;
        }
        #endregion
    }
}
