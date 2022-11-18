#include <iostream>
using namespace std;

int main()
{
    int myNumber = Foo1(2, 2);
    cout << myNumber;
}



int Foo1(int first, int second) 
{
    return pow(first * second, 2);
}