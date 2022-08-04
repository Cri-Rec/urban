using System;
using System.IO;

namespace urban
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] Codes = File.ReadAllLines(args[0]);
            foreach(string Code in Codes)
            {
                CmdCheck(Code);
            }
            DirectoryInfo di = new DirectoryInfo(@"Var\");
            FileInfo[] files = di.GetFiles();
            foreach (FileInfo file in files)
            {
                file.Delete();
            }
        }
        ///<summary>
        ///处理命令
        ///</summary>
        static void CmdCheck(string code)
        {
            string[] codes = code.Split('：');
            string arg = ""; //初始化
            string fun = codes[0];
            if(codes.Length > 0)
            {
                for(int i = 1;i < codes.Length; i++)
                {
                    arg = arg + codes[i] + " ";
                }
                arg = arg.Remove(arg.Length - 1, 1);
            }
            if(fun == "说")
            {
                string text = GetVar(arg);
                text = text.Remove(0, 1); //去除首字符
                text = text.Remove(text.Length - 1, 1); //去除尾字符
                Console.WriteLine(text);
            }
            else if (fun == "设置变量")
            {
                string[] argl = arg.Split('=');
                string data = "";
                for (int i = 1; i < codes.Length; i++)
                {
                    data = data + argl[i] + " ";
                }
                data = data.Remove(data.Length - 1, 1);
                SetVar(argl[0], data);
            }
        }
        ///<summary>
        ///获取变量
        ///</summary>
        static string GetVar(string strs)
        {
            string TopStr = strs.Substring(0, 1);
            string EndStr = strs.Substring(strs.Length - 1, 1);
            if(TopStr == @"”" && EndStr == @"”")
            {
                //这是一个字符串
                return strs;
            }else if (TopStr == "“" && EndStr == "”")
            {
                return strs;
            }
            else
            {
                //这是一个变量
                string VarData = File.ReadAllText(@"Var\" + strs + ".var");
                return VarData;
            }
        }
        /// <summary>
        /// 更改变量值
        /// </summary>
        static void SetVar(string VarName,string VarDat)
        {
            string VarData = GetVar(VarDat);
            File.WriteAllText(@"Var\" + VarName + ".var",VarData);
        }
    }
}
