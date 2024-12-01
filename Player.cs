using CyberguardGame;

public class Player{
    
    public int X {get; private set;}
    
    public int Y {get; private set;}

    public Player(int x, int y){
        
        X = x;
        Y = y;
    }

    public void Move(Keys key, Maze maze){
        
        int newX = X;
        int newY = Y;

        switch (key){
            
            case Keys.W:
                newY--;             // Ruch w górę
                break;
            case Keys.A:
                newX--;             // Ruch w lewo
                break;
            case Keys.S:
                newY++;             // Ruch w dół
                break;
            case Keys.D:
                newX++;             // Ruch w prawo
                break;
        }

        // Sprawdzenie granic oraz czy nowa pozycja nie jest ścianą
        if (newX >= 0 && newX < maze.Width && newY >= 0 && newY < maze.Height && maze.Grid[newY, newX] != 1){
            
            X = newX;
            Y = newY;
        }
    }
}