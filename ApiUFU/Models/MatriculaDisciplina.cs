using Microsoft.EntityFrameworkCore;

namespace ApiUFU.Models
{
    //[PrimaryKey(nameof(AlunoId), nameof(DisciplinaId))]
    public class MatriculaDisciplina
    {
        public int Id { get; set; }
        public string AlunoId { get; set; }
        public string DisciplinaId { get; set; }
        public int SemestreId { get; set; }
        public int Nota { get; set; }

        public MatriculaDisciplina() { }
        public MatriculaDisciplina(string AlunoId, string DisciplinaId, int SemestreId, int Nota = 0)
        {
            this.AlunoId = AlunoId;
            this.DisciplinaId = DisciplinaId;
            this.SemestreId = SemestreId;
            this.Nota = Nota;
        }

    }
}
