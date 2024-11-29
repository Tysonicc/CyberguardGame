using System.Drawing;

namespace CyberguardGame;

public class Maze
{
    public int[,] Grid { get; private set; } // Reprezentacja labiryntu
    public int Width { get; private set; }    // Szerokość labiryntu
    public int Height { get; private set; }   // Wysokość labiryntu

    public (int X, int Y) EndPoint { get; set; }

    // Konstruktor klasy Labirynt, przyjmujący szerokość i wysokość
    public Maze(int width, int height)
    {
        Width = width;
        Height = height;
        Grid = new int[height, width]; // Inicjalizacja macierzy

    }

    // Metoda generująca labirynt na podstawie macierzy
    public void GenerateMaze(int[,] matrix, (int X, int Y) endPoint)
    {
        if (matrix.GetLength(0) != Height || matrix.GetLength(1) != Width)
        {
            throw new ArgumentException("Rozmiar macierzy nie zgadza się z wymiarami labiryntu.");
        }

        Grid = matrix; // Ustawienie macierzy labiryntu
        this.EndPoint = endPoint; // Ustawienie punktu końcowego
    }

    // Metoda rysująca labirynt w stylu Windows XP
    public void DrawMaze(Graphics g, int cellWidth, int cellHeight, (int X, int Y) start, (int X, int Y) end)
    {
        // Kolory charakterystyczne dla Windows XP
        Brush wallBrush = new SolidBrush(Color.Black);    // Czarny kolor dla ścian
        Brush emptySpaceBrush = new SolidBrush(Color.White); // Biały dla pustych przestrzeni
        Brush startBrush = new SolidBrush(Color.Green);   // Zielony dla punktu startowego
        Brush endBrush = new SolidBrush(Color.Red);       // Czerwony dla punktu końcowego

        // Rysowanie labiryntu
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                // Rysowanie ścian (czarne) i pustych przestrzeni (białe)
                if (Grid[y, x] == 1) // Ściana
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

        // Rysowanie punktu końcowego w kolorze czerwonym
        g.FillRectangle(endBrush, end.X * cellWidth, end.Y * cellHeight, cellWidth, cellHeight);
    }
}