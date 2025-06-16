#include <iostream>
using namespace std;
#define MAX 100

struct Fila
{
    int nos[MAX];
    int ini;
    int fim;
};

Fila* init()
{
    Fila *f = new Fila;
    f->ini = 0;
    f->fim = 0;
    return f;
}

int isEmpty(Fila *f)
{
    return (f->ini == f->fim);
}

int incrementa(int i)
{
    return (i == MAX-1 ? 0 : ++i);
}

int count(Fila *f)
{
    int qtde = 0;
    int i = f->ini;
    while (i != f->fim)
    {
        qtde++;
        i = incrementa(i);
    }
    return qtde;
}

int enqueue(Fila *f, int v)
{
    int podeEnfileirar = (incrementa(f->fim) != f->ini);
    if (podeEnfileirar)
    {
        f->nos[f->fim] = v;
        f->fim = incrementa(f->fim);
    }
    return podeEnfileirar;
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
        ret = f->nos[f->ini];
        f->ini = incrementa(f->ini);
    }
    return ret;
}

void freeFila(Fila *f)
{
    delete f;
}

int main()
{
    Fila* senhasGeradas = init();
    Fila* senhasAtendidas = init();
    
    int opcao;
    int proximaSenha = 1;
    
    do {
        cout << "\n--- SISTEMA DE FILAS ---" << endl;
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
                    cout << "Nao e possivel sair, ainda existem " << count(senhasGeradas) << " senhas." << endl;
                    opcao = -1;
                }
                break;
                
            case 1:
                if (enqueue(senhasGeradas, proximaSenha))
                {
                    cout << "Senha gerada: " << proximaSenha << endl;
                    proximaSenha++;
                }
                else
                {
                    cout << "Nao foi possivel gerar nova senha. Fila cheia!" << endl;
                }
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