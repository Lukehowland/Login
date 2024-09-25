using System.Collections.Generic;
using System.IO;

public class CifradoModel
{
    public string TextoOriginal { get; set; }
    public string TextoCifrado { get; set; }
    public int TamañoClave { get; set; }
    public Dictionary<char, char> TablaCifrado { get; private set; }

    public CifradoModel()
    {
        TablaCifrado = new Dictionary<char, char>();
    }

    public string CifrarTexto()
    {
        char[] arr = TextoOriginal.ToCharArray();
        for (int i = 0; i < arr.Length; i++)
        {
            char cifrado = (char)(arr[i] + TamañoClave);
            TablaCifrado[arr[i]] = cifrado;  // Guardar en la tabla de cifrado
            arr[i] = cifrado;
        }
        return new string(arr);
    }

    public string DescifrarTexto(Dictionary<char, char> tablaCifrado)
    {
        char[] arr = TextoCifrado.ToCharArray();
        for (int i = 0; i < arr.Length; i++)
        {
            char original = tablaCifrado[arr[i]];
            arr[i] = original;
        }
        return new string(arr);
    }

    public void GuardarTablaCifrado(string rutaArchivo)
    {
        using (StreamWriter sw = new StreamWriter(rutaArchivo))
        {
            foreach (var par in TablaCifrado)
            {
                sw.WriteLine($"{par.Key}:{par.Value}");
            }
        }
    }

    public static Dictionary<char, char> CargarTablaCifrado(string rutaArchivo)
    {
        var tablaCifrado = new Dictionary<char, char>();
        foreach (var linea in File.ReadAllLines(rutaArchivo))
        {
            var partes = linea.Split(':');
            if (partes.Length == 2)
            {
                tablaCifrado[partes[1][0]] = partes[0][0]; // invertir el mapeo para descifrar
            }
        }
        return tablaCifrado;
    }
}