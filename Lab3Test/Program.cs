//Синхронизировать информацию на консольных экранах для двух запусков с помощью буфера в разделяемой памяти. Синхронизация при помощи семафоров.
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO.MemoryMappedFiles;

namespace SynhroSem
{
    class Program
    {

        public void writeFromMap()
        {
            Random rnd = new Random();//объект класса Random
            
            char[] message1 = rnd.Next(0, 2).ToString().ToCharArray();//случайная генерация числа
            //Размер введенного сообщения
            int size1 = message1.Length;

            //Создание участка разделяемой памяти
            //Первый параметр - название участка, 
            //второй - длина участка памяти в байтах: тип char  занимает 2 байта 
            //плюс четыре байта для одного объекта типа Integer
            MemoryMappedFile sharedMemory1 = MemoryMappedFile.CreateOrOpen("MemoryFile", size1 * 2 + 4);
            //Создаем объект для записи в разделяемый участок памяти
            using (MemoryMappedViewAccessor writer = sharedMemory1.CreateViewAccessor(0, size1 * 2 + 4))
            {
                //запись в разделяемую память
                //запись размера с нулевого байта в разделяемой памяти
                writer.Write(0, size1);
                //запись сообщения с четвертого байта в разделяемой памяти
                writer.WriteArray<char>(4, message1, 0, message1.Length);
            }
        }

        public void readFromMap()
        {
            // Массив для сообщения из общей памяти
            char[] message2;
            //Размер введенного сообщения
            int size2;

            //Получение существующего участка разделяемой памяти
            //Параметр - название участка
            MemoryMappedFile sharedMemory2 = MemoryMappedFile.OpenExisting("MemoryFile");
            //Сначала считываем размер сообщения, чтобы создать массив данного размера
            //Integer занимает 4 байта, начинается с первого байта, поэтому передаем цифры 0 и 4
            using (MemoryMappedViewAccessor reader = sharedMemory2.CreateViewAccessor(0, 4, MemoryMappedFileAccess.Read))
            {
                size2 = reader.ReadInt32(0);
            }

            //Считываем сообщение, используя полученный выше размер
            //Сообщение - это строка или массив объектов char, каждый из которых занимает два байта
            //Поэтому вторым параметром передаем число символов умножив на из размер в байтах 
            //А первый параметр - смещение - 4 байта, которое занимает размер сообщения
            using (MemoryMappedViewAccessor reader = sharedMemory2.CreateViewAccessor(4, size2 * 2, MemoryMappedFileAccess.Read))
            {
                //Массив символов сообщения
                message2 = new char[size2];
                reader.ReadArray<char>(0, message2, 0, size2);
            }
            //Console.WriteLine("Получено:");
            Console.WriteLine(message2);
        }


        static void Main(string[] args)
        {


            Program program = new Program();

            bool isProc = false;
            //проверка на количество запусков приложения
            if (Process.GetProcessesByName("SynhroSem").Length > 1)
            {
                isProc = true;
            }
            //семафор
            Semaphore semaphore = new Semaphore(2, 2);

            while (true)
            {

                if (isProc)
                {
                    semaphore.WaitOne();//синхронизация вывода

                    program.readFromMap();//вывод с разделяемой памяти

                    semaphore.Release();//синхронизация вывода
                    //Console.WriteLine("2 процесса");
                }
                else
                {
                    
                    program.writeFromMap();//ввод в разделяемую память

                    semaphore.WaitOne();//синхронизация вывода

                    program.readFromMap();//вывод с разделяемой памяти

                    semaphore.Release();//синхронизация вывода
                    //Console.WriteLine("1 процесс");
                }
               

                Thread.Sleep(500);
            }

            





            
        }
    }
}
