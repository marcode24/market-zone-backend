namespace Application.Validations.FileHandling;

using Microsoft.AspNetCore.Http;

public static class ExcelFileValidator
{
  private static readonly string[] ValidExtensions = [".xls", ".xlsx"];

  public static bool IsExcelFile(IFormFile file)
  {
    return IsValidExtension(file) && IsValidMagicNumber(file);
  }

  private static bool IsValidExtension(IFormFile file)
  {
    var extension = Path.GetExtension(file.FileName);
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

    byte[] xlsxMagicNumber = [0x50, 0x4B, 0x03, 0x04];
    byte[] xlsMagicNumber = [0xD0, 0xCF, 0x11, 0xE0];

    bool isValidMagicNumber = header.Take(4).SequenceEqual(xlsxMagicNumber)
      || header.Take(4).SequenceEqual(xlsMagicNumber);

    return isValidMagicNumber;
  }
}
