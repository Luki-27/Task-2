namespace CSV_API.Models
{
    public class Receipt
    {
        public string BusinessUnit { get; set; }
        public string ReceiptMethodID { get; set; }
        public string RemittanceBank { get; set; }
        public string ReceiptNumber { get; set; }
        public string ReceiptAmount { get; set; }
        public string Invoicenumberreference { get; set; }
        public string InvoiceAmount { get; set; }
        public string Comments { get; set; }
    }
}
