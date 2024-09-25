using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

public class CifradoController : Controller
{
    [Authorize]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Cifrar(string texto, int tamañoClave)
    {
        var modelo = new CifradoModel { TextoOriginal = texto, TamañoClave = tamañoClave };
        modelo.TextoCifrado = modelo.CifrarTexto();

        // Ruta dentro de wwwroot para accesibilidad pública
        string carpetaArchivos = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "archivos");
        if (!Directory.Exists(carpetaArchivos))
        {
            Directory.CreateDirectory(carpetaArchivos);
        }

        // Guardar el texto cifrado en un archivo dentro de wwwroot/archivos
        string rutaCifrado = Path.Combine(carpetaArchivos, "cifrado.txt");
        System.IO.File.WriteAllText(rutaCifrado, modelo.TextoCifrado);

        // Generar y guardar la tabla de cifrado ASCII en un archivo dentro de wwwroot/archivos
        string rutaTablaCifrado = Path.Combine(carpetaArchivos, "tabla_cifrado.txt");
        modelo.GuardarTablaCifrado(rutaTablaCifrado);

        // Pasar la información del archivo a la vista
        ViewBag.RutaCifrado = "/archivos/cifrado.txt";
        ViewBag.RutaTablaCifrado = "/archivos/tabla_cifrado.txt";

        return View("Resultado", modelo);
    }

    [HttpPost]
    public IActionResult Descifrar(IFormFile archivoCifrado, IFormFile archivoTablaCifrado)
    {
        if (archivoCifrado == null || archivoTablaCifrado == null)
        {
            return BadRequest("Los archivos no fueron subidos correctamente.");
        }

        // Guardar los archivos temporalmente en el servidor
        string rutaArchivoCifrado = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", archivoCifrado.FileName);
        using (var stream = new FileStream(rutaArchivoCifrado, FileMode.Create))
        {
            archivoCifrado.CopyTo(stream);
        }

        string rutaArchivoTablaCifrado =
            Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", archivoTablaCifrado.FileName);
        using (var stream = new FileStream(rutaArchivoTablaCifrado, FileMode.Create))
        {
            archivoTablaCifrado.CopyTo(stream);
        }

        // Leer el contenido de los archivos
        var textoCifrado = System.IO.File.ReadAllText(rutaArchivoCifrado);
        var tablaCifrado = CifradoModel.CargarTablaCifrado(rutaArchivoTablaCifrado);

        // Usar la tabla para descifrar el texto
        var modelo = new CifradoModel { TextoCifrado = textoCifrado };
        modelo.TextoOriginal = modelo.DescifrarTexto(tablaCifrado);

        return View("ResultadoDescifrado", modelo);
    }
}