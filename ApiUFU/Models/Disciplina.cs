using ApiUFU.Utils;

namespace ApiUFU.Models
{
    public class Disciplina
    {
        public string Id { get; set; }
        public string? IdDisciplina { get; set; }
        public int CursoId { get; set; }
        public Periodo Periodo { get; set; }
        public string Titulo { get; set; }
        public bool Obrigatoria { get; set; }

    }
}
