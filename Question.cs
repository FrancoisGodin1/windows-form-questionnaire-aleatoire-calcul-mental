using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestScholaNova
{
   public  class Question
    {
        int nb1;
        int nb2;
        char operateur;
        Random _random = new Random();
        static List<char> _lesOperateurs = new List<char> { '+', '-', '/', '*' };
        static List<Division> _divisionsEntieres = GetDisvisionsEntieres();

        public Question()
        {
            operateur = _lesOperateurs[_random.Next(0, 3)];
            if (operateur == '/')
            {
                int index = _random.Next(_divisionsEntieres.Count);
                nb1 = _divisionsEntieres[index].nb1;
                nb2 = _divisionsEntieres[index].nb2;
            }
            else
            {
                nb1 = _random.Next(0, 20);
                nb2 = _random.Next(0, 20);
            }
        }

        public double GetResultat()
        {
            switch (operateur)
            {
                case '+':
                    return nb1 + nb2;
                case '-':
                    return nb1 - nb2;
                case '/':
                    return nb1 / Convert.ToDouble(nb2);
                case '*':
                    return  nb1 * nb2;
                default:
                    return  0;
            }
        }

        private static List<Division> GetDisvisionsEntieres()
        {
            List<Division> listDiv = new List<Division>();
            for (int i=1;i<20;i++)
                for (int j = 2; j < 20; j++)
                    if (i != j && i % j == 0)
                    {
                        var div = new Division { nb1 = i, nb2 = j };
                        listDiv.Add((div));
                    }
            return listDiv;
        }
        public override string ToString()
        {
            return $"{nb1}{operateur}{nb2}";
        }
    }

    public class Division
    {
        public int nb1 { get; set; }
        public int nb2 { get; set; }
    }
}
