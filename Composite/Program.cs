using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    internal class Program
    {   
        static void Main(string[] args)
        {
            Employee omerfaruk = new Employee { Name = "Ömer Faruk CAVLAK" };
            
            Employee halil = new Employee { Name = "Halil CAVLAK" };

            omerfaruk.AddSubordinate(halil);

            Employee recep = new Employee { Name = "Recep CAVLAK" };

            Contractor mehmet = new Contractor { Name = "Mehmet"};

            recep.AddSubordinate(mehmet);

            
            omerfaruk.AddSubordinate(recep);

            Employee ahmet = new Employee { Name = "Ahmet" };

            halil.AddSubordinate(ahmet);

            Console.WriteLine(omerfaruk.Name);
            foreach (Employee manager in omerfaruk)
            {
                Console.WriteLine("=>  {0}", manager.Name);
                foreach (IPerson employee in manager)
                {
                    Console.WriteLine("=> => {0}", employee.Name);
                }
            }

            Console.ReadLine();
        }
    }

    interface IPerson
    {
        string Name { get; set; }
    }

    class Contractor : IPerson
    {
        public string Name { get; set; }
    }

    class Employee : IPerson, IEnumerable<IPerson>
    {
        List<IPerson> _subordinates = new List<IPerson>();

        public void AddSubordinate(IPerson person)
        {
            _subordinates.Add(person);
        }
        public void RemoveSubordinate(IPerson person)
        {
            _subordinates.Remove(person);
        }

        public IPerson GetSubordinate(int index)
        {
            return _subordinates[index];
        }

        public string Name { get; set; }

        public IEnumerator<IPerson> GetEnumerator()
        {
            foreach (var subordinate in _subordinates)
            {
                yield return subordinate;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
