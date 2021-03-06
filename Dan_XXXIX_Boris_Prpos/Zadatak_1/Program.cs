﻿using System;
using System.Threading;

namespace Zadatak_1
{
    class Program
    {

        static void Main(string[] args)
        {
            Song song = new Song();

            bool spin = true;
            while (spin)
            {
                Console.WriteLine("---Welcome to audio player---\n");
                Console.WriteLine("1: Add song");
                Console.WriteLine("2: View songs");
                Console.WriteLine("3: Start audio player");
                Console.WriteLine("4: Exit");

                string pick = Console.ReadLine();
                switch (pick)
                {
                    case "1":
                        song.AddSong();
                        break;
                    case "2":
                        song.ReadFromFile();
                        break;
                    case "3":
                        Thread SongStarter = new Thread(() => song.StartSong());
                        SongStarter.Start();
                        SongStarter.Join();
                        break;
                    case "4":
                        spin = false;
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please select one option from the list.");
                        break;
                }
            }

        }

    }
}
