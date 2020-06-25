using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Zadatak_1
{
    class Song
    {
        static int durationMs = 0;
        static CountdownEvent countdown = new CountdownEvent(1);
        static CountdownEvent countdown2 = new CountdownEvent(1);
        int ID;
        string Author { get; set; }
        string Name { get; set; }
        string Duration { get; set; }
        string path = @"../../Music.txt";

        public Song()
        {

        }
        public Song(int i, string a, string n, string d)
        {
            Author = a;
            Name = n;
            Duration = d;
            ID = i;
        }
        /// <summary>
        /// Method for adding new songs
        /// </summary>
        public void AddSong()
        {
            while (true)
            {
                //author input and validation
                Console.WriteLine("Enter song author or press ~ to exit:");
                string inputAuthor = Console.ReadLine();
                if (inputAuthor == "~")
                {
                    break;
                }
                while (String.IsNullOrEmpty(inputAuthor))
                {
                    Console.WriteLine("Invalid input. Please try again:");
                    inputAuthor = Console.ReadLine();
                }
                //title input and validation
                Console.WriteLine("Enter song name or press ~ to exit:");
                string inputName = Console.ReadLine();
                if (inputName == "~")
                {
                    break;
                }
                while (String.IsNullOrEmpty(inputName))
                {
                    Console.WriteLine("Invalid input. Please try again:");
                    inputName = Console.ReadLine();
                }
                //duration input and validation
                Console.WriteLine("Enter song duration in format hh:mm:ss or press ~ to exit:");
                string inputDuration = Console.ReadLine();
                if (inputDuration == "~")
                {
                    break;
                }
                //calling method for duration validation (format must be hh:mm:ss)
                while (DurationValidation(inputDuration) == false)
                {
                    Console.WriteLine("Please enter song duration in correct format. Try again:");
                    inputDuration = Console.ReadLine();
                }
                //caling method for id calculation
                int id = CalculateID();
                Song s = new Song(id, inputAuthor, inputName, inputDuration);
                Console.WriteLine("Song is created!");
                Console.WriteLine(s.SongDisplay(s));
                //writing to file in specified format
                s.WriteToFile(s);
                Console.WriteLine("Song is added to the music.txt file.");
            }
        }
        /// <summary>
        /// Taking data from file
        /// </summary>
        public void ReadFromFile()
        {
            StreamReader sr = new StreamReader(path);
            List<string> songList = new List<string>();
            string line = "";
            int count = 0;
            while ((line = sr.ReadLine()) != null)
            {
                //putting lines to list
                songList.Add(line);
                count++;
            }
            sr.Close();
            //in case that file is empty
            if (songList.Count == 0 || count==0 )
            {
                Console.WriteLine("There are not any songs in file.");
            }
            else
            {
                //displaying content from file
                foreach (string item in songList)
                {
                    Console.WriteLine(item);
                }           
            }
        }
        /// <summary>
        /// Method used to write songs to file
        /// </summary>
        /// <param name="s"></param>
        public void WriteToFile(Song s)
        {
            StreamWriter sw = new StreamWriter(path, true);
            //calling method that specifies format
            sw.WriteLine(SongDisplay(s));

            sw.Close();
        }
        /// <summary>
        /// MEthod specifies format for text writing
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public string SongDisplay(Song s)
        {
            string write = s.ID + " " + s.Author + ":" + s.Name + " " + s.Duration + "";
            return write;
        }
        /// <summary>
        /// Method validates input for song duration
        /// </summary>
        /// <param name="duration"></param>
        /// <returns></returns>
        public bool DurationValidation(string duration)
        {
            char[] array = duration.ToCharArray();
            //every char must be exact (hh:mm:ss) => number or ":", some number can not be bigger than 5 =>(59:59:59)
            if (array.Count() == 8 && Char.IsDigit(array[0]) && Char.IsDigit(array[1]) && array[2] == ':' && Char.IsDigit(array[3]) && Char.IsDigit(array[4]) && array[5] == ':' && Char.IsDigit(array[6]) && Char.IsDigit(array[7]) && Convert.ToInt32(array[0].ToString()) < 6 && Convert.ToInt32(array[3].ToString()) < 6 && Convert.ToInt32(array[6].ToString()) < 6)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Method takes last record from file and increments its ID
        /// </summary>
        /// <returns></returns>
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

            if (songListforID.Count == 0)
            {
                return lastID;
            }
            //Taking last ID from file and adding 1 to it=>look format in file to understand better
            else
            {
                string lastRecord = songListforID[songListforID.Count - 1];
                string[] lastRecArray = lastRecord.Split(' ').ToArray();
                lastID = Convert.ToInt32(lastRecArray[0]);
            }
            return lastID + 1;
        }
        /// <summary>
        /// Method takes string that represents song, than splits song and takes duration and than splits duration into hh,mm,ss
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
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
            //converting string hh:mm:ss into int 
            totalSeconds = s + (m * 60) + (h * 3600);
            return totalSeconds;
        }
        /// <summary>
        /// Based on selected id, this method targets string that represents wanted song
        /// after that, it calculates duration for that song
        /// </summary>
        /// <returns></returns>
        public int PickSong()
        {
            int duration = 0;
            //calling method to display available songs
            ReadFromFile();
            while (true)
            {
                StreamReader sr = new StreamReader(path);
                List<string> songList = new List<string>();
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    songList.Add(line);
                }
                sr.Close();
                //list of IDs taken from strings that represent song
                List<int> IDlist = new List<int>();

                for (int i = 0; i < songList.Count; i++)
                {
                    string[] array = songList[i].Split(' ').ToArray();
                    IDlist.Add(Convert.ToInt32(array[0]));
                }
                if (songList.Count == 0)
                {
                    break;
                }
                //oportunity for user to exit from method
                Console.WriteLine("Press ~ to exit or any other key to continue:");
                string input = Console.ReadLine();

                if (input == "~")
                {
                    break;
                }
                //input and validation for song selection based on displayed list
                Console.WriteLine("Select song from the list:");
                int pickedSongID;
                bool tryPick = Int32.TryParse(Console.ReadLine(), out pickedSongID);
                while (!tryPick || !IDlist.Contains(pickedSongID))
                {
                    Console.WriteLine("Please select one of the options from song list.");
                    tryPick = Int32.TryParse(Console.ReadLine(), out pickedSongID);
                }
                string targetSong = "";
                //finding song with selected id, comparing user input with id from file(id from file = part of the string that represents song)
                for (int i = 0; i < songList.Count; i++)
                {
                    string[] array = songList[i].Split(' ').ToArray();
                    if (Convert.ToInt32(array[0]) == pickedSongID)
                    {
                        //calculating duration using method
                        duration = CalculateDuration(songList[i]);
                        //extracting wanted song
                        targetSong = songList[i];
                    }
                }
                Console.WriteLine("Name of the selected song is:{0}\nTime of reproduction:{1}\n\nPress enter at any time to stop.\n", ExtractTitle(targetSong), DateTime.Now.ToString("h:mm:ss tt"));
                break;
            }
            return duration;
        }
        /// <summary>
        /// Method splits song in order to extract title=> see code and than see format in file, it will be easier to understand
        /// </summary>
        /// <param name="song"></param>
        /// <returns></returns>
        public string ExtractTitle(string song)
        {
            string[] array = song.Split(':').ToArray();
            string afterDot = array[1];
            string[] afterDotArray = afterDot.Split(' ').ToArray();
            string[] title = afterDotArray.Take(afterDotArray.Length - 1).ToArray();
            string titleRet = String.Concat(title);
            return titleRet;
        }
        /// <summary>
        /// Method that will display comercials every 200 mS
        /// </summary>
        public void PopComercial()
        {
            //taking comercials from file
            string pathComercial = @"../../Comercial.txt";
            StreamReader sr = new StreamReader(pathComercial);
            string line = "";
            //inserting comercials into list
            List<string> comercials = new List<string>();
            while ((line = sr.ReadLine()) != null)
            {
                comercials.Add(line);
            }
            Random rnd = new Random();
            //it runs until it gets countdown.signal in line 338
            while (!countdown.IsSet)
            {
                //displays random comercial from the list
                Thread.Sleep(200);
                int random = rnd.Next(0, 5);
                Console.WriteLine("\t" + comercials[random]);
            }
        }
        /// <summary>
        /// Method that shows how long song is playing
        /// </summary>
        public void StartSong()
        {
            Thread EventExitThread = new Thread(() => EventExit());
            Thread ComercialStarter = new Thread(() => PopComercial());
            //getting duration of the song using method
            int durationSec = PickSong();
            //converting time into mS
            durationMs = durationSec * 1000;   
            //starting thread that pops comercial
            ComercialStarter.Start();
            //starting thread allows to exit with enter
            EventExitThread.Start();
            //every 1s message is displayed
            //countdown2 is signal from line 349=>means that enter is pressed and while loop breaks
            while (durationMs != 0 && !countdown2.IsSet)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Song is playing...");
                //with this =>while will stop after enough iterations
                durationMs -= 1000;
            }
            //signaling to comercials to stop=>line 305
            countdown.Signal();

            Thread.Sleep(2000);
            Console.WriteLine("\nSong has finished.\n");
            //reset countdown counter
            countdown.Reset();
        }
        /// <summary>
        /// Method uses event to stop song
        /// </summary>
        public void EventExit()
        {
            Delegate d = new Delegate();
            //if enter is pressed ecent is called
            if (Console.ReadKey(true).Key == ConsoleKey.Enter)
            {
                //signals to line 330
                countdown2.Signal();
                d.Exit();
                countdown2.Reset();
            }
        }
    }
    /// <summary>
    /// Contains everything for delegate
    /// </summary>
    class Delegate
    {
        public delegate void Notification();

        public event Notification OnNotification;

        public void Exit()
        {
            OnNotification += () =>
            {
                Thread.Sleep(2001);
                Console.WriteLine("Song is stopped.");
            };
            OnNotification.Invoke();
        }
    }

}
