namespace KironTestWebAPI.Models.Entities
{
    public class BankHolidays
    {
        public class Division
        {
            public int id;
            public string? division;

        }

        public class Events
        {
            public int id;
            public string? title;
        }

        public class BankHolidayEvents
        {
            public int id;
            public int bankHolidayId;
            public int eventId;
            public string? date;
            public string? notes;
            public bool bunting;
        }
    }

    public class AllBankHolidays
    {
        public string? region;
        public List<Division>? divisions;
    }

    public class Division
    {
        public string? division;
        public List<Events>? events;
    }

    public class Events
    {
        public string? title;
        public string? date;
        public string? notes;
        public bool bunting;
    }
}
