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

Fila *init()
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

int count(Fila *f)
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

int main()
{
    Fila *senhasGeradas = init();
    Fila *senhasAtendidas = init();
    
    int opcao;
    int proximaSenha = 1;
    
    do {
        cout << "\n--- SISTEMA DE ATENDIMENTO ---" << endl;
        cout << "Senhas aguardando atendimento: " << count(senhasGeradas) << endl;
        cout << "0. Sair" << endl;
        cout << "1. Gerar senha" << endl;
        cout << "2. Realizar atendimento" << endl;
        cout << "Escolha uma opcao: ";
        cin >> opcao;
        
        switch(opcao)
        {
            case 0:
                if (!isEmpty(senhasGeradas))
                {
                    cout << "Nao e possivel encerrar, ainda existem " << count(senhasGeradas) << " senhas." << endl;
                    opcao = -1;
                }
                break;
                
            case 1:
                enqueue(senhasGeradas, proximaSenha);
                cout << "Senha gerada: " << proximaSenha << endl;
                proximaSenha++;
                break;
                
            case 2:
                if (!isEmpty(senhasGeradas))
                {
                    int senhaAtendida = dequeue(senhasGeradas);
                    cout << "\nAtendendo senha: " << senhaAtendida << endl;
                    enqueue(senhasAtendidas, senhaAtendida);
                }
                else
                {
                    cout << "\nNenhuma senha para atender!" << endl;
                }
                break;
                
            default:
                cout << "Opcao invalida!" << endl;
                break;
        }
        
    } while (opcao != 0);

    cout << "Total de senhas atendidas: " << count(senhasAtendidas) << endl;
    
    freeFila(senhasGeradas);
    freeFila(senhasAtendidas);
    
    return 0;
}