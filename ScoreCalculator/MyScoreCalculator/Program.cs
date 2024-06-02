using System;
using System.Diagnostics;

public class MyScoreCalculator
{
    public int TotalScore(int[] arr)
    {
        int score = 0;
        foreach (var num in arr)
        {
            if (num % 2 == 0)
            {
                score += 1;
            }
            else
            {
                score += 3;
            }
            if (num == 8)
            {
                score += 5;
            }
        }
        return score;
    }

    public static void Main(string[] args)
    {
        var calculator = new MyScoreCalculator();

        int[] input1 = { 1, 2, 3, 4, 5 };
        int[] input2 = { 15, 25, 35 };
        int[] input3 = { 8, 8 };

        Console.WriteLine("Score for input1: " + calculator.TotalScore(input1)); 
        Console.WriteLine("Score for input2: " + calculator.TotalScore(input2)); 
        Console.WriteLine("Score for input3: " + calculator.TotalScore(input3)); 
    }
   
}
