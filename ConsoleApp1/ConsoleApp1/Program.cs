using ConsoleApp1;

//NegativeFirstPositiveSecond();

var q = new MyQueue<int>(3);
q.Enqueue(1);
q.Enqueue(2);
q.Enqueue(3);
Console.WriteLine(q);
q.Dequeue();
q.Dequeue();
Console.WriteLine(q);
q.Enqueue(4);
q.Enqueue(5);
q.Dequeue();
q.Enqueue(6);
Console.WriteLine(q);

void NegativeFirstPositiveSecond()
{
    int N = 20;
    var rd = new Random();
    var arr = new int[N];
    for(int i = 0; i < N; i++)
    {
        var newR = rd.Next(-100, 100);
        arr[i] = newR;
        Console.Write(newR + " ");
    }
    Console.WriteLine();
    Console.WriteLine();

    var q = new MyQueue<int>(N);
    var posCounter = 0;
    for(int i = 0; i < N; i++)
    {
        if (arr[i] < 0) Console.Write($"{arr[i]} ");
        else
        {
            q.Enqueue(arr[i]);
            posCounter++;
        }
    }
    for(int i = 0; i < posCounter; i++)
    {
        Console.Write($"{q.Dequeue()} ");
    }
    
}


//bool isbracketsequencecorrect(string sequence)
//{
//    mystack<char> stack = new mystack<char>(sequence.length);
//    for(int i = 0; i < sequence.length; i++)
//    {
//        if (sequence[i].equals("{") || sequence[i].equals("(") || sequence[i].equals("[") || sequence[i].equals("<"))
//        {
//            stack.push(sequence[i]);
//        }
//        if (sequence[i].equals("}") || sequence[i].equals(")") || sequence[i].equals("]") || sequence[i].equals(">"))
//        {

//        }
//    }
//    return true;
//}



void FindBracketsPairs(string sequence)
{
    MyStack<int> stack = new MyStack<int>(sequence.Length);
    for (int i = 0; i < sequence.Length; i++)
    {
        if (sequence[i] == '(')
        {
            stack.Push(i);
        }
        if (sequence[i] == ')')
        {
            if (stack.IsEmpty())
            {
                Console.WriteLine("Неправильная скобочная последовательность");
                break;
            }
            Console.WriteLine($"{stack.Pop()} - {i}");
        }
    }
    if (!stack.IsEmpty())
    {
        Console.WriteLine("Неправильная скобочная последовательность");
    }
}