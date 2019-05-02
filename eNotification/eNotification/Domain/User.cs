using System.ComponentModel.DataAnnotations;

namespace eNotification.Domain
{
    public class User
    {
        #region Fields

        private string _login;
        private string _password;

        #endregion

        #region Constructors

        public User() { }

        public User(string login, string password)
        {
            _login = login;
            _password = password;
        }

        #endregion

        [Required(ErrorMessage = "Введите имя пользователя")]
        public string Login
        {
            get { return _login; }
            set { _login = value; }
        }

        [Required(ErrorMessage = "Введите пароль")]
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
