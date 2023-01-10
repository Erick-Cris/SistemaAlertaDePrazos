using AlertaDePrazosLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertaDePrazosLibrary.Entities
{
    public class Estagio
    {
        public int Id { get; set; }
        public string AlunoId { get; set; }
        public int CursoId { get; set; }
        public DateTime DataInicio { get; set; }
        public TipoEstagio Tipo { get; set; }
        public StatusEstagio Status { get; set; }
    }
}
