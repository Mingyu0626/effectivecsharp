using System;
using UnityEngine;
public class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public int Subtract(int a, int b)
    {
        return a - b;
    }

    public double Multiplication(int a, int b)
    {
        return (double)a * b;
    }

    public double? Division(int dividend, int divisor)
    {
        if (divisor == 0)
        {
            Debug.LogError("0으로 나눌 수는 없습니다.");
            return null;
        }
        return (double)dividend / divisor;
    }
}
