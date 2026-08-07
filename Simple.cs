using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Simple : MonoBehaviour
{
    Dictionary<string, bool> dic = new Dictionary<string, bool>();
    Dictionary<string, List<int>> dics = new Dictionary<string, List<int>>();

    void Start()
    {
        dics.Add("11", new List<int>());
        dics["11"][0] = 1;

        dic.Add("1234", true);
        dic.Add("0070", false);        
        dic["1111"] = true;
        dic.TryGetValue("1235", out bool isC);
        dic.ContainsKey("");


        bool isCheck = dic["1234"];

        int result = 0;
        Sum(1, 2,ref result);

        Hashtable hash = new Hashtable();
        hash.Add("11", 11);
        hash.Add(1f, 11);
        hash.Add(2, new List<int>());
    }

    public void Sum(int a, int b, ref int c)
    {
        c = a + b;
    }

    void Update()
    {
        
    }
}
