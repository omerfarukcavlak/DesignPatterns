using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChainofResponsibility
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            VicePresident vicePresident = new VicePresident();
            President president = new President();

            manager.SetSuccesor(vicePresident);
            vicePresident.SetSuccesor(president);

            Expense expense1 = new Expense{Detail = "Training1", Amount = 50};
            manager.HandleExpense(expense1);
            Expense expense2 = new Expense{Detail = "Training2", Amount = 150};
            manager.HandleExpense(expense2);
            Expense expense3 = new Expense{Detail = "Training3", Amount = 1500};
            manager.HandleExpense(expense3);
            Console.ReadLine();
        }
    }


    class Expense
    {
        public string Detail { get; set; }
        public decimal Amount { get; set; }

    }

    abstract class ExpenseHandlerBase
    {
        protected ExpenseHandlerBase Succesor;
        public abstract void HandleExpense(Expense expense);
        public void SetSuccesor(ExpenseHandlerBase succesor)
        {
            Succesor = succesor;
        }

    }


    class Manager : ExpenseHandlerBase
    {
        public override void HandleExpense(Expense expense)
        {
            if (expense.Amount<100)
            {
                Console.WriteLine( "{0} : Manager handled the expense!" , expense.Detail);
            }
            else if(Succesor != null)
            {
                Succesor.HandleExpense(expense);
            }
        }
    }

    class VicePresident : ExpenseHandlerBase
    {
        public override void HandleExpense(Expense expense)
        {
            if (expense.Amount > 100 && expense.Amount <= 1000)
            {
                Console.WriteLine("{0} : Vice President handled the expense!", expense.Detail);
            }
            else if (Succesor != null)
            {
                Succesor.HandleExpense(expense);
            }
        }
    }
    class President : ExpenseHandlerBase
    {
        public override void HandleExpense(Expense expense)
        {
            if (expense.Amount > 1000)
            {
                Console.WriteLine("{0} : President handled the expense!", expense.Detail);
            }
        }
    }
}
