using System.Diagnostics;
using Biblioteca.Models;
using Biblioteca.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Controllers
{
    public class HomeController : Controller
    {
        private readonly BibliotecaLecturaContext _context;
        public HomeController(BibliotecaLecturaContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Página para modificar libros
        public async Task<IActionResult> Modificar()
        {
            var libros = await _context.Libros.ToListAsync();
            return View(libros);
        }

        // Método para agregar un nuevo libro
        [HttpPost]
        public async Task<IActionResult> AgregarLibro(Libro libro)
        {
            if (!ModelState.IsValid)
            {
                return View("Modificar", await _context.Libros.ToListAsync());
            }

            // Agregar nuevo libro
            _context.Libros.Add(libro);
            await _context.SaveChangesAsync();
            return RedirectToAction("Modificar");
        }

        // Método para actualizar un libro existente
        [HttpPost]
        public async Task<IActionResult> ActualizarLibro(int idLibro, Libro libro)
        {
            if (!ModelState.IsValid)
            {
                // Si el modelo no es válido, mostrar los libros con los errores
                TempData["ErrorMessage"] = "Por favor, completa todos los campos correctamente.";
                return RedirectToAction("Modificar");
            }

            // Buscar el libro existente
            var libroExistente = await _context.Libros.FindAsync(idLibro);
            if (libroExistente == null)
            {
                TempData["ErrorMessage"] = $"No se encontró el libro con ID {idLibro} para actualizar.";
                return RedirectToAction("Modificar");
            }

            // Actualizar los campos
            libroExistente.Titulo = libro.Titulo;
            libroExistente.Autor = libro.Autor;
            libroExistente.Genero = libro.Genero;
            libroExistente.Sinopsis = libro.Sinopsis;
            libroExistente.PortadaUrl = libro.PortadaUrl;

            _context.Libros.Update(libroExistente);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "El libro ha sido actualizado correctamente.";
            return RedirectToAction("Modificar");
        }





        // Acción para eliminar un libro
        [HttpPost]
        public async Task<IActionResult> EliminarLibro(int idLibro)
        {
            var libro = await _context.Libros.FindAsync(idLibro);
            if (libro != null)
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();
            }
            else
            {
                TempData["ErrorMessage"] = $"No se encontró el libro con ID {idLibro} para eliminar.";
            }
            return RedirectToAction("Modificar");
        }


        public async Task<IActionResult> Clasificar()
        {
            var libros = await _context.Libros
                .Include(l => l.Calificaciones)
                .Select(l => new
                {
                    l.IdLibro,
                    l.Titulo,
                    l.Genero,
                    l.PortadaUrl,
                    PromedioCalificacion = l.Calificaciones.Any() ? l.Calificaciones.Average(c => c.Puntuacion) : 0
                })
                .ToListAsync();

            return View(libros);
        }

        [HttpGet]
        public async Task<IActionResult> Calificar(int idLibro, int puntuacion)
        {
            var calificacion = new Calificacione
            {
                IdLibro = idLibro,
                Puntuacion = puntuacion,
                FechaHora = DateTime.Now
            };

            _context.Calificaciones.Add(calificacion);
            await _context.SaveChangesAsync();

            return RedirectToAction("Clasificar");
        }

        public IActionResult Explorar()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Top5()
        {
            var top5Libros = _context.Libros
                .Select(libro => new
                {
                    libro.IdLibro,
                    libro.Titulo,
                    libro.Autor,
                    libro.Genero,
                    libro.Sinopsis,
                    libro.PortadaUrl,
                    PuntuacionTotal = _context.Calificaciones
                        .Where(c => c.IdLibro == libro.IdLibro)
                        .Sum(c => c.Puntuacion) / 5.0
                })
                .OrderByDescending(l => l.PuntuacionTotal)
                .Take(5)
                .ToList();

            return Json(top5Libros);
        }

        [HttpGet]
        public IActionResult BuscarLibros(string titulo, string genero)
        {
            var query = _context.Libros.AsQueryable();

            if (!string.IsNullOrEmpty(titulo))
                query = query.Where(l => l.Titulo.Contains(titulo));

            if (!string.IsNullOrEmpty(genero))
                query = query.Where(l => l.Genero.Contains(genero));

            var libros = query.Select(l => new
            {
                l.IdLibro,
                l.Titulo,
                l.Autor,
                l.Genero,
                l.Sinopsis,
                l.PortadaUrl,
                PuntuacionTotal = _context.Calificaciones
                    .Where(c => c.IdLibro == l.IdLibro)
                    .Sum(c => c.Puntuacion) / 5.0
            }).ToList();

            return Json(libros);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
