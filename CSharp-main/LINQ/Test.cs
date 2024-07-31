using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LINQ
{
    public class Test
    {
        public void Task1438(){
                 Console.WriteLine("Iniciando Download de arquivos de Retorno");
            Console.WriteLine("Consultando Diretório de Retorno");

            IEnumerable<SftpFile> EnumerableFiles = _sftpService.ListAllFiles(); // A biblioteca SFTP o método de listar o diretório é um Enumerable

            List<SftpFile> listFilesRemotePath = EnumerableFiles.ToList();

            ListResponse<EstablishmentExternalIntegrationFile> listREMLocalFiles = _establishmentExternalIntegrationFileRepository.GetAllExternalIntegrationFiles().Result;

            ListResponse<EstablishmentExternalIntegrationRETCode> retCodes = _establishmentExternalIntegrationRETCodeRepository.GetAllCodeRet().Result;

            if (listFilesRemotePath == null || listFilesRemotePath.Count <= 0)
            {
                Console.WriteLine("Não há arquivos no diretório de retorno");
            }

            Console.WriteLine($"O Diretório remoto possui {listFilesRemotePath.Count} Arquivos");

            try
            {
                //var queryCompareFileNames = from localfile in listREMLocalFiles.Items
                //                            join remotePath in listFilesRemotePath on localfile.NameFile.Substring(0, 8) equals remotePath.Name into fileGroup
                //                            from remotefile in fileGroup
                //                            where remotefile.Name.Contains(localfile.NameFile.Substring(0, 8))
                //                            select new { localfile = localfile.NameFile, remotePath = fileGroup };


                //var queryCompareFileNamesJoin = from localfile in listREMLocalFiles.Items
                //                                join remotePath in listFilesRemotePath on localfile.NameFile.Substring(0, 8) equals remotePath.Name.Substring(0, 8)
                //                                where !remotePath.Name.StartsWith(".")
                //                                where remotePath.Name.Contains(localfile.NameFile.Substring(0, 8))
                //                                select new { localfile = localfile.NameFile };

                //var all = listFilesRemotePath.Where(remote => listREMLocalFiles.Items.All(local => local.NameFile.Contains(remote.Name.Substring(0, 8))));

                //var all2 = listREMLocalFiles.Items.Where(local => listFilesRemotePath.All(remote => remote.Name.Contains(local.NameFile.Substring(0, 8))));

                //List<string> compareNames = listLocalFiles.Where(local => remoteFiles.Contains(local)).ToList();


                var listLocalFiles = listREMLocalFiles.Items
                    .Select(local => local.NameFile.Substring(0,8)).ToList();

                var remoteFiles = listFilesRemotePath
                    .Where(remotefile => !remotefile.Name.StartsWith("."))
                    .Select(remote=> remote.Name).ToList();

                var queryCompareFileNames = from localfile in listREMLocalFiles.Items
                                            from remotefile in remoteFiles
                                            where remotefile.Contains(localfile.NameFile.Substring(0, 8))
                                            select new {localfile = localfile.NameFile};

                Console.WriteLine(listLocalFiles.Count());
                Console.WriteLine(remoteFiles.Count());
                Console.WriteLine(queryCompareFileNames.Count());

                foreach (var fileName in queryCompareFileNames)
                {
                    string filedownload = _sftpService.DownloadRemoteFile(GeneratePathFile(), fileName.localfile);

                    List<DetailModelRet> listDetailsFileDownload = ReadContent($"{Directory.GetCurrentDirectory()}\\{filedownload}");
                    foreach (var detail in listDetailsFileDownload)
                    {
                        var listCodeRets = retCodes.Items
                             .Where(item => item.CodeRet.Equals(detail.CodigoRetorno));

                        foreach (var retcode in listCodeRets)
                        {
                            Console.WriteLine($"Id RetCode:{retcode.RetCodeId},Nome Arquivo Rem{fileName.localfile},Nome arquivo Ret{filedownload}");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
    }
}