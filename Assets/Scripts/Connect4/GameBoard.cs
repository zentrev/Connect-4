using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoard : MonoBehaviour
{

    public static int Width = 7;
    public static int Height = 6;

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
        if (!CheckCollum(collum)) return;

        for(int i = 0; i < Height; i++)
        {
            if(board[i,collum] != ePlaceHolder.NONE)
            {
                board[i - 1, collum] = (m_doritosTurn) ? ePlaceHolder.DORITO : ePlaceHolder.ILUMINATI;
                CheckForWin(i,collum);
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
        return false;
    }
}
