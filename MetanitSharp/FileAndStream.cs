using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
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
                    case 's':
                        usingFileStream();
                        usingSeek();
                        closeFileStream();
                        break;
                    case 'm':
                        usingStreamReader();
                        usingStreamWriter();
                        break;
                    case 'b':
                        usingBinaryReader();
                        break;
                    case 'z':
                        usingGZip();
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
            Console.WriteLine("S - чтение и запись в файл через FileStream");
            Console.WriteLine("M - StreamReader и StreamWriter");
            Console.WriteLine("B - BinaryReader и BinaryWriter");
            Console.WriteLine("Z - архивация GZipStream");
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

        static void usingFileStream()
        {
            string filePath = @"C:\Metanit\note.txt";

            Console.WriteLine("Введите строку для записи в файл:");
            string text = Console.ReadLine();

            // запись в файл
            using (FileStream fstream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] array = System.Text.Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);
                Console.WriteLine("Текст записан в файл");
            }

            // чтение из файла
            using (FileStream fstream = File.OpenRead(filePath))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                string textFromFile = System.Text.Encoding.Default.GetString(array);
                Console.WriteLine("Текст из файла: {0}", textFromFile);
            }
        }

        static void usingSeek()
        {
            string text = "hello world";
            string filePath = @"C:\Metanit\note.dat";

            // запись в файл
            using (FileStream fstream = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                // преобразуем строку в байты
                byte[] input = Encoding.Default.GetBytes(text);
                // запись массива байтов в файл
                fstream.Write(input, 0, input.Length);
                Console.WriteLine($"Текст записан в файл {filePath}");

                // перемещаем указатель в конец файла, до конца файла- пять байт
                fstream.Seek(-5, SeekOrigin.End); // минус 5 символов с конца потока

                // считываем четыре символов с текущей позиции
                byte[] output = new byte[4];
                fstream.Read(output, 0, output.Length);
                // декодируем байты в строку
                string textFromFile = Encoding.Default.GetString(output);
                Console.WriteLine("Текст из файла: {0}", textFromFile); // worl

                // заменим в файле слово world на слово house
                string replaceText = "house";
                fstream.Seek(-5, SeekOrigin.End); // минус 5 символов с конца потока
                input = Encoding.Default.GetBytes(replaceText);
                fstream.Write(input, 0, input.Length);

                // считываем весь файл
                // возвращаем указатель в начало файла
                fstream.Seek(0, SeekOrigin.Begin);
                output = new byte[fstream.Length];
                fstream.Read(output, 0, output.Length);
                // декодируем байты в строку
                textFromFile = Encoding.Default.GetString(output);
                Console.WriteLine("Текст из файла: {0}", textFromFile); // hello house
            }
        }

        static void closeFileStream()
        {
            string filePath = @"C:\Metanit\note.dat";
            FileStream fstream = null;
            try
            {
                fstream = new FileStream(filePath, FileMode.OpenOrCreate);

                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);

                array[fstream.Length + 1] = 2; // IndexOutOfRange

                fstream.Write(array, 0, array.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (fstream != null)
                {
                    fstream.Close();

                    Console.WriteLine("Файл закрыт");
                    try
                    {
                        Console.WriteLine(fstream.Length);
                    }
                    catch (Exception ex2)
                    {
                        Console.WriteLine(ex2.Message);
                    }
                }
            }

        }

        static void usingStreamReader()
        {
            string path = @"C:\Metanit\cats.txt";

            try
            {
                Console.WriteLine("******считываем весь файл********");
                using (StreamReader sr = new StreamReader(path))
                {
                    Console.WriteLine(sr.ReadToEnd());
                }

                Console.WriteLine();
                Console.WriteLine("******считываем построчно********");
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }

                Console.WriteLine();
                Console.WriteLine("******считываем блоками********");
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
                {
                    char[] array = new char[4];
                    // считываем 4 символа
                    sr.Read(array, 0, 4);

                    Console.WriteLine(array);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void usingStreamWriter()
        {
            string readPath = @"C:\Metanit\cats.txt";
            string writePath = @"C:\Metanit\ourcats.txt";

            string text = "";
            try
            {
                using (StreamReader sr = new StreamReader(readPath, System.Text.Encoding.Default))
                {
                    text = sr.ReadToEnd();
                }
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }

                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("Дозапись");
                    sw.Write(4.5);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void usingBinaryReader()
        {
            State[] states = new State[2];
            states[0] = new State("Германия", "Берлин", 357168, 80.8);
            states[1] = new State("Франция", "Париж", 640679, 64.7);

            string path = @"C:\Metanit\states.dat";

            try
            {
                // создаем объект BinaryWriter
                using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
                {
                    // записываем в файл значение каждого поля структуры
                    foreach (State s in states)
                    {
                        writer.Write(s.name);
                        writer.Write(s.capital);
                        writer.Write(s.area);
                        writer.Write(s.people);
                    }
                }
                // создаем объект BinaryReader
                using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
                {
                    // пока не достигнут конец файла
                    // считываем каждое значение из файла
                    while (reader.PeekChar() > -1)
                    {
                        string name = reader.ReadString();
                        string capital = reader.ReadString();
                        int area = reader.ReadInt32();
                        double population = reader.ReadDouble();

                        Console.WriteLine("Страна: {0}  столица: {1}  площадь {2} кв. км   численность населения: {3} млн. чел.",
                            name, capital, area, population);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void usingGZip()
        {
            string sourceFile = "C://Metanit/autumn.bmp"; // исходный файл
            string compressedFile = "C://Metanit/autumn.gz"; // сжатый файл
            string targetFile = "C://Metanit/autumn_new.bmp"; // восстановленный файл

            // создание сжатого файла
            Compress(sourceFile, compressedFile);
            // чтение из сжатого файла
            Decompress(compressedFile, targetFile);
        }

        public static void Compress(string sourceFile, string compressedFile)
        {
            // поток для чтения исходного файла
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.OpenOrCreate))
            {
                // поток для записи сжатого файла
                using (FileStream targetStream = File.Create(compressedFile))
                {
                    // поток архивации
                    using (GZipStream compressionStream = new GZipStream(targetStream, CompressionMode.Compress))
                    {
                        sourceStream.CopyTo(compressionStream); // копируем байты из одного потока в другой
                        Console.WriteLine("Сжатие файла {0} завершено. Исходный размер: {1}  сжатый размер: {2}.",
                            sourceFile, sourceStream.Length.ToString(), targetStream.Length.ToString());
                    }
                }
            }
        }

        public static void Decompress(string compressedFile, string targetFile)
        {
            // поток для чтения из сжатого файла
            using (FileStream sourceStream = new FileStream(compressedFile, FileMode.OpenOrCreate))
            {
                // поток для записи восстановленного файла
                using (FileStream targetStream = File.Create(targetFile))
                {
                    // поток разархивации
                    using (GZipStream decompressionStream = new GZipStream(sourceStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(targetStream);
                        Console.WriteLine("Восстановлен файл: {0}", targetFile);
                    }
                }
            }
        }
        struct State
        {
            public string name;
            public string capital;
            public int area;
            public double people;

            public State(string n, string c, int a, double p)
            {
                name = n;
                capital = c;
                people = p;
                area = a;
            }
        }
    }
}
