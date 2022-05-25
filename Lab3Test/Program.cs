using (Semaphore semaphore = new Semaphore(3, 3, "testing"))
{
    
    if (!semaphore.WaitOne(5000))
    {
        Console.WriteLine("Sorry too late");
    }

    Console.WriteLine("Hello world");
    Thread.Sleep(100000000);
}