#include <iostream>
#include <windows.h>
#include <locale.h>
#include <fstream>

using namespace std;

int main() {
    string line;

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

                cout << sobrenome << ", " << resto << endl;

            } else {
                cout << line << endl;
            }
        }
    }

    arq.close();
    return 0;
}