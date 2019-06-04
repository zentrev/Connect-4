using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{

    public static int Width = 7;
    public static int Height = 6;
    public static int WinLength = 4;

    public enum ePlaceHolder
    {
        NONE,
        DORITO,
        ILUMINATI,
    }

    public ePlaceHolder[,] board = new ePlaceHolder[Height,Width];

    bool m_doritosTurn = true;

    public void ClearBoard()
    {
        for(int i = 0; i < Height; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                board[i,j] = ePlaceHolder.NONE;
            }
        }
    }

    public void Start()
    {
        ClearBoard();
    }

    public void PlaceChip(int collum)
    {
        if (CheckCollum(collum)) throw new System.Exception("Collum already full");

        for(int i = 0; i < Height; i++)
        {
            if(board[i,collum] == ePlaceHolder.NONE)
            {
                board[i, collum] = (m_doritosTurn) ? ePlaceHolder.DORITO : ePlaceHolder.ILUMINATI;
                Debug.Log("chip placed at " + (i - 1) + " : " + collum);
                if (CheckForWin(i,collum))
                {
                    //won
                }
                m_doritosTurn = !m_doritosTurn;
                return;
            }
        }
    }

    public void DebugBoard()
    {
        Debug.Log("Debug Board-----------------------------------------");
        for (int i = 0; i < Height; i++)
        {
            string row = "";
            for (int j = 0; j < Width; j++)
            {
                row += board[i, j].ToString() + " : ";
            }
            Debug.Log(row);
        }
    }

    public bool CheckCollum(int collum)
    {
        if (board[Height-1, collum] != ePlaceHolder.NONE) return true;
        return false;
    }

    bool CheckForWin(int row, int collum)
    {
        ePlaceHolder player = (m_doritosTurn) ? ePlaceHolder.DORITO : ePlaceHolder.ILUMINATI;
        int c = ((collum - 3) > 0) ? (collum - 3) : 0;
        for (int f = 0; f < 2; f++)
        {
            int n = (((row - 3) > 0) ? (row - 3) : 0);
            for (int r = n; r < (((row + 3) < Height) ? (row + 3) : (Height - 1)); r++)
            {
                int inARowCount = 0;
                int inACollumCount = 0;
                int inADiagonalCount = 0;
                //check diagonal
                if (board[r, c] == player) inADiagonalCount++;
                else inADiagonalCount = 0;
                //check row
                if (board[row, c] == player) inARowCount++;
                else inARowCount = 0;
                //check colum
                if (board[r, collum] == player) inACollumCount++;
                else inACollumCount = 0;

                if (inARowCount >= WinLength || inACollumCount >= WinLength || inADiagonalCount >= WinLength)
                {
                    return true;
                }

                if (f == 0) {
                    c++;
                }
                else
                {
                    c--;
                }
            }
            c--;
        }

        return false;
    }
}
