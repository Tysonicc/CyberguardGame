using System;
using System.Windows.Forms; // Dodajemy przestrzeń nazw dla obsługi klawiszy

namespace MazeGame
{
    public class Player
    {
        public int X { get; set; } // Pozycja gracza na osi X
        public int Y { get; set; } // Pozycja gracza na osi Y

        // Konstruktor klasy gracza, przyjmuje początkowe pozycje X i Y
        public Player(int x, int y)
        {
            X = x;
            Y = y;
        }

        // Metoda do poruszania graczem w zależności od naciśniętego klawisza
        public void Move(Keys key, int maxX, int maxY)
        {
            switch (key)
            {
                // Poruszanie w górę, jeśli Y jest większe od 0
                case Keys.W:
                    if (Y > 0) Y--;
                    break;

                // Poruszanie w dół, jeśli Y jest mniejsze od maksymalnej wysokości
                case Keys.S:
                    if (Y < maxY - 1) Y++;
                    break;

                // Poruszanie w lewo, jeśli X jest większe od 0
                case Keys.A:
                    if (X > 0) X--;
                    break;

                // Poruszanie w prawo, jeśli X jest mniejsze od maksymalnej szerokości
                case Keys.D:
                    if (X < maxX - 1) X++;
                    break;
            }
        }
    }
}
