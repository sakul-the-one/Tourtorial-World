using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Threading.Tasks;

namespace LGCON {
    public class SaveScript
    {
        public string path;
        Dictionary<string, string> saved = new Dictionary<string, string>();
        private char[] Readed;
        private int CurrentChar = 0;
        public void Read()
        {
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader SR = new StreamReader(fs);
            string ThatWhatWeHadeReaded = SR.ReadToEnd();
            Readed = ThatWhatWeHadeReaded.ToCharArray(0, ThatWhatWeHadeReaded.Length);
            SR.Close();
            Analize();
        }
        public void Write(string[] mes, bool append = false) 
        {
            FileStream fs = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            foreach(string s in mes) 
            {
                sw.WriteLine(s);
            }
            sw.Close();
        }
        public void Log() 
        {
            foreach (KeyValuePair<string, string> st in saved) 
            {
                Debug.Log(st.Key+ " : "+ st.Value);
            }
        }
        public string GetVariable(string VarName) 
        {
            string Value = string.Empty;
            foreach (KeyValuePair<string, string> st in saved)
            {
                //Debug.Log(st.Key + " : " + st.Value);
                if (st.Key == VarName) Value = st.Value;
            }
            if (Value == string.Empty) 
            {
                Debug.LogError("Key Not Found: " + VarName);
                Value = "Key Not Found: " + VarName;
            }
            return Value;
        }
        private void Analize() 
        {
            try {
                string VarName = "";
                string Value = "";
                while (Readed[CurrentChar] != ':')
                {
                    VarName += Readed[CurrentChar];
                    CurrentChar++;
                }
                if (Readed[CurrentChar] == ':')
                {
                    CurrentChar++;
                    while (Readed[CurrentChar] != '\n')
                    {
                        Value += Readed[CurrentChar];
                        CurrentChar++;
                    }
                }
                if (Readed[CurrentChar] == '\n')
                {
                    saved.Add(VarName, Value);
                    CurrentChar++;
                        Analize();
                }
            }
            catch (Exception ex) {
                Debug.LogWarning(ex.Message);
            }
        }     
    }
}
