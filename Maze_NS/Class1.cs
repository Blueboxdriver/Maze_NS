using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maze_NS
{
    public class Person
    {
        private string name;
        private int age;

        public Person(string initialName, int initialAge)
        {
            this.age = initialAge;
            this.name = initialName;
        }

        public void PrintPerson()
        {
            Console.WriteLine(this.name + ", age " + this.age + " years");
        }

        public void GrowOlder()
        {
            if (this.age >= 100)
            {
                Console.WriteLine("This is the end of the line, " + this.name + " There's no more aging for you. Life is finite, and you've reached the end.");
            } else
            {
                this.age++;
            }
        }

        public int GetAge()
        {
            return this.age;
        }

    }
}
