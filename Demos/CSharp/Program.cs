using System;

namespace CSharp
{
   class Program
   {
      static void Main(string[] args)
      {
         Console.WriteLine(Funktionen.Hallo("DWX"));
         Console.WriteLine(Funktionen.AddCurried(3)(5));
         Console.WriteLine(Funktionen.Add10(5));
         Console.WriteLine(Funktionen.Add10alt1(5));
         Console.WriteLine(Funktionen.Add10alt2(5));
         Console.WriteLine(Funktionen.Pipe);
      }
   }
}
