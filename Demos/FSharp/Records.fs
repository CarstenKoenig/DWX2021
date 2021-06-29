module Records

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

let min =
   { max with FirstName = "Min" }

let { FirstName = fn; LastName = ln } = min

let patternMatch =
   function
   | { FirstName = "Min"; LastName = _ } -> "Hi Min"
   | { LastName = "Mustermann" } -> "Hey a Mustermann"
   | p -> $"Hello {p.FirstName}"