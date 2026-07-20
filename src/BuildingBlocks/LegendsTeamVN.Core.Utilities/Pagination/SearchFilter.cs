namespace LegendsTeamVN.Core.Utilities.Pagination;

public abstract record SearchFilter
{
    private int _pageNumber = 1;
    private int _pageSize = 10;

    private const int MaxPageSize = 100;

    public int PageNumber
    {
        get => _pageNumber;
        set => _pageNumber = value < 1 ? 1 : value;
    }

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : (value < 1 ? 10 : value);
    }

    public string? SearchTerm { get; set; }

    public string? SortColumn { get; set; }

    public SortDirection SortDirection { get; set; } = SortDirection.Descending;
}
