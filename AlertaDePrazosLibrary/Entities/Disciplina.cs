using AlertaDePrazosLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertaDePrazosLibrary.Entities
{
    public class Disciplina
    {
        public string Id { get; set; }
        public string? IdDisciplina { get; set; }
        public int CursoId { get; set; }
        public Periodo Periodo { get; set; }
        public string Titulo { get; set; }
        public bool Obrigatoria { get; set; }
        public int CargaHoraria { get; set; }

    }
}
