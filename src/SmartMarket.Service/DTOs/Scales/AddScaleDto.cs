namespace SmartMarket.Service.DTOs.Scales
{
    public class AddScaleDto
    {
        public string Name { get; set; }
        public int UpdateTimeInterval { get; set; }
        public string SelectFilePath { get; set; }
        public string TXTFileName { get; set; }
    }
}
