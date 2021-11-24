using System;

namespace OzonEdu.MerchandiseService.HttpModels
{
   public sealed class EmployeeItemResponse
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public DateTimeOffset birthDay { get; set; }
        public DateTimeOffset hiringDate { get; set; }
        public string email { get; set; }
    }
}