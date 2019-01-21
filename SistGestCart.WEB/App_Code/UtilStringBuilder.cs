using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;

/// <summary>
/// Define un StringBuilder que soporta agregar saltos de linea.
/// </summary>
public class UtilStringBuilder
{
    private StringBuilder _stb;
    private int _tabLevel;

    /// <summary>
    /// Inicializa un nuevo objeto UT_StringBuilder.
    /// </summary>
    public UtilStringBuilder()
    {
        this._tabLevel = 0;
        this._stb = new StringBuilder();
    }

    /// <summary>
    /// Agrega el objeto indicado al texto.
    /// </summary>
    /// <param name="obj_pAdd">El objeto a agregar.</param>
    public void Append(object obj_pAdd)
    {
        this.Append(obj_pAdd.ToString());
    }

    /// <summary>
    /// Agrega una cadena al texto.
    /// </summary>
    /// <param name="str_pLine">La cadena a agregar</param>
    public void Append(string str_pLine)
    {
        this._stb.Append(this.DoIndent() + str_pLine);
    }

    /// <summary>
    /// Agrega una cadena aplicándole un formato.
    /// </summary>
    /// <param name="str_pFormat">El formato a aplicar.</param>
    /// <param name="obj_pArgs">Los objetos a agregar.</param>
    public void AppendFormat(string str_pFormat, params object[] obj_pArgs)
    {
        this.Append(string.Format(str_pFormat, obj_pArgs));
    }

    /// <summary>
    /// Agrega una linea vacía al texto.
    /// </summary>
    public void AppendLine()
    {
        this.Append(Environment.NewLine);
    }

    /// <summary>
    /// Agrega una linea al texto.
    /// </summary>
    /// <param name="str_pLine">La linea a agregar.</param>
    public void AppendLine(string str_pLine)
    {
        this.Append(str_pLine + Environment.NewLine);
    }

    /// <summary>
    /// Agrega una linea al texto aplicándole formato.
    /// </summary>
    /// <param name="str_pFormat">El formato a aplicarle.</param>
    /// <param name="obj_pArgs">Los objetos a agregarle.</param>
    public void AppendLineFormat(string str_pFormat, params object[] obj_pArgs)
    {
        this.AppendFormat(str_pFormat + Environment.NewLine, obj_pArgs);
    }

    /// <summary>
    /// Agrega una linea sin aplicarle indentado.
    /// </summary>
    /// <param name="str_pLine">La linea a agregar.</param>
    public void AppendLineNoIndent(string str_pLine)
    {
        this._stb.Append(str_pLine + Environment.NewLine);
    }

    /// <summary>
    /// Agrega una cadena al texto sin incluir el indentado.
    /// </summary>
    /// <param name="str_pLine">La cadena a agregar.</param>
    public void AppendNoIndent(string str_pLine)
    {
        this._stb.Append(str_pLine);
    }

    private string DoIndent()
    {
        StringBuilder stb_Stream = new StringBuilder();
        for (int int_I = 0; int_I <= this._tabLevel - 1; int_I++)
        {
            stb_Stream.Append("\t");
        }
        return stb_Stream.ToString();
    }

    /// <summary>
    /// Quita caracteres de una cadena a partir una posición y un número de caracteres indicados.
    /// </summary>
    /// <param name="int_pStartIndex">La posición inicial a partir de la cual se quitarán caracteres.</param>
    /// <param name="int_pLength">La cantidad de caracteres a quitar.</param>
    public void Remove(int int_pStartIndex, int int_pLength)
    {
        this._stb.Remove(int_pStartIndex, int_pLength);
    }

    /// <summary>
    /// Reemplaza un texto por otro.
    /// </summary>
    /// <param name="str_pOldValue">El texto a reemplazar</param>
    /// <param name="str_pNewValue">El texto que reemplazará al antiguo valor.</param>
    public void Replace(string str_pOldValue, string str_pNewValue)
    {
        this._stb.Replace(str_pOldValue, str_pNewValue);
    }

    /// <summary>
    /// Retorna el valor construido.
    /// </summary>
    /// <returns>Retorna el valor construido.</returns>
    public override string ToString()
    {
        return this._stb.ToString();
    }

    /// <summary>
    /// Obtiene o establece un valor que indica el número de indentaciones por cada linea de texto nueva.
    /// </summary>
    public int IndentLevel
    {
        get
        {
            return this._tabLevel;
        }
        set
        {
            this._tabLevel = value;
        }
    }

    /// <summary>
    /// Obtiene o establece la longitud de la cadena construida.
    /// </summary>
    public int Length
    {
        get
        {
            return this._stb.Length;
        }
        set
        {
            this._stb.Length = value;
        }
    }
}
