using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamProgram.Data;
using ExamProgram.Models;

namespace ExamProgram.Controllers
{
    public class ExamsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExamsController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Exams.Include(e => e.Student).Include(e => e.Subject);
            return View(await applicationDbContext.ToListAsync());
        }

        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        
        public IActionResult Create()
        {
            ViewData["Subjects"] = new SelectList(_context.Subjects, "Id", "SubjectName");
            ViewData["Students"] = new SelectList(_context.Students, "Id", "FirstName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SubjectId,StudentId,ExamDate,Grade")] Exam exam)
        {
            // Gelen değerleri kontrol et
            Console.WriteLine($"SubjectId: {exam.SubjectId}, StudentId: {exam.StudentId}, ExamDate: {exam.ExamDate}, Grade: {exam.Grade}");

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(exam);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Hata: {ex.Message}");
                    ModelState.AddModelError(string.Empty, "Bir hata oluştu.");
                }
            }
            else
            {
                // Validasyon hatalarını konsola yazdır
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validasyon Hatası: {error.ErrorMessage}");
                }
            }

            ViewData["Subjects"] = new SelectList(_context.Subjects, "Id", "SubjectName", exam.SubjectId);
            ViewData["Students"] = new SelectList(_context.Students, "Id", "FirstName", exam.StudentId);
            return View(exam);
        }




        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            ViewData["Subjects"] = new SelectList(_context.Subjects, "Id", "SubjectName", exam.SubjectId);
            ViewData["Students"] = new SelectList(_context.Students, "Id", "FirstName", exam.StudentId);
            return View(exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SubjectId,StudentId,ExamDate,Grade")] Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Exams.Any(e => e.Id == exam.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewData["Subjects"] = new SelectList(_context.Subjects, "Id", "SubjectName", exam.SubjectId);
            ViewData["Students"] = new SelectList(_context.Students, "Id", "FirstName", exam.StudentId);
            return View(exam);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exam = await _context.Exams
                .Include(e => e.Student)
                .Include(e => e.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam != null)
            {
                _context.Exams.Remove(exam);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamExists(int id)
        {
            return _context.Exams.Any(e => e.Id == id);
        }
    }
}
