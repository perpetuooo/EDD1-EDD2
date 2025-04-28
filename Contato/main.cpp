#include <iostream>
#include <locale.h>
#include <ctime>

using namespace std;

class Data {
    private:
        int dia;
        int mes;
        int ano;
    
    public:
        Data() {
            time_t t = time(nullptr);
            tm* now = localtime(&t);

            this->dia = now->tm_mday;
            this->mes = now->tm_mon + 1;
            this->ano = now->tm_year + 1900;
        }

        Data(int dia, int mes, int ano) {
            this->dia = dia;
            this->mes = mes;
            this->ano = ano;
        }

        void setDia(int dia) {
            this->dia = dia;
        }

        void setMes(int mes) {
            this->mes = mes;
        }

        void setAno(int ano) {
            this->ano = ano;
        }

        int getDia() {
            return this->dia;
        }

        int getMes() {
            return this->mes;
        }

        int getAno() {
            return this->ano;
        }
};


class Contato {
    private:
        string email;
        string nome;
        string telefone;
        Data dtnasc;

    public:
        Contato(string email, string nome, string telefone, Data dtnasc) {
            this->email = email;
            this->nome = nome;
            this->telefone = telefone;
            this->dtnasc = dtnasc;
        }

        string getNome() {
            return this->nome;
        }

        string getEmail() {
            return this->email;
        }

        string getTelefone() {
            return this->telefone;
        }

        int getIdade() {
            Data hoje;

            if (hoje.getDia() >= dtnasc.getDia() && hoje.getMes() >= dtnasc.getMes()) {
                return hoje.getAno() - dtnasc.getAno();
            
            } else {
                return hoje.getAno() - dtnasc.getAno() - 1;
            }
        }
};


int main() {
    int opt, n, d, m, a;
    string email, nome, telefone;
    
    Contato* contatos[5];
    n = 0;

    while (1) {
        cout << "\n--- PROJETO CONTATO ---\n\n0. Finalizar\n1. Criar contato (max 5)\n2. Exibir contatos\n\n";
        cin >> opt;

        if (opt == 0) {
            for (int i = 0; i < n; i++) {
                delete contatos[i];
            }

            return 0;

        } else if (opt == 1) {
            if (n >= 5) {
                cout << "\n*** NAO EH POSSIVEL CRIAR MAIS CONTATOS... ***\n";

            } else {
                cout << "\nDigite o seu nome: ";
                cin >> nome;
                cout << "Digite o seu email: ";
                cin >> email;
                cout << "Digite o seu telefone: ";
                cin >> telefone;
                cout << "Digite o dia da sua data de nascimento: ";
                cin >> d;
                cout << "Digite o mes da sua data de nascimento: ";
                cin >> m;
                cout << "Digite o ano da sua data de nascimento: ";
                cin >> a;

                Data nasc(d, m, a);
                contatos[n] = new Contato(email, nome, telefone, nasc);

                n++;
            }

        } else if (opt == 2) {
            for (int i=0; i<n; i++) {
                cout << "\n --- CONTATO N" << (i + 1) << " --- \n"
                "NOME: " << contatos[i]->getNome() << endl << 
                "EMAIL: " << contatos[i]->getEmail() << endl << 
                "TELEFONE: " << contatos[i]->getTelefone() << endl <<
                "IDADE: " << contatos[i]->getIdade() << endl;
            }

        } else {
            cout << "\n*** OPCAO INVALIDA, TENTE NOVAMENTE! ***\n";
        }
    }
}