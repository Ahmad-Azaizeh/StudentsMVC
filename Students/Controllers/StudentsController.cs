using Microsoft.AspNetCore.Mvc;
using Students.Application.Interfaces;
using Students.Domain.Entities;

namespace Students.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IRepository<Student> _repo;

        public StudentsController(IRepository<Student> repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _repo.GetAllAsync();
            return View(students);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }

            await _repo.AddAsync(student);
            await _repo.SaveChangesAsync();

            TempData["SuccessMessage"] = "Student Created successfully";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            var student = await _repo.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            _repo.Update(student);
            await _repo.SaveChangesAsync();

            TempData["SuccessMessage"] = "Student updated successfully";
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var student = await _repo.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var student = await _repo.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            _repo.Delete(student);
            await _repo.SaveChangesAsync();

            TempData["SuccessMessage"] = "Student deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
