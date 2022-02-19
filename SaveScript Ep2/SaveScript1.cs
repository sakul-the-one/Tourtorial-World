using LGCON;
using UnityEngine;
using System;

public class SaveScript1 : MonoBehaviour
{
    public GameObject HousePrefarb;
    SaveScript saver = new SaveScript("D:\\T.LGCon");

    public void SaveUI()
    {
        GameObject[] Houses = GameObject.FindGameObjectsWithTag("House");

        string[] mes =
        {
         "len:" + Houses.Length
        };

        saver.Write(mes);

        for (int i = 0; i < Houses.Length; i++)
        {
            Houses[i].SendMessage("Save", i);
        }
    }

    public void LoadUI()
    {
        //Destroy Old
        GameObject[] Houses = GameObject.FindGameObjectsWithTag("House");

        foreach (GameObject G in Houses)
        {
            Destroy(G);
        }

        //Load New
        saver.Read();
        for (int i = 0; i <= Convert.ToInt32(saver.GetVariable("len")) - 1; i++)
        {
            try
            {
                float x = Convert.ToSingle(saver.GetVariable("T" + i, "x"));
                float y = Convert.ToSingle(saver.GetVariable("T" + i, "y"));
                float z = Convert.ToSingle(saver.GetVariable("T" + i, "z"));

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
