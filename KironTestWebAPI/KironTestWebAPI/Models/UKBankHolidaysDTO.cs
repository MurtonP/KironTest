namespace KironTestWebAPI.Models
{
    public class UKBankHolidaysDTO
    {
        public string Division { get; set; }
        private class Events
        {
            public string Title { get; set; }
            public string Date { get; set; }
            public string Notes { get; set; }
            public bool Bunting { get; set; }
        }
    }
}
