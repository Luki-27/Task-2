using CsvHelper.Configuration;

namespace CSV_API.Models
{
    public class ReceiptMap:ClassMap< Receipt>
    {
        public ReceiptMap()
        {
            Map(m => m.BusinessUnit).Name("Business Unit");
            Map(m => m.ReceiptMethodID).Name("Receipt Method ID");
            Map(m => m.RemittanceBank).Name("Remittance Bank");
            Map(m => m.ReceiptNumber).Name("Receipt Number");
            Map(m => m.ReceiptAmount).Name("Receipt Amount");
            Map(m => m.Invoicenumberreference).Name("Invoice number reference");
            Map(m => m.InvoiceAmount).Name("Invoice Amount");
        }
    }
}
