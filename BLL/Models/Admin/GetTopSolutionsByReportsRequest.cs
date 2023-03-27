namespace BLL.Models.Admin;

public class GetTopSolutionsByReportsRequest
{
    public int Take { get; set; } = 10;
    public int Skip { get; set; } = 0;
}