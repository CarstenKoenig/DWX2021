using System;

static class Funktionen
{
   public static string Hallo(string name)
     => $"Hallo {name}!";

   public static int AddNotCurried(int a, int b)
     => a + b;
   public static Func<int,int> AddCurried(int a)
     => b => a + b;

   public static Func<int, int> Add10
      = AddCurried(10);

   public static Func<int,int> Add10alt1
      => FunExtensions.PartialApply<int,int,int>(AddNotCurried, 10);

   public static Func<int,int> Add10alt2
      => FunExtensions.Curry<int,int,int>(AddNotCurried)(10);

   public static void PrintName(string punct, string name)
      => Console.WriteLine($"Hallo {name}{punct}");

   public static Action<string> PrintNameExcl(string name)
      => FunExtensions.PartialApply<string,string>(PrintName, "!");

   public static int Add(this int a, int b)
     => a + b;

   public static int Pipe =
      1.Add(2).Add(3);
}

public static class FunExtensions
{
   public static Func<T1,Func<T2,T3>> Curry<T1,T2,T3>(Func<T1,T2,T3> f)
      => v1 => v2 => f(v1,v2);
   public static Func<T2,T3> PartialApply<T1,T2,T3>(Func<T1,T2,T3> f, T1 val1)
      => val2 => f(val1, val2);

   public static Action<T2> PartialApply<T1,T2>(Action<T1,T2> f, T1 val1)
      => val2 => f(val1, val2);

}