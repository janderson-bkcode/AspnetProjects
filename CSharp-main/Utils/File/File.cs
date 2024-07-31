using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utils.File
{
public class File
{
        //Aqui está um exemplo de um método em C# que lê um arquivo SFTP remoto e retorna um array de bytes:
    public static byte[] ReadSFTPFile(string host, string username, string password, string remoteFilePath)
    {
            byte[] fileBytes;
            using (var sftpClient = new SftpClient(host, username, password))
            {
                sftpClient.Connect();
                using (var memoryStream = new MemoryStream())
                {
                    sftpClient.DownloadFile(remoteFilePath, memoryStream);
                    fileBytes = memoryStream.ToArray();
                }
                sftpClient.Disconnect();
            }
            return fileBytes;
    }

//Aqui está um exemplo de um método em C# que lê um arquivo e retorna um array de bytes:
public static byte[] ReadFile(string filePath)
{
    byte[] fileBytes;
    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
    {
        using (var binaryReader = new BinaryReader(fileStream))
        {
            fileBytes = binaryReader.ReadBytes((int)fileStream.Length);
        }
    }
    return fileBytes;
}
//Aqui está um exemplo de um método em C# que lê um arquivo remoto e retorna um array de bytes:
public static byte[] ReadRemoteFile(string url)
{
    byte[] fileBytes;
    using (var webClient = new WebClient())
    {
        fileBytes = webClient.DownloadData(url);
    }
    return fileBytes;
}

    }
}