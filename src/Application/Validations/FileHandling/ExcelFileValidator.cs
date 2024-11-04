namespace Application.Validations.FileHandling;

using Microsoft.AspNetCore.Http;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public static class ExcelFileValidator
{
  private static readonly string[] ValidExtensions = [".xls", ".xlsx"];
  private static readonly byte[] XlsxMagicNumber = [0x50, 0x4B, 0x03, 0x04];
  private static readonly byte[] XlsMagicNumber = [0xD0, 0xCF, 0x11, 0xE0];

  public static bool IsExcelFile(IFormFile file)
  {
    return IsValidExtension(file) && IsValidMagicNumber(file);
  }

  public static bool ValidatePermissionTemplate(Stream excelStream)
  {
    XSSFWorkbook workbook = new(excelStream);
    int hiddenSheetIndex = workbook.GetSheetIndex("Hidden");
    if (hiddenSheetIndex == -1) return false;

    ISheet hiddenSheet = workbook.GetSheetAt(hiddenSheetIndex);
    IRow hiddenRow = hiddenSheet.GetRow(0);
    if (hiddenRow is null) return false;

    string signatureCell = hiddenRow.GetCell(0).StringCellValue;
    string identifierCell = hiddenRow.GetCell(1).StringCellValue;

    return signatureCell == "TEMPLATE-SIGNATURE"
        && identifierCell == "UNIQUE-IDENTIFIER12345";
  }

  private static bool IsValidExtension(IFormFile file)
  {
    var extension = Path.GetExtension(file.FileName)?.ToLowerInvariant();
    bool isValidExtension = ValidExtensions.Contains(extension);
    return isValidExtension;
  }

  private static bool IsValidMagicNumber(IFormFile file)
  {
    byte[] header = new byte[4];
    using (var stream = file.OpenReadStream())
    {
      stream.Read(header, 0, header.Length);
    }

    bool isValidMagicNumber = header.SequenceEqual(XlsxMagicNumber)
      || header.SequenceEqual(XlsMagicNumber);

    return isValidMagicNumber;
  }
}
