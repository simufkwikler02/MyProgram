using System;

namespace FileCabinetGenerator
{
    public static class Program
    {
        private static string? format;
        private static string? path;
        private static string? amount;
        private static string? id;

        public static void Main(string[] args)
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
                    amount = args[i].Split('=', 2)[1];
                }
                else if (string.Equals(args[i], "-a", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    amount = args[i];
                }

                if (args[i].Split('=', 2)[0].Equals("--start-id", StringComparison.OrdinalIgnoreCase))
                {
                    id = args[i].Split('=', 2)[1];
                }
                else if (string.Equals(args[i], "-i", StringComparison.OrdinalIgnoreCase))
                {
                    i++;
                    id = args[i];
                }

            }
        }
    }
}
