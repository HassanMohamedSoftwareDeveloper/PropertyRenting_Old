using PropertyRenting.Api.Enums;

namespace PropertyRenting.Api.Helpers;

public class QueryHepler
{
    private readonly IWebHostEnvironment _env;

    public QueryHepler(IWebHostEnvironment env)
    {
        _env = env;
    }

    public string GetQuery(FolderName folderName, ReportName reportName)
    {
        var _fullPath = string.Format("{0}\\App_Data\\{1}\\{2}", _env.WebRootPath, folderName.ToEnumString(), reportName + ".sql");

        return ReadStringFile(_fullPath);
    }

    private static string ReadStringFile(string filePath)
    {
        using StreamReader sr = new(filePath);
        return sr.ReadToEnd();
    }
}
