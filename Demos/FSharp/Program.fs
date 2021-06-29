// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

// Define a function to construct a message to print
[<EntryPoint>]
let main argv =
    Console.WriteLine (Funktionen.hallo "DWX")
    0 // return an integer exit code