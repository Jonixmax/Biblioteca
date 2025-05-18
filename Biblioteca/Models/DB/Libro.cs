using System;
using System.Collections.Generic;

namespace Biblioteca.Models.DB;

public partial class Libro
{
    public int IdLibro { get; set; }

    public string Titulo { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public string Genero { get; set; } = null!;

    public string Sinopsis { get; set; } = null!;

    public string PortadaUrl { get; set; } = null!;

    public virtual ICollection<Calificacione> Calificaciones { get; set; } = new List<Calificacione>();
}
