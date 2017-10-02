using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter10.DictionarySample
{
    [Serializable]
    public class Employee
    {
        private string name;
        private decimal salary;
        private readonly EmployeeId id;

        public Employee(EmployeeId id,string name,decimal salary)
        {
            this.id = id;
            this.name = name;
            this.salary = salary;
        }

        public override string ToString()
        {
            return string.Format("{0}: {1,-20} {2:C}", id.ToString(), name, salary);
        }
    }
}
