module Funktionen

let hallo name = $"Hallo {name}!"
let hallo2 (name : string) = $"Hallo {name}!"

// int -> int -> int
let add a b = a + b

let add2 a = fun b -> a + b

let add10 = add 10

// ('a*'b -> 'c) -> 'a -> 'b -> 'c
let curry f a b = f (a,b)

// ('a*'b -> 'c) -> 'a -> 'b -> 'c
let partialApply f a =
   fun b -> f (a,b)

let add'(a,b) = a+b

let add10' = 
   partialApply add' 10

let add10'' = 
   curry add' 10

// string * string -> unit
let printName (punct, name) =
   printfn "Hallo %s%s" name punct

let printNameExcl =
   partialApply printName "!"

let pipe =
   1
   |> add 2 
   |> add 3

let inline srtpAdd a b =
   a + b

let inline trim (s : ^s when ^s : (member Trim : unit -> ^s)) =
   (^s : (member Trim : unit -> ^s) s)