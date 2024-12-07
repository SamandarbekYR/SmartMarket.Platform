namespace SmartMarketDesktop.DTOs.DTOs.Scales
{
    public class ScaleDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int UpdateTimeInterval { get; set; }
        public string SelectFilePath { get; set; }
        public string TXTFileName { get; set; }
    }
}
