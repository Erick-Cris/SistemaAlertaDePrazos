using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertaDePrazosLibrary.Entities.AlertaDePrazos
{
    public class Alerta
    {
        public int Id { get; set; }
        public int RegraId { get; set; }
        public string MatriculaAluno { get; set; }
        public DateTime DataAlerta { get; set; }
    }
}
