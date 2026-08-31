using Students.Application.Interfaces;
using Students.Application.Models;
using Students.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Students.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _repo;

        public StudentService(IRepository<Student> repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<StudentModel>> GetAllAsync(params string[] includes)
        {
            var students = await _repo.GetAllAsync();

            return students.Select(s => new StudentModel
            {
                Id = s.Id,
                FullName = s.FullName,
                Email = s.Email,
                Age = s.Age,
                Major = s.Major
            });
        }

        public async Task<StudentModel?> GetByIdAsync(int id, params string[] includes)
        {
            var student = await _repo.GetByIdAsync(id);

            return student is null ? null : new StudentModel
            {
                Id = student.Id,
                FullName = student.FullName,
                Email = student.Email,
                Age = student.Age,
                Major = student.Major
            };
        } 

        public async Task AddAsync(StudentModel model)
        {
            var student = new Student
            {
                FullName = model.FullName,
                Email = model.Email,
                Age = model.Age,
                Major = model.Major
            };
            await _repo.AddAsync(student);
            await _repo.SaveChangesAsync();
        }

        public async Task UpdateAsync(StudentModel model)
        {
            var student = await _repo.GetByIdAsync(model.Id);
            if (student is null) return;

            student.FullName = model.FullName;
            student.Email = model.Email;
            student.Age = model.Age;
            student.Major = model.Major;

            _repo.Update(student);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _repo.GetByIdAsync(id);
            if (student is null) return;

            _repo.Delete(student);
            await _repo.SaveChangesAsync();
        }
    }
}
