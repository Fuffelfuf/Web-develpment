public interface IVariativeService
{
    int GetNumber();
    string GetText();
    byte[] GenerateExcelFile();
}

public class VariativeService : IVariativeService
{
    public int GetNumber() => 64;

    public string GetText() => "Hello, user!";

    public byte[] GenerateExcelFile()
    {
        using var workbook = new ClosedXML.Excel.XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Sheet 1");
        worksheet.Cell(1, 1).Value = "Hello";
        worksheet.Cell(1, 2).Value = "User";

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}