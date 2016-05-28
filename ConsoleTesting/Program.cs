using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConsoleTesting
{
    public interface IOne
    {
        void One();
    }

    public interface ITwo
    {
        void Two();
    }

    public class OneTwo: IOne, ITwo
    {
        public void One()
        {
        }

        public void Two()
        {
        }
    }

    class Program
    {

        public static void  Main(string[] args)
        {
            IOne  one= new OneTwo();
            ITwo two = one as ITwo;

            Console.WriteLine(two is IOne);
            Console.Read();
        }

    }
}
