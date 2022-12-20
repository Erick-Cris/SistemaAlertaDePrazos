namespace ApiAlertaDePrazos.Models
{
    public class Regra
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Parametros { get; set; }
        public bool IsActive { get; set; }
    }
}
