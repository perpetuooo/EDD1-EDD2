namespace EscolaWinForms
{
    public class Escola
    {
        private readonly System.Collections.Generic.List<Curso> _cursos = new System.Collections.Generic.List<Curso>();
        public System.Collections.Generic.IReadOnlyList<Curso> Cursos => _cursos;
        public const int CapacidadeMaximaCursos = 5;
        private readonly System.Collections.Generic.List<Aluno> _alunos = new System.Collections.Generic.List<Aluno>();
        public System.Collections.Generic.IReadOnlyList<Aluno> Alunos => _alunos;
        public bool AdicionarCurso(Curso curso)
        {
            if (curso == null) return false;
            if (_cursos.Count >= CapacidadeMaximaCursos) return false;
            if (_cursos.Exists(c => c.Id == curso.Id)) return false;
            _cursos.Add(curso);
            return true;
        }
        public Curso PesquisarCurso(int id)
        {
            return _cursos.Find(c => c.Id == id);
        }
        public bool RemoverCurso(Curso curso)
        {
            if (curso == null) return false;
            var cursoExistente = PesquisarCurso(curso.Id);
            if (cursoExistente == null) return false;
            if (cursoExistente.Disciplinas.Count > 0) return false;
            return _cursos.RemoveAll(c => c.Id == curso.Id) > 0;
        }
        public Aluno ObterOuCriarAluno(int id, string nome)
        {
            var alunoExistente = _alunos.Find(a => a.Id == id);
            if (alunoExistente != null)
            {
                if (!string.IsNullOrWhiteSpace(nome) && alunoExistente.Nome != nome) alunoExistente.Nome = nome;
                return alunoExistente;
            }
            var novoAluno = new Aluno(id, nome);
            _alunos.Add(novoAluno);
            return novoAluno;
        }
        public Aluno PesquisarAluno(int id)
        {
            return _alunos.Find(a => a.Id == id);
        }
        public Disciplina PesquisarDisciplinaNoCurso(int idCurso, int idDisciplina)
        {
            var curso = PesquisarCurso(idCurso);
            return curso?.PesquisarDisciplina(idDisciplina);
        }
    }
}
