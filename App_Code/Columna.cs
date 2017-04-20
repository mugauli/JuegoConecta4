using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

/// <summary>
/// Descripción breve de Columna
/// </summary>
public class Columna
{
    public byte columnNumber { get; set; }
    public List<Casilla> casillas { get; set; }

}