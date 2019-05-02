using eNotification.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NotificationService.Models
{
    public class MainModel
    {
        public MainModel()
        {
            Schedule = new Schedule();
            Doctors = new List<SelectListItem>();
            ScheduleList = new List<Schedule>();
            Filters = new List<SelectListItem>
            {
                new SelectListItem { Selected=true, Text="Только активные записи", Value = ((int)FilterTypes.Active).ToString() },
                new SelectListItem { Text="Только записи не прошедшие отправку", Value = ((int)FilterTypes.Error).ToString() }
            };
        }

        public bool ShowConfirmationMessage { get; set; }

        public Schedule Schedule { get; set; }

        public Doctor Doctor { get; set; }

        public IEnumerable<SelectListItem> Doctors { get; set; }

        public List<Schedule> ScheduleList { get; set; }

        public IEnumerable<SelectListItem> Filters { get; set; }

        public int SelectedTypeFilterID { get; set; }

        public int SelectedDoctorFilterID { get; set; }

        public Specialization Specialization { get; set; }

        public List<Doctor> DoctorList { get; set; }
    }

    public enum FilterTypes
    {
        Active = 0,
        Error = 1
    }

    // Add copy of eNotification.DAL.Enums.Specialization with ru-RU culture
    // to use it in UI
    public enum Specialization
    {
        Стоматолог = 0,
        Дерматолог = 1,
        Массажист = 2,
        Невролог = 3,
        Терапевт = 4,
        Врач_УЗИ = 5,
        ЛОР = 6,
        Гинеколог = 7,
        Хирург = 8,
        Психолог = 9,
        Уролог = 10,
        Травматолог = 11
    }

}