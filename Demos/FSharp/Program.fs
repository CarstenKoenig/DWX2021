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
    0 // return an integer exit code