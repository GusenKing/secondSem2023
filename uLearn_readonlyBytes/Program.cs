using NUnitLite;

namespace hashes;

class Program
{
	static void Main(string[] args)
	{
		new AutoRun().Execute(args);
		var rb = new ReadonlyBytes(new byte[] { 1, 2, 3, });
		rb[2] = 5;
	}
}