using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Song
    {
        string Author { get; set; }
        string Name { get; set; }
        string Duration { get; set; }
        string path = @"../../Music.txt";

        public Song()
        {

        }
        public Song(string a, string n, string d)
        {
            Author = a;
            Name = n;
            Duration = d;
        }
        public void AddSong()
        {
            while (true)
            {
                Console.WriteLine("Enter song author or press ~ to exit:");
                string inputAuthor = Console.ReadLine();
                if (inputAuthor=="~")
                {
                    break;
                }
                while (String.IsNullOrEmpty(inputAuthor))
                {
                    Console.WriteLine("Invalid input. Please try again:");
                    inputAuthor = Console.ReadLine();
                }
                Console.WriteLine("Enter song name or press ~ to exit:");
                string inputName = Console.ReadLine();
                if (inputName=="~")
                {
                    break;
                }
                while (String.IsNullOrEmpty(inputName))
                {
                    Console.WriteLine("Invalid input. Please try again:");
                    inputName = Console.ReadLine();
                }
                Console.WriteLine("Enter song duration in format hh:mm:ss or press ~ to exit:");
                string inputDuration = Console.ReadLine();
                if (inputDuration=="~")
                {
                    break;
                }
                while (DurationValidation(inputDuration)==false)
                {
                    Console.WriteLine("Please enter song duration in correct format. Try again:");
                    inputDuration = Console.ReadLine();
                }
                Song s = new Song(inputAuthor, inputName, inputDuration);
                Console.WriteLine("Song is created!");
                Console.WriteLine(s.SongDisplay(s));

                s.WriteToFile(s);
                Console.WriteLine("Song is added to the music.txt file.");
            }
        }
        public void ReadFromFile()
        {
            StreamReader sr = new StreamReader(path);
            List<string> songList = new List<string>();
            string line = "";
            while ((line=sr.ReadLine())!=null)
            {
                songList.Add(line);
            }
            sr.Close();

            if (songList.Count==0)
            {
                Console.WriteLine("There are not any songs in file.");
            }
            else
            {
                foreach (string item in songList)
                {
                    Console.WriteLine(item);
                }
            }
        }
        public void WriteToFile(Song s)
        {
            StreamWriter sw = new StreamWriter(path, true);

            sw.WriteLine(SongDisplay(s));

            sw.Close();
        }
        public string SongDisplay(Song s)
        {
             string write = "["+s.Author+"]:["+s.Name+"] ["+s.Duration+"]" ;
            return write;
        }
        public bool DurationValidation(string duration)
        {
            char[] array = duration.ToCharArray();

            if (array.Count() ==8 && Char.IsDigit(array[0]) && Char.IsDigit(array[1]) && array[2]==':' && Char.IsDigit(array[3]) && Char.IsDigit(array[4]) && array[5]==':' && Char.IsDigit(array[6]) && Char.IsDigit(array[7]) )
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        

    }

}
