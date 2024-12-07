using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarketDesktop.ViewModels.Entities.Scales
{
    [Table("sclaes")]
    public class ScaleView : BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("update_time_interval")]
        public int UpdateTimeInterval { get; set; }
        [Column("select_file_path")]
        public string SelectFilePath { get; set; }
        [Column("txt_file_name")]
        public string TXTFileName { get; set; }
    }
}
