using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlertaDePrazosLibrary.Entities
{
    [PrimaryKey(nameof(AlunoId), nameof(DisciplinaId))]
    public class MatriculaDisciplina
    {
        public string AlunoId { get; set; }
        public string DisciplinaId { get; set; }
        public int SemestreId { get; set; }

        public MatriculaDisciplina() { }
        public MatriculaDisciplina(string AlunoId, string IdSemestre, int SemestreId)
        {
            this.AlunoId = AlunoId;
            this.DisciplinaId = IdSemestre;
            this.SemestreId = SemestreId;
        }

    }
}
