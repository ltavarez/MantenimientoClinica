using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Mantenimiento.DTO;
using Mantenimiento.Helpers;
using Mantenimiento.Mail;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mantenimiento.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Mantenimiento.Controllers
{
    [Authorize]
    public class DoctorController : Controller
    {
        private readonly ConsultorioMedicoContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;


        public DoctorController(ConsultorioMedicoContext context,IHostingEnvironment hostingEnvironment, IMapper mapper,IEmailSender emailSender)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
            this._mapper = mapper;
            this._emailSender = emailSender;
        }

        // GET: Doctor
      
        public async Task<IActionResult> Index(string nombreDoctor = null)
        {

            var doctorList =  _context.Doctor.AsQueryable();
            if (!string.IsNullOrEmpty(nombreDoctor))
            {
                doctorList = _context.Doctor.Where(x => x.Nombre.Contains(nombreDoctor));
            }

            ViewBag.NombreSession = HttpContext.Session.GetString(Configuration.KeyNombre);

            var list = await doctorList.ToListAsync();

            var message = new Message(new string[] { "itlaprueba2@gmail.com" },"Test Email","Esto es una prueba");

            await _emailSender.SendEmailAsync(message);

            return View(list);
        }

        // GET: Doctor/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            HttpContext.Session.Clear();

            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .FirstOrDefaultAsync(m => m.Id == id);

            ViewBag.Especialidades = await _context.Especialidad
                .Where(x => x.DoctorEspecialidad.Any(d => d.IdDoctor == id)).ToListAsync();



            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorDto model)
        {
            var doctor = new Doctor();
            if (ModelState.IsValid)
            {

                string uniqueName = null;
                if (model.Photo != null)
                {
                    var folderPath = Path.Combine(hostingEnvironment.WebRootPath, "images");
                    uniqueName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    var filePath = Path.Combine(folderPath, uniqueName);

                    if (filePath != null) model.Photo.CopyTo(new FileStream(filePath, mode: FileMode.Create));
                }
             

                 doctor = _mapper.Map<Doctor>(model);

                doctor.ProfilePhoto = uniqueName;

                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Doctor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }


            var doctorDto = _mapper.Map<DoctorDto>(doctor);
            return View(doctorDto);
        }

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorDto dto)
        {
            if (id != dto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var doctor = await _context.Doctor.FirstOrDefaultAsync(d => d.Id == dto.Id);
                    
                    string uniqueName = null;
                    if (dto.Photo != null)
                    {
                        var folderPath = Path.Combine(hostingEnvironment.WebRootPath, "images");
                        uniqueName = Guid.NewGuid().ToString() + "_" + dto.Photo.FileName;
                        var filePath = Path.Combine(folderPath, uniqueName);

                        var filePathDelete = Path.Combine(folderPath, doctor.ProfilePhoto);

                        if (!string.IsNullOrEmpty(doctor.ProfilePhoto))
                        {
                            if (System.IO.File.Exists(filePathDelete))
                            {
                                var fileInfo = new System.IO.FileInfo(filePathDelete);
                                fileInfo.Delete();
                            }
                        }

                        if (filePath != null) dto.Photo.CopyTo(new FileStream(filePath, mode: FileMode.Create));
                    }

                    doctor.Nombre = dto.Nombre;
                    doctor.Correo = dto.Correo;
                    doctor.Telefono = dto.Telefono;
                    doctor.FechaNacimiento = dto.FechaNacimiento;
                    doctor.ProfilePhoto = uniqueName;

                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(dto.Id))
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
            return View(dto);
        }

        // GET: Doctor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctor.FindAsync(id);
            _context.Doctor.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.Id == id);
        }
    }
}
