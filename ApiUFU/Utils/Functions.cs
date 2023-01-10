using AlertaDePrazosLibrary.Entities;
using AlertaDePrazosLibrary.Enums;

namespace ApiUFU.Utils
{
    public class Functions
    {
        public static List<Disciplina> ValidaDisciplinas(List<Disciplina> disciplinasAnteriores, List<Disciplina> disciplinasAtuais)
        {
            List<Disciplina> disciplinasPossiveis = new List<Disciplina>();

            foreach (Disciplina disciplina in disciplinasAtuais)
            {
                if (disciplina.IdDisciplina != null)
                {
                    if (disciplinasAnteriores.Where(x => x.Id == disciplina.IdDisciplina).Count() > 0)
                        disciplinasPossiveis.Add(disciplina);
                }
                else
                {
                    disciplinasPossiveis.Add(disciplina);
                }
            }
            return disciplinasPossiveis;
        }

        public static List<Disciplina> ValidaDisciplinasV2(Aluno aluno, List<MatriculaDisciplina> disciplinasAnteriores, List<Disciplina> disciplinasAtuais)
        {
            List<Disciplina> disciplinasPossiveis = new List<Disciplina>();

            foreach (Disciplina disciplina in disciplinasAtuais)
            {
                if (disciplina.IdDisciplina != null)
                {
                    if (disciplinasAnteriores.Where(x => x.DisciplinaId == disciplina.IdDisciplina && x.Nota >= 60 && x.AlunoId == aluno.Id).Count() > 0)
                        disciplinasPossiveis.Add(disciplina);
                }
                else
                {
                    disciplinasPossiveis.Add(disciplina);
                }
            }
            return disciplinasPossiveis;
        }

        public static bool VerificaEstagioNaoObrigatorio(Aluno aluno, List<MatriculaDisciplina> matriculaDisciplina, List<Disciplina> disciplinasBloqueantes)
        {
            List<MatriculaDisciplina> disciplinasEliminadas = matriculaDisciplina.Where(x => x.AlunoId == aluno.Id && x.Nota >= 60).ToList();
            if (disciplinasBloqueantes.Where(x => disciplinasEliminadas.Any(y => y.DisciplinaId == x.Id)).ToList().Count == disciplinasBloqueantes.Count)
                return true;
            else
                return false;
        }
        public static Estagio AlunoFazEstagio(Aluno aluno, List<MatriculaDisciplina> matriculaDisciplinas, List<Disciplina> disciplinas, List<Estagio> estagios, Semestre semestre, Curso curso, bool estagioEmAndamento)
        {
            List<MatriculaDisciplina> disciplinasEliminadas = matriculaDisciplinas.Where(x => x.AlunoId == aluno.Id && x.Nota >= 60).ToList();
            bool alunoEstagioObrigatorioAprovado = estagios.Where(x => x.AlunoId == aluno.Id && x.Status == StatusEstagio.Aprovado && x.Tipo == TipoEstagio.Obrigatorio).ToList().Count > 0;
            bool alunoEstagioNaoObrigatorioAprovado = estagios.Where(x => x.AlunoId == aluno.Id && x.Status == StatusEstagio.Aprovado && x.Tipo == TipoEstagio.NaoObrigatorio).ToList().Count > 0;
            bool podeFazerEstagioObrigatorio = disciplinas.Where(x => disciplinasEliminadas.Any(y => y.DisciplinaId == x.Id)).ToList().Count == disciplinas.Where(x => x.Periodo == Periodo.Primeiro || x.Periodo == Periodo.Segundo).ToList().Count;
            bool podeFazerEstagioNaoObrigatorio = disciplinas.Where(x => disciplinasEliminadas.Any(y => y.DisciplinaId == x.Id)).ToList().Count > 25;//ERICK - Verificar se o mesmo vale para os outros cursos

            if (!alunoEstagioObrigatorioAprovado)
            {
                if (new Random().Next(1, 11) > 5)
                    if (podeFazerEstagioObrigatorio)
                        return new Estagio()
                        {
                            AlunoId = aluno.Id,
                            CursoId = curso.Id,
                            Tipo = TipoEstagio.Obrigatorio,
                            DataInicio = semestre.DataInicio,
                            Status = estagioEmAndamento ? StatusEstagio.EmAndamento : (StatusEstagio) new Random().Next(1, 3)
                        };
            }
            else
            {
                if (!alunoEstagioNaoObrigatorioAprovado)
                    if (podeFazerEstagioNaoObrigatorio && new Random().Next(1, 11) > 7)
                        return new Estagio()
                        {
                            AlunoId = aluno.Id,
                            CursoId = curso.Id,
                            Tipo = TipoEstagio.NaoObrigatorio,
                            DataInicio = semestre.DataInicio,
                            Status = estagioEmAndamento ? StatusEstagio.EmAndamento : (StatusEstagio)new Random().Next(1, 3)
                        };
            }

            return null;
        }
    }
}
