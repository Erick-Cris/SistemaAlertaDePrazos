namespace ApiUFU.Models
{
    public class Aluno
    {
        public string Id { get; set; }
        public int CursoId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataIngresso { get; set; }
        public string Email { get; set; }
    }
}
