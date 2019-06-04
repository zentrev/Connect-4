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
        if (!CheckCollum(collum)) throw new System.Exception("Collum already full");

        for(int i = 0; i < Height; i++)
        {
            if(board[i,collum] != ePlaceHolder.NONE)
            {
                board[i - 1, collum] = (m_doritosTurn) ? ePlaceHolder.DORITO : ePlaceHolder.ILUMINATI;
                if (CheckForWin(i,collum))
                {
                    //won
                }
                m_doritosTurn = !m_doritosTurn;
                return;
            }
        }
    }

    public bool CheckCollum(int collum)
    {
        if (board[Height, collum] != ePlaceHolder.NONE) return true;
        return false;
    }

    bool CheckForWin(int row, int collum)
    {
        ePlaceHolder player = (m_doritosTurn) ? ePlaceHolder.DORITO : ePlaceHolder.ILUMINATI;
        int c = ((collum - 3) > 0) ? (collum - 3) : 0;

        for (int f = 0; f < 2; f++)
        {
            int n = (((row - 3) > 0) ? (row - 3) : 0);
            int l = (((row + 3) < Height) ? (row + 3) : (Height - 1));
            int s = 1;
            if (f == 1) {
                n = (((row + 3) < Height) ? (row + 3) : Height);
                l = (((row - 3) < 0) ? (row - 3) : 0);
                s = -1;
            }
            for (int r = n; r != l; r += s)
            {
                int inARowCount = 0;
                if (board[r, c] == player)
                {
                    inARowCount++;
                    if (inARowCount >= WinLength)
                    {
                        return true;
                    }
                }
                else
                {
                    inARowCount = 0;
                }

                if (f == 0) {
                    c++;
                }
                else
                {
                    c--;
                }
            }
        }

        return false;
    }
}
