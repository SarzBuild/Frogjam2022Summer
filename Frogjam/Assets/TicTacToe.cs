using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{

    // TO DO:
    // signal start of player's turn
    // dialogue
    // make "play again?" option show up

    // Contains tiles that make up the board
    // 0 is top left, 1 is top center, 2 is top right, 3 is center left, etc.
    public GameObject[] Tiles;
    // List of spawned pieces
    private List<GameObject> _piecesList;

    // Prefabs for x and o
    public GameObject X;
    public GameObject O;

    // Piece types to populate the board
    public enum Pieces
    { 
        X,
        O,
        None
    }

    [HideInInspector] public Pieces PlayerPiece; // Piece used by the player
    private Pieces _frogPiece; // Piece used by the frog

    // int[][] for the board.
    // (0,0) is top left, (2,2) is bottom right
    public Pieces[,] Board; 

    // Turn tracker
    [HideInInspector] public bool IsPlayerTurn = true;
    private (int, int) _lastPlayerMove;
    private (int, int) _lastFrogMove;

    // End of game state trackers
    private bool _boardIsFull = false;
    private bool _victory = false;

    private void Awake()
    {
        _piecesList = new List<GameObject>();
        Board = new Pieces[3, 3];
        StartNewGame();
    }

    // Places a piece on the board
    public void PlacePiece(int tileIndex, Pieces piece)
    {
        (int, int) newPieceCoordinates = GetCoordinatesFromTileIndex(tileIndex);
        if (Board[newPieceCoordinates.Item1, newPieceCoordinates.Item2] != Pieces.None)
        {
            // Space occupied!
            Debug.Log("Space occupied!");
            // TODO: Frog says "that's cheating!"
            // TODO: Piece is destroyed
            return;
        }

        // Update board
        Board[newPieceCoordinates.Item1, newPieceCoordinates.Item2] = piece;
        // Instantiate piece
        GameObject pieceToInstantiate;
        if(piece == Pieces.X)
        {
            pieceToInstantiate = X;
        }
        else
        {
            pieceToInstantiate = O;
        }
        // Track moves
        if(IsPlayerTurn)
        {
            _lastPlayerMove = newPieceCoordinates;
        }
        else
        {
            _lastFrogMove = newPieceCoordinates;
        }

        _piecesList.Add(Instantiate(pieceToInstantiate, new Vector3(Tiles[tileIndex].transform.position.x, Tiles[tileIndex].transform.position.y, 1), Quaternion.identity));
        CheckForGameEnd(newPieceCoordinates, piece);

        if(IsPlayerTurn && _piecesList.Count > 0) // Player's turn switches are handled here. Froggerina's turn switches are handled in her coroutine, which first call this function.
        {
            SwitchTurns();
        }
        
    }
    
    // Check if the game is over
    private void CheckForGameEnd((int, int) latestPieceCoordinates, Pieces pieceJustPlaced)
    {
        // Check row
        for (int x = 0; x <= 2; x++)
        {
            if(Board[x, latestPieceCoordinates.Item2] != pieceJustPlaced)
            {
                break;
            }
            if(x == 2)
            {
                _victory = true;
                GameEnd();
            }
        }
        // Check column
        for (int y = 0; y <= 2; y++)
        {
            if (Board[latestPieceCoordinates.Item1, y] != pieceJustPlaced)
            {
                break;
            }
            if (y == 2)
            {
                _victory = true;
                GameEnd();
            }
        }

        int tileIndex = GetTileIndexFromCoordinates(latestPieceCoordinates.Item1, latestPieceCoordinates.Item2);
        // Check diagonals, if applicable (code's a bit gross but hey we jammin)
        if (tileIndex % 2 == 0)
        {
            if(latestPieceCoordinates.Item1 + latestPieceCoordinates.Item2 == 2)
            {
                // Check spaces 2, 4, and 6
                bool loopBreak = false;
                for(int x = 0; x <= 2; x++)
                {
                    for(int y = 0; y <= 2; y++)
                    {
                        if(x + y == 2)
                        {
                            if(Board[x, y] != pieceJustPlaced)
                            {
                                loopBreak = true;
                                break;
                            }
                            if (x == 2)
                            {
                                _victory = true;
                                GameEnd();
                            }
                        }
                        
                    }
                    if(loopBreak)
                    {
                        break;
                    }
                }
            }
            if(tileIndex % 4 == 0)
            {
                // Check spaces 0, 4, and 8
                bool loopBreak = false;
                for (int x = 0; x <= 2; x++)
                {
                    for (int y = 0; y <= 2; y++)
                    {
                        if (GetTileIndexFromCoordinates(x, y) % 4 == 0)
                        {
                            if(Board[x, y] != pieceJustPlaced)
                            {
                                loopBreak = true;
                                break;
                            }
                            if (x == 2)
                            {
                                _victory = true;
                                GameEnd();
                            }
                        }
                        
                    }
                    if (loopBreak)
                    {
                        break;
                    }
                }
            }
        }
        // Check if board full
        for (int x = 0; x <= 2; x++)
        {
            for (int y = 0; y <= 2; y++)
            {
                if(Board[x, y] == Pieces.None)
                {
                    return; // Board is not full, return
                }
            }
        }
        // Board is full
        _boardIsFull = true;
        GameEnd();
    }

    // Switch the active player
    private void SwitchTurns()
    {
        IsPlayerTurn = !IsPlayerTurn;
        if (!IsPlayerTurn)
        {
            StartCoroutine(FrogTurn());
        }
        else
        {
            // TODO: signal to player that it's their turn
        }
    }

    IEnumerator FrogTurn()
    {
        int initialSeconds = (int)Random.Range(1, 3);
        yield return new WaitForSeconds(initialSeconds);
        // Frog determines where to play and places its piece. There might be dialogue within this function.
        (int, int) frogMove = DetermineFrogMove();
        // Slight delay, to allow player to read dialogue
        int premoveSeconds = (int)Random.Range(2, 4);
        yield return new WaitForSeconds(premoveSeconds);
        // Place tile
        int tileToPlace = GetTileIndexFromCoordinates(frogMove.Item1, frogMove.Item2);
        PlacePiece(tileToPlace, _frogPiece);
        yield return new WaitForSeconds(1);
        SwitchTurns();
        yield return null;
    }

    // Frog chooses where to play, and selects dialogue accordingly
    private (int,int) DetermineFrogMove()
    {
        // First: win if possible
        List<(int, int)> winningSpaces = FindWinningSpaces(_lastFrogMove, _frogPiece);
        if(winningSpaces.Count > 0)
        {
            // Frog will win! Throw out some dialogue!
            Debug.Log("Found a winning space!");
            return (winningSpaces[0]);
        }
        // Second: look for lethals that must be blocked
        List<(int,int)> dangerousSpaces = FindWinningSpaces(_lastPlayerMove, PlayerPiece);
        if(dangerousSpaces.Count > 0)
        {
            if(dangerousSpaces.Count > 1)
            {
                // The player has trapped the frog with multiple victory conditions
                // TODO: frog whines and stalls for time
            }
            Debug.Log("Block dangerous space!");
            // Block the first lethal on the list
            return (dangerousSpaces[0]);
        }

        // If no lethals are found: choosing a random empty tile is fine for now
        for(int i = 0; i <= 200; i++)
        {
            int tileIndex = (int)Random.Range(0, 9);
            (int, int) tileCoords = GetCoordinatesFromTileIndex(tileIndex);
            if(Board[tileCoords.Item1, tileCoords.Item2] == Pieces.None)
            {
                return tileCoords;
            }
        }
        // In case we somehow didn't randomly hit an empty tile, just iterate through play in the first empty one
        for(int x = 0; x <= 2; x++)
        {
            for(int y = 0; y <= 2; y++)
            {
                if(Board[x, y] == Pieces.None)
                {
                    return (x, y);
                }
            }
        }
        // We should never get to this point, but 
        Debug.Log("Found no eligible move for Froggerina!");
        return (3, 3);
    }

    // Returns all empty spaces that are dangerous to the frog
    private List<(int,int)> FindWinningSpaces((int, int) latestPieceCoordinates, Pieces pieceToCheck)
    {
        List<(int, int)> winningSpaces = new List<(int, int)>(); // Contains all tiles in which someone can win
        List<(int,int)> tilesInLine = new List<(int, int)>(); // Contains the line of 3 tiles that we are checking for winning spaces at any given time
        
        // Check row
        for (int x = 0; x <= 2; x++)
        {
            tilesInLine.Add((x, latestPieceCoordinates.Item2));
        }
        if(IsLineDangerous(tilesInLine, pieceToCheck))
        {
            // Find empty space and add to dangerous tiles
            winningSpaces.Add(FindEmptySpace(tilesInLine));
        }
        tilesInLine.Clear();

        // Check column
        for (int y = 0; y <= 2; y++)
        {
            tilesInLine.Add((latestPieceCoordinates.Item1, y));

        }
        if (IsLineDangerous(tilesInLine, pieceToCheck))
        {
            // Find empty space and add to dangerous tiles
            winningSpaces.Add(FindEmptySpace(tilesInLine));
        }
        tilesInLine.Clear();

        // Check diagonals, if applicable
        int tileIndex = GetTileIndexFromCoordinates(latestPieceCoordinates.Item1, latestPieceCoordinates.Item2);
        if (tileIndex % 2 == 0)
        {
            if (latestPieceCoordinates.Item1 + latestPieceCoordinates.Item2 == 2)
            {
                // Check spaces 2, 4, and 6
                for (int x = 0; x <= 2; x++)
                {
                    for (int y = 0; y <= 2; y++)
                    {
                        if (x + y == 2)
                        {
                            tilesInLine.Add((x, y));
                        }
                    }
                }
                if (IsLineDangerous(tilesInLine, pieceToCheck))
                {
                    // Find empty space and add to dangerous tiles
                    winningSpaces.Add(FindEmptySpace(tilesInLine));
                }
                tilesInLine.Clear();
            }
            if (tileIndex % 4 == 0)
            {
                // Check spaces 0, 4, and 8
                for (int x = 0; x <= 2; x++)
                {
                    for (int y = 0; y <= 2; y++)
                    {
                        if (GetTileIndexFromCoordinates(x, y) % 4 == 0)
                        { 
                            tilesInLine.Add((x, y));
                        }
                    }
                }
                if (IsLineDangerous(tilesInLine, pieceToCheck))
                {
                    // Find empty space and add to dangerous tiles
                    winningSpaces.Add(FindEmptySpace(tilesInLine));
                }
                tilesInLine.Clear();
            }
        }

        return winningSpaces;
    }

    // Checks a specific line of tiles for danger
    private bool IsLineDangerous(List<(int,int)> tilesInLine, Pieces pieceToCheck)
    {
        // It's dangerous if we find exactly one empty tile (calling this function, we already know that all pieces are of the same type or empty. Code architecture is bad but whatever)
        bool foundEmptyTile = false;

        foreach((int,int) tile in tilesInLine)
        {
            if((foundEmptyTile && Board[tile.Item1, tile.Item2] == Pieces.None) || (Board[tile.Item1, tile.Item2] != pieceToCheck && Board[tile.Item1, tile.Item2] != Pieces.None))
            {
                // Two empty tiles, or an opposite piece. No win here
                return false;
            }
            else if(Board[tile.Item1, tile.Item2] == Pieces.None)
            {
                foundEmptyTile = true;
            }
            
        }
        if (foundEmptyTile)
        {
            return true;
        }
        else
            return false;
    }

    // Returns the first empty space found in a line
    private (int,int) FindEmptySpace(List<(int, int)> tilesInLine)
    {
        foreach ((int, int) tile in tilesInLine)
        {
            if (Board[tile.Item1, tile.Item2] == Pieces.None)
            {
                return tile;
            }
        }
        // If none are found, it returns a value outside the grid (this should never happen, so we can use this to debug)
        Debug.Log("Called FindEmptySpace, and found no empty spaces!");
        return (3,3);
    }

    // Victory
    private void GameEnd()
    {
        StopAllCoroutines();
        if(_victory)
        {
            if(IsPlayerTurn)
            {
                // Player won
                Debug.Log("Player won! Starting new game.");
            }
            else
            {
                // Froggerina won
                Debug.Log("Froggerina won! Starting new game.");
            }
        }
        else if (_boardIsFull)
        {
            // Draw
            Debug.Log("It's a draw! Starting new game.");
        }
        else
        {
            Debug.Log("ERROR: game ended with no winner and no draw.");
        }

        StartNewGame();
    }

    // Starts a new game
    private void StartNewGame()
    {
        // Wipe board
        for(int x = 0; x <= 2; x++)
        {
            for (int y = 0; y <= 2; y++)
            {
                Board[x, y] = Pieces.None;
            }
        }
        // Destroy all pieces
        foreach(GameObject piece in _piecesList)
        {
            Destroy(piece);
        }
        _piecesList.Clear();
        // TODO: make "Play again?" option show up
        IsPlayerTurn = true;
        PlayerPiece = Pieces.X;
        _frogPiece = Pieces.O;
    }

    // Next two functions: convert between the board's 2D coordinates and the board's tile objects (which are stored in a 1D array).
    private (int, int) GetCoordinatesFromTileIndex(int tileIndex)
    {
        int xPos = tileIndex % 3;
        int yPos = tileIndex / 3;
        return (xPos, yPos);
    }

    private int GetTileIndexFromCoordinates(int xPos, int yPos)
    {
        return yPos * 3 + xPos;
    }
}
