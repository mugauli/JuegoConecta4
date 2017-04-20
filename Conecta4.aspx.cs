using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Conecta4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    [System.Web.Services.WebMethod(EnableSession = true)]
    public static string RevisarJugada(string json, int nColumna, int nCasilla,int turno)
    {

        var jsonData = JArray.Parse(json);

        var ColumnaLts = new List<Columna>();

        foreach (var column in jsonData)
        {
            //Obtener numero de columna
            var columna = new Columna();
            columna.columnNumber = (byte)column.SelectToken("columnNumber");

            //Obtener valores de las casillas por columna
            var casillasLts = new List<Casilla>();
            for (int i = 1; i < 7; i++)
            {
                var casilla = new Casilla();
                casilla.casillaNumber = (byte)i;
                casilla.valor = (byte)column.SelectToken("casilla" + i);
                casillasLts.Add(casilla);
            }
            columna.casillas = casillasLts;

            ColumnaLts.Add(columna);
        }

        var _revisionGanador = new RevisionGanador();

        var resultado = _revisionGanador.revisarGanadorMatriz(ColumnaLts,nColumna,nCasilla,(byte)turno);

        //var matriz = (List<Columna>)HttpContext.Current.Session["matriz"];

        //var columnaW = matriz.Where(x => x.columnNumber == columna).First();

        return resultado ? "1": "0";
    }
}