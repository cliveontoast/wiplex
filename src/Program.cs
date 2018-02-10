using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {
            string searchPattern = "*LI.jpg";
            string video = ".mp4";

            if (args.Length == 0)
            {
                Console.WriteLine("No directory given to process" + System.Environment.NewLine);

                Console.WriteLine("First argument is the folder i.e. \"c:\\somewhere with spaces\\\"");
                Console.WriteLine("Second optional agument is the search pattern, i.e. default of " + searchPattern);
                Console.WriteLine("Third optional agument is the search pattern for video file, where file ends with i.e. default of " + video);
                Console.WriteLine("Press any key to exit");
                Console.ReadKey();
                return;
            }
            var folder = args[0];
            if (args.Length > 1)
            {
                searchPattern = args[1];
            }
            if (args.Length > 2)
            {
                video = args[2];
            }
            var imageFiles = System.IO.Directory.GetFiles(folder, searchPattern);
            Console.WriteLine("poo supprise now lets get going with the number of files found " + imageFiles.Length);
            
            foreach (var imageFile in imageFiles)
            {
                Console.WriteLine("Processing " + imageFile);
                //var zipFile = Ionic.Zip.ZipFile.Read(imageFile);
                try
                {
                    using (var zipfs = System.IO.File.OpenRead(imageFile))
                    {
                        System.Console.Write("opened file " + imageFile);
                        var z = new System.IO.Compression.ZipArchive(zipfs);
                        foreach (var item in z.Entries)
                        {
                            if (item.Name.EndsWith(video))
                            {
                                System.Console.Write(" found video");
                                using (var zm4 = item.Open())
                                {
                                    var newFile = System.IO.Path.Combine(folder, System.IO.Path.GetFileNameWithoutExtension(imageFile) + video);
                                    using (var fs = System.IO.File.OpenWrite(newFile))
                                    {
                                        fs.Seek(0, System.IO.SeekOrigin.Begin);
                                        System.Console.Write(" unzipping");
                                        zm4.CopyTo(fs);
                                    }
                                    System.Console.WriteLine(" Success");
                                }
                                continue;
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("not " + e.Message);
                }

            }
        }
    }
}
