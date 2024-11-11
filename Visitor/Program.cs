using System;
using System.Collections.Generic;

namespace Visitor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Manager omerfaruk = new Manager
            {
                Name = "Ömer Faruk",
                Salary = 1000
            };
            Manager halil = new Manager
            {
                Name = "Halil",
                Salary = 900
            };

            Worker timur = new Worker { Name = "Timur", Salary = 800 };
            Worker yusuf = new Worker { Name = "Yusuf", Salary = 800 };

            omerfaruk.Subordinates.Add(halil);
            halil.Subordinates.Add(timur);
            halil.Subordinates.Add(yusuf);

            OrganisationalStructure organisationalStructure = new OrganisationalStructure(omerfaruk);

            PayrolVisitor payrolVisitor = new PayrolVisitor();
            PayriseVisitor payriseVisitor = new PayriseVisitor();

            organisationalStructure.Accept(payrolVisitor);
            organisationalStructure.Accept(payriseVisitor);

            Console.ReadLine();
        }
    }

    class OrganisationalStructure
    {
        public EmployeeBase Employee;

        public OrganisationalStructure(EmployeeBase firstEmployee)
        {
            Employee = firstEmployee;
        }

        public void Accept(VisitorBase visitor)
        {
            Employee.Accept(visitor);
        }

    }

    abstract class EmployeeBase
    {
        public abstract void Accept(VisitorBase visitor);
        public string Name { get; set; }
        public decimal Salary { get; set; }
    }

    class Manager : EmployeeBase
    {
        public Manager()
        {
            Subordinates = new List<EmployeeBase>();
        }
        public List<EmployeeBase> Subordinates { get; set; }
        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);

            foreach (var employee in Subordinates)
            {
                employee.Accept(visitor);
            }
        }
    }

    class Worker : EmployeeBase
    {
        public override void Accept(VisitorBase visitor)
        {
            visitor.Visit(this);
        }
    }
    abstract class VisitorBase
    {
        public abstract void Visit(Worker worker);
        public abstract void Visit(Manager manager);
    }

    class PayrolVisitor : VisitorBase
    {
        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} paid {1}", worker.Name, worker.Salary);
        }

        public override void Visit(Manager manager)
        {
            Console.WriteLine("{0} paid {1}", manager.Name, manager.Salary);
        }
    }

    class PayriseVisitor : VisitorBase
    {
        public override void Visit(Worker worker)
        {
            Console.WriteLine("{0} salary increased to{1}", worker.Name, worker.Salary * (decimal)1.1);
        }

        public override void Visit(Manager manager)
        {
            Console.WriteLine("{0} salary increased {1}", manager.Name, manager.Salary * (decimal)1.2);
        }
    }
}
