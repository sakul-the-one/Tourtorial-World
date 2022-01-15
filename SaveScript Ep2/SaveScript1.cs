using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LGCON;
using System.Text;
using System;

public class SaveScript1 : MonoBehaviour
{
    public GameObject HousePrefarb;
    SaveScript ss = new SaveScript("D:\\T.LGCon");

    public void SaveUI() 
    {
        GameObject[] Houses = GameObject.FindGameObjectsWithTag("House");

        string[] mes =
        {
         "len:" + Houses.Length
        };

        ss.Write(mes);

        for (int i =0; i<Houses.Length; i++) 
        {
            Houses[i].SendMessage("Save", i);
        }
    }
    public void LoadUI()
    {
        //Destroy Old
        GameObject[] Houses = GameObject.FindGameObjectsWithTag("House");
        ss.Read();
        foreach (GameObject G in Houses) 
        {
            Destroy(G);
        }
        //Load New
        for (int i = 0; i <= Convert.ToInt32(ss.GetVariable("len")) - 1; i++)
        {
            try
            {
                float x = Convert.ToSingle(ss.GetVariable("T" + i, "x"));
                float y = Convert.ToSingle(ss.GetVariable("T" + i, "y"));
                float z = Convert.ToSingle(ss.GetVariable("T" + i, "z"));

                GameObject House = Instantiate(HousePrefarb, new Vector3(x, y, z), transform.rotation);
                House.name = "T" + i;
            }
            catch (Exception ex)
            {
                Debug.LogWarning(ex.Message + "T:" + i);
            }
        }
    }
}
