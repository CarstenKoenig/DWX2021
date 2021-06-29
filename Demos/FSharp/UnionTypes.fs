namespace UnionTypes

open System

type Maybe<'a> =
    | Nothing
    | Just of 'a

module Maybe =

    let example1 : Maybe<int> = Nothing
    let example2 = Just 42

    let map f =
        function
        | Nothing -> Nothing
        | Just a -> Just(f a)

    let withDefault a =
        function
        | Nothing -> a
        | Just a -> a

    [<Struct>]
    type MaybeBuilder =
        member __.Bind(opt, binder) =
            match opt with
            | Just value -> binder value
            | Nothing -> Nothing
        member __.Return(value) = Just value

    let maybe = MaybeBuilder()

    let tryParse (txt : string) =
        match Double.TryParse txt with
        | (true, n) -> Just n
        | _ -> Nothing

    let saveSqrt x =
        if x < 0.0 then Nothing else Just (sqrt x)

    let tryCalcSqrt txt =
        maybe {
            let! x = tryParse txt
            let! sqrt = saveSqrt x
            return sqrt
        }