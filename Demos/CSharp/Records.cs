namespace Records
{
   public record Person(string FirstName, string LastName);

   public record Person2
   {
      public string FirstName { get; init; }
      public string LastName { get; init; }
   }

}