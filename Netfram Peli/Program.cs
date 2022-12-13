using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

using Raylib_CsLo;
using RenderTexture2D = Raylib_CsLo.RenderTexture;
using Texture2D = Raylib_CsLo.Texture;

namespace Netfram_Peli
{
    public class Program
    {
        //Setting up things for "graphics"
        const int screen_width = 800;
        const int screen_height = 600;
        public static int fps = 360;

        static void Main(string[] args)
        {
            Raylib.InitWindow(screen_width, screen_height, "Raylib");
            Raylib.SetTargetFPS(fps);

            Raylib.ClearBackground(Raylib.DARKGRAY);
            //Loading the background image what we want to draw 
            Raylib_CsLo.Image background = Raylib.LoadImage("C:\\Users\\s2000950\\Desktop\\NetframPelipropjekti\\Netfram Peli\\vikingfight.png");
            //Setting it to be a texture
            Texture2D texture = Raylib.LoadTextureFromImage(background);

            //Doing all the drawing needed for "graphics"
            while (!Raylib.WindowShouldClose())
            {
                Raylib.BeginDrawing();
                
                //Drawing the texture
                Raylib.DrawTexture(texture, screen_width / 1 - texture.width, screen_height / 1 - texture.height, Raylib.WHITE);

                //Draw some texts here
                Raylib.DrawText("VIKING FIGHT!", 12, 12, 20, Raylib.WHITE);
                Raylib.DrawText("\nWelcome to the game:", 12, 20, 20, Raylib.WHITE);
                Raylib.DrawText("\nPress esc to close this window:", 12, 60, 20, Raylib.WHITE);
                Raylib.DrawText("\nAnd start the fight:", 12, 100, 20, Raylib.WHITE);

                //Drawing current fps
                Raylib.DrawText("FPS: " + Raylib.GetFPS(), 600, 12, 20, Raylib.WHITE);

                //Drawing a green circle
                Raylib.DrawCircle(screen_width / 2, 400, 50, Raylib.DARKBLUE);

                Raylib.EndDrawing();
            }

            //After ending drawing lets unload the texture to save resources
            Raylib.UnloadTexture(texture);

            //Closing the window
            Raylib.CloseWindow();

            //After that lets start the game on console

            Console.Clear();
            //Calling Init from Battle.cs
            Battle.Init();

            // Introducing player to the game
            Battle.WriteLine("VIKING FIGHT!");
            Battle.WriteLine("\nWelcome to the game:");
            Battle.WriteLine("\n\n\nPress enter to continue:");
            Console.ReadLine();
            Console.Clear();

            //Tell to player about the armies
            Battle.WriteLine("[---------- Status ----------]\n");
            Battle.WriteLine("\nPlayer Army:  Enemy Army:");
            Console.WriteLine();
            Battle.pArmy.ForEach(pUnit => Battle.WriteLine(pUnit.Name, ConsoleColor.DarkYellow));
            int y = 5;
            foreach (var eUnit in Battle.eArmy)
            {
                Battle.WriteAt(eUnit.Name, 15, y, ConsoleColor.DarkMagenta);
                y++;
            }

            // Starting the fight
            Console.WriteLine();
            Battle.WriteLine("\nBattle Begins.");
            Console.ReadLine();
            Console.Clear();

            Battle.PlayerFighting();

            //Telling who won the game
            Battle.WriteLine("Thanks for playing!\n");
            Battle.WhatTeamWon();
        }
    }
}