#include <iostream>
#include <windows.h>
#include <locale.h>
#include <fstream>

using namespace std;

int main() {
    string msg, cleanMsg;
    bool p = true;

    setlocale(LC_ALL, "");
    
    cout << "Mensagem: ";
    cin >> msg;
    
    for (char c : msg) {
        if (c != ' ') {
            cleanMsg += toupper(c);
        }
    }
    
    int size = cleanMsg.size();

    for (int i = 0; i < size / 2; i++) {
        if (cleanMsg[i] != cleanMsg[size - 1 - i]) {
            p = false;
            break;
        }
    }
    
    if (p) {
        cout << "É um palíndromo!\n";
    } else {
        cout << "Não é um palíndromo...\n";;
    }
    
    return 0;
}