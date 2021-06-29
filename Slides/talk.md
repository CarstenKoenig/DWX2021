---
author: Carsten König
title: Funktionale Programmierung in .NET
date: 01. Juli 2021
github: https://github.com/CarstenKoenig/DWX2021
---

# Funktionale Programmierung in _C#_

---

### Brauchen wir _F#_ überhaupt noch?

---

### Was ist "funktionale Programmierung"

(heute)

---

### Once upon a time ...

:::notes
Geschichte der funktionalen Programmierung
:::

![Elm](../images/Elm.png)

:::notes

Agenda:

:::

# Agenda

- Funktionen
- Datentypen / Immutability
- funktionale Muster / Unterstützung
- Zukunft

# Funktionen

---

## Definieren

<table><tr><td>

```fsharp

module Funktionen

// Inferenz 'a -> string
let hallo name =
  $"Hallo {name}!"

let hallo2 (name : string) =
  $"Hallo {name}!"

// Beispiel
Console.WriteLine (Funktionen.hallo "DWX")
```

</td><td>

```csharp
static class Funktionen
{
   public static string Hallo(string name)
     => $"Hallo {name}!";
}

// Beispiel
Console.WriteLine(Funktionen.Hallo("DWX"));
```

</td></tr></table>

:::notes

- beachte Typ-Inferenz
- Modul = statische Klasse
- Expressions
- C# OOP Entwickler vermutlich nicht glücklich

:::

---

## Currying

<table><tr><td>

```fsharp
// int -> int -> int
let add a b = 
  a + b

let add2 a =
  fun b -> a + b

Console.WriteLine (add 3 5)
```

</td><td>

```csharp
int AddNotCurried(int a, int b)
  => a + b;

Func<int,int> AddCurried(int a)
  => b => a + b;

Console.WriteLine(Funktionen.AddCurried(3)(5));
```

</td></tr></table>

:::notes

- Brot & Butter - Technik
- lasse `public static` weg
- beachte Signatur F# (`int` default) - könnte durch verwendung von `add 5.0` geändert werden
- kann `Func<int, Func<int,int>>` zugewiesen werden

:::

---

## Partial Applikation

<table><tr><td>

```fsharp
// add : int -> (int -> int)

let add10 =
  add 10

add10 5 // = 15
```

</td><td>

```csharp
// Func<int,int> AddCurried(int a)

Func<int, int> Add10
  => AddCurried(10);

Add10(5) // = 15
```

</td></tr></table>

:::notes

- Danke Currying ist *partial application* geschenkt
- Beachte Definition

:::

---

## Higher-Order

<table><tr><td>

```fsharp
// ('a*'b -> 'c) -> 'a -> 'b -> 'c
let curry f a b = f (a,b)

// ('a*'b -> 'c) -> 'a -> 'b -> 'c
let partialApply f a =
   fun b -> f (a,b)

// Beispiele
let add'(a,b) = a+b

let add10alt1 = 
   partialApply add' 10

let add10alt2 = 
   curry add' 10
```

</td><td>

```csharp
Func<T1,Func<T2,T3>> Curry<T1,T2,T3>(Func<T1,T2,T3> f)
   => v1 => v2 => f(v1,v2);

Func<T2,T3> PartialApply<T1,T2,T3>(Func<T1,T2,T3> f, T1 v1)
   => v2 => f(v1, v2);

// Beispiele
Func<int,int> Add10alt1
  => PartialApply<int,int,int>(AddNotCurried, 10);

Func<int,int> Add10alt2
  => Curry<int,int,int>(AddNotCurried)(10);
```

</td></tr></table>

:::notes

- Higher-Order: Funktion als Rückgabe oder Argument
- Beachte in F# ist das "das Selbe"
- C# inferiert hier die Typ-Parameter **nicht**

:::

---

### `Action` / `Func`

<table><tr><td>

```fsharp
// string * string -> unit
let printName (punct, name) =
   printfn "Hallo %s%s" name punct

let printNameExcl =
   partialApply printName "!"
```

</td><td>

```csharp
void PrintName(string punct, string name)
  => Console.WriteLine($"Hallo {name}{punct}");

Action<string> PrintNameExcl(string name)
  => FunExtensions.PartialApply<string,string,?>(PrintName, "!");

// Brauchen
Action<T2> PartialApply<T1,T2>(Action<T1,T2> f, T1 v1)
  => v2 => f(v1, v2);
```

</td></tr></table>

:::notes

- beachte `?`
- `Action<>` vs `Func<>`
- in F# ist `unit` (~ `void`) ein Typ wie jeder andere
- Überladung mit `Action` dann fällt `?` weg
- Typparameter weiterhin nötig

:::

---

## Pipe

<table><tr><td>

```fsharp
// = 3 + (2 + 1) = 6
let pipe =
   1
   |> add 2 
   |> add 3
```

</td><td>

```csharp
public class IntExtensions
{
   public static int Add(this int a, int b)
     => a + b;
}

int Pipe =
  1.Add(2).Add(3);
```

</td></tr></table>

:::notes

- in F# läuft hier viel über partial-applikation
- Über (Extension-)Methoden prima umsetzbar
- Deswegen auch nicht wirklich schlimm, dass Currying/Partial-Applikation nasty ist

:::

# Typen

:::notes

- in FP wollen wir *immutable* / *value-types*
- in C# *opt-in* über `readonly` / nur-Getter, `init` etc.
- in F# ist das der Default (*opt-out* mit `mutable`)
- Pattern-Matching ist immer ein Thema

:::
---

## Records

<table><tr><td>

```fsharp
type Person =
   {
      FirstName : string
      LastName : string
   }

let max =
   {
      FirstName = "Max"
      LastName = "Mustermann"  
   }
```

</td><td>

```csharp
public record Person(string FirstName, string LastName);

public record Person2
{
  public string FirstName { get; init; }
  public string LastName { get; init; }
}

var max = new Person("Max", "Mustermann");
```

</td></tr></table>

:::notes

- kein eigenes "Constraint" (sind `class`)
- sind *immutable* (shallow)
- Value equality (Vorsicht: der Typ selbst muss gleich sein Person1 /= Person2)
- in F# gibt es noch annonyme Records
- schönen "ToString" (build-in formatting for display)
- kann von anderen Records "erben" (in F# geht sowas ähnliches über annonyme Records)

:::

---

### Record-Update / nondestructive mutation

<table><tr><td>

```fsharp
//  { FirstName = "Min"; LastName = "Mustermann" }
let min =
   { max with FirstName = "Min" }
```

</td><td>

```csharp
var min =
   max with { FirstName = "Min" };
```

</td></tr></table>

:::notes

- erzeugt neuen Record mit gleichen Feldern auser den geänderten
- copy mit `max with { }`

:::

---

### Deconstruction

<table><tr><td>

```fsharp
let { FirstName = fn; LastName = ln } = min
```

</td><td>

```csharp
min.Deconstruct(out var fn, out var ln);
var (fn2, ln2) = min;
```

</td></tr></table>

:::notes

- Records implementieren autmatisch "Deconstruct" Methoden
- die Funktionieren direkt mit Tupeln

:::

---

### Pattern-Match

<table><tr><td>

```fsharp
let patternMatch =
   function
   | { FirstName = "Min"; LastName = _ } -> 
      "Hi Min"
   | { LastName = "Mustermann" } -> 
      "Hey a Mustermann"
   | p -> 
      $"Hello {p.FirstName}"
```

</td><td>

```csharp
string PatternMatchRecords (Person person) =>
   person switch
      {
         (FirstName: "Min", LastName: _) => 
            "Hi Min",
         (_, "Mustermann") => 
            "Hey a Mustermann",
         Person p => 
            $"Hello {p.FirstName}"
      };
```

</td></tr></table>

:::notes

- F# erscheint logischer (nur Teile, keine Angabe von Person)
- Unterstütz die neuen Vergleiche etc. von C# 9

:::

---

## Union Types

---

### in *F#*

```fsharp
type Maybe<'a> =
   | Nothing
   | Just of 'a

// Beispiele

let example1 : Maybe<int> = Nothing

let example2 = Just 42 
```

:::notes

- neuer Typ `Maybe`
- generisch
- zwei *Datenkonstruktoren* `Nothing` / `Just`
- Equality, Immutable, ...

:::

---

### Pattern-Matching

```fsharp
module Maybe =

   let withDefault a =
      function
      | Nothing -> a
      | Just a -> a

// Beispiele

Maybe.withDefault 0 Nothing // = 0
Maybe.withDefault 0 (Just 42) // = 42

```

:::notes

- erkennt fehlende / doppelte cases

:::

---

### in *C#*

*Übersetzung* in Klassen

```csharp
public abstract class Maybe<T>
{
   public abstract Tres Match<Tres> (
      Func<Tres> onNothing,
      Func<T, Tres> onJust );

   private Maybe() { }
   public sealed class NothingCase : Maybe<T> { ... }
   public sealed class JustCase : Maybe<T> { ... }
}
```

:::notes

:::

---

### Beispiel

*Übersetzung* in Klassen

```csharp
public abstract class Maybe<T>
{
   public static Maybe<T> Just(T value) => new JustCase(value);
   public static Maybe<T> Nothing => new NothingCase();
}

// Beipsiel
var nothing = Maybe<int>.Nothing;
var just42 = Maybe<int>.Just(42);
```

:::notes

:::

---

### NothingCase

```csharp
public sealed class NothingCase : Maybe<T>
{
   internal NothingCase() { }

   public override Tres Match<Tres>(
      Func<Tres> onNothing, 
      Func<T, Tres> onJust)
      => onNothing();
}
```

:::notes

:::

---

### JustCase

```csharp
public sealed class JustCase : Maybe<T>
{
   public T Value { get; init; }
   internal JustCase(T value)
   { Value = value; }

   public override Tres Match<Tres>(
      Func<Tres> onNothing, 
      Func<T, Tres> onJust)
      => onJust(Value);
}
```

:::notes

:::

---

### Pattern-Matching

```csharp
public abstract class Maybe<T>
{
   public T WithDefault(T defaultValue)
      => Match(() => defaultValue, x => x);

   public T WithDefault2(T defaultValue)
      => this switch
         {
            JustCase j => j.Value,
            NothingCase => defaultValue,
            // sonst Warnung
            _ => throw new InvalidOperationException()
         };
}
```

:::notes

:::

# Muster

<table><tr><td>

```fsharp
```

</td><td>

```csharp
```

</td></tr></table>

:::notes

:::

---

## Funktor / *Map*able

---

## "Monaden"

---

## Libs

---

# Zukunft

---

## Traits

- C# "Static Abstract Interface Members"
- Lambda Improvements
- Patternmatching - List Patterns
- in F# ... naja (Links)

---

## Union-Types?

[dotnet/csharplang/proposals/discriminated-unions](https://github.com/dotnet/csharplang/blob/main/proposals/discriminated-unions.md)

```csharp
enum class Maybe<T>
{
    Just(T value),
    Nothing
}
```

# Test

---

<table>
<tr>
<th>Json 1</th>
<th>Markdown</th>
</tr>
<tr>
<td>
```json
{
  "id": 1,
  "username": "joe",
  "email": "joe@example.com",
  "order_id": "3544fc0"
}
```
</td>
<td>
```json
{
  "id": 5,
  "username": "mary",
  "email": "mary@example.com",
  "order_id": "f7177da"
}
```

</td>
</tr>
</table>

# Fragen?

# Vielen Dank

## Links und co.

- Code & Slides [github.com/CarstenKoenig/DWX2021](https://github.com/CarstenKoenig/DWX2021)
