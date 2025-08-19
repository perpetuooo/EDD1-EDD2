#include <iostream>
#include <windows.h>
#include <locale.h>
#include <fstream>

using namespace std;

int main() {
    string line;
    string abr;

    setlocale(LC_ALL, "");
    
    ifstream arq("nomes.txt");

    if (!arq.is_open()) {
        cout << "Erro ao abrir o arquivo\n";

        return 1;
    }
    while (getline(arq, line)) {
        if (!line.empty()) {
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
                        if (nomesMeio.empty()) break;

                        abr += nomesMeio[0];
                        abr += ". ";

                        if (pos == string::npos) break;

                        nomesMeio = nomesMeio.substr(pos + 1);
                    }

                    if (!nomesMeio.empty()) {
                        abr += nomesMeio[0];
                        abr += ".";
                    }
                    
                } else {
                    abr = resto;
                }
                
                cout << sobrenome << ", " << abr << endl;

            } else {
                cout << line << endl;
            }
        }
    }

    arq.close();
    return 0;
}