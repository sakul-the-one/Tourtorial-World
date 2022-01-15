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
        Dictionary<string, Dictionary<string, string>> clases = new Dictionary<string, Dictionary<string, string>>();


        private char[] Readed;
        private int CurrentChar = 0;

        public SaveScript(string Path) 
        {
            this.path = Path;
        }

        public void Read()
        {
            saved.Clear();
            clases.Clear();
            FileStream fs = new FileStream(path, FileMode.Open);
            StreamReader SR = new StreamReader(fs);
            string ThatWhatWeHadeReaded = SR.ReadToEnd();
            Readed = ThatWhatWeHadeReaded.ToCharArray(0, ThatWhatWeHadeReaded.Length);
            SR.Close();
            Analize();
            
        }
        public void Write(string[] mes, bool append = false) 
        {
            if(!append) 
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                foreach(string s in mes) 
                {
                    sw.WriteLine(s);
                }
                sw.Close();
            }
            else 
            {
                FileStream fs = new FileStream(path, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                foreach (string s in mes)
                {
                    sw.WriteLine(s);
                }
                sw.Close();
            }
        }
        public void Write(string ClassName ,string[] mes, bool append = false)
        {
            if (!append)
            {
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(ClassName+"(");
                foreach (string s in mes)
                {
                    sw.WriteLine(s);
                }
                sw.WriteLine(")");
                sw.Close();
            }
            else
            {
                FileStream fs = new FileStream(path, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(ClassName + "(");
                foreach (string s in mes)
                {
                    sw.WriteLine(s);
                }
                sw.WriteLine(")");
                sw.Close();
            }
        }
        public void Log() 
        {
            foreach (KeyValuePair<string, string> st in saved) 
            {
                Debug.Log(st.Key+ " => "+ st.Value);
            }
        }
        public void LogAll()
        {
            foreach (KeyValuePair<string, Dictionary<string, string>> st in clases)
            {
                foreach(KeyValuePair<string, string> vars in st.Value) 
                {
                    Debug.Log("Class: '" + st.Key + "' VarName: '" + vars.Key + "' Value: '"+ vars.Value + "'");
                }
            }
        }
        public string GetVariable(string VarClass, string VarName)
        {
            bool foundClass = false;
            foreach (KeyValuePair<string, Dictionary<string, string>> st in clases)
            {
                if( VarClass == st.Key) 
                {
                    foundClass = true;
                    foreach (KeyValuePair<string, string> vars in st.Value)
                    {
                        if (vars.Key == VarName) return vars.Value;
                    }
                }
            }
            if (foundClass)
                Debug.LogWarning("Found Class, but not Value. Class Name: '" + VarClass + "' VarName: '" + VarName + "'");
            else Debug.LogWarning("Found nothing from both. Class Name: '" + VarClass + "' VarName: '" + VarName + "'");
            return "0";
        }
        public string GetVariable(string VarName) 
        {
            string Value = string.Empty;
            foreach (KeyValuePair<string, string> st in saved)
            {
                if (st.Key == VarName) return st.Value;
            }
            if (Value == string.Empty) 
            {
                Debug.LogError("Key Not Found: '" + VarName+"'");
                Value = "0";
            }
            return Value;
        }

        public string GetVarName()
        {
            string VarName = string.Empty;

            while (true)
            {
                VarName += Readed[CurrentChar];
                CurrentChar++;
                if (Readed[CurrentChar] == ':') break;
                else if (Readed[CurrentChar] == '(') break;
                else if (Readed[CurrentChar] == '\n') break;
            }

            return VarName.Replace("\n", "").Replace(" ", "");
        }

        public string ReadValue()
        {
            string value = string.Empty;

            while (Readed[CurrentChar] != '\n')
            {
                if (Readed[CurrentChar] != '\n')
                {
                    CurrentChar++;
                    value += Readed[CurrentChar];
                }
                else break;
            }
            return value; 
        }

        private void Analize() 
        {
            try {
                string VarName = "";
                VarName = GetVarName();

                if (Readed[CurrentChar] == ':')
                {
                    saved.Add(VarName, ReadValue());
                    Analize();
                }
                else if (Readed[CurrentChar] == '(')
                {
                    CurrentChar += 2;
                    clases.Add(VarName, Analize2());
                    Analize();
                }
                else 
                {
                    CurrentChar++;
                    Analize();
                }
            }
            catch (Exception ex) {

                Debug.LogWarning(ex.Message);
            }
        }
        private Dictionary<string, string> Analize2() 
        {
            Dictionary<string, string> RT = new Dictionary<string, string>();

            while (Readed[CurrentChar] != ')') 
            {
                string VarName = string.Empty;
                VarName = GetVarName();

                if (Readed[CurrentChar] == ':') RT.Add(VarName, ReadValue());
                else if (Readed[CurrentChar] == '\n') break;
            }
            CurrentChar++;
            return RT;
        }
    }
}
