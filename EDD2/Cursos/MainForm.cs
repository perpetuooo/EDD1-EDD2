using System;
using System.Linq;
using System.Windows.Forms;
namespace EscolaWinForms
{
    public class MainForm : Form
    {
        private readonly Escola _escola = new Escola();
        private readonly ListBox _listBoxLog = new ListBox { Dock = DockStyle.Left, Width = 420 };
        private readonly Panel _panelBotoes = new Panel { Dock = DockStyle.Fill };
        public MainForm()
        {
            Text = "Gestão da Escola - WinForms";
            Width = 1000;
            Height = 640;
            Controls.Add(_panelBotoes);
            Controls.Add(_listBoxLog);
            InitializeButtons();
            Log("Aplicação iniciada. Use os botões para executar operações.");
        }
        private void InitializeButtons()
        {
            var nomesOpcoes = new[]
            {
                "0 - Sair",
                "1 - Adicionar curso",
                "2 - Pesquisar curso",
                "3 - Remover curso",
                "4 - Adicionar disciplina no curso",
                "5 - Pesquisar disciplina",
                "6 - Remover disciplina do curso",
                "7 - Matricular aluno na disciplina",
                "8 - Remover aluno da disciplina",
                "9 - Pesquisar aluno"
            };
            var y = 10;
            for (int i = 0; i < nomesOpcoes.Length; i++)
            {
                var botao = new Button
                {
                    Text = nomesOpcoes[i],
                    Tag = i,
                    Left = 10,
                    Top = y,
                    Width = 520,
                    Height = 36
                };
                botao.Click += Botao_Click;
                _panelBotoes.Controls.Add(botao);
                y += 46;
            }
        }
        private void Botao_Click(object sender, EventArgs e)
        {
            var botao = sender as Button;
            var opcao = (int)botao.Tag;
            switch (opcao)
            {
                case 0:
                    Close();
                    break;
                case 1:
                    OpcaoAdicionarCurso();
                    break;
                case 2:
                    OpcaoPesquisarCurso();
                    break;
                case 3:
                    OpcaoRemoverCurso();
                    break;
                case 4:
                    OpcaoAdicionarDisciplinaNoCurso();
                    break;
                case 5:
                    OpcaoPesquisarDisciplina();
                    break;
                case 6:
                    OpcaoRemoverDisciplinaDoCurso();
                    break;
                case 7:
                    OpcaoMatricularAlunoNaDisciplina();
                    break;
                case 8:
                    OpcaoRemoverAlunoDaDisciplina();
                    break;
                case 9:
                    OpcaoPesquisarAluno();
                    break;
            }
        }
        private void OpcaoAdicionarCurso()
        {
            var inputId = Prompt.ShowDialog("ID do curso (inteiro):", "Adicionar Curso");
            if (!int.TryParse(inputId, out var idCurso))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var descricao = Prompt.ShowDialog("Descrição do curso:", "Adicionar Curso");
            var curso = new Curso(idCurso, descricao);
            var sucesso = _escola.AdicionarCurso(curso);
            Log(sucesso ? $"Curso '{descricao}' (id {idCurso}) adicionado." : "Não foi possível adicionar o curso (capacidade atingida ou id duplicado).");
        }
        private void OpcaoPesquisarCurso()
        {
            var inputId = Prompt.ShowDialog("ID do curso para pesquisar:", "Pesquisar Curso");
            if (!int.TryParse(inputId, out var idCurso))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var curso = _escola.PesquisarCurso(idCurso);
            if (curso == null)
            {
                Log($"Curso com id {idCurso} não encontrado.");
                return;
            }
            var mensagem = $"Curso: {curso.Descricao} (id {curso.Id})\nDisciplinas ({curso.Disciplinas.Count}):\n" + string.Join("\n", curso.Disciplinas.Select(d => $"  - {d.Descricao} (id {d.Id}) - Alunos: {d.Alunos.Count}"));
            MessageBox.Show(mensagem, "Resultado da Pesquisa");
            Log($"Pesquisa curso id {idCurso} exibida.");
        }
        private void OpcaoRemoverCurso()
        {
            var inputId = Prompt.ShowDialog("ID do curso para remover:", "Remover Curso");
            if (!int.TryParse(inputId, out var idCurso))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var curso = _escola.PesquisarCurso(idCurso);
            if (curso == null)
            {
                Log($"Curso com id {idCurso} não encontrado.");
                return;
            }
            var sucesso = _escola.RemoverCurso(curso);
            Log(sucesso ? $"Curso id {idCurso} removido." : $"Não foi possível remover o curso id {idCurso} — verifique se não há disciplinas associadas.");
        }
        private void OpcaoAdicionarDisciplinaNoCurso()
        {
            var inputIdCurso = Prompt.ShowDialog("ID do curso onde adicionar a disciplina:", "Adicionar Disciplina");
            if (!int.TryParse(inputIdCurso, out var idCurso))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var curso = _escola.PesquisarCurso(idCurso);
            if (curso == null)
            {
                Log($"Curso id {idCurso} não encontrado.");
                return;
            }
            var inputIdDisc = Prompt.ShowDialog("ID da disciplina (inteiro):", "Adicionar Disciplina");
            if (!int.TryParse(inputIdDisc, out var idDisc))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var descricao = Prompt.ShowDialog("Descrição da disciplina:", "Adicionar Disciplina");
            var disciplina = new Disciplina(idDisc, descricao);
            var sucesso = curso.AdicionarDisciplina(disciplina);
            Log(sucesso ? $"Disciplina '{descricao}' (id {idDisc}) adicionada ao curso '{curso.Descricao}'." : "Não foi possível adicionar a disciplina (capacidade atingida ou id duplicado).");
        }
        private void OpcaoPesquisarDisciplina()
        {
            var inputIdCurso = Prompt.ShowDialog("ID do curso onde está a disciplina:", "Pesquisar Disciplina");
            if (!int.TryParse(inputIdCurso, out var idCurso))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var inputIdDisc = Prompt.ShowDialog("ID da disciplina:", "Pesquisar Disciplina");
            if (!int.TryParse(inputIdDisc, out var idDisc))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var disciplina = _escola.PesquisarDisciplinaNoCurso(idCurso, idDisc);
            if (disciplina == null)
            {
                Log($"Disciplina id {idDisc} não encontrada no curso id {idCurso}.");
                return;
            }
            var mensagem = $"Disciplina: {disciplina.Descricao} (id {disciplina.Id})\nAlunos matriculados ({disciplina.Alunos.Count}):\n" + string.Join("\n", disciplina.Alunos.Select(a => $"  - {a.Nome} (id {a.Id})"));
            MessageBox.Show(mensagem, "Resultado da Pesquisa");
            Log($"Pesquisa disciplina id {idDisc} no curso id {idCurso} exibida.");
        }
        private void OpcaoRemoverDisciplinaDoCurso()
        {
            var inputIdCurso = Prompt.ShowDialog("ID do curso onde está a disciplina:", "Remover Disciplina");
            if (!int.TryParse(inputIdCurso, out var idCurso))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var curso = _escola.PesquisarCurso(idCurso);
            if (curso == null)
            {
                Log($"Curso id {idCurso} não encontrado.");
                return;
            }
            var inputIdDisc = Prompt.ShowDialog("ID da disciplina a remover:", "Remover Disciplina");
            if (!int.TryParse(inputIdDisc, out var idDisc))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var disciplina = curso.PesquisarDisciplina(idDisc);
            if (disciplina == null)
            {
                Log($"Disciplina id {idDisc} não encontrada no curso id {idCurso}.");
                return;
            }
            var sucesso = curso.RemoverDisciplina(disciplina);
            Log(sucesso ? $"Disciplina id {idDisc} removida do curso id {idCurso}." : $"Não foi possível remover a disciplina id {idDisc} — verifique se não há alunos matriculados.");
        }
        private void OpcaoMatricularAlunoNaDisciplina()
        {
            var inputIdCurso = Prompt.ShowDialog("ID do curso da disciplina:", "Matricular Aluno");
            if (!int.TryParse(inputIdCurso, out var idCurso))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var curso = _escola.PesquisarCurso(idCurso);
            if (curso == null)
            {
                Log($"Curso id {idCurso} não encontrado.");
                return;
            }
            var inputIdDisc = Prompt.ShowDialog("ID da disciplina:", "Matricular Aluno");
            if (!int.TryParse(inputIdDisc, out var idDisc))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var disciplina = curso.PesquisarDisciplina(idDisc);
            if (disciplina == null)
            {
                Log($"Disciplina id {idDisc} não encontrada no curso id {idCurso}.");
                return;
            }
            var inputIdAluno = Prompt.ShowDialog("ID do aluno (inteiro):", "Matricular Aluno");
            if (!int.TryParse(inputIdAluno, out var idAluno))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var nomeAluno = Prompt.ShowDialog("Nome do aluno (use vazio para manter nome existente):", "Matricular Aluno");
            var aluno = _escola.ObterOuCriarAluno(idAluno, nomeAluno);
            if (!aluno.PodeMatricular(curso))
            {
                MessageBox.Show("Aluno não pode se matricular neste curso/disciplinas: ou já pertence a outro curso ou atingiu 6 disciplinas.");
                return;
            }
            if (disciplina.Alunos.Count >= Disciplina.CapacidadeMaximaAlunos)
            {
                MessageBox.Show("Disciplina está cheia (15 alunos)." );
                return;
            }
            if (aluno.CursoMatriculado == null)
                aluno.CursoMatriculado = curso;
            var sucesso = disciplina.MatricularAluno(aluno);
            Log(sucesso ? $"Aluno {aluno.Nome} (id {aluno.Id}) matriculado na disciplina {disciplina.Descricao} (id {disciplina.Id})." : $"Falha ao matricular aluno id {aluno.Id} na disciplina id {disciplina.Id}.");
        }
        private void OpcaoRemoverAlunoDaDisciplina()
        {
            var inputIdCurso = Prompt.ShowDialog("ID do curso da disciplina:", "Remover Aluno da Disciplina");
            if (!int.TryParse(inputIdCurso, out var idCurso))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var curso = _escola.PesquisarCurso(idCurso);
            if (curso == null)
            {
                Log($"Curso id {idCurso} não encontrado.");
                return;
            }
            var inputIdDisc = Prompt.ShowDialog("ID da disciplina:", "Remover Aluno da Disciplina");
            if (!int.TryParse(inputIdDisc, out var idDisc))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var disciplina = curso.PesquisarDisciplina(idDisc);
            if (disciplina == null)
            {
                Log($"Disciplina id {idDisc} não encontrada no curso id {idCurso}.");
                return;
            }
            var inputIdAluno = Prompt.ShowDialog("ID do aluno a remover:", "Remover Aluno");
            if (!int.TryParse(inputIdAluno, out var idAluno))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var aluno = _escola.PesquisarAluno(idAluno);
            if (aluno == null)
            {
                Log($"Aluno id {idAluno} não encontrado.");
                return;
            }
            var sucesso = disciplina.DesmatricularAluno(aluno);
            Log(sucesso ? $"Aluno id {idAluno} removido da disciplina id {idDisc}." : $"Falha ao remover aluno id {idAluno} da disciplina id {idDisc} (provavelmente não estava matriculado).");
        }
        private void OpcaoPesquisarAluno()
        {
            var inputIdAluno = Prompt.ShowDialog("ID do aluno para pesquisar:", "Pesquisar Aluno");
            if (!int.TryParse(inputIdAluno, out var idAluno))
            {
                MessageBox.Show("ID inválido.");
                return;
            }
            var aluno = _escola.PesquisarAluno(idAluno);
            if (aluno == null)
            {
                Log($"Aluno id {idAluno} não encontrado.");
                return;
            }
            var disciplinas = aluno.DisciplinasMatriculadas.Any()
                ? string.Join("\n", aluno.DisciplinasMatriculadas.Select(d => $"  - {d.Descricao} (id {d.Id})"))
                : "Nenhuma";
            var mensagem = $"Aluno: {aluno.Nome} (id {aluno.Id})\nCurso matriculado: {aluno.CursoMatriculado?.Descricao ?? "Nenhum"}\nDisciplinas:\n{disciplinas}";
            MessageBox.Show(mensagem, "Resultado da Pesquisa");
            Log($"Pesquisa aluno id {idAluno} exibida.");
        }
        private void Log(string texto)
        {
            var timeStamp = DateTime.Now.ToString("HH:mm:ss");
            _listBoxLog.Items.Insert(0, $"[{timeStamp}] {texto}");
        }
    }
}
