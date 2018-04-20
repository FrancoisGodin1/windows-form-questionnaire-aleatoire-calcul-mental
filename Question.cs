using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestScholaNova
{
   public  class Question
    {
        private int nb1;
        private int nb2;
        private char operateur;
        private Random _random = new Random();
        private static List<char> _lesOperateurs = new List<char> { '+', '-', '/', '*' };
        private static List<Division> _divisionsEntieres = GetDisvisionsEntieres();

        public Question()
        {
            operateur = _lesOperateurs[_random.Next(0, 3)];
            if (operateur == '/') // si c'est une division je prend une division entiere aléatoire de ma liste
            {
                int index = _random.Next(_divisionsEntieres.Count);
                nb1 = _divisionsEntieres[index].nb1;
                nb2 = _divisionsEntieres[index].nb2;
            }
            else // sinon je génère 2 arguments aléatoires
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
        /// <summary>
        /// Genere une list d'objet division composé uniquement de division entière sur un intervale de nombre de 1 à 20
        /// </summary>
        private static List<Division> GetDisvisionsEntieres()
        {
            List<Division> listDiv = new List<Division>();
            for (int i=1;i<20;i++)
                for (int j = 1; j < 20; j++)
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
