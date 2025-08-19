#include <iostream>
#include <string>
using namespace std;

struct Funcionario
{
	int prontuario;
	string nome;
	double salario;
	Funcionario* ant;
};

Funcionario* init()
{
	return NULL;
}

bool isEmpty(Funcionario* lista)
{
	return (lista == NULL);
}

Funcionario* find(Funcionario* lista, int prontuario)
{
	Funcionario* aux = lista;
	while (aux != NULL && aux->prontuario != prontuario)
	{
		aux = aux->ant;
	}
	return aux;
}

Funcionario* insert(Funcionario* lista, int prontuario, string nome, double salario)
{
	if (find(lista, prontuario) != NULL)
	{
		cout << "Erro: Prontuario ja cadastrado.\n";
		return lista;
	}
	Funcionario* novo = new Funcionario();
	novo->prontuario = prontuario;
	novo->nome = nome;
	novo->salario = salario;
	novo->ant = lista;
	return novo;
}

Funcionario* remove(Funcionario* lista, int prontuario)
{
	Funcionario* aux = lista;
	Funcionario* apoio = NULL;

	while (aux != NULL && aux->prontuario != prontuario)
	{
		apoio = aux;
		aux = aux->ant;
	}

	if (aux == NULL)
	{
		cout << "Funcionario com prontuario " << prontuario << " nao encontrado.\n";
		return lista;
	}

	if (apoio == NULL)
	{
		lista = aux->ant;
	}
	else
	{
		apoio->ant = aux->ant;
	}
	delete aux;
	cout << "Funcionario removido com sucesso.\n";
	return lista;
}

void listar(Funcionario* lista)
{
	Funcionario* aux = lista;
	double total = 0.0;

	cout << "\n------ Lista de Funcionarios ------\n";
	while (aux != NULL)
	{
		cout << "Prontuario: " << aux->prontuario << endl;
		cout << "Nome: " << aux->nome << endl;
		cout << "Salario: R$ " << aux->salario << "\n" << endl;
		total += aux->salario;
		aux = aux->ant;
	}
	cout << "Total dos salarios: R$ " << total << endl;
	cout << "-----------------------------------\n";
}

int main()
{
	Funcionario* lista = init();
	int opcao;

	while (true)
	{
		cout << "\n====== MENU ======\n";
		cout << "0. Sair\n";
		cout << "1. Incluir Funcionario\n";
		cout << "2. Excluir Funcionario\n";
		cout << "3. Pesquisar Funcionario\n";
		cout << "4. Listar Funcionarios\n";
		cout << "Escolha uma opcao: ";
		cin >> opcao;

		if (opcao == 0)
		{
			cout << "Saindo...\n";
			break;
		}

		else if (opcao == 1)
		{
			int pront;
			string nome;
			double salario;

			cout << "Prontuario: ";
			cin >> pront;
			cout << "Nome: ";
			cout << "Salario: ";
			cin >> salario;

			lista = insert(lista, pront, nome, salario);
		}

		else if (opcao == 2)
		{
			int pront;
			cout << "Prontuario do funcionario a excluir: ";
			cin >> pront;
			lista = remove(lista, pront);
		}

		else if (opcao == 3)
		{
			int pront;
			cout << "Prontuario do funcionario a pesquisar: ";
			cin >> pront;
			Funcionario* f = find(lista, pront);
			if (f != NULL)
			{
				cout << "Funcionario encontrado:\n";
				cout << "Nome: " << f->nome << endl;
				cout << "Salario: R$ " << f->salario << endl;
			}
			else
			{
				cout << "Funcionario nao encontrado.\n";
			}
		}

		else if (opcao == 4)
		{
			listar(lista);
		}

		else
		{
			cout << "Opcao invalida!\n";
		}
	}

	return 0;
}
