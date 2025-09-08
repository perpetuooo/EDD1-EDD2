namespace EscolaWinForms
{
    public class Aluno
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Curso CursoMatriculado { get; set; }
        public System.Collections.Generic.List<Disciplina> DisciplinasMatriculadas { get; } = new System.Collections.Generic.List<Disciplina>();
        public Aluno(int id, string nome)
        {
            Id = id;
            Nome = nome ?? string.Empty;
        }
        public bool PodeMatricular(Curso curso)
        {
            if (CursoMatriculado == null) return DisciplinasMatriculadas.Count < 6;
            return CursoMatriculado == curso && DisciplinasMatriculadas.Count < 6;
        }
    }
}
