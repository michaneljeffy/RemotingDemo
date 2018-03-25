using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotingDemo.CommonService.Model
{
    [Serializable]
    public class Person
    {
        public Person()
        {

        }

        private string name;
        private string sex;
        private int age;

        public string Name
        {
            get { return name; }

            set { name = value; }
        }

        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }

        public int Age
        {
            get { return age; }

            set { age = value; }
        }
    }
}
