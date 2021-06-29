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
```

</td><td>

```csharp
```

</td></tr></table>

---

## Partial Applikation

---

## Higher-Order

:::notes

- `Action<>` vs `Func<>`

:::

---

# Typen

---

## Records

---

## Immutability

---

## Tupel

---

## Union Types

---

## Typ-Inferenz

---

# Muster

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

---

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
