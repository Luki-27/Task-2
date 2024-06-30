using CSV_API.Models;
using CsvHelper.Configuration;
using CsvHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;


namespace CSV_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CSV_Controller : ControllerBase
    {

        [Authorize]
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadCsvFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded or its empty.");
            }

            var allowedExtensions = new[] { ".csv" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (string.IsNullOrEmpty(fileExtension) || !allowedExtensions.Contains(fileExtension))
            {
                return BadRequest("Invalid file extension. Only CSV files are allowed.");
            }

            List<Receipt> records;

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                stream.Position = 0;

                using (var reader = new StreamReader(stream))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                }))
                {
                    csv.Context.RegisterClassMap<ReceiptMap>();
                    try
                    {
                        records = new List<Receipt>(csv.GetRecords<Receipt>());
                    }
                    catch (Exception ex)
                    {
                        return BadRequest($"One or more required headers are missing from the CSV file.");
                    }
                }
            }
            return Ok(ConvertData(records));
        }
        private object ConvertData(List<Receipt> records)
        {

            var TargetView = records
                .GroupBy(gp => new { gp.BusinessUnit, gp.ReceiptMethodID })
                .Select(rec => new
                {
                    BusinessUnit = rec.Key.BusinessUnit,
                    ReceiptMethodID = rec.Key.ReceiptMethodID,
                    Transactions = rec.Select(rec => new
                    {
                        RemittanceBank = rec.RemittanceBank,
                        ReceiptNumber = rec.ReceiptNumber,
                        ReceiptAmount = rec.ReceiptAmount,
                        Invoicenumberreference = rec.Invoicenumberreference,
                        InvoiceAmount = rec.InvoiceAmount,
                    })

                });
            return TargetView;

        }
    }
}
