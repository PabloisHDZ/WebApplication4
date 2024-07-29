//using Microsoft.AspNetCore.Mvc;
//using WebApplication4.Data;
//using WebApplication4.Models;
//using ClosedXML.Excel;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using System.IO;
//using System.Linq;

//public class ImportController : Controller
//{
//    private readonly dbboot _context;

//    public ImportController(dbboot context)
//    {
//        _context = context;
//    }

//    [HttpPost]
//    public async Task<IActionResult> Upload(IFormFile file)
//    {
//        if (file == null || file.Length == 0)
//        {
//            return BadRequest("No file uploaded");
//        }

//        using (var stream = new MemoryStream())
//        {
//            await file.CopyToAsync(stream);
//            using (var workbook = new XLWorkbook(stream))
//            {
//                var worksheet = workbook.Worksheets.First();
//                foreach (var row in worksheet.RowsUsed().Skip(1)) // Assuming first row is header
//                {
//                    var vehiculo = new Vehiculo
//                    {
//                        Marca = row.Cell(1).GetValue<string>(),
//                        Modelo = row.Cell(2).GetValue<string>(),
//                        Año = row.Cell(3).GetValue<int>(),
//                        Capacidad = row.Cell(4).GetValue<decimal>(),
//                        CompañiaID = row.Cell(5).GetValue<int>()
//                    };
//                    _context.Vehiculos.Add(vehiculo);
//                }
//                await _context.SaveChangesAsync();
//            }
//        }

//        return Ok("File successfully uploaded and data imported.");
//    }
//}
