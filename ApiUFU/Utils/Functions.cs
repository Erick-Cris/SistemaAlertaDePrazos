using ApiUFU.Models;

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
    }
}
