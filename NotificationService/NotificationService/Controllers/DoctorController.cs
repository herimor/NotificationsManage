using eNotification.DAL;
using eNotification.Domain;
using NotificationService.Models;
using System.Web.Mvc;

namespace NotificationService.Controllers
{
    public class DoctorController : Controller
    {
        private readonly SqlDoctorProvider _doctorProvider;
        private readonly MainModel _mainModel;


        public DoctorController()
        {
            _doctorProvider = new SqlDoctorProvider();
            _mainModel = new MainModel();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Addition(bool showConfirmationMessage = false)
        {
            _mainModel.ShowConfirmationMessage = showConfirmationMessage;

            return View(_mainModel);
        }

        [HttpPost]
        public ActionResult Addition(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();

                _doctorProvider.AddDoctor(doctor);

                return Addition(true);
            }
            _mainModel.ShowConfirmationMessage = false;

            return View(_mainModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Viewer()
        {
            var doctorList = _doctorProvider.GetAllDoctors();
         
            foreach (var doctor in doctorList)
            {
                doctor.SpecializationName = ((Specialization)doctor.SpecializationId).ToString();
            }

            _mainModel.DoctorList = doctorList;

            return View(_mainModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Edit(int doctorId, string firstName, string lastName, string patronymic, int specializationId)
        {
            _mainModel.Doctor = new Doctor(doctorId, firstName, lastName, patronymic, specializationId);

            return View(_mainModel);
        }

        [HttpPost]
        public ActionResult Edit(Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _doctorProvider.UpdateDoctor(doctor);

                return RedirectToAction("Viewer");
            }

            return View(_mainModel);
        }

        [HttpGet]
        [Authorize]
        public ActionResult Delete(int doctorId)
        {
            _doctorProvider.DeleteDoctor(doctorId);

            return RedirectToAction("Viewer");
        }
    }
}