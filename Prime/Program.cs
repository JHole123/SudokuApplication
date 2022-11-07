using System;
using System.Collections.Generic;

List<int> PrimeSet = PrimeSieve(50);
List<int> FirstDerivative = DiscreteDerivative(PrimeSet);
List<int> SecondDerivative = DiscreteDerivative(FirstDerivative);
foreach (int i in PrimeSet)
{
    Console.WriteLine(i);
}
Console.WriteLine("\n\n");
foreach (int i in FirstDerivative)
{
    Console.WriteLine(i);
}
Console.WriteLine("\n\n");

foreach (int i in SecondDerivative)
{
    Console.WriteLine(i);
}
Console.WriteLine("\n\n");


List<int> PrimeSieve(int n)
{
    List<int> primes = new(new int[] { 2, 3 });
    int c = 2;
    int x = 4;
    bool alpha = true;
    while (n > c)
    {
        foreach (int i in primes)
        {
            if (x % i == 0) { alpha = false; break; }
        }
        if (alpha) { primes.Add(x); c++; }
        x++;
        alpha = true;
    }
    return primes;
}

List<int> DiscreteDerivative(List<int> set)
{
    List<int> result = new();
    for (int i = 1; i < set.Count; i++)
    {
        result.Add(set[i] - set[i - 1]);
    }
    return result;
} 