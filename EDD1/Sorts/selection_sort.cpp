#include <iostream>

using namespace std;
#define MAX 100

int length(int a[]) {
    int n = 0;

    for (int i = 0; a[i] != '\0'; i++) {
        n++;
    }

    return n;
}

void selectionSort(int b[], int n) {
    for (int i = 0; i < n - 1; i++) {
        int minIndex = i;

        for (int j = i + 1; j < n; j++) {
            if (b[j] < b[minIndex]) {
                minIndex = j;
            }
        }

        int temp = b[i];
        b[i] = b[minIndex];
        b[minIndex] = temp;
    }
}

int main() {
    int b[MAX] = {49, 38, 58, 87, 34, 93, 26, 13};
    int n = length(b);

    selectionSort(b, n);

    cout << "Array ordenado:\n";
    for (int i = 0; i < n; i++) {
        cout << b[i] << " ";
    }

    return 0;
}
