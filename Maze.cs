using System.Drawing;

namespace CyberguardGame;

public class Maze                                   /** Klasa reprezentująca labirynt */
{
    public int[,] Grid {get; private set;}          /** Macierz reprezentująca labirynt */
    public int Width {get; private set;}            /** Szerokość labiryntu */
    public int Height {get; private set;}           /** Wysokość labiryntu */
    public (int X, int Y) EndPoint {get; set;}

    public Maze(int width, int height)              /** Konstruktor klasy, który inicjalizuje szerokość i wysokość labiryntu */
    {
        Width = width;                              /** Ustawienie szerokości labiryntu */
        Height = height;                            /** Ustawienie wysokości labiryntu */
        Grid = new int[height, width];              /** Inicjalizacja macierzy */
    }

    /** Generowanie labiryntu na podstawie danej macierzy */
    public void GenerateMaze(int[,] matrix, (int X, int Y) endPoint)
    {
        /** Sprawdzenie, czy rozmiar macierzy zgadza się z wymiarami labiryntu  */
        if (matrix.GetLength(0) != Height || matrix.GetLength(1) != Width)
        {
            throw new ArgumentException("Rozmiar macierzy nie zgadza się z wymiarami labiryntu.");
        }

        Grid = matrix;                              /** Ustawienie macierzy labiryntu */
        this.EndPoint = endPoint;                   /** Ustawienie punktu końcowego */
    }
}