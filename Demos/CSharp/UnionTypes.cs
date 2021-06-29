using System;

public abstract class Maybe<T>
{
   public abstract Tres Match<Tres>(Func<Tres> onNothing, Func<T, Tres> onJust);

   public override string ToString()
      => Match(() => "Nothing", v => $"Just {v}");
   private Maybe() { }
   public sealed class NothingCase : Maybe<T>
   {
      internal NothingCase() { }

      public override Tres Match<Tres>(Func<Tres> onNothing, Func<T, Tres> onJust)
      {
         return onNothing();
      }
   }
   public sealed class JustCase : Maybe<T>
   {
      public T Value { get; init; }
      internal JustCase(T value)
      {
         Value = value;
      }
      public override Tres Match<Tres>(Func<Tres> onNothing, Func<T, Tres> onJust)
      {
         return onJust(Value);
      }
   }

   public T WithDefault(T defaultValue)
      => Match(() => defaultValue, x => x);

   public T WithDefault2(T defaultValue)
      => this switch
      {
         JustCase j => j.Value,
         NothingCase => defaultValue,
         // C# besteht hierauf:
         _ => throw new InvalidOperationException()
      };

   private static readonly NothingCase _Nothing = new NothingCase();
   public static Maybe<T> Just(T value) => new JustCase(value);
   public static Maybe<T> Nothing => _Nothing;
}

public static class MaybeExtensions
{
   public static Maybe<B> SelectMany<A, B>(this Maybe<A> maybe, Func<A, Maybe<B>> f)
      => maybe.Match(() => Maybe<B>.Nothing, f );

   public static Maybe<V> SelectMany<T, U, V>(this Maybe<T> m, Func<T, Maybe<U>> k, Func<T, U, V> s)
      => m.SelectMany(x => k(x).SelectMany(y => Maybe<V>.Just(s(x, y))));
}