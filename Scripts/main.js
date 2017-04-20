var turno = true;
var terminado = false;

var revisarjugada = function (json,nCol,nCas) {

    //alert(json);
    $.ajax({
        type: "POST",
        url: '/Conecta4.aspx/RevisarJugada',
        data: "{ json: '" + json + "', nColumna : " + nCol + ", nCasilla : " + nCas + ", turno : " + (turno ? 1 : 2) + "}",
        async : false,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {

            if (response.d == "1") {
                alert("Felicidades has ganado.");
                terminado = true;
            }
            else
            {
                $(".btnCNTurno").removeClass("casillaAmarilla");
                $(".btnCNTurno").removeClass("casillaRoja");
                $(".btnCNTurno").addClass(colorTurno(!turno));
            }
           
            // alert("succ");
            console.log(response);
        },
        failure: function (response) {
            alert(response.d);
        }, error: function (err) {
            alert("Error" + JSON.stringify(err, null, 2));
        }

    });

}

var casillasOcupadas = function (columna) {

    var actives = columna.find(".actived");
    // alert(actives.length);
    return actives.length;

}

var colorTurno = function (T) {

    if (T)
        return "casillaAmarilla";
    else
        return "casillaRoja";

}

var numCol = function (columna) {

    if (columna.hasClass("col1"))
        return 1;
    else if (columna.hasClass("col2"))
        return 2;
    else if (columna.hasClass("col3"))
        return 3;
    else if (columna.hasClass("col4"))
        return 4;
    else if (columna.hasClass("col5"))
        return 5;
    else if (columna.hasClass("col6"))
        return 6;
    else if (columna.hasClass("col7"))
        return 7;


}

var numCasilla = function (casilla) {


    if (casilla.hasClass("btnConecta6")) return 6;
    else if (casilla.hasClass("btnConecta5")) return 5;
    else if (casilla.hasClass("btnConecta4")) return 4;
    else if (casilla.hasClass("btnConecta3")) return 3;
    else if (casilla.hasClass("btnConecta2")) return 2;
    else if (casilla.hasClass("btnConecta1")) return 1;

}

var casillaActiva = function (casilla) {

    if (casilla.hasClass("casillaAmarilla"))
        return 1;
    else if (casilla.hasClass("casillaRoja"))
        return 2;
    else 
        return 0;
}

var agregarFicha = function (columna) {
    var casillasOcup = casillasOcupadas(columna);

    if (casillasOcup >= 6) {
        alert("Columna llena");

    }
    else {
        columna.find(".btnConecta" + (casillasOcup + 1)).addClass("actived").addClass(colorTurno(turno));
        revisarjugada(createJSON(), numCol(columna), (casillasOcup + 1));

        turno = !turno;
    }


}

function createJSON() {
    jsonObj = [];
 

    $(".colCN").each(function () {
        json2 = [];
        item = {}
        item["columnNumber"] = numCol($(this));

        var casillas = $(this).find(".btnCN");

        casillas.each(function () {
            item["casilla" + numCasilla($(this))] = casillaActiva($(this));
        });
        jsonObj.push(item);

    });
    
    console.log(jsonObj);
    return JSON.stringify(jsonObj);
}

$(document).ready(function () {

    $(".colCN").click(function () {
        if (!terminado)
            agregarFicha($(this));
        else
            alert("El juego ha terminado.");

    });
    $(".btnCNTurno").addClass(colorTurno(turno));
});