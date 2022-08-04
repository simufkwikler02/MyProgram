using System;
using System.Globalization;
using System.Text;
using FileCabinetApp;

namespace FileCabinetGenerator
{
    public static class Program2
    {
        private static string? format;
        private static string? path;
        private static int amount;
        private static int id;
        private static char[] letters = "abcdefghigklmnopqrstuvwxwz".ToCharArray();

        public static void Main(string[] args)
        {
            ReadParametrs(args);
            format = format ?? string.Empty;
            path = path ?? string.Empty;

            List<FileCabinetRecord> list = new List<FileCabinetRecord>();
            for (int i = id; i < id + amount; i++)
            {
                var record = fileCabinetRecordGenerator(); 
                record.Id = i;
                list.Add(record);
            }

            StreamWriter? fstream;
            FileInfo? fileInfo;
            DirectoryInfo? directory;
            try
            {
                fileInfo = new FileInfo(path);
                directory = fileInfo.Directory;
                if (!directory.Exists)
                {
                    throw new ArgumentException(nameof(directory));
                }

                if (fileInfo.Exists)
                {
                    Console.WriteLine($"File is exist - rewrite {path}? [Y/N]");
                    var com = Console.ReadLine() ?? string.Empty;
                    switch (com)
                    {
                        case "y":
                            goto case "Y";
                        case "Y":
                            break;
                        case "n":
                            goto case "N";
                        case "N":
                            Console.WriteLine("Export canceled");
                            return;
                        default:
                            Console.WriteLine($"There is no '{com}' command.");
                            return;
                    }
                }
            }
            catch
            {
                Console.WriteLine($"Export failed: can't open file {path}.");
                return;
            }

            fstream = new StreamWriter(path, false);
            var snapshot = new FileCabinetServiceSnapshot(list);
            switch (format)
            {
                case "csv":
                    snapshot.SaveToCsv(fstream);
                    break;
                case "xml":
                    snapshot.SaveToXml(fstream);
                    break;
                default:
                    Console.WriteLine($"There is no '{format}' command.");
                    fstream.Close();
                    return;
            }

            Console.WriteLine($"All records are exported to file {path}.");
            fstream.Close();

        }

        private static FileCabinetRecord fileCabinetRecordGenerator()
        {
            var validator = new ValidatorBuilder().CreateDefault();
            var record = new FileCabinetRecord();
            record.Id = 0;
            Random random = new Random();
            StringBuilder? stringBuilder= new StringBuilder();
            var parametrs = validator.ParametrsInfo();

            do
            {
                stringBuilder.Clear();
                for (int i = 0; i < 10; i++)
                {
                    stringBuilder.Append(letters[random.Next(0, letters.Length - 1)]);
                }
                record.FirstName = stringBuilder.ToString();

                stringBuilder.Clear();
                for (int i = 0; i < 10; i++)
                {
                    stringBuilder.Append(letters[random.Next(0, letters.Length - 1)]);
                }
                record.LastName = stringBuilder.ToString();

                try
                {
                    record.DateOfBirth = new DateTime(random.Next(parametrs.From.Year, parametrs.To.Year), random.Next(1, 12), random.Next(1, 31));
                }
                catch
                {
                    record.DateOfBirth = new DateTime(1000,1,1);
                }
                
                record.Property1 = (short)random.Next(parametrs.MinProperty1, parametrs.MaxProperty1);
                record.Property2 = (decimal)random.NextDouble();
                record.Property3 = letters[random.Next(0, letters.Length - 1)];

            }while(!validator.ValidateParametrs(record));

            return record;

        }

        private static void ReadParametrs(string[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Split('=', 2)[0].Equals("--output-type", StringComparison.OrdinalIgnoreCase))
                {
                    format = args[i].Split('=', 2)[1];
                }
                else if (string.Equals(args[i], "-t", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    format = args[i];
                }

                if (args[i].Split('=', 2)[0].Equals("--output", StringComparison.OrdinalIgnoreCase))
                {
                    path = args[i].Split('=', 2)[1];
                }
                else if (string.Equals(args[i], "-o", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    path = args[i];
                }

                if (args[i].Split('=', 2)[0].Equals("--records-amount", StringComparison.OrdinalIgnoreCase))
                {
                    amount = Convert.ToInt32(args[i].Split('=', 2)[1],CultureInfo.CurrentCulture);
                }
                else if (string.Equals(args[i], "-a", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    amount = Convert.ToInt32(args[i],CultureInfo.CurrentCulture);
                }

                if (args[i].Split('=', 2)[0].Equals("--start-id", StringComparison.OrdinalIgnoreCase))
                {
                    id = Convert.ToInt32(args[i].Split('=', 2)[1], CultureInfo.CurrentCulture);
                }
                else if (string.Equals(args[i], "-i", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    id = Convert.ToInt32(args[i],CultureInfo.CurrentCulture);
                }

            }
        }
    }
}
