using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertaDePrazosLibrary.Entities
{
    public class MatriculaDisciplina
    {
        public int Id { get; set; }
        public string AlunoId { get; set; }
        public string DisciplinaId { get; set; }
        public int SemestreId { get; set; }
        public int? Nota { get; set; }
        public bool Trancamento { get; set; } 

        public MatriculaDisciplina() { }
        public MatriculaDisciplina(string AlunoId, string DisciplinaId, int SemestreId, int Nota = 0, bool Trancamento = false)
        {
            this.AlunoId = AlunoId;
            this.DisciplinaId = DisciplinaId;
            this.SemestreId = SemestreId;
            this.Nota = Nota;
            this.Trancamento = Trancamento;
        }

    }
}
