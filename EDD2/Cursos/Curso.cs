namespace EscolaWinForms
{
    public class Curso
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        private readonly System.Collections.Generic.List<Disciplina> _disciplinas = new System.Collections.Generic.List<Disciplina>();
        public System.Collections.Generic.IReadOnlyList<Disciplina> Disciplinas => _disciplinas;
        public const int CapacidadeMaximaDisciplinas = 12;
        public Curso(int id, string descricao)
        {
            Id = id;
            Descricao = descricao ?? string.Empty;
        }
        public bool AdicionarDisciplina(Disciplina disciplina)
        {
            if (disciplina == null) return false;
            if (_disciplinas.Count >= CapacidadeMaximaDisciplinas) return false;
            if (_disciplinas.Exists(d => d.Id == disciplina.Id)) return false;
            _disciplinas.Add(disciplina);
            return true;
        }
        public Disciplina PesquisarDisciplina(int id)
        {
            return _disciplinas.Find(d => d.Id == id);
        }
        public bool RemoverDisciplina(Disciplina disciplina)
        {
            if (disciplina == null) return false;
            if (_disciplinas.Exists(d => d.Id == disciplina.Id) == false) return false;
            if (disciplina.Alunos.Count > 0) return false;
            return _disciplinas.RemoveAll(d => d.Id == disciplina.Id) > 0;
        }
    }
}
