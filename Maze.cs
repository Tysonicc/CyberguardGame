using System.Drawing;

namespace CyberguardGame;

public class Maze
{
    public int[,] Grid {get; private set;}          // Reprezentacja labiryntu
    public int Width {get; private set;}            // Szerokość labiryntu
    public int Height {get; private set;}           // Wysokość labiryntu
    public (int X, int Y) EndPoint {get; set;}

    public Maze(int width, int height)
    {
        Width = width;
        Height = height;
        Grid = new int[height, width];              // Inicjalizacja macierzy
    }

    // Generowanie labiryntu na podstawie macierzy
    public void GenerateMaze(int[,] matrix, (int X, int Y) endPoint)
    {
        if (matrix.GetLength(0) != Height || matrix.GetLength(1) != Width)
        {
            throw new ArgumentException("Rozmiar macierzy nie zgadza się z wymiarami labiryntu.");
        }

        Grid = matrix;                              // Ustawienie macierzy labiryntu
        this.EndPoint = endPoint;                   // Ustawienie punktu końcowego
    }
}