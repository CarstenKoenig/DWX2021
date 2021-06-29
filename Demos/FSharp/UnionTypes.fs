namespace UnionTypes

type Maybe<'a> =
   | Nothing
   | Just of 'a

module Maybe =

   let example1 : Maybe<int> =
      Nothing
   let example2 =
      Just 42 

   let map f =
      function
      | Nothing -> Nothing
      | Just a -> Just (f a)

   let withDefault a =
      function
      | Nothing -> a
      | Just a -> a