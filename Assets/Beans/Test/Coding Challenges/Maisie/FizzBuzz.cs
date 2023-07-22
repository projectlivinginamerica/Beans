using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FizzBuzzSolution_Maisie : FizzBuzzSolution
{
    public override IList<string> FizzBuzz(int n) {

        if(n < 1 || n > 10000) return null; 

        List<string> answer = new List<string>();
        
        for(int i = 1; i <= n; i++)
        {
            if(i % 15 == 0) // can do this because both 3 and 5 have no shared factors, this would not work otherwise
            {
                answer.Add("FizzBuzz");
            }
            else if(i % 5 == 0)
            {
                answer.Add("Buzz");
            }
            else if(i % 3 == 0)
            {
                answer.Add("Fizz");
            }
            else
            {
                answer.Add(i.ToString());
            }
        }

        return answer;
    }
}
