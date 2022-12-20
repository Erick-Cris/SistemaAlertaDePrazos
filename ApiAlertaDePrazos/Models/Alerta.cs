namespace ApiAlertaDePrazos.Models
{
    public class Alerta
    {
        public int Id { get; set; }
        public int RegraId { get; set; }
        public string MatriculaAluno { get; set; }
        public DateTime DataAlerta { get; set; }
    }
}
