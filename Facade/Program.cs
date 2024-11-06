using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CustomerManager customerManager = new CustomerManager();
            customerManager.Save();
            Console.ReadLine();
        }
    }

    class Logging:ILogging
    {
        public void Log()
        {
            Console.WriteLine("Looged!");
        }
    }

    internal interface ILogging
    {
        void Log();
    }

    class Caching:ICaching
    {
        public void Cache()
        {
            Console.WriteLine("Cached!");
        }
    }

    internal interface ICaching
    {
        void Cache();
    }

    class Validation : IValidate
    {
        public void Validate()
        {
            Console.WriteLine("Validated!");
        }
    }

    internal interface IValidate
    {
        void Validate();
    }

    class Authorize:IAuthorize
    {
        public void CheckUser()
        {
            Console.WriteLine("User Checked!");
        }
    }

    internal interface IAuthorize
    {
        void CheckUser();
    }

    class CustomerManager
    {

        CrossCuttongConcernsFacade _concerns;
        public CustomerManager()
        {
           _concerns = new CrossCuttongConcernsFacade();
        }

        public void Save()
        {
            _concerns.Caching.Cache();
            _concerns.Logging.Log();
            _concerns.Authorize.CheckUser();
            _concerns.Validation.Validate();
            Console.WriteLine("Saved!");
        }
    }

    class CrossCuttongConcernsFacade
    {
        public ILogging Logging;
        public ICaching Caching;
        public IAuthorize Authorize;
        public IValidate Validation;

        public CrossCuttongConcernsFacade()
        {
            Logging = new Logging();
            Caching = new Caching();
            Authorize = new Authorize();
            Validation = new Validation();
        }
    }

}
