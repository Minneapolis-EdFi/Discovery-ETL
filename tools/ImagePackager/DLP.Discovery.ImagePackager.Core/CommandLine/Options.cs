using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace DLP.Discovery.ImagePackager.Core.CommandLine
{
    public class Options
    {
        [Option('a', "action", Required = true, HelpText = "Action to be executed, values can be: [copy]|[compress]")]
        public string Action { get; set; }

        [Option('s', "source",Required = true, HelpText = "Folder where Dicovery images are located")]
        public string SourceImagePath { get; set; }

        [Option('d', "destination", Required = true, HelpText = "Folder where extracted images will be located")]
        public string DestinationPath { get; set; }

        [Option('i', "inputfile", Required = true, HelpText = "File holding information about students and schools")]
        public string InputSourceFile { get; set; }

        [Option('o', "overwrite", Required = false, HelpText = "When set, picture files will be overwritten. Otherwise they will be skipped if exists.", Default = false)]
        public bool OverwriteExistingImages { get; set; }
    }
}
