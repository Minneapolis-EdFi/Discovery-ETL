using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DLP.Discovery.ImagePackager.Core.CommandLine;
using DLP.Discovery.ImagePackager.Core.Compression;
using DLP.Discovery.ImagePackager.Core.DataAccess;

namespace DLP.Discovery.ImagePackager.Core
{
    public interface IStudentsProcessorService
    {
        void Execute(Options options);
    }

    public class StudentsProcessorService : IStudentsProcessorService
    {
        private readonly IStudentRepositoryService _repository;
        private readonly IImageExtractionService _imageExtractor;
        private readonly IDistrictCompressor _compressor;

        public StudentsProcessorService(IStudentRepositoryService repository, IImageExtractionService imageExtractor, IDistrictCompressor compressor)
        {
            _repository = repository;
            _imageExtractor = imageExtractor;
            _compressor = compressor;
        }

        public void Execute(Options options)
        {
            var proceed = true;

            if (!System.IO.Directory.Exists(options.SourceImagePath))
            {
                Console.WriteLine("Source image path is null or does not exists.");
                proceed = false;
            }

            if (!System.IO.Directory.Exists(options.DestinationPath))
            {
                Console.WriteLine("Destination path is null or does not exists.");
                proceed = false;
            }

            if (proceed)
            {
                Console.WriteLine("Discovering students...");
                var students = _repository.LoadStudentPhotoInformation(options);
                Console.WriteLine("Found {0} images from students, extracting and renaming images...", students.Count);
                var outputLogFile = System.IO.Path.Combine(options.DestinationPath, "log.txt");
                var processedFile = "";
                using (var logStream = new System.IO.StreamWriter(outputLogFile))
                {
                    logStream.WriteLine("Image file copying process started at:[{0}]", DateTime.Now);
                    foreach (var student in students)
                    {
                        try
                        {
                            logStream.WriteLine("Processing image for student [{0}]", student.StudentUSI);
                            processedFile = _imageExtractor.ProcessStudentImage(student, options);

                            if (string.IsNullOrEmpty(student.FileName))
                            {
                                throw new Exception(string.Format("Source file name is not present for student [{0}]", student.StudentUSI));
                            }

                            if (string.IsNullOrEmpty(processedFile))
                            {
                                logStream.WriteLine("Skipping image for student [{0}] as it already exists.", student.StudentUSI);
                                Console.Write("-");
                            }
                            else
                            {
                                logStream.WriteLine("Student image succesfully created as [{0}].", System.IO.Path.GetFileName(processedFile));
                                Console.Write(".");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Write("x");
                            logStream.WriteLine(ex.Message);
                        }

                        logStream.Flush();
                    }

                    //_compressor.CompressAllFolders(System.IO.Path.Combine(options.DestinationPath, "Content"), System.IO.Path.Combine(options.DestinationPath, "Compressed"));
                }

                Console.WriteLine("");
                Console.WriteLine("Image processing complete.");
                Console.WriteLine("Output log generated at: [{0}]", outputLogFile);
            }
        }
    }
}
