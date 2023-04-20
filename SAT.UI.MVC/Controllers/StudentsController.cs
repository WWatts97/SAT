using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SAT.DATA.EF.Models;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using SAT.UI.MVC.Utilities;

namespace SAT.UI.MVC.Controllers
{
    [Authorize(Roles ="Admin, Scheduling")]
    public class StudentsController : Controller
    {
        private readonly SATContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public StudentsController(SATContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Students
        
        public async Task<IActionResult> Index()
        {
            var sATContext = _context.Students.Include(s => s.Ss);
            return View(await sATContext.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Ss)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        [Authorize(Roles ="Admin")]
        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["Ssid"] = new SelectList(_context.StudentStatuses, "Ssid", "Ssname");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,Ssid, Image")] Student student)
        {
            if (ModelState.IsValid)
            {

                #region File Upload - Create

                if (student.Image != null)
                {

                    string ext = Path.GetExtension(student.Image.FileName);

                    string[] validExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    if (validExts.Contains(ext.ToLower()) && student.Image.Length < 4_194_303)
                    {
                        student.PhotoUrl = Guid.NewGuid() + ext;

                        string webRootPath = _webHostEnvironment.WebRootPath;

                        string fullImagePath = webRootPath + "/images/";

                        using (var memoryStream = new MemoryStream())
                        {
                            await student.Image.CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;

                                ImageUtility.ResizeImage(fullImagePath, student.PhotoUrl, img, maxImageSize, maxThumbSize);
                            }
                        }
                    }

                }
                else
                {
                    student.PhotoUrl = "NoImage.png";
                }

                #endregion

                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ssid"] = new SelectList(_context.StudentStatuses, "Ssid", "Ssname", student.Ssid);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["Ssid"] = new SelectList(_context.StudentStatuses, "Ssid", "Ssname", student.Ssid);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,Ssid, Image")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                #region EDIT File Upload
                //retain old image file name so we can delete if a new file was uploaded
                string oldImageName = student.PhotoUrl;

                //Check if the user uploaded a file
                if (student.Image != null)
                {
                    //get the file's extension
                    string ext = Path.GetExtension(student.Image.FileName);

                    //list valid extensions
                    string[] validExts = { ".jpeg", ".jpg", ".png", ".gif", ".webp" };

                    //check the file's extension against the list of valid extensions
                    if (validExts.Contains(ext.ToLower()) && student.Image.Length < 4_194_303)
                    {
                        //generate a unique file name
                        student.PhotoUrl = Guid.NewGuid() + ext;
                        //build our file path to save the image
                        string webRootPath = _webHostEnvironment.WebRootPath;
                        string fullPath = webRootPath + "/images/";

                        //Delete the old image
                        if (oldImageName != "NoImage.png")
                        {
                            ImageUtility.Delete(fullPath, oldImageName);
                        }

                        //Save the new image to webroot
                        using (var memoryStream = new MemoryStream())
                        {
                            await student.Image.CopyToAsync(memoryStream);
                            using (var img = Image.FromStream(memoryStream))
                            {
                                int maxImageSize = 500;
                                int maxThumbSize = 100;
                                ImageUtility.ResizeImage(fullPath, student.PhotoUrl, img, maxImageSize, maxThumbSize);
                            }
                        }

                    }
                }
                #endregion
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Ssid"] = new SelectList(_context.StudentStatuses, "Ssid", "Ssname", student.Ssid);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Ss)
                .FirstOrDefaultAsync(m => m.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'SATContext.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.StudentId == id)).GetValueOrDefault();
        }
    }
}
