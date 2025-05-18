using System;
using System.Collections.Generic;

namespace Biblioteca.Models.DB;

public partial class Calificacione
{
    public int IdCalificacion { get; set; }

    public int IdLibro { get; set; }

    public int? Puntuacion { get; set; }

    public DateTime? FechaHora { get; set; }

    public virtual Libro IdLibroNavigation { get; set; } = null!;
}
