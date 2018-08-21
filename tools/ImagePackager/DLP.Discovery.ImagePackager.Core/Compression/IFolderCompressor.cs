using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLP.Discovery.ImagePackager.Core.Compression
{
    public interface IFolderCompressionService
    {
        void CompressFolder(string inputFolder, string destination);
    }

    public class FolderCompressionService : IFolderCompressionService
    {
        public void CompressFolder(string inputFolder, string destination)
        {
            ZipFile.CreateFromDirectory(inputFolder, destination, CompressionLevel.Fastest, false);
        }
    }

    public interface IDistrictCompressor
    {
        void CompressAllFolders(string inputFolder, string outputFolder);
    }

    public class DistrictCompressor : IDistrictCompressor
    {
        private readonly IFolderCompressionService _compressionService;

        public DistrictCompressor(IFolderCompressionService compressionService)
        {
            _compressionService = compressionService;
        }

        public void CompressAllFolders(string inputFolder, string outputFolder)
        {
            if (!System.IO.Directory.Exists(inputFolder))
            {
                throw new DirectoryNotFoundException("Unable to produce school folders. Input folder is not present.");
            }

            if (!System.IO.Directory.Exists(outputFolder))
            {
                System.IO.Directory.CreateDirectory(outputFolder);
            }

            var directories = System.IO.Directory.GetDirectories(inputFolder);
            foreach (var schoolDirectory in directories)
            {
                var schoolName = System.IO.Path.GetDirectoryName(schoolDirectory);
                var zipFileName = System.IO.Path.Combine(outputFolder, string.Format("{0}.zip", schoolName));
                _compressionService.CompressFolder(schoolDirectory, zipFileName);
            }
        }
    }
}
