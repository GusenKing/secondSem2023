using AssociativeArray;

var ar = new AssociatArray<string, int>();
ar.Set("cat", 1);
ar.Set("dog", 2);
ar.Set("doh", 3);
ar.Set("bird", 4);

Console.WriteLine(ar);

Console.WriteLine(ar.Get("bird"));
ar.Delete("cat");

Console.WriteLine(ar);