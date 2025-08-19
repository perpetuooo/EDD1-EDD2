#include <iostream>
using namespace std;

struct No
{
    int dado;
    No *prox;
};

struct Fila 
{
    No *ini;
    No *fim;
};

struct Guiche
{
    int id;
    Fila *senhasAtendidas;
    Guiche *prox;
};

struct ListaGuiches
{
    Guiche *inicio;
};

Fila *initFila()
{
    Fila *f = new Fila;
    f->ini = NULL;
    f->fim = NULL;
    return f;
}

int isEmpty(Fila *f)
{
    return (f->ini == NULL);
}

int countFila(Fila *f)
{
    int qtde = 0;
    No *no;
    no = f->ini;
    while (no != NULL)
    {
        qtde++;
        no = no->prox;
    }
    return qtde;
}

void enqueue(Fila *f, int v)
{
    No *no = new No;
    no->dado = v;
    no->prox = NULL;
    if (isEmpty(f))
    {
        f->ini = no;
    }
    else
    {
        f->fim->prox = no;		
    }
    f->fim = no;
}

int dequeue(Fila *f)
{
    int ret;
    if (isEmpty(f))
    {
        ret = -1;
    }
    else
    {
        No *no = f->ini;
        ret = no->dado;
        f->ini = no->prox;
        if (f->ini == NULL)
        {
            f->fim = NULL;
        }
        delete no;
    }
    return ret;
}

void freeFila(Fila *f)
{
    No *no;
    no = f->ini;
    while (no != NULL)
    {
        No *aux = no->prox;
        delete no;
        no = aux;
    }
    delete f;
}

ListaGuiches *initListaGuiches()
{
    ListaGuiches *lista = new ListaGuiches;
    lista->inicio = NULL;
    return lista;
}

int countGuiches(ListaGuiches *lista)
{
    int qtde = 0;
    Guiche *guiche = lista->inicio;
    while (guiche != NULL)
    {
        qtde++;
        guiche = guiche->prox;
    }
    return qtde;
}

Guiche *buscarGuiche(ListaGuiches *lista, int id)
{
    Guiche *guiche = lista->inicio;
    while (guiche != NULL)
    {
        if (guiche->id == id)
        {
            return guiche;
        }
        guiche = guiche->prox;
    }
    return NULL;
}

bool guicheExiste(ListaGuiches *lista, int id)
{
    return buscarGuiche(lista, id) != NULL;
}

void adicionarGuiche(ListaGuiches *lista, int id)
{
    if (guicheExiste(lista, id))
    {
        cout << "Guiche " << id << " ja existe!" << endl;
        return;
    }
    
    Guiche *novoGuiche = new Guiche;
    novoGuiche->id = id;
    novoGuiche->senhasAtendidas = initFila();
    novoGuiche->prox = lista->inicio;
    lista->inicio = novoGuiche;
    
    cout << "Guiche " << id << " aberto com sucesso!" << endl;
}

void listarSenhasGuiche(ListaGuiches *lista, int id)
{
    Guiche *guiche = buscarGuiche(lista, id);
    if (guiche == NULL)
    {
        cout << "Guiche " << id << " nao encontrado!" << endl;
        return;
    }
    
    cout << "\n--- SENHAS ATENDIDAS PELO GUICHE " << id << " ---" << endl;
    
    if (isEmpty(guiche->senhasAtendidas))
    {
        cout << "Nenhuma senha foi atendida por este guiche." << endl;
        return;
    }
    
    No *no = guiche->senhasAtendidas->ini;
    cout << "Senhas: ";
    while (no != NULL)
    {
        cout << no->dado;
        if (no->prox != NULL)
        {
            cout << ", ";
        }
        no = no->prox;
    }
    cout << endl;
    cout << "Total: " << countFila(guiche->senhasAtendidas) << " senhas" << endl;
}

int contarTotalSenhasAtendidas(ListaGuiches *lista)
{
    int total = 0;
    Guiche *guiche = lista->inicio;
    while (guiche != NULL)
    {
        total += countFila(guiche->senhasAtendidas);
        guiche = guiche->prox;
    }
    return total;
}

void freeListaGuiches(ListaGuiches *lista)
{
    Guiche *guiche = lista->inicio;
    while (guiche != NULL)
    {
        Guiche *aux = guiche->prox;
        freeFila(guiche->senhasAtendidas);
        delete guiche;
        guiche = aux;
    }
    delete lista;
}

int main()
{
    Fila *senhasGeradas = initFila();
    ListaGuiches *guiches = initListaGuiches();
    
    int opcao;
    int proximaSenha = 1;
    
    do {
        cout << "\n--- SISTEMA DE ATENDIMENTO v2.0 ---" << endl;
        cout << "Senhas aguardando atendimento: " << countFila(senhasGeradas) << endl;
        cout << "Guiches abertos: " << countGuiches(guiches) << endl;
        cout << "0. Sair" << endl;
        cout << "1. Gerar senha" << endl;
        cout << "2. Abrir guiche" << endl;
        cout << "3. Realizar atendimento" << endl;
        cout << "4. Listar senhas atendidas" << endl;
        cout << "Escolha uma opcao: ";
        cin >> opcao;
        
        switch(opcao)
        {
            case 0:
                if (!isEmpty(senhasGeradas))
                {
                    cout << "Nao e possivel encerrar, ainda existem " << countFila(senhasGeradas) << " senhas aguardando atendimento." << endl;
                    opcao = -1;
                }
                break;
                
            case 1:
                enqueue(senhasGeradas, proximaSenha);
                cout << "Senha gerada: " << proximaSenha << endl;
                proximaSenha++;
                break;
                
            case 2:
                {
                    int idGuiche;
                    cout << "Digite o ID do guiche: ";
                    cin >> idGuiche;
                    adicionarGuiche(guiches, idGuiche);
                }
                break;
                
            case 3:
                if (isEmpty(senhasGeradas))
                {
                    cout << "Nenhuma senha para atender!" << endl;
                    break;
                }
                
                if (countGuiches(guiches) == 0)
                {
                    cout << "Nenhum guiche esta aberto!" << endl;
                    break;
                }
                
                {
                    int idGuiche;
                    cout << "Digite o ID do guiche para realizar o atendimento: ";
                    cin >> idGuiche;
                    
                    Guiche *guiche = buscarGuiche(guiches, idGuiche);
                    if (guiche == NULL)
                    {
                        cout << "Guiche " << idGuiche << " nao encontrado!" << endl;
                    }
                    else
                    {
                        int senhaAtendida = dequeue(senhasGeradas);
                        enqueue(guiche->senhasAtendidas, senhaAtendida);
                    }
                }
                break;
                
            case 4:
                if (countGuiches(guiches) == 0)
                {
                    cout << "Nenhum guiche esta aberto!" << endl;
                    break;
                }
                
                {
                    int idGuiche;
                    cout << "Digite o ID do guiche para listar as senhas atendidas: ";
                    cin >> idGuiche;
                    listarSenhasGuiche(guiches, idGuiche);
                }
                break;
                
            default:
                cout << "Opcao invalida!" << endl;
                break;
        }
        
    } while (opcao != 0);

    cout << "\nTotal de senhas atendidas: " << contarTotalSenhasAtendidas(guiches) << endl;
    
    freeFila(senhasGeradas);
    freeListaGuiches(guiches);
    
    return 0;
}