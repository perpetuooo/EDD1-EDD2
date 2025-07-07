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

void cs(int a[], int n) {
    int maxN = a[0];

    for (int i = 1; i < n; i++) {
        if (a[i] > maxN)
            maxN = a[i];
    }

    int count[maxN + 1] = {0};

    for (int i = 0; i < n; i++) {
        count[a[i]]++;
    }


    int index = 0;

    for (int i = 0; i <= maxN; i++) {
        while (count[i] > 0) {
            a[index++] = i;
            count[i]--;
        }
    }
}

int main() {
    int a[MAX] = {7, 2, 1, 4, 4, 4, 8, 5};
    int n = length(a);

    cs(a, n);

    cout << "Array ordenado:\n";
    for (int i = 0; i < n; i++) {
        cout << a[i] << " ";
    }

    return 0;
}
