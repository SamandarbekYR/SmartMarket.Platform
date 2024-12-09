namespace SmartMarket.Service.DTOs.Partner
{
    public class FilterPartnerDto
    {
        public Guid? Id { get; set; }
        public DateTime? FromDateTime { get; set; }
        public DateTime? ToDateTime { get; set; }
    }
}
