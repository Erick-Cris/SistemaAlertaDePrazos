using Microsoft.EntityFrameworkCore;

namespace ApiUFU.Models
{
    [PrimaryKey(nameof(AlunoId), nameof(DisciplinaId))]
    public class MatriculaDisciplina
    {
        public string AlunoId { get; set; }
        public string DisciplinaId { get; set; }
        public int IdSemestre { get; set; }

    }
}
