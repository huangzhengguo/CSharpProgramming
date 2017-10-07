using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chapter15
{
    public class TypeView
    {
        static StringBuilder OutputText = new StringBuilder();

        public static void AnalyzeType(Type t)
        {
            AddToOutput("Type Name: " + t.Name);
            AddToOutput("Full Name: " + t.FullName);
            AddToOutput("Namespace: " + t.Namespace);

            Type tBase = t.BaseType;
            if (t.BaseType != null)
            {
                AddToOutput("Base Type: " + tBase.Name);
            }

            Type tUnderlyingSystem = t.UnderlyingSystemType;
            if (t.UnderlyingSystemType != null)
            {
                AddToOutput("UnderlyingSystem Type: " + tUnderlyingSystem.Name);
            }

            AddToOutput("\nPIBLIC MEMBERS:");
            MemberInfo[] Members = t.GetMembers();
            foreach (MemberInfo NextMember in Members)
            {
                AddToOutput(NextMember.DeclaringType + " " +
                    NextMember.MemberType + " " + NextMember.Name);
            }

            MessageBox.Show(OutputText.ToString(), "Analysis of type" + t.Name);
        }

        public static void AddToOutput(string text)
        {
            OutputText.Append("\n" + text);
        }
    }
}
