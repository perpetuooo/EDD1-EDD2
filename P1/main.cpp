#include <iostream>
#include <locale.h>
#include <ctime>
#include <fstream>

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


string print_bibliografico(string nome) {
    string line = nome;
    string abr = "";

    int blank = line.find_last_of(' ');
    if (blank != string::npos) {
        string sobrenome = line.substr(blank + 1);
        string resto = line.substr(0, blank);

        for (char &c : sobrenome) {
            if (c >= 'a' && c <= 'z') {
                c = c - 'a' + 'A';
            }
        }

        int startBlank = resto.find_first_of(' ');
        if (startBlank != string::npos) {
            abr = resto.substr(0, startBlank + 1);
            string nomesMeio = resto.substr(startBlank + 1);

            size_t pos = 0;
            while ((pos = nomesMeio.find(' ')) != string::npos) {
                if (!nomesMeio.empty()) {
                    abr += nomesMeio[0];
                    abr += ". ";
                }
                nomesMeio = nomesMeio.substr(pos + 1);
            }
            if (!nomesMeio.empty()) {
                abr += nomesMeio[0];
                abr += ".";
            }
        } else {
            abr = resto;
        }

        return sobrenome + ", " + abr;
    } else {
        return nome;  // Caso tenha apenas um nome
    }
}


int main() {
    int i, media, d, m, a;
    string email, nome, telefone, linha;

    Contato* contatos[10];
    string nomes[10];
    string emails[10];

    srand(time(0));
    ifstream arquivoNomes("nomes.txt");
    ifstream arquivoEmails("emails.txt");

    for (int i = 0; i < 10 && getline(arquivoNomes, linha); i++) {
        nomes[i] = linha;
    }
    arquivoNomes.close();

    for (int i = 0; i < 10 && getline(arquivoEmails, linha); i++) {
        emails[i] = linha;
    }
    arquivoEmails.close();

    for (int i = 0; i < 10; i++) {
        nome = nomes[i];
        email = emails[i];
        telefone = "9" + to_string(rand() % 900000000 + 100000000);

        d = rand() % 28 + 1;
        m = rand() % 12 + 1;
        a = rand() % (2015 - 1950 + 1) + 1950;

        Data nasc(d, m, a);
        contatos[i] = new Contato(email, nome, telefone, nasc);
    }

    // for (i=0; i<10; i++) {
    //     cout << "\nDigite o seu nome: ";
    //     cin >> nome;
    //     cout << "Digite o seu email: ";
    //     cin >> email;
    //     cout << "Digite o seu telefone: ";
    //     cin >> telefone;
    //     cout << "Digite o dia da sua data de nascimento: ";
    //     cin >> d;
    //     cout << "Digite o mes da sua data de nascimento: ";
    //     cin >> m;
    //     cout << "Digite o ano da sua data de nascimento: ";
    //     cin >> a;

    //     Data nasc(d, m, a);
    //     contatos[i] = new Contato(email, nome, telefone, nasc);
    // }

    // Média de idade.
    media = 0;

    for (int i = 0; i < 10; i++) {
        media += contatos[i]->getIdade();
    }

    media /= 10;

    cout << "A média de idade entre os contatos é de " << media << " anos." << endl;

    // Contatos cuja idade é maior que a média.
    for (i=0; i<10; i++) {
        if (contatos[i]->getIdade() > media) {
            cout << "O contato " << print_bibliografico(contatos[i]->getNome()) << " tem " << contatos[i]->getIdade() << " anos." << endl;
        }
    }

    // Contatos maiores de idade.
    for (i=0; i<10; i++) {
        if (contatos[i]->getIdade() >= 18) {
            cout << "O contato " << print_bibliografico(contatos[i]->getNome()) << " é maior de idade." << endl;
        }
    }

    // Contato(s) mais velho(S).
    int maxIdade = 0;

    for (int i = 0; i < 10; i++) {
        if (contatos[i]->getIdade() > maxIdade) {
            maxIdade = contatos[i]->getIdade();
        }
    }

    cout << "\nContato(s) mais velho(s) com " << maxIdade << " anos:\n";

    for (int i = 0; i < 10; i++) {
        if (contatos[i]->getIdade() == maxIdade) {
            cout << print_bibliografico(contatos[i]->getNome()) << "\n";
        }
    }


    for (int i = 0; i < 10; i++) {
        delete contatos[i];
    }

    return 0;
}