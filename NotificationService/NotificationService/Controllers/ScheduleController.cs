using eNotification.DAL;
using eNotification.Domain;
using NotificationService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static eNotification.DAL.Enums;

namespace NotificationService.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly SqlNotificationsProvider _notificationProvider;
        private readonly SqlDoctorProvider _doctorProvider;
        private readonly List<Doctor> _doctorList;
        private readonly MainModel _mainModel;

        public ScheduleController()
        {
            _notificationProvider = new SqlNotificationsProvider();
            _doctorProvider = new SqlDoctorProvider();

            _mainModel = new MainModel();

            _doctorList = _doctorProvider.GetAllDoctors();
            _mainModel.Doctors = _doctorList.ToSelectListItems();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Addition(bool showConfirmationMessage = false)
        {
            _mainModel.ShowConfirmationMessage = showConfirmationMessage;

            return View(_mainModel);
        }

        [HttpPost]
        public ActionResult Addition(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                var doctor = _doctorList.FirstOrDefault(d => d.Id == schedule.DoctorId);
                if (doctor != null)
                    schedule.Doctor = doctor;

                schedule.PhoneNumber = schedule.PhoneNumber.RemoveSpecialCharacters();

                _notificationProvider.AddSchedule(schedule);
                ModelState.Clear();

                return Addition(true);
            }
            _mainModel.ShowConfirmationMessage = false;

            return View(_mainModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Viewer()
        {
            _mainModel.ScheduleList = _notificationProvider.GetScheduleByProcedureType(DBProcedures.GetAllSchedule).
                Where(sc => sc.SendingStatus != (int)SendStatus.Failed).
                ToList();

            return View(_mainModel);
        }

        [HttpPost]
        public ActionResult Viewer(MainModel model)
        {
            var selectFailedEntries = model.SelectedTypeFilterID == (int)FilterTypes.Error ? true : false;

            if (model.SelectedDoctorFilterID != 0)
                _mainModel.ScheduleList = _notificationProvider.GetScheduleByProcedureType(DBProcedures.GetScheduleByDoctorId, model.SelectedDoctorFilterID);
            else
                _mainModel.ScheduleList = _notificationProvider.GetScheduleByProcedureType(DBProcedures.GetAllSchedule);

            if (selectFailedEntries)
                _mainModel.ScheduleList = _mainModel.ScheduleList.Where(sc => sc.SendingStatus == (int)SendStatus.Failed).ToList();
            else
                _mainModel.ScheduleList = _mainModel.ScheduleList.Where(sc => sc.SendingStatus != (int)SendStatus.Failed).ToList();

            return View(_mainModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int scheduleId, DateTime appointmentDate, string phoneNumber, int doctorId)
        {
            var schedule = new Schedule(
                scheduleId,
                _doctorList.FirstOrDefault(d => d.Id == doctorId),
                phoneNumber, appointmentDate)
            {
                DoctorId = doctorId
            };

            _mainModel.Schedule = schedule;

            return View(_mainModel);
        }

        [HttpPost]
        public ActionResult Edit(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                var doctor = _doctorList.FirstOrDefault(d => d.Id == schedule.DoctorId);
                if (doctor != null)
                    schedule.Doctor = doctor;

                schedule.PhoneNumber = schedule.PhoneNumber.RemoveSpecialCharacters();

                _notificationProvider.UpdateScheduleByProcedure(schedule, DBProcedures.PartialScheduleUpdate);

                return RedirectToAction("Viewer");
            }

            return View(_mainModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int scheduleId)
        {
            _notificationProvider.DeleteSchedule(scheduleId);

            return RedirectToAction("Viewer");
        }
    }
}