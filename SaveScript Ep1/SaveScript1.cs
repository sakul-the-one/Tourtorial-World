using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LGCON;
using System.Text;

public class SaveScript1 : MonoBehaviour
{
    private void Start()
    {
        //FileStream fs = new FileStream("D:\\t.txt", FileMode.Create);
        //StreamWriter SW = new StreamWriter(fs);
        //SW.WriteLine("Hi: Hello There!\nTT: Hi, this is a Message!\n");
        //SW.Close();
        string path = "D:\\t.txt";
        string[] mes =
        {
        "Name: Sakul",
        "Age: 1001",
        "Health: 2001"
        };

        SaveScript t = new SaveScript();
        t.path = path;
        t.Write(mes);
        t.Read();
        t.Log();
        Debug.Log("............................................");
        Debug.Log(t.GetVariable("Name"));
        Debug.Log(t.GetVariable("Name..zug"));
    }
}
