using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteAlways]
public class FizzBuzzTester : MonoBehaviour
{
    [DelayedAttribute]
    public string fizzBuzzClassName;
    public List<int> testValues = new List<int>();

    private FizzBuzzSolution fbObj;
    [TextArea(minLines: 30, maxLines: 70)]
    public string fizzBuzzOutput;

    private Type fizzBuzzType;

    // Update is called once per frame
    void Update()
    {
        fizzBuzzOutput = "";
        if(fizzBuzzClassName == null) return;
        fizzBuzzType = Type.GetType(fizzBuzzClassName);
        if(fizzBuzzType == null) return;

        fbObj = (FizzBuzzSolution) Activator.CreateInstance(fizzBuzzType);
        if(fbObj == null) return;

        for (int i = 0; i < testValues.Count; i++)
        {
            fizzBuzzOutput += "Test Value: " + testValues[i].ToString() + ": \n";
            IList<string> output = fbObj.FizzBuzz(testValues[i]);
            if(output == null) continue;
            for (int j = 0; j < output.Count; j++)
            {
                fizzBuzzOutput += "   " + output[j] + "\n";
            }
            fizzBuzzOutput += "\n";
        }
    }
}

public abstract class FizzBuzzSolution
{
    public abstract IList<string> FizzBuzz(int n);
}
