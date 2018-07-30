using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using DLP.Discovery.ImagePackager.Core.CommandLine;
using DLP.Discovery.ImagePackager.Core.DataAccess;

namespace DLP.Discovery.ImagePackager.Core
{
    public interface IImageExtractionService
    {
        /// <summary>
        /// Copies image student file from source to destination folder
        /// </summary>
        /// <param name="student"></param>
        /// <param name="options"></param>
        /// <returns>Returns false if </returns>
        string ProcessStudentImage(DataAccess.StudentInfo student, Options options);
    }

    public class ImageExtractionService : IImageExtractionService
    {
        public string ProcessStudentImage(StudentInfo student, Options options)
        {
            var extension = System.IO.Path.GetExtension(student.FileName);
            var sourceFile = System.IO.Path.Combine(options.SourceImagePath, student.FileName);
            var destinationFile = System.IO.Path.Combine(options.DestinationPath, @"Content\Students\", $"{student.StudentUSI}{extension}");
            var destinationFolder = System.IO.Path.GetDirectoryName(destinationFile);

            if (!System.IO.Directory.Exists(destinationFolder))
            {
                System.IO.Directory.CreateDirectory(destinationFolder);
            }

            bool proceedToCopy = true;
            if (System.IO.File.Exists(sourceFile))
            {
                if (System.IO.File.Exists(destinationFile))
                {
                    if (!options.OverwriteExistingImages)
                    {
                        destinationFile = "";
                        proceedToCopy = false;
                    }
                    else
                    {
                        System.IO.File.Delete(destinationFile);
                    }
                }

                if (proceedToCopy)
                {
                    System.IO.File.Copy(sourceFile, destinationFile);
                }
            }
            else
            {   
                throw new Exception($"ERROR: File [{sourceFile}] is not present for student [{student.StudentUSI}].");
            }

            return destinationFile;
        }
    }
}
