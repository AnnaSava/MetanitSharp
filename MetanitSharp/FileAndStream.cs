using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetanitSharp
{
    class FileAndStream
    {
        public static void Run()
        {
            char key;

            while (true)
            {
                printMenu();

                key = Console.ReadKey().KeyChar;

                Console.WriteLine();
                switch (key)
                {
                    case 'd':
                        usingDrive();
                        break;
                    case 'r':
                        showDirs();
                        showDirInfo();
                        createAndRemoveDir();
                        moveDir();
                        break;
                    case 'f':
                        usingFiles();
                        break;
                    case 'x': return;
                }
                Console.ReadKey();
            }
        }

        static void printMenu()
        {
            Console.WriteLine("Нажмите клавишу для вывода информации");
            Console.WriteLine("D - работа с дисками");
            Console.WriteLine("R - работа с каталогами");
            Console.WriteLine("F - работа с файлами");
            Console.WriteLine("X - выход из раздела");
        }

        static void usingDrive()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();

            foreach (DriveInfo drive in drives)
            {
                Console.WriteLine("Название: {0}", drive.Name);
                Console.WriteLine("Тип: {0}", drive.DriveType);
                if (drive.IsReady)
                {
                    Console.WriteLine("Файловая система: {0}", drive.DriveFormat);
                    Console.WriteLine("Объем диска: {0}", drive.TotalSize);
                    Console.WriteLine("Свободное пространство: {0}", drive.TotalFreeSpace);
                    Console.WriteLine("Метка: {0}", drive.VolumeLabel);

                }
                Console.WriteLine();
            }
        }

        static void showDirs()
        {
            string dirName = "C:\\";

            if (Directory.Exists(dirName))
            {
                Console.WriteLine("Подкаталоги:");
                string[] dirs = Directory.GetDirectories(dirName);
                foreach (string s in dirs)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
                Console.WriteLine("Файлы:");
                string[] files = Directory.GetFiles(dirName);
                foreach (string s in files)
                {
                    Console.WriteLine(s);
                }
            }
        }

        static void showDirInfo()
        {
            string dirName = "C:\\Program Files";

            DirectoryInfo dirInfo = new DirectoryInfo(dirName);

            Console.WriteLine("Название каталога: {0}", dirInfo.Name);
            Console.WriteLine("Полное название каталога: {0}", dirInfo.FullName);
            Console.WriteLine("Время создания каталога: {0}", dirInfo.CreationTime);
            Console.WriteLine("Корневой каталог: {0}", dirInfo.Root);
        }

        static void createAndRemoveDir()
        {
            string path = @"C:\MetanitDir";
            string subpath = @"program\avalon";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
            DirectoryInfo subDir = dirInfo.CreateSubdirectory(subpath);
            Console.WriteLine($"Создан каталог {subDir.FullName}");
            Console.Write("Нажмите любую клавишу для продолжения");
            Console.ReadLine();

            try
            {
                subDir.Delete(true);
                Console.WriteLine($"Удален каталог {subDir.FullName}");

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.Write("Нажмите любую клавишу для продолжения");
            Console.ReadLine();

            try
            {
                Directory.Delete(path, true);
                Console.WriteLine($"Удален каталог {path}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void moveDir()
        {
            string oldPath = @"C:\MetanitDir1";
            string newPath = @"C:\MetanitDir2";

            if (Directory.Exists(oldPath) == false)
            {
                Directory.CreateDirectory(oldPath);
                Console.WriteLine($"Создан каталог {oldPath}");
                Console.Write("Нажмите любую клавишу для продолжения");
                Console.ReadLine();
            }

            DirectoryInfo dirInfo = new DirectoryInfo(oldPath);
            if (dirInfo.Exists && Directory.Exists(newPath) == false)
            {
                dirInfo.MoveTo(newPath);
                Console.WriteLine($"Каталог {oldPath} перемещен в {newPath}");
                Console.Write("Нажмите любую клавишу для продолжения");
                Console.ReadLine();
            }

            try
            {
                Directory.Delete(newPath, true);
                Console.WriteLine($"Удален каталог {newPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void usingFiles()
        {
            // TODO: создавать нужные папки и файлы

            usingFileInfo();
            usingFile();

            // TODO: удалять папки и файлы
        }

        static void usingFileInfo()
        {
            string path = @"C:\Metanit\file.txt";
            string newPath = @"C:\Metanit\Files\file.txt";

            FileInfo fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                Console.WriteLine("Имя файла: {0}", fileInf.Name);
                Console.WriteLine("Время создания: {0}", fileInf.CreationTime);
                Console.WriteLine("Размер: {0}", fileInf.Length);

                fileInf.MoveTo(newPath);
                Console.WriteLine($"Файл перемещен в {newPath}");
                Console.WriteLine($"Полное имя файла fileInf {fileInf.FullName}");
                Console.Write("Нажмите любую клавишу для продолжения");
                Console.ReadLine();

                fileInf.CopyTo(path, true);
                Console.WriteLine($"Файл скопирован в {path}");
                Console.WriteLine($"Полное имя файла fileInf {fileInf.FullName}"); 
                Console.Write("Нажмите любую клавишу для продолжения");
                Console.ReadLine();

                fileInf.Delete();
                Console.WriteLine($"Файл {fileInf.FullName} удален");
                Console.Write("Нажмите любую клавишу для продолжения");
                Console.ReadLine();
            }
        }

        static void usingFile()
        {
            string path = @"C:\Metanit\file.txt";
            string newPath = @"C:\Metanit\Files\file.txt";
            
            if (File.Exists(path))
            {
                Console.WriteLine("Имя файла: {0}", path);
                Console.WriteLine("Время создания: {0}", File.GetCreationTime(path));

                File.Move(path, newPath);
                Console.WriteLine($"Файл перемещен в {newPath}");
                Console.Write("Нажмите любую клавишу для продолжения");
                Console.ReadLine();

                File.Copy(newPath, path);
                Console.WriteLine($"Файл скопирован в {path}");
                Console.Write("Нажмите любую клавишу для продолжения");
                Console.ReadLine();

                File.Delete(newPath);
                Console.WriteLine($"Файл {newPath} удален");
                Console.Write("Нажмите любую клавишу для продолжения");
                Console.ReadLine();
            }
        }
    }
}
