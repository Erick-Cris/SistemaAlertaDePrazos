using Microsoft.EntityFrameworkCore;

namespace ApiUFU.Models
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
