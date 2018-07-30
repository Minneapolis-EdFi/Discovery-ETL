﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using DLP.Discovery.ImagePackager.Core;
using DLP.Discovery.ImagePackager.Core.CommandLine;
using StructureMap;

namespace DLP.Discovery.ImagePackager
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container(_ =>
            {
                _.Scan(x =>
                {
                    x.TheCallingAssembly();
                    x.AssemblyContainingType<IImageExtractionService>();
                    x.WithDefaultConventions();
                });
            });

            var result = Parser.Default.ParseArguments<Options>(args)
                .WithParsed(options =>
                {
                    var processor = container.GetInstance<IStudentsProcessorService>();
                    processor.Execute(options);
                });
        }
    }
}