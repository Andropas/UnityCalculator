using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public Text inputText;
    public Text resultText;
    public Text buttonText;
    public GameObject calculator;
    public string value;
    public static List<string> ex;

    public AudioSource audioSource;

    public void Pi()
    {
        inputText.text += "Pi";
        ex.Add(Mathf.PI.ToString());

        Update_result();
    }

    public void e()
    {
        inputText.text += "e";
        ex.Add(Mathf.Epsilon.ToString());

        Update_result();
    }

    public void On_click()
    {
        inputText.text += value;
        if ((Char.IsDigit(value, 0) || value == ",") && (ex.Count != 0) && Char.IsDigit(ex[ex.Count-1], 0))
        {
            ex[ex.Count-1] = ex[ex.Count-1] + value;
            
        }
        else
            ex.Add(value);
        
        Update_result();
    }

    private void Update_result()
    {
        try
        {
            resultText.text = calculator.GetComponent<Calculator>().Calculate(ex).ToString();
        }
        catch (Exception _e)
        {

        }
    }

    public void Clear()
    {
        inputText.text = "";
        resultText.text = "";
        ex = new List<string>();

        Update_result();
    }

    public void Result()
    {
        
        ex = new List<string>() {calculator.GetComponent<Calculator>().Calculate(ex).ToString()};
        inputText.text = ex[0];

        Update_result();
        
    }

    public void Delete()
    {   
        if ((!inputText.text.Contains("Pi") || inputText.text.IndexOf("Pi") != inputText.text.Length - 2)
        && (!inputText.text.Contains("e") || inputText.text.IndexOf("e") != inputText.text.Length - 1)
        && (!inputText.text.Contains("sin(") || inputText.text.IndexOf("sin(") != inputText.text.Length - 4)
        && (!inputText.text.Contains("cos(") || inputText.text.IndexOf("cos(") != inputText.text.Length - 4)
        && (!inputText.text.Contains("tg(") || inputText.text.IndexOf("tg(") != inputText.text.Length - 3)
        && (!inputText.text.Contains("ctg(") || inputText.text.IndexOf("ctg(") != inputText.text.Length - 4))
        {
            inputText.text = inputText.text.Substring(0, inputText.text.Length - 1);
            if (Char.IsDigit(ex[ex.Count-1][0]) || ex[ex.Count-1][0] == ',')
            {
                ex[ex.Count-1] = ex[ex.Count-1].Substring(0, ex[ex.Count-1].Length-1);
                if (ex[ex.Count - 1].Length == 0)
                    ex.RemoveAt(ex.Count-1);
            }
            else
                ex.RemoveAt(ex.Count-1);
        }
        else
        {
            if (inputText.text.Contains("Pi") && inputText.text.IndexOf("Pi") == inputText.text.Length - 2)
                inputText.text = inputText.text.Substring(0, inputText.text.Length - 2);
            else if (inputText.text.Contains("e") && inputText.text.IndexOf("e") == inputText.text.Length - 1)
                inputText.text = inputText.text.Substring(0, inputText.text.Length - 1);
            else if (inputText.text.Contains("sin(") && inputText.text.IndexOf("sin(") == inputText.text.Length - 4)
                inputText.text = inputText.text.Substring(0, inputText.text.Length - 4);
            else if (inputText.text.Contains("cos(") && inputText.text.IndexOf("cos(") == inputText.text.Length - 4)
                inputText.text = inputText.text.Substring(0, inputText.text.Length - 4);
            else if (inputText.text.Contains("tg(") && inputText.text.IndexOf("tg(") == inputText.text.Length - 3)
                inputText.text = inputText.text.Substring(0, inputText.text.Length - 3);
            else if (inputText.text.Contains("ctg(") && inputText.text.IndexOf("ctg(") == inputText.text.Length - 4)
                inputText.text = inputText.text.Substring(0, inputText.text.Length - 4);
            ex.RemoveAt(ex.Count-1);
        }

        Update_result();
    }

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.time = 0.15F;
        ex = calculator.GetComponent<Calculator>().expression;
        if (value == "")
            value = buttonText.text;
    }
}
