using Microsoft.AspNetCore.Mvc;
using Students.Application.Interfaces;
using Students.Application.Models;
using Students.Domain.Entities;

namespace Students.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllAsync();
            return View(students);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StudentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _studentService.AddAsync(model);

            TempData["SuccessMessage"] = "Student Created successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var student = await _studentService.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(StudentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _studentService.UpdateAsync(model);

            TempData["SuccessMessage"] = "Student updated successfully";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetByIdAsync(id);

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
            var student = await _studentService.GetByIdAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            await _studentService.DeleteAsync(id);

            TempData["SuccessMessage"] = "Student deleted successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
