using System.Diagnostics;
using Lab3Test;

try
{
    var clientName = Process.GetProcessesByName(nameof(Lab3Test)).Length;

    while (true)
    {
        var v = Console.ReadLine();
        Connection.Write(clientName + ": " + (v ?? ""));
        var sem = Semaphore.OpenExisting(Connection.SemaphoreName);
        sem.Release();
    }
}
catch
{
    Console.WriteLine("Only 2 clients is allowed");
}