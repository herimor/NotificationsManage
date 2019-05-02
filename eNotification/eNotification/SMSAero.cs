// SmsAero.ru API

using System;
using System.Net;
using System.IO;
using System.Text;
using eNotification.Domain;

namespace eNotification
{
    public class SMSAero
    {
        #region Fields

        private readonly User _user;

        #endregion

        #region Constants

        const string From = "news";
        const bool POST_type = false;

        #endregion

        #region Constructors

        public SMSAero(User user)
        {
            _user = user;
        }

        #endregion

        #region Methods

        /**
         * Отправка одного сообщения
         * @param to - номер получателя
         * @param text - текст сообщения
         * @param digital - 0-прямой канал, 1-цифровой
         * @param type - тип отправки (читать документацию по api для типов)
         * @return array(response)
         */
        public string SendMessage(string phones, string message, int digital, int type)
        {
            var method = "send";
            return Send("&to=" + phones + "&text=" + message + "&digital=" + digital + "&type=" + type, method);
        }

        /**
        * Статус отправленного сообщения
        * @param $id - идентификатор сообщения
        * @return array(response)
        */
        public string GetMessageStatus(int id)
        {
            var method = "status";
            return Send("&id=" + id, method);
        }

        /**
         * Запрос баланса
         * @return array(response)
         */
        public string GetBalance()
        {
            var method = "balance";
            return Send("", method);
        }

        /**
         * Запрос тарифа
         * @return array(response)
         */
        public string CheckTariff()
        {
            string method = "checktarif";
            return Send("", method);
        }

        private string POST(string Data, string method)
        {
            Data += "&user=" + _user.Login;
            Data += "&password=" + _user.Password.ToMD5();
            Data += "&from=" + From;
            string url = ("https" + "://gate.smsaero.ru/" + method + "/");
            System.Net.WebRequest req = System.Net.WebRequest.Create(url);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = UTF8Encoding.UTF8.GetBytes(Data);
            req.ContentLength = sentData.Length;
            System.IO.Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            System.Net.WebResponse res = req.GetResponse();
            System.IO.Stream ReceiveStream = res.GetResponseStream();
            System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8);
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
            {
                String str = new String(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }

            return Out;
        }

        // отправка методом GET

        private string GET(string Data, string method)
        {
            Data += "&user=" + _user.Login;
            Data += "&password=" + _user.Password.ToMD5();
            Data += "&from=" + From;
            string url = ("https" + "://gate.smsaero.ru/" + method + "/");
            WebRequest req = WebRequest.Create(url + "?" + Data);
            WebResponse resp = req.GetResponse();
            Stream stream = resp.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string Out = sr.ReadToEnd();
            sr.Close();

            return Out;
        }

        // Выбор типа отправки

        private string Send(string data, string method)
        {
            return POST_type ? POST(data, method) : GET(data, method);
        }

        #endregion
    }
}
