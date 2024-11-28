using System;
using System.Drawing;

namespace MazeGame
{
    public class Maze
    {
        public int[,] Grid { get; private set; } // Reprezentacja labiryntu
        public int Width { get; private set; }   // Szerokość labiryntu
        public int Height { get; private set; }  // Wysokość labiryntu

        // Konstruktor klasy Labirynt, przyjmujący szerokość i wysokość
        public Maze(int width, int height)
        {
            Width = width;
            Height = height;
            Grid = new int[width, height];
        }

        // Metoda generująca labirynt na podstawie macierzy
        public void GenerateMaze(int[,] matrix)
        {
            Grid = matrix;
        }

        // Metoda rysująca labirynt w stylu Windows XP
        public void DrawMaze(Graphics g, int cellWidth, int cellHeight, (int X, int Y) start, (int X, int Y) end)
        {
            // Kolory charakterystyczne dla Windows XP
            Brush wallBrush = new SolidBrush(Color.FromArgb(0, 0, 0));    // Czarny kolor dla ścian
            Brush emptySpaceBrush = new SolidBrush(Color.FromArgb(255, 255, 255)); // Biały dla pustych przestrzeni
            Brush startBrush = new SolidBrush(Color.FromArgb(0, 255, 0));   // Zielony dla punktu startowego
            Brush endBrush = new SolidBrush(Color.FromArgb(255, 0, 0));     // Czerwony dla punktu końcowego

            // Rysowanie labiryntu
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    // Rysowanie ścian (czarne) i pustych przestrzeni (białe)
                    if (Grid[x, y] == 1) // Ściana
                    {
                        g.FillRectangle(wallBrush, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                    }
                    else // Pusta przestrzeń
                    {
                        g.FillRectangle(emptySpaceBrush, x * cellWidth, y * cellHeight, cellWidth, cellHeight);
                    }
                }
            }

            // Rysowanie punktu startowego
            g.FillRectangle(startBrush, start.X * cellWidth, start.Y * cellHeight, cellWidth, cellHeight);

            // Rysowanie punktu końcowego
            g.FillRectangle(endBrush, end.X * cellWidth, end.Y * cellHeight, cellWidth, cellHeight);
        }
    }
}
