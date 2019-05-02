using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using eNotification.Domain;
using static eNotification.DAL.Enums;
using eNotification.Extensions;
using eNotification;
using eNotification.DAL;

namespace eNotificationTests
{
    [TestClass]
    public class DomainTests
    {
        [TestMethod]
        public void DoctorCreationTest()
        {
            var fName = "testName";
            var sName = "secondName";
            var patronymic = "patronymic";
            var specializationId = (int) Specialization.Dermatologist;

            var doctor = new Doctor(1 ,fName, sName, patronymic, specializationId);

            Assert.IsTrue(string.IsNullOrEmpty(doctor.SpecializationName));
        }

        [TestMethod]
        public void ScheduleCreationTest()
        {
            var fName = "testName";
            var sName = "secondName";
            var patronymic = "patronymic";
            var specializationId = (int) Specialization.Dermatologist;

            var doctor = new Doctor(1, fName, sName, patronymic, specializationId);

            var schedule = new Schedule(1, doctor,"9991234567",DateTime.Now.AddDays(1));

            Assert.AreEqual(1, schedule.SendingStatus);
        }

        [TestMethod]
        public void ScheduleIsSendingTest()
        {
            var fName = "testName";
            var sName = "secondName";
            var patronymic = "patronymic";
            var specializationId = (int) Specialization.Dermatologist;

            var doctor = new Doctor(1, fName, sName, patronymic, specializationId);

            var schedule = new Schedule(1, doctor, "9991234567", DateTime.Now.AddDays(1), (int) SendStatus.Approved);

            Assert.AreEqual(2, schedule.SendingStatus);
        }

        [TestMethod]
        public void UserCreationTest()
        {
            var login = "testUser";
            var password = "password111";

            var user = new User(login, password);

            Assert.AreEqual(login, user.Login);
        }

        [TestMethod]
        public void EncryptTest()
        {
            var clearText = "textToEncrypt";
            var encryptedText = "VHLMjYZXtGfymeZU8Ksy0LXlAt7Mnvd37rrusknNpJ8=";

            var encryptResult = clearText.Encrypt();

            Assert.AreEqual(encryptedText, encryptResult);
        }

        [TestMethod]
        public void DecryptTest()
        {
            var encryptedText = "VHLMjYZXtGfymeZU8Ksy0LXlAt7Mnvd37rrusknNpJ8=";
            var clearText = "textToEncrypt";

            var decryptResult = encryptedText.Decrypt();

            Assert.AreEqual(clearText, decryptResult);
        }

        [TestMethod]
        public void ToMD5ExtensionTest()
        {
            var clearText = "textToMD5";
            var md5Text = "0D00DA2A40C1C59E23D38F72ABFDE3B2";

            var md5Result = clearText.ToMD5();

            Assert.AreEqual(md5Text, md5Result);
        }

        [TestMethod]
        public void RemoveWhiteSpacesExtensionTest()
        {
            var whitespaceText = "me ss a ge Wi th W hi te s pa ce s";
            var clearText = "messageWithWhitespaces";

            var clearWhiteSpaceResult = whitespaceText.RemoveWhitespace();

            Assert.AreEqual(clearText, clearWhiteSpaceResult);
        }

        [TestMethod]
        public void AddCountryCodeExtensionTest()
        {
            var numberWithoutCode = "9881234567";
            var numberWithCode = "79881234567";

            var addCodeResult = numberWithoutCode.AddCountryCode();

            Assert.AreEqual(numberWithCode, addCodeResult);
        }
    }
}
