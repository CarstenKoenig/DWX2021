// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

// Define a function to construct a message to print
[<EntryPoint>]
let main argv =
    Console.WriteLine (Funktionen.hallo "DWX")
    Console.WriteLine (Funktionen.add 3 5)
    Console.WriteLine (Funktionen.add10 5)
    Console.WriteLine (Funktionen.add10' 5)
    Console.WriteLine (Funktionen.add10'' 5)
    Funktionen.printNameExcl "DWX"
    Console.WriteLine (Funktionen.pipe)

    Console.WriteLine(Records.patternMatch(Records.max))
    Console.WriteLine(Records.patternMatch(Records.min))

    Console.WriteLine(UnionTypes.Maybe.tryCalcSqrt "36")
    Console.WriteLine(UnionTypes.Maybe.tryCalcSqrt "-36")
    Console.WriteLine(UnionTypes.Maybe.tryCalcSqrt "xx")
    0 // return an integer exit code