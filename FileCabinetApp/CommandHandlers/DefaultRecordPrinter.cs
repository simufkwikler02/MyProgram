using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCabinetApp.CommandHandlers
{
    internal class DefaultRecordPrinter : IRecordPrinter
    {
        public void Print(IEnumerable<FileCabinetRecord> records)
        {
            if (!records.Any())
            {
                Console.WriteLine("records were not created");
                return;
            }
            else
            {
                foreach (var record in records)
                {
                    string month = record.DateOfBirth.Month switch
                    {
                        1 => "Jan",
                        2 => "Feb",
                        3 => "Mar",
                        4 => "Apr",
                        5 => "May",
                        6 => "Jun",
                        7 => "Jul",
                        8 => "Aug",
                        9 => "Sep",
                        10 => "Oct",
                        11 => "Nov",
                        12 => "Dec",
                        _ => "incorrect format."
                    };
                    Console.WriteLine();
                    Console.WriteLine($"#{record.Id}, {record.FirstName}, {record.LastName}, {record.DateOfBirth.Year}-{month}-{record.DateOfBirth.Day}");
                    Console.WriteLine($"property1 (short):{record.Property1}  property2 (decimal):{record.Property2}  property3 (char):{record.Property3}");
                }
            }
        }
    }
}
