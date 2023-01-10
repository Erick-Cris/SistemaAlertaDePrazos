using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertaDePrazosLibrary.Entities
{
    public class Semestre
    {
        public int Id { get; set; }
        public int ordem { get; set; }
        public int ano { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

    }
}
