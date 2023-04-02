using System;
using System.Text;

namespace hashes;

public class GhostsTask : 
	IFactory<Document>, IFactory<Vector>, IFactory<Segment>, IFactory<Cat>, IFactory<Robot>, 
	IMagic
{
	Document document;
    byte[] docContent = new byte[] { 1, 2, 3, 4 };
    Vector vector;
	Segment segment;
	Cat cat;
	Robot robot;

	public GhostsTask()
	{
		vector = new Vector(0, 0);
		segment = new Segment(vector, vector);
		cat = new Cat("Barsik", "kot", new DateTime(2010, 1, 1));
		robot = new Robot("Good-Boy");
		document = new Document("book", Encoding.UTF8, docContent);
	}
    public void DoMagic()
	{
		vector.Add(new Vector(1, 397));
		cat.Rename("kot");
		Robot.BatteryCapacity++;
		docContent[3] = 10;
	}

	// Чтобы класс одновременно реализовывал интерфейсы IFactory<A> и IFactory<B> 
	// придется воспользоваться так называемой явной реализацией интерфейса.
	// Чтобы отличать методы создания A и B у каждого метода Create нужно явно указать, к какому интерфейсу он относится.
	// На самом деле такое вы уже видели, когда реализовывали IEnumerable<T>.

	Vector IFactory<Vector>.Create()
	{
		return vector;
	}

	Segment IFactory<Segment>.Create()
	{
		return segment;
	}

    Document IFactory<Document>.Create()
    {
		return document;
	}

    Robot IFactory<Robot>.Create()
    {
		return robot;
    }

    Cat IFactory<Cat>.Create()
    {
		return cat;
    }

    // И так даллее по аналогии...
}