using lesson;


var arOp = new ArOperations();
MyDelegate deleg = new MyDelegate(arOp.Sum);
Console.WriteLine(deleg.Invoke(5, 6));

var mc = new MyClass(5);
Console.WriteLine(mc);
Console.WriteLine(mc.SomeMethod(delegate(int x, int y) { return x + y; }));
Console.WriteLine(mc.SomeMethod( (x, y) => x > y ? x : y));

public delegate int MyDelegate(int k, int l);
