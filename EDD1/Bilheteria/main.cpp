#include <iostream>
#include <time.h>
using namespace std;

#define FIL 15
#define POL 40 

void randomize(int **y) {
    int r, i, x;

    srand(time(0));

    for (i=0; i<FIL; i++) {
        for (x=0; x<10; x++) {
            r = (rand() % 100) % 40;
            y[i][r] = 1;
        }

    }

}

int main() {
    int **mat;
    int i, x, opt, fil, pol, fat, qtd;

    mat = new int*[FIL];

    for (i=0; i<FIL; i++) {
        mat[i] = new int [POL];
    }

    for (i=0; i<FIL; i++) {
        for (x=0; x<POL; x++) {
             mat[i][x] = 0;
        }
    }

    randomize(mat);

    while (1) {
        cout << "\n--- PROJETO BILHETERIA ---\n\n0. Finalizar\n1. Reservar poltrona\n2. Mapa de ocupacao\n3. Faturamento\n\n";
        cin >> opt;

        if (opt == 0) {
            for (i=0; i<FIL; ++i) {
                delete[] mat[i];
            }

            delete[] mat;
            return 0;

        } else if (opt == 1) {
            cout << "\nEscolha uma fileira (1-15): ";
            cin >> fil;
            cout << "Escolha uma poltrona (1-40): ";
            cin >> pol;

            if ((fil > 15 || fil < 0) || (pol > 40 || pol < 0)) {
                cout << "\n*** OPCAO INVALIDA, TENTE NOVAMENTE! ***\n";
    
            } else if (mat[fil - 1][pol - 1] == 1) {
                cout <<"\n*** POLTRONA OCUPADA, TENTE NOVAMENTE! ***\n";
            
            } else {
                mat[fil - 1][pol - 1] = 1;
                cout << "\nPOLTRONA RESERVADA COM SUCESSO!\n";
            }

        } else if (opt == 2) {
            for (i=0; i<FIL; i++) {
                for (x=0; x<POL; x++) {
                    if (mat[i][x] == 0) {
                        cout << " . ";
                    } else {
                        cout << " # ";
                    }
                }

                cout << endl;
            }

        } else if (opt == 3) {
            fat = qtd = 0;
            
            for (i=0; i<FIL; i++) {
                for (x=0; x<POL; x++) {
                     if (mat[i][x] == 1) {
                        qtd++;

                        if (i >= 1 && i < 6) {
                            fat += 50;

                        } else if (i >= 6 && i < 10) {
                            fat += 30;

                        } else {
                            fat += 15;
                        }
                     }
                }
            }

            cout << "\nQtde de lugares ocupados: " << qtd << "\nValor da bilheteria: R$" << fat << endl;

        } else {
            cout << "\n*** OPCAO INVALIDA, TENTE NOVAMENTE! ***\n";
        }
    }

    return 0;
}