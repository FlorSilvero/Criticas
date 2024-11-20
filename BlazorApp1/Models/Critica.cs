public class Critica
{
    public int Id { get; set; }
    public string? Descripcion { get; set; }
    public int Puntaje { get; set; }
    public Pelicula Pelicula { get; set; }
    public int PeliculaId { get; set; }
    //public ApplicationUser? Usuario { get; set; }
    //public string? UsuarioId { get; set; }

//UsuarioId, PeliculaId, Puntaje, Descripcion

    //desde aca
        public Critica()
    {

    }

    public Critica(int id, string descripcion, int puntaje, int peliculaId)//, string usuarioId)
    {
        Id = id;
        Descripcion = descripcion;
        Puntaje = puntaje;
        PeliculaId = peliculaId;
       // UsuarioId = usuarioId;
    }

    public override string ToString()
    {
        return $"Id:{Id}, {Descripcion}, {Puntaje}, {PeliculaId}"; //, {UsuarioId}";
    }

}