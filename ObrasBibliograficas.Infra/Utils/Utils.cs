using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObrasBibliograficas.Infra.Utils
{
    public static class Utils
    {
        public static string ToAuthorName(this string name)
        {
            if (name.IndexOf(" ") < 0)
                return name.ToUpper();

            string[] preposicao = new string[] { "DE", "DO", "DA", "DOS", "DAS" };
            string[] parentesco = new string[] { "SOBRINHA", "JUNIOR", "FILHO", "SOBRINHO", "NETO", "NETA", "FILHA" };
            string[] array = name.Split(" ");

            string firstName = string.Empty;
            string lastName = string.Empty;

            for (int cont = 0; cont < array.Length; cont++)
            {
                if (cont == 0)
                    firstName += LetterUpper(array[cont]);

                else if (preposicao.Contains(array[cont].ToUpper()))
                    firstName += $" {LetterUpper(array[cont])} ";

                else if (parentesco.Contains(array[cont + 1].ToUpper()) && (parentesco.Contains(array[cont].ToUpper())
                    || !(cont == (array.Length - 1))))
                    lastName += $" {array[cont]} ";

                else if (cont == (array.Length - 1))
                    lastName += $" {array[cont]} ";
                else
                    firstName += $" {LetterUpper(array[cont])} ";
            }

            return string.Join(' ', string.Concat(lastName, ",").ToUpper(), firstName);
        }



        private static string LetterUpper(string name) => char.ToUpper(name[0]) + name.Substring(1);
    }
}
