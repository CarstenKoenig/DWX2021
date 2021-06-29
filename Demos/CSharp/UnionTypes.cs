using System;

public abstract class Maybe<T>
{
   public abstract Tres Match<Tres>(Func<Tres> onNothing, Func<T, Tres> onJust);

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