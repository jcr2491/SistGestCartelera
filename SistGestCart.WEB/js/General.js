function ContadorTexto(txt, maxlength){
    if(txt.value.length > maxlength){
    txt.value = txt.value.substring(0, maxlength);
    }
}

function Len(str)
{  return String(str).length;  }

function Mid(str, start, len)
{
    if (start < 0 || len < 0) return "";

    var iEnd, iLen = String(str).length;
    if (start + len > iLen)
    iEnd = iLen;
    else
    iEnd = start + len;

    return String(str).substring(start,iEnd);
}       
 
function InStr(strSearch, charSearchFor)
{
    for (i=0; i < Len(strSearch); i++)
    {
    if (charSearchFor == Mid(strSearch, i, 1))
    {
    return i;
    }
    }
    return -1;
}

function Left(str, n)
{
    if (n <= 0)     
    return "";
    else if (n > String(str).length)   
    return str;
    else 
    return String(str).substring(0,n);
}

function Right(str, n)
{
    if (n <= 0)
    return "";
    else if (n > String(str).length)
    return str; 
    else { 
    var iLen = String(str).length;
    return String(str).substring(iLen, iLen - n);
    }
}        

function SoloLetras(obj){
    k = event.keyCode;
    if (k>=48 && k<=57){
    event.keyCode=0 
    }
}  

function SoloNumeros(obj){
    k = event.keyCode;
    if (k>=48 && k<=57){
    }else{
    event.keyCode=0 
    }
}

function SoloEnteros() {
    k = event.keyCode;
    if (k >= 48 && k <= 57) { }
    else { event.keyCode = 0; }
}

function SoloFecha(obj){
    //valido para formato dd/mm/yyyy   
    f = obj.value;
    switch(f.length){
    case 2: case 5:  
    f=f+"/"; 
    obj.value=f;
    break;
    }
}

function SoloPlaca(){
    //alert('')
    k = event.keyCode;
    if(k>=48 && k<=57){}
    else if(k>=65 && k<=90){}
    else if(k>=97 && k<=122){}
    else if(k>=97 && k<=122){}
    else if(k==35){}
    else if(k==45){}
    else{ event.keyCode=0; }	
}      
 
function showDown(evt) {

    //NUEVO PARA FACILITAR LA BUSquEDA
    //if (event.keyCode == 13 && (event.srcElement.name  == 'ctl00$cphMain$txtMarcaBusqueda' || event.srcElement.name  == 'ctl00$cphMain$txtModeloBusqueda' ))
    //{
    //return false;
    //}
    //

    evt = (evt)? evt : ((event)? event : null);
    if (evt){
    if (event.keyCode == 13 && (event.srcElement.type!= "select-one" && event.srcElement.type!= "text" && event.srcElement.type!= "textarea" && event.srcElement.type!= "submit" && event.srcElement.type!= "image" )) {
    cancelKey(evt);
    }else if (event.keyCode == 8 && (event.srcElement.type!= "text" && event.srcElement.type!= "textarea" && event.srcElement.type!= "password")) {
    cancelKey(evt);
    }else if (event.keyCode == 116) {
    cancelKey(evt);
    }else if (event.keyCode == 122) {
    cancelKey(evt);
    }else if (event.ctrlKey && (event.keyCode == 78 || event.keyCode == 82)) {
    cancelKey(evt);
    }else if (event.altKey && event.keyCode==37 ){
    return false;
    }
    }
}

function cancelKey(evt) {
    if (evt.preventDefault) {
    evt.preventDefault();
    return false;
    }else{
    evt.keyCode = 0;
    evt.returnValue = false;
    }
}

function trim(cadena)
{
    for(i=0; i<cadena.length; )
    {
    if(cadena.charAt(i)==" ")
    cadena=cadena.substring(i+1, cadena.length);
    else
    break;
    }

    for(i=cadena.length-1; i>=0; i=cadena.length-1)
    {
    if(cadena.charAt(i)==" ")
    cadena=cadena.substring(0,i);
    else
    break;
    }

    return cadena;
}

function SoloTelefono(obj){
    valor =obj.value;
    k = event.keyCode;
    if(k>=48 && k<=57){}
    else{ 
    switch(k){
    case 32:
    case 45:
    case 35:
    case 42:
    return;
    }
    event.keyCode=0; 
    }
}

function SoloEnteros(){
    k = event.keyCode;
    if(k>=48 && k<=57){}
    else{ event.keyCode=0; }
}

function LimpiarControlBusqueda(ctrl){
    var ctrlDocAseg =$get(ctrl); 
    ctrlDocAseg.value=''; 
    ctrlDocAseg.focus();
}
function SoloNombres(Contador, obj, len){
    k = event.keyCode;

    switch(k){
    case 32:
    case 241: //ñ
    case 209: //Ñ
    break;
    default:
    if (k>=65 && k<=90){   }
    else if(k>=97 && k<=122){   } //a-z
    else if(k>=192 && k<=197){   } //A
    else if(k>=200 && k<=207){   } //E  I
    else if(k>=210 && k<=214){   } //O
    else if(k>=217 && k<=220){   } //U

    else if(k>=224 && k<=229){   } // a
    else if(k>=232 && k<=246){   } // e i ñ o
    else if(k>=250 && k<=252){   } // u
    else
    {
    event.keyCode=0;
    return false;
    } 
    }
    if(Contador==1){
    ContadorTexto(obj,len)
    }
    return true;
}

function SoloDireccion(Contador, obj, len){
    k = event.keyCode;

    switch(k){
    case 32:    
    case 45://  -
    case 35://  #    
    case 46://  .
    break;
    default:
    if (k>=65 && k<=90){   } //A-Z
    else if(k>=48 && k<=57){   }  //0-9
    else if(k>=97 && k<=122){   }  //a-z
    else if(k>=192 && k<=197){   } //A
    else if(k>=200 && k<=207){   } //E  I
    else if(k>=210 && k<=214){   } //O
    else if(k>=217 && k<=220){   } //U

    else if(k>=224 && k<=229){   } // a
    else if(k>=232 && k<=246){   } // e i ñ o
    else if(k>=250 && k<=252){   } // u
    else
    {
    event.keyCode=0;
    return false;
    } 
    }
    if(Contador==1){
    ContadorTexto(obj,len)
    }
    return true;
}

function SoloDecimales(obj){
    valor = obj.value;
    k = event.keyCode;
    if(k==46){
    if(valor.length==0){
    event.keyCode=0;
    }else{
    if(valor.indexOf(".")!=-1){
    event.keyCode = 0;
    }
    }
    }else{
    if(k>=48 && k<=57){}
    else{ event.keyCode=0; }	
    }
}   

function ComparaFechas(dtf1, dtf2){
    //la fecha 2 debe ser necesariamente mayor a fecha1
    fi = dtf1.split("/");
    ff = dtf2.split("/");
    _fechai = fi[0]*10000 + fi[1]*100 + fi[2];
    _fechaf = ff[0]*10000 + ff[1]*100 + ff[2];
    n = _fechaf - _fechai;
    if(n>0) return true;
    else return false;
}

function SoloFecha(obj){
    //valido para formato dd/mm/yyyy
    f = obj.value;
    switch(f.length){
    case 2: case 5:  
    f=f+"/"; 
    obj.value=f;
    break;
    }
} 
       
function SoloHora(obj){
    k = event.keyCode;
    if(k>=48 && k<=57){}
    else{ 
    switch(k){
    case 58:
    return;
    }
    event.keyCode=0; 
    }  
}

function SoloURL(){
    k = event.keyCode;
    switch(k){
    case 32: event.keyCode=0;
    }
}

//compara las longitudes
function longitudMinima(valor,lmin){
    return (valor.length>=lmin)
}

function SoloDecimales(obj){
    valor = obj.value;
    k = event.keyCode;
    if(k==46){
    if(valor.length==0){
    event.keyCode=0;
    }else{
    if(valor.indexOf(".")!=-1){
    event.keyCode = 0;
    }
    }
    }else{
    if(k>=48 && k<=57){}
    else{ event.keyCode=0; }	
    }
}

function Escribe_Fechas(elEvento,ctrol)
{

    var evento = elEvento || window.event;    
    
    if(evento.keyCode == 8)
    {
    
    } 
    else 
    {
        //var fecha = document.getElementById(ctrol);
        
        var fecha = ctrol.value;
        
        if(fecha.value.length == 2 || fecha.value.length == 5)
        {
            fecha.value += "/";
        }
    }
}

//

function validarFechas(inicio, fin){
    try{     
        inicio=new Date(inicio);
        fin=new Date(fin);    
        var msj;
        if(inicio>fin){                
            window.location='../ErrorPage/Error.aspx?Mensaje=' + 'La fecha de inicio debe ser menor a la fecha final';
        }else{
            document.getElementById('button').click();
            return;
        }  
    }catch(ex){
        //alert(ex.description);
    }
}

//TxtFechaIni.Attributes.Add("onblur", "esFechaValida(this)")
//TxtFechaFin.Attributes.Add("onblur", "esFechaValida(this)")
function esFechaValida(fecha){
    if (fecha != undefined && fecha.value != "" ){
       if (!/^\d{2}\/\d{2}\/\d{4}$/.test(fecha.value)){
           alert("formato de fecha no valido (dd/mm/aaaa)");
           return false;
       }
       var dia  =  parseInt(fecha.value.substring(0,2),10);
       var mes  =  parseInt(fecha.value.substring(3,5),10);
       var anio =  parseInt(fecha.value.substring(6),10);

    switch(mes){
        case 1:
        case 3:
        case 5:
        case 7:
        case 8: 
        case 10:
        case 12:
            numDias=31;
            break;
        case 4: case 6: case 9: case 11:
            numDias=30;
            break;
        case 2:
            if (comprobarSiBisisesto(anio)){ numDias=29 }else{ numDias=28};
            break;
        default:
            alert("Fecha introducida errÃ³nea");
            return false;
    }

        if (dia>numDias || dia==0){
            alert("Fecha introducida errÃ³nea");
            return false;
        }
        return true;
    }
}

//Escribe Fecha en formato dd/mm/yyyy
function IsNumeric(valor) 
{ 
    var log=valor.length; var sw="S"; 
    for (x=0; x<log; x++) 
    { v1=valor.substr(x,1); 
    v2 = parseInt(v1); 
    //Compruebo si es un valor numérico 
    if (isNaN(v2)) { sw= "N";} 
    } 
    if (sw=="S") {return true;} else {return false; } 
} 

var primerslap=false; 
var segundoslap=false; 
function formateafecha(fecha) 
{ 
    var long = fecha.length; 
    var dia; 
    var mes; 
    var ano; 

    if ((long>=2) && (primerslap==false)) { dia=fecha.substr(0,2); 
    if ((IsNumeric(dia)==true) && (dia<=31) && (dia!="00")) { fecha=fecha.substr(0,2)+"/"+fecha.substr(3,7); primerslap=true; } 
    else { fecha=""; primerslap=false;} 
    } 
    else 
    { dia=fecha.substr(0,1); 
    if (IsNumeric(dia)==false) 
    {fecha="";} 
    if ((long<=2) && (primerslap=true)) {fecha=fecha.substr(0,1); primerslap=false; } 
    } 
    if ((long>=5) && (segundoslap==false)) 
    { mes=fecha.substr(3,2); 
    if ((IsNumeric(mes)==true) &&(mes<=12) && (mes!="00")) { fecha=fecha.substr(0,5)+"/"+fecha.substr(6,4); segundoslap=true; } 
    else { fecha=fecha.substr(0,3);; segundoslap=false;} 
    } 
    else { if ((long<=5) && (segundoslap=true)) { fecha=fecha.substr(0,4); segundoslap=false; } } 
    if (long>=7) 
    { ano=fecha.substr(6,4); 
    if (IsNumeric(ano)==false) { fecha=fecha.substr(0,6); } 
    else { if (long==10){ if ((ano==0) || (ano<1900) || (ano>2100)) { fecha=fecha.substr(0,6); } } } 
    } 

    if (long>=10) 
    { 
    fecha=fecha.substr(0,10); 
    dia=fecha.substr(0,2); 
    mes=fecha.substr(3,2); 
    ano=fecha.substr(6,4); 
    // Año no viciesto y es febrero y el dia es mayor a 28 
    if ( (ano%4 != 0) && (mes ==02) && (dia > 28) ) { fecha=fecha.substr(0,2)+"/"; } 
    } 
    return (fecha); 
} 
//********************************************************************************************************************************

/* xPOPUPS*/

function myPopUp(URL) {
    window.open(URL, "Informacion", "width=600,height=400,top=200,left=250,scrollbars=yes,resizable=yes,directories=no,location=no,menubar=no,status=no,titlebar=no,toolbar=no")
}                
     
     
/*Funciones para convertir a mayusculas y minusculas */
function M(txt){
    txt.value=txt.value.toUpperCase();
}
function min(txt){
    txt.value=txt.value.toLowerCase();
}
/*---------------------------------------------------*/

function seleccionarCombo(combo, find) {
   var cantidad = combo.length;
   for (i = 0; i < cantidad; i++) {
      if (combo[i].value == find) {
         combo[i].selected = true;
      }   
   }
}

function ContadorTexto(txt, maxlength){
    if(txt.value.length > maxlength){
        txt.value = txt.value.substring(0, maxlength);
    }
}

function SoloUsuario(){
   k = event.keyCode;
    if (k>=65 && k<=90){   } //A-Z
    else if(k>=97 && k<=122){   } //a-z
    else
    {
        event.keyCode=0;
    } 
}

function SoloLetras(Contador, obj, len){
   k = event.keyCode;
    if (k>=65 && k<=90){   } //A-Z
    else if(k>=97 && k<=122){   } //a-z
    else
    {
        event.keyCode=0;
    } 
}

function SoloNombres(Contador, obj, len){
   k = event.keyCode;
   
   switch(k){
    case 32:
    case 39:
    case 241: //ñ
    case 209: //Ñ
        break;
    default:
        if (k>=65 && k<=90){   }
        else if(k>=97 && k<=122){   } //a-z
        else if(k>=192 && k<=197){   } //A
        else if(k>=200 && k<=207){   } //E  I
        else if(k>=210 && k<=214){   } //O
        else if(k>=217 && k<=220){   } //U
        
        else if(k>=224 && k<=229){   } // a
        else if(k>=232 && k<=246){   } // e i ñ o
        else if(k>=250 && k<=252){   } // u
        else
        {
            event.keyCode=0;
            return false;
        } 
   }
   if(Contador==1){
    ContadorTexto(obj,len)
   }
   return true;
}

function SoloDireccion(Contador, obj, len){
   k = event.keyCode;
   
   switch(k){
    case 32:    
    case 45://  -
    case 35://  #    
    case 46://  .
    case 209: //Ñ
        break;
    default:
        if (k>=65 && k<=90){   } //A-Z
        else if(k>=48 && k<=57){   }  //0-9
        else if(k>=97 && k<=122){   }  //a-z
        else if(k>=192 && k<=197){   } //A
        else if(k>=200 && k<=207){   } //E  I
        else if(k>=210 && k<=214){   } //O
        else if(k>=217 && k<=220){   } //U
        
        else if(k>=224 && k<=229){   } // a
        else if(k>=232 && k<=246){   } // e i ñ o
        else if(k>=250 && k<=252){   } // u
        else
        {
            event.keyCode=0;
            return false;
        } 
   }
   if(Contador==1){
    ContadorTexto(obj,len)
   }
   return true;
}
    
function SoloDecimales(obj){
	valor = obj.value;
	k = event.keyCode;
	if(k==46){
		if(valor.length==0){
			event.keyCode=0;
		}else{
			if(valor.indexOf(".")!=-1){
				event.keyCode = 0;
			}
		}
	}else{
		if(k>=48 && k<=57){ }
		else{ event.keyCode=0; }	
	}
}

function SoloTelefono(obj){
    //valor =obj.value;
	k = event.keyCode;
	if(k>=48 && k<=57){}
	else{ 
	    switch(k){
	        case 32://espacio
	        //case 40://  (
	        //case 41://  )
	        //case 45://  -
            //case 35://  #
            case 42://  *
	        return;
	    }
	    event.keyCode=0; 
	}
}
   
function ComparaFechas(dtf1, dtf2){
    //la fecha 2 debe ser necesariamente mayor a fecha1
	fi = dtf1.split("/");
	ff = dtf2.split("/");
	_fechai = fi[0]*10000 + fi[1]*100 + fi[2];
	_fechaf = ff[0]*10000 + ff[1]*100 + ff[2];
	n = _fechaf - _fechai;
	if(n>0) return true;
	else return false;
}
function SoloFecha(obj){
   //valido para formato dd/mm/yyyy
   f = obj.value;
   switch(f.length){
    case 2: case 5:  
        f=f+"/"; 
        obj.value=f;
        break;
   }
}        
function SoloHora(obj){
	k = event.keyCode;
	if(k>=48 && k<=57){}
	else{ 
	    switch(k){
	        case 58:
	        return;
	    }
	    event.keyCode=0; 
	}  
}
function SoloURL(){
    k = event.keyCode;
	switch(k){
	    case 32: event.keyCode=0;
	}
}

//--------------------------------------------------------------------
var ventanapdf;
function Abrir(ruta) {
    ventanapdf = window.open(ruta, '', 'width=1100px, height=650px,top=' + ((screen.height - 650) / 2) + ',left=' + ((screen.width - 1200) / 2) + ',status=no, directories=no, toolbar=no');
}

var contrem = 0;
var ventanarem;
openpolizasremotas = function(ruta) {
    if (contrem <= 0) {
        contrem++;
        ventanarem = window.open(ruta, '_self', '');
    } else {
        contrem = 0;
        ventanarem.close();
        openpolizasremotas(ruta);
    }
}

function ValidNum(e) {
    var tecla = document.all ? tecla = e.keyCode : tecla = e.which;
    return ((tecla > 47 && tecla < 58) || tecla == 46);   
}

function ValidLetras(e) {
    tecla = (document.all) ? e.keyCode : e.which; // 2
    if (tecla == 8) return true; // 3
    patron = /^[a-z áéíóúñüàè]+$/i; // 4
    te = String.fromCharCode(tecla); // 5
    return patron.test(te); // 6
}

function maximizeWindow() {
    if (window.screen) {
        this.moveTo(0, 0);
        this.resizeTo(screen.availWidth, screen.availHeight)
    }
}

//-------------------------------------------------------------
function doClick(buttonName, e) {
    //the purpose of this function is to allow the enter key to 
    //point to the correct button to click.
    var key;

    if (window.event)
        key = window.event.keyCode;     //IE
    else
        key = e.which;     //firefox

    if (key == 13) {
        //Get the button the user wants to have clicked
        var btn = document.getElementById(buttonName);
        if (btn != null) { //If we find the button click it
            btn.click();
            event.keyCode = 0
        }
    }
}

function SoloEnterosLetrasYEspacios(obj) {
    k = event.keyCode;

    if (k == 8 || k == 32) {
    }
    else if (k >= 48 && k <= 57) {
    }
    else if (k >= 65 && k <= 90) {
    }
    else if (k >= 97 && k <= 122) {
    }
    else {
        event.keyCode = 0;
    }
}