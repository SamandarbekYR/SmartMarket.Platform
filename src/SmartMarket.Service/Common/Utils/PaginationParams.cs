namespace SmartMarket.Service.Common.Utils;

public class PaginationParams
{
    private const int maxPageSize = 50;
    private int pageSize;
    public int PageSize
    {
        get
        {
            return pageSize;
        }
        set
        {
            if (value < maxPageSize) pageSize = value;
            else throw new Exception($"Page size must be less {maxPageSize}");
        }
    }
    public int PageIndex { get; set; }

    public PaginationParams()
    {

    }
    public PaginationParams(int pageIndex, int pageSize)
    {
        PageSize = pageSize;
        PageIndex = pageIndex;
    }

    public int SkipCount()
        => (PageIndex - 1) * PageSize;
}
