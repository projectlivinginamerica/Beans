using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.WebPages;
using TMPro;
using UnityEngine;

public class FizzBuzz : MonoBehaviour
{

  public   TextMeshProUGUI text;

    List<string> GetFizzBuzz(int n)
    {
        if (n < 1 || n > 10000)
        {
            return null;
        }

        List<string> answer = new List<string>();
        for (int i = 1; i <= n; i++)
        {
            string newEntry = new string("");
            if (i % 3 == 0)
            {
                newEntry += "Fizz";
            }

            if (i % 5 == 0)
            {
                newEntry += "Buzz";
            }

            if (String.IsNullOrEmpty(newEntry))
            {
                newEntry += i.ToString();
            }

            answer.Add(newEntry);
        }

        return answer;  
    }

    // Start is called before the first frame update
    void Start()
    {
        List<string> FizzBuzzList = GetFizzBuzz(50);
        for (int i = 0; i < FizzBuzzList.Count; i++)
        {
            text.text +=  (i + 1) + " : " + FizzBuzzList[i] + "<br>";  
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
