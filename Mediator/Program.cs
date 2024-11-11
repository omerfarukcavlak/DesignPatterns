using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mediator mediator = new Mediator();

            Teacher omerfaruk = new Teacher(mediator);
            omerfaruk.Name = "Ömer Faruk";

            mediator.Teacher = omerfaruk;

            Student halil = new Student(mediator);
            halil.Name = "Halil";

            Student recep = new Student(mediator);
            recep.Name = "Recep";

            mediator.Students = new List<Student> { halil, recep };

            omerfaruk.SendNewImageUrl("slide1.jpg");
            halil.SendQuestion("is it true?");
            

            Console.ReadLine();
        }
    }

    abstract class CourseMember
    {
        protected Mediator Mediator;
        protected CourseMember(Mediator mediator)
        {
            Mediator = mediator;
        }
    }

    class Teacher : CourseMember
    {


        public Teacher(Mediator mediator) : base(mediator)
        {

        }
        public string Name { get; set; }

        public void RecieveQuestion(string question, Student student)
        {
            Console.WriteLine("{0} recieved quesiton from {1},{2}", Name, student.Name, question);
            AnswerQuestion("True",student);
        }

        public void SendNewImageUrl(string url)
        {
            Console.WriteLine("{0} changed slide {1}", Name, url);
            Mediator.UpdateImage(url);
        }

        public void AnswerQuestion(string answer, Student student)
        {
            Console.WriteLine("{0} answered question {1},{2}", Name, student.Name, answer);
            Mediator.SendAnswer(answer,student);
        }


    }

    class Student : CourseMember
    {
        public Student(Mediator mediator) : base(mediator)
        {
        }

        public string Name { get; set; }

        public void RecieveImage(string url)
        {
            Console.WriteLine("{0} received image : {1}", Name, url);
        }

        public void RecieveAnswer(string answer)
        {
            Console.WriteLine("{0} received answer {1}", Name, answer);
        }

        public void SendQuestion(string question)
        {
            Console.WriteLine("{0} asked this question : {1}",Name,question);
            Mediator.SendQuestion(question,this);
        }
    }

    class Mediator
    {
        public Teacher Teacher { get; set; }
        public List<Student> Students { get; set; }

        public void UpdateImage(string url)
        {
            foreach (var student in Students)
            {
                student.RecieveImage(url);
            }
        }

        public void SendQuestion(string question, Student student)
        {
            Teacher.RecieveQuestion(question, student);
        }

        public void SendAnswer(string answer, Student student)
        {
            student.RecieveAnswer(answer);
        }
    }
}
