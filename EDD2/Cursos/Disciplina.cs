namespace EscolaWinForms
{
    public class Disciplina
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        private readonly System.Collections.Generic.List<Aluno> _alunos = new System.Collections.Generic.List<Aluno>();
        public System.Collections.Generic.IReadOnlyList<Aluno> Alunos => _alunos;
        public const int CapacidadeMaximaAlunos = 15;
        public Disciplina(int id, string descricao)
        {
            Id = id;
            Descricao = descricao ?? string.Empty;
        }
        public bool MatricularAluno(Aluno aluno)
        {
            if (aluno == null) return false;
            if (_alunos.Count >= CapacidadeMaximaAlunos) return false;
            if (_alunos.Exists(a => a.Id == aluno.Id)) return false;
            _alunos.Add(aluno);
            if (!aluno.DisciplinasMatriculadas.Contains(this)) aluno.DisciplinasMatriculadas.Add(this);
            return true;
        }
        public bool DesmatricularAluno(Aluno aluno)
        {
            if (aluno == null) return false;
            var removed = _alunos.RemoveAll(a => a.Id == aluno.Id) > 0;
            if (removed)
            {
                aluno.DisciplinasMatriculadas.RemoveAll(d => d.Id == this.Id);
                if (aluno.DisciplinasMatriculadas.Count == 0) aluno.CursoMatriculado = null;
            }
            return removed;
        }
    }
}
