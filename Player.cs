using CyberguardGame;

public class Player
{
    public int X {get; private set;}
    public int Y {get; private set;}
    public Player(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Move(Keys key, Maze maze)
    {
        int newX = X;
        int newY = Y;

        switch (key)
        {
            case Keys.W:
                newY--;
                break;
            case Keys.A:
                newX--;
                break;
            case Keys.S:
                newY++;
                break;
            case Keys.D:
                newX++;
                break;
        }

        if (newX >= 0 && newX < maze.Width && newY >= 0 && newY < maze.Height && maze.Grid[newY, newX] != 1)
        {
            X = newX;
            Y = newY;
        }
    }
}