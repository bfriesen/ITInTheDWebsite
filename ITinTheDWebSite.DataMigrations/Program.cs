using FluentMigrator.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ITinTheDWebSite.DataMigrations
{
    class Program
    {
        private static void Main(string[] args)
        {
            var isEmpty = args == null || args.Length == 0;
            var isEmptyRollback = args != null && args.Length == 1 && args[0].ToLower() == "/rollback";

            if (isEmpty || isEmptyRollback)
            {
                args = new string[]
                {
                    @"-c=Data Source=.\SQLEXPRESS;Integrated Security=true;Initial Catalog=ITintheD;",
                    "--db=sqlserver2012",
                    "--target=ITinTheDWebSite.DataMigrations.exe",
                    "/verbose=TRUE",
                    "/pause"
                };

                if (isEmptyRollback)
                {
                    args = args.Concat(new[] { "-t=rollback" }).ToArray();
                }
            }

            try
            {
                new MigratorConsole(args.Except(new[] { "/pause" }).ToArray());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (args.Any(arg => arg != null && arg.ToLower() == "/pause"))
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
                Console.WriteLine();
            }
        }
    }
}
