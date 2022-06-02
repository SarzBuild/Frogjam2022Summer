using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    public TicTacToe TicTacToeManager;
    private bool _playerHoldingPiece;
    private GameObject _pieceHeld;

    private void Update()
    {
        if(TicTacToeManager.IsPlayerTurn)
        {
            Vector2 mousePosition = GetMousePositionInWorld();

            // Pick up piece from bag
            if (Input.GetMouseButtonDown(0) && !_playerHoldingPiece)
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
                if(hit.collider != null)
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("TicTacToeBag"))
                    {
                        _playerHoldingPiece = true;
                        if (TicTacToeManager.PlayerPiece == TicTacToe.Pieces.X)
                        {
                            _pieceHeld = Instantiate(TicTacToeManager.X, new Vector3(mousePosition.x, mousePosition.y, 0), Quaternion.identity);
                        }
                        else
                        {
                            _pieceHeld = Instantiate(TicTacToeManager.O, new Vector3(mousePosition.x, mousePosition.y, 0), Quaternion.identity);
                        }
                    }
                }
            }

            // Move piece around
            if (Input.GetMouseButton(0) && _playerHoldingPiece)
            {
                _pieceHeld.transform.position = new Vector3(mousePosition.x, mousePosition.y, 0);
            }

            // Drop piece
            if (Input.GetMouseButtonUp(0) && _playerHoldingPiece)
            {
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
                if(hit.collider != null)
                {
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("TicTacToeBoard"))
                    {
                        TicTacToeManager.PlacePiece(System.Array.IndexOf(TicTacToeManager.Tiles, hit.collider.gameObject), TicTacToeManager.PlayerPiece);
                    }
                }
                Destroy(_pieceHeld);
                _playerHoldingPiece = false;
            }
        }
        
    }

    private Vector2 GetMousePositionInWorld()
    {
        // Converts screen position of a click to world position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePosition.x, mousePosition.y);
    }
}
