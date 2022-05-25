using System.Diagnostics;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace Lab3Test;

public class Connection
{
    public static string FileName { get; set; } = "file/chat";
    public static string SemaphoreName { get; set; } = "xyz";

    public static void Write(string v)
    {
        var vv = Read() + "\n" + v;
        var Buffer = Encoding.ASCII.GetBytes(vv);
        var mmf = MemoryMappedFile.CreateOrOpen(FileName, 1000);
        var accessor = mmf.CreateViewAccessor();
        accessor.Write(54, (ushort)Buffer.Length);
        accessor.WriteArray(54 + 2, Buffer, 0, Buffer.Length);
    }

    public static string Read()
    {
        try
        {
            var mmf = MemoryMappedFile.OpenExisting(FileName);
            var accessor = mmf.CreateViewAccessor();
            var Size = accessor.ReadUInt16(54);
            var Buffer = new byte[Size];
            accessor.ReadArray(54 + 2, Buffer, 0, Buffer.Length);
            return Encoding.ASCII.GetString(Buffer);
        }
        catch
        {
            return "";
        }
    }
}