namespace ApiUFU.Models
{
    public class Estagio
    {
        public int Id { get; set; }
        public string AlunoId { get; set; }
        public int CursoId { get; set; }
        public DateTime DataInicio { get; set; }
        public int Tipo { get; set; }
        public int Status { get; set; }
    }
}
