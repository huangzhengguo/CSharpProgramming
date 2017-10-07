using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chapter15;
using System.Reflection;
using System.Windows.Forms;

namespace LookUpWhatsNew
{
    internal class WhatsNewChecker
    {
        private static readonly StringBuilder outputText = new StringBuilder(1000);
        private static DateTime backDateTo = new DateTime(2010, 2, 1);

        static void Main(string[] args)
        {
            Assembly theAssembly = Assembly.Load("VectorClass");
            Attribute supportsAttribut = Attribute.GetCustomAttribute(theAssembly, typeof(SupportsWhatsNewAttribute));

            string name = theAssembly.FullName;
            AddToMessage("Assembly: " + name);

            if (supportsAttribut == null)
            {
                AddToMessage("This Assembly does not support WhatsNew attributes");
                return;
            }
            else
            {
                AddToMessage("Defined types:");
            }

            Type[] types = theAssembly.GetTypes();
            foreach (Type t in types)
            {
                DisplayTypeInfo(t);
            }

            MessageBox.Show(outputText.ToString(), "What\'s New sine" + backDateTo.ToLongDateString());
            Console.ReadLine();
        }

        private static void DisplayTypeInfo(Type type)
        {
            // make sure we only pick up classes
            if (!(type.IsClass))
            {
                return;
            }

            AddToMessage("\nclass" + type.Name);
            Attribute[] attributes = Attribute.GetCustomAttributes(type);
            if (attributes.Length == 0)
            {
                AddToMessage("No changes to this class");
            }
            else
            {
                foreach (Attribute attri in attributes)
                {
                    WriteAttributeInfo(attri);
                }
            }

            MemberInfo[] memberInfos = type.GetMethods();
            AddToMessage("CHANGES TO METHODS OF THIS CALSS");
            foreach (MethodInfo methodInof in memberInfos)
            {
                object[] attribs2 = methodInof.GetCustomAttributes(typeof(LastModifiedAttribute), false);

                if (attribs2 != null)
                {
                    AddToMessage(methodInof.ReflectedType + " " + methodInof.Name + "()");
                    foreach (Attribute nextAttr in attribs2)
                    {
                        WriteAttributeInfo(nextAttr);
                    }
                }
            }
        }

        private static void WriteAttributeInfo(Attribute attribute)
        {
            LastModifiedAttribute lastModifiedAttribute = attribute as LastModifiedAttribute;
            if (lastModifiedAttribute == null)
            {
                return;
            }

            DateTime dateTime = lastModifiedAttribute.DateModified;
            if (dateTime < backDateTo)
            {
                return;
            }

            AddToMessage("MODIFIDE: " + dateTime.ToLongDateString() + ":");
            AddToMessage(" " + lastModifiedAttribute.Changes);
            if (lastModifiedAttribute.Issues != null)
            {
                AddToMessage(" Outstanding issue: " + lastModifiedAttribute.Issues);
            }
        }

        static void AddToMessage(string text)
        {
            outputText.Append("\n" + text);
        }
    }
}
