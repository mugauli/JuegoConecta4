using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Descripción breve de RevisionGanador
/// </summary>
public class RevisionGanador
{
    public RevisionGanador()
    {

    }

    public bool revisarGanadorMatriz(List<Columna> matriz, int columna, int casilla, byte turno)
    {
        var rVertical = revisarVertical(matriz, columna, casilla, turno);

        if (rVertical) return rVertical;

        var rHorizontal = revisarHorizontal(matriz, columna, casilla, turno);

        if (rHorizontal) return rHorizontal;

        var rRevisarDiagonalSupDerInfIzq = RevisarDiagonalSupDerInfIzq(matriz, columna, casilla, turno);

        if (rRevisarDiagonalSupDerInfIzq) return rRevisarDiagonalSupDerInfIzq;

        var rRevisarDiagonalSupIzqInfDer = RevisarDiagonalSupIzqInfDer(matriz, columna, casilla, turno);

        if (rRevisarDiagonalSupIzqInfDer) return rRevisarDiagonalSupIzqInfDer;

        return false;
    }

    #region revison Diagonal
    private bool RevisarDiagonalSupDerInfIzq(List<Columna> matriz, int columna, int casilla, byte turno)
    {
        bool continuidad = (columna != 7 && casilla != 6), continuidad2 = (columna != 0 && casilla != 0);

        int continuidad_i = 1, continuidad_i2 = 1;

        while (continuidad)
        {
            if (columna + continuidad_i != 7 && casilla + continuidad_i != 6)
                if (revisarCasilla(matriz, columna + continuidad_i, casilla + continuidad_i, turno) && continuidad)
                {
                    continuidad_i++;
                }
                else
                {
                    continuidad = false;
                    break;
                }
            else
                break;
        }
        while (continuidad2)
        {
            if (columna - continuidad_i2 != 0 && casilla - continuidad_i2 != 0)
                if (revisarCasilla(matriz, columna - continuidad_i2, casilla-continuidad_i2, turno) && continuidad2)
                {
                    continuidad_i++;
                    continuidad_i2++;
                }
                else
                {
                    continuidad2 = false;
                    break;
                }
            else
                break;
        }

        return continuidad_i > 3;
    }
    private bool RevisarDiagonalSupIzqInfDer(List<Columna> matriz, int columna, int casilla, byte turno)
    {
        bool continuidad = (columna != 0 && casilla != 6), continuidad2 = (columna != 7 && casilla != 0);

        int continuidad_i = 1, continuidad_i2 = 1;

        while (continuidad)
        {
            if (columna - continuidad_i != 0 && casilla + continuidad_i != 6)
                if (revisarCasilla(matriz, columna - continuidad_i, casilla + continuidad_i, turno) && continuidad)
                {
                    continuidad_i++;
                }
                else
                {
                    continuidad = false;
                    break;
                }
            else
                break;
        }
        while (continuidad2)
        {
            if (columna + continuidad_i != 0 && casilla - continuidad_i2 != 0)
                if (revisarCasilla(matriz, columna + continuidad_i2, casilla - continuidad_i2, turno) && continuidad2)
                {
                    continuidad_i++;
                    continuidad_i2++;
                }
                else
                {
                    continuidad2 = false;
                    break;
                }
            else
                break;
        }

        return continuidad_i > 3;
    }
    #endregion

    #region revisaVertical
    private bool revisarVertical(List<Columna> matriz, int columna, int casilla, byte turno)
    {
        var getColumn = matriz.Where(x => x.columnNumber == columna).First();

        var cont = getColumn.casillas.Where(x => x.valor == turno).Count();

        if (cont > 1 && casilla > 3)
        {

            bool continuidad = true;
            int continuidad_i = 1;

            for (int j = 1; j < 4; j++)
            {

                if (revisarCasilla(matriz, columna, casilla - continuidad_i, turno) && continuidad)
                {
                    continuidad_i++;
                }
                else
                {

                    continuidad = false;
                    break;
                }

            }

            return continuidad_i > 3;


        }
        else
            return false;


    }

    #endregion

    #region revision Horizontal
    private bool revisarHorizontal(List<Columna> matriz, int columna, int casilla, byte turno)
    {

        bool continuidad = true, continuidad2 = true;
        int continuidad_i = 1, continuidad_i2 = 1;

        while (continuidad)
        {
            if (columna + continuidad_i < 8)
                if (revisarCasilla(matriz, columna + continuidad_i, casilla, turno) && continuidad)
                {
                    continuidad_i++;
                }
                else
                {
                    continuidad = false;
                    break;
                }
            else
                break;
        }
        while (continuidad2)
        {
            if (columna - continuidad_i2 > 0)
                if (revisarCasilla(matriz, columna - continuidad_i2, casilla, turno) && continuidad2)
                {
                    continuidad_i++;
                    continuidad_i2++;
                }
                else
                {
                    continuidad2 = false;
                    break;
                }
            else
                break;
        }


        return continuidad_i > 3;

    }
    #endregion

    #region Shared

    private bool revisarCasilla(List<Columna> matriz, int columna, int casilla, byte turno)
    {
        var objColumna = matriz.Where(x => x.columnNumber == columna).First();

        var dato = objColumna.casillas.Where(x => x.casillaNumber == casilla).First();

        return dato.valor == turno;

    }
    #endregion

}