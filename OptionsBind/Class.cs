using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionsBind
{
    public class Class
    {
        public string ClassNo { get; set; }

        public string ClassName { get; set; }

        public List<Student> Students { get; set; }
    }

    public class Student
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
