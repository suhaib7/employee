using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace mongoEmployee.Controllers
{
    public class TrainingController : Controller
    {
        private readonly ITrainingRepository _db;
        public TrainingController(ITrainingRepository db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Training> trainings = _db.GetAllTrainings().ToList();
            return View(trainings);
        }

        public IActionResult Create(Training training) {
            _db.Create(training);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SaveTraining([FromBody] Training training)
        {
            try
            {
                if (training == null)
                {
                    return BadRequest("Training data is null");
                }

                _db.Create(training);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        public void SaveNewTraining(string name, string hours)
        {
            try
            {
                Training training = new Training
                {
                    Name = name,
                    Hours = hours
                };

                _db.Create(training);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Training> training = _db.GetAllTrainings().ToList();
            return Json(new { data = training });
        }

        [HttpDelete("training/delete/{id}")]
        public IActionResult Delete(string id)
        {
            try { 
                var trainingToDelete = _db.GetTrainingDetails(id);

                if (trainingToDelete == null) {
                    return Json(new { success = false, message = "Training not found" });
                }
                _db.Delete(id);
                return Json(new { success = true, message = "Training deleted successfully" });

            }
            catch(Exception) {
                return Json(new { success = false, message = $"Error deleting employee: " });
            }
        }

        #endregion
    }
}
