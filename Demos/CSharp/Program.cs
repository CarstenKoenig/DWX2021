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

         var max = new Records.Person("Max", "Mustermann");
         var min = max with { FirstName = "Min" };

         Console.WriteLine(min);

         min.Deconstruct(out var fn, out var ln);
         var (fn2, ln2) = min;

         Console.WriteLine(PatternMatchRecords(min));
         Console.WriteLine(PatternMatchRecords(max));

         var nothing = Maybe<int>.Nothing;
         var just42 = Maybe<int>.Just(42);

         Console.WriteLine(nothing.WithDefault(0));
         Console.WriteLine(just42.WithDefault2(0));

      }

      static string PatternMatchRecords (Records.Person person) =>
         person switch
            {
               (FirstName: "Min", LastName: _) => "Hi Min",
               { LastName: "Mustermann" } => "Hey a Mustermann",
               Records.Person p => $"Hello {p.FirstName}",
               // nicht nötig - C# merkt das nicht
               _ => $"Hello {person.FirstName}"
            };
   }
}
