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
        int ID;
        string Author { get; set; }
        string Name { get; set; }
        string Duration { get; set; }
        string path = @"../../Music.txt";

        public Song()
        {

        }
        public Song(int i,string a, string n, string d)
        {
            Author = a;
            Name = n;
            Duration = d;
            ID = i;
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
                int id = CalculateID();
                Song s = new Song(id,inputAuthor, inputName, inputDuration);
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
                foreach (string item in songList)
                {
                    Console.WriteLine(CalculateDuration(item));
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
            string write = s.ID+" "+s.Author+":"+s.Name+" "+s.Duration+"" ;
            return write;
        }
        public bool DurationValidation(string duration)
        {
            char[] array = duration.ToCharArray();

            if (array.Count() ==8 && Char.IsDigit(array[0]) && Char.IsDigit(array[1]) && array[2]==':' && Char.IsDigit(array[3]) && Char.IsDigit(array[4]) && array[5]==':' && Char.IsDigit(array[6]) && Char.IsDigit(array[7]) && Convert.ToInt32(array[0].ToString())<6 && Convert.ToInt32(array[3].ToString()) < 6 && Convert.ToInt32(array[6].ToString()) < 6)
            {
                return true;
            }
           
            else
            {
                return false;
            }
        }
        public int CalculateID()
        {
            int lastID = 0;
            StreamReader sr = new StreamReader(path);
            List<string> songListforID = new List<string>();
            string line = "";
            while ((line = sr.ReadLine()) != null)
            {
                songListforID.Add(line);
            }
            sr.Close();

            if (songListforID.Count==0)
            {
                return lastID;
            }
            else
            {
                string lastRecord = songListforID[songListforID.Count - 1];
                string[] lastRecArray = lastRecord.Split(' ').ToArray();
                lastID = Convert.ToInt32(lastRecArray[0]);
            }       
            return lastID + 1;
        }
        public int CalculateDuration(string song)
        {
            int h = 0;
            int m = 0;
            int s = 0;
            int totalSeconds;
            List<string> stringList = song.Split(' ').ToList();

            string duration = stringList[stringList.Count - 1];

            string[] times = duration.Split(':').ToArray();
            h = Convert.ToInt32(times[0]);
            m = Convert.ToInt32(times[1]);
            s = Convert.ToInt32(times[2]);

            totalSeconds = s + (m * 60) + (h * 3600);
            return totalSeconds;
        }
      
        

    }

}
