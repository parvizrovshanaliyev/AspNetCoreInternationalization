using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace CultureInfoDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //
            Console.OutputEncoding= Encoding.UTF8;
            //
            CultureInfo en = new CultureInfo("en-US");
            CultureInfo.CurrentCulture = en;
            Console.WriteLine(CultureInfo.CurrentCulture);
            //
            DisplayCurrentCulture();
            // DateTimeDisplayDemo();
            DateTimeParseDemo();
            StringSortedDemo();
            NumberParsingDemo();
            /////////////////
            CultureInfo.CurrentCulture = new CultureInfo("bs-Latn-BA");
            DisplayCurrentCulture();
            // DateTimeDisplayDemo();
            DateTimeParseDemo();
            StringSortedDemo();
            NumberParsingDemo();

        }

        static void DisplayCurrentCulture()
        {
            Console.WriteLine("++++++++++++++++++++++++++++++");
            Console.WriteLine(CultureInfo.CurrentCulture.Name);
            Console.WriteLine(CultureInfo.CurrentCulture.DisplayName);
            Console.WriteLine("++++++++++++++++++++++++++++++");
        }

        static void DateTimeDisplayDemo()
        {
            Console.WriteLine(DateTime.Now);
        }

        static void DateTimeParseDemo()
        {
            string dateString = "24.12.2016";
            DateTime date = DateTime.Parse(dateString,
                new CultureInfo("bs-Latn-BA"));
            Console.WriteLine(date.ToString("D")); 
        }

        static void StringSortedDemo()
        {
            string[] surnames =
            {
                "Zoltan",
                "Anderson",
                "Çelik", // non-English
                "Davis",
                "Şeriş", // non-English
                "Cooper"
            };

            foreach (var surname in surnames
                .OrderBy(x=>x , StringComparer.Ordinal))
            {
                Console.WriteLine(surname);
            }
        }

        static void NumberParsingDemo()
        {
            string numberAsString = "1.500";
            decimal number = decimal.Parse(numberAsString , 
                new CultureInfo("en-US")) + 1;
            // demeli pul vahidleri ile ishleyerken  gelen price bir basa ui 
            // otursek client oz culture sini deyishse pul vahidi ona cevrile biler
            // yanlish yol
            Console.WriteLine("{0:C}",number);
            // dogru yol
            Console.WriteLine(number.ToString("C"), new CultureInfo("en-US"));
        }
    }
}
