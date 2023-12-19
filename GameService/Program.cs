using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace GameService
{
    class Program
    {
        /// <summary>
        /// Точка входа
        /// </summary>
        /// <param name="args">Массив параметров командной строки</param>
        static void Main(string[] args)
        {
            try
            {
                // Информация о исполняемом файле
                Assembly a = Assembly.GetExecutingAssembly();
                // Имя исполняемого файла
                string name = a.Location;
                // Создание службы
                var svc = new AppService();

                // Первый параметр командной строки или пустая строка, если не задан
                string arg1 = args.FirstOrDefault() ?? string.Empty;
                // В нижний регистр
                arg1 = arg1.ToLower();
                // Селектор вариантов запуска
                switch (arg1)
                {
                    case "":
                        if (Environment.UserInteractive)
                        {
                            Console.WriteLine("Для запуска в консольном режиме используйте параметр console");
                        }
                        else
                        {
                            // Запуск службы операционной системы Windows
                            ServiceBase.Run(svc);
                        }
                        break;

                    case "console":
                        svc.Start();
                        Console.WriteLine("Для завершения нажмите Enter");
                        Console.ReadLine();
                        svc.Stop();
                        break;

                    case "install":
                        // Установка службы
                        ManagedInstallerClass.InstallHelper(new string[] { name });
                        Console.WriteLine("Для завершения нажмите Enter");
                        Console.ReadLine();
                        break;

                    case "delete":
                        // Установка службы
                        ManagedInstallerClass.InstallHelper(new string[] { "/u", name });
                        Console.WriteLine("Для завершения нажмите Enter");
                        Console.ReadLine();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
