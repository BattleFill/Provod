using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Provod
{
    public class ConsoleFileManager
    {
        private static string currentPath = "";

        public void Start()
        {
            while (true)
            {
                DisplayDrives();
                string input = Console.ReadLine();
                if (input.ToLower() == "exit") break;

                if (int.TryParse(input, out int selectedIndex))
                {
                    if (selectedIndex > 0 && selectedIndex <= DriveInfo.GetDrives().Length)
                    {
                        DriveInfo drive = DriveInfo.GetDrives()[selectedIndex - 1];
                        currentPath = drive.Name;
                        DisplayDirectoryContents(currentPath);
                        ExploreDirectory(currentPath);
                    }
                }
            }
        }

        private static void ExploreDirectory(string path)
        {
            while (true)
            {
                string input = Console.ReadLine();
                if (input.ToLower() == "exit")
                {
                    currentPath = Directory.GetParent(path)?.FullName ?? "";
                    return;
                }

                if (int.TryParse(input, out int selectedIndex))
                {
                    string[] entries = Directory.GetFileSystemEntries(path);
                    if (selectedIndex > 0 && selectedIndex <= entries.Length)
                    {
                        string selectedEntry = entries[selectedIndex - 1];
                        if (File.Exists(selectedEntry))
                        {
                            try
                            {
                                System.Diagnostics.Process.Start(selectedEntry);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                            }
                        }
                        else if (Directory.Exists(selectedEntry))
                        {
                            currentPath = selectedEntry;
                            DisplayDirectoryContents(currentPath);
                        }
                    }
                }
            }
        }

        private static void DisplayDrives()
        {
            Console.Clear();
            DriveInfo[] drives = DriveInfo.GetDrives();

            Console.WriteLine("Select a drive:");
            for (int i = 0; i < drives.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {drives[i].Name} - {drives[i].DriveFormat} ({drives[i].AvailableFreeSpace / (1024 * 1024 * 1024)}GB free of {drives[i].TotalSize / (1024 * 1024 * 1024)}GB)");
            }
            Console.WriteLine("Type 'exit' to exit.");
        }

        private static void DisplayDirectoryContents(string path)
        {
            Console.Clear();
            Console.WriteLine($"Contents of {path}:");
            string[] entries = Directory.GetFileSystemEntries(path);

            for (int i = 0; i < entries.Length; i++)
            {
                string entry = entries[i];
                string type = File.Exists(entry) ? "File" : "Directory";
                Console.WriteLine($"{i + 1}. {Path.GetFileName(entry)} - {type}");
            }
            Console.WriteLine("Type 'exit' to go up one level.");
        }
    }
}
