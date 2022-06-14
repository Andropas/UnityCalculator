using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{   
    public List<string> expression = new List<string>();

    private List<List<int>> Get_scopes(List<string> l)
    {
        List<List<int>> scopes = new List<List<int>>();
        for (int c=0; c<l.Count; c++)
            if (l[c].Contains("("))
                scopes.Add(new List<int>() {c});
            else if (l[c] == ")")
            {
                for (int s=1; s<scopes.Count+1; s++)
                    if (!(scopes[scopes.Count-s].Count >= 2))
                    {
                        scopes[scopes.Count-s].Add(c);
                        break;
                    }
            }
        return scopes;
    }

    private bool Inside_pairs(int i, List<List<int>> pairs)
    {
        foreach (List<int> p in pairs)
            if (i > p[0] && i < p[1])
                return true;
        return false;
    }

    private decimal Pow(decimal a, decimal b)
    {
        return (decimal)Mathf.Pow((float)a, (float)b);
    }

    public decimal Factorial(int n)
    {
        decimal res = 1;
        for (int i=1; i<=n; i++)
            res *= i;
        return res;
    }

    public decimal Calculate(List<string> ex, int sign=1)
    {
        List<List<int>> scopes = Get_scopes(ex);

        for (int i=0; i<ex.Count; i++)
            if (ex[i] == "+" && !Inside_pairs(i, scopes))
                return Calculate(ex.GetRange(0, i)) + Calculate(ex.GetRange(i+1,ex.Count-i-1));
        for (int i=0; i<ex.Count; i++)
            if (ex[i] == "-" && !Inside_pairs(i, scopes))
                return Calculate(ex.GetRange(0, i), sign) + Calculate(ex.GetRange(i+1,ex.Count-i-1), -1);
        for (int i=0; i<ex.Count; i++)
            if (ex[i] == "*" && !Inside_pairs(i, scopes))
                return Calculate(ex.GetRange(0, i)) * Calculate(ex.GetRange(i+1,ex.Count-i-1));
        for (int i=0; i<ex.Count; i++)
            if (ex[i] == "/" && !Inside_pairs(i, scopes))
                return Calculate(ex.GetRange(0, i)) / Calculate(ex.GetRange(i+1,ex.Count-i-1));
        for (int i=0; i<ex.Count; i++)
            if (ex[i] == "%" && !Inside_pairs(i, scopes))
                return Calculate(ex.GetRange(0, i)) % Calculate(ex.GetRange(i+1,ex.Count-i-1));
        for (int i=0; i<ex.Count; i++)
            if (ex[i] == "^" && !Inside_pairs(i, scopes))
                return Pow(Calculate(ex.GetRange(0, i)), Calculate(ex.GetRange(i+1,ex.Count-i-1)));
        
        if (ex[0] == "(")
            return Calculate(ex.GetRange(1, scopes[0][1]-1))*sign;
        else if (ex[0] == "sin(")
            return (decimal)(int)(Mathf.Sin((float)Calculate(ex.GetRange(1, scopes[0][1]-1)))*1000000)/1000000*sign;
        else if (ex[0] == "cos(")
            return (decimal)Mathf.Cos((float)Calculate(ex.GetRange(1, scopes[0][1]-1)))*sign;
        else if (ex[0] == "tg(")
            return (decimal)(int)(Mathf.Tan((float)Calculate(ex.GetRange(1, scopes[0][1]-1)))*1000000)/1000000*sign;
        else if (ex[0] == "exp(")
            return (decimal)Mathf.Exp((float)Calculate(ex.GetRange(1, scopes[0][1]-1)))*sign;
        else if (ex[0] == "sqrt(")
            return (decimal)Mathf.Sqrt((float)Calculate(ex.GetRange(1, scopes[0][1]-1)))*sign;
        else if (ex[0] == "log(")
            return (decimal)Mathf.Log10((float)Calculate(ex.GetRange(1, scopes[0][1]-1)))*sign;
        else if (ex[0] == "!")
            return Factorial((int)Calculate(ex.GetRange(1, ex.Count-1)))*sign;
        return decimal.Parse(ex[0])*sign;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
