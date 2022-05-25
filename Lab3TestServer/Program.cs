// See https://aka.ms/new-console-template for more information

using Lab3Test;

var semaphore = new Semaphore(0, 2, Connection.SemaphoreName);

while (true)
{
    var v = semaphore.WaitOne(500);
    if (v) OnChanged();
}

void OnChanged()
{
    Console.Clear();
    Console.WriteLine(Connection.Read());
}