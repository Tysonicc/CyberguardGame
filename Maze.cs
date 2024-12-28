using System.Drawing;

namespace CyberguardGame;

/** Klasa reprezentujaca labirynt */
public class Maze                                   
{
    /** Macierz reprezentujaca labirynt */
    public int[,] Grid {get; private set;}
    /** Szerokosc labiryntu */
    public int Width {get; private set;}
    /** Wysokosc labiryntu */
    public int Height {get; private set;}           
    
    public (int X, int Y) EndPoint {get; set;}

    /** Konstruktor klasy, ktory inicjalizuje szerokosz i wysokosc labiryntu */
    public Maze(int width, int height)              
    {
        /** Ustawienie szerokosci labiryntu */
        Width = width;
        /** Ustawienie wysokosci labiryntu */
        Height = height;
        /** Inicjalizacja macierzy */
        Grid = new int[height, width];              
    }

    /** Generowanie labiryntu na podstawie danej macierzy */
    public void GenerateMaze(int[,] matrix, (int X, int Y) endPoint)
    {
        /** Sprawdzenie, czy rozmiar macierzy zgadza sie z wymiarami labiryntu  */
        if (matrix.GetLength(0) != Height || matrix.GetLength(1) != Width)
        {
            throw new ArgumentException("Rozmiar macierzy nie zgadza sie z wymiarami labiryntu.");
        }

        /** Ustawienie macierzy labiryntu */
        Grid = matrix;
        /** Ustawienie punktu końcowego */
        this.EndPoint = endPoint;                   
    }
}