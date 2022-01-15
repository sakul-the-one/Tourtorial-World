using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LGCON;

public class LoaadScript : MonoBehaviour
{
    public void Save(int value) 
    {
        SaveScript ss = new SaveScript("D:\\T.LGCon");
        string[] mes =
            {
        "x:"+transform.position.x,
        "y:"+transform.position.y,
        "z:"+transform.position.z
        };
        ss.Write("T"+value, mes, true);
    }
}
