using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TestJob.Interfaces;
using System.IO.Compression;
using TestJob.Helpers;
using System.IO;

namespace TestJob.Commands
{
    [Description("-zip <Имя файла анкеты> <Путь для сохранения архива> - Запаковать указанную анкету в архив и сохранить архив по указанному пути")]
    public class ZipProfile : ICommand
    {
        public string CommandName { get; private set; } = "-zip";
        public string DefExt = ".txt";
        

        private readonly string inputDirectory;
        public ZipProfile(Configuration configuration) {
            this.inputDirectory = configuration.WorkingDirectory;
        }
        public IResponse Execute(SessionContext sessionContext, string cmd)
        {
            const string ZIP_EXT = ".zip";
            var str = cmd.Split(DefExt);
            var filename = str[0]  ;
            var directory = str[1].Trim();
            var inputFile  = Path.Combine(inputDirectory, filename + DefExt);
            var outputFile = Path.Combine(directory, filename + ZIP_EXT);
           


            if (!System.IO.File.Exists(inputFile)) {
                throw new Exception ($"Файл {filename} не найден");              
            }

           if (!(System.IO.Directory.Exists(directory)))
            {
                Console.WriteLine($"Директория {directory} не найдена, создаем");
                System.IO.Directory.CreateDirectory(directory);
            }

            if (System.IO.File.Exists(outputFile) ) {
                Console.WriteLine("Архив сущетсвует и будет перезаписан");
                System.IO.File.Delete(outputFile);
            }

            using (ZipArchive archive = ZipFile.Open (outputFile, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(inputFile, filename + DefExt);
            }
            return new Response(ResponseType.Ok);
        }    
    }
}
