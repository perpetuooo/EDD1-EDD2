#include <iostream>
#include <windows.h>
#include <locale.h>

using namespace std;

void gotoxy(int x, int y) {
    COORD coord = {x, y};
    SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), coord);
}

int main() {
    string msg;
    
    setlocale(LC_ALL, "");
    
    cout << "Mensagem: ";
    cin >> msg;
    
    int posX = (80 - msg.length()) / 2;
    
    for (int linha = 5; linha <= 20; linha++) {
        gotoxy(posX, linha);
        cout << msg;
    }
    
    gotoxy(0, 21);

    return 0;
}