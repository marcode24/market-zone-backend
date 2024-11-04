using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace Application.Modules.Permissions.FileTemplates;
public static class PermissionTemplateGenerator
{
  private const int MaxTextLength = 50;

  public static byte[] CreatePermissionTemplateGenerator()
  {
    XSSFWorkbook workbook = new();
    ISheet sheet = workbook.CreateSheet("Permissions");
    IRow headerRow = sheet.CreateRow(0);

    var columns = new List<ColumnConfig>
      {
        new() { HeaderName = "Nombre", MaxLength = MaxTextLength, ColumnIndex = 0, Centered = false },
        new() { HeaderName = "Tipo", MaxLength = MaxTextLength, ColumnIndex = 1, Centered = false },
      };


    for (int i = 0; i < columns.Count; i++)
    {
      var column = columns[i];
      ICellStyle headerStyle = CreateHeaderStyle(workbook, column.Centered);
      headerRow.CreateCell(column.ColumnIndex).SetCellValue(column.HeaderName);
      headerRow.GetCell(column.ColumnIndex).CellStyle = headerStyle;

      if (column.MaxLength > 0)
      {
        AddColumnValidation(sheet, column.ColumnIndex, column.MaxLength);
        AddColumnConditionalFormatting(sheet, column.ColumnIndex, column.MaxLength);
      }
    }

    sheet.CreateFreezePane(0, 1);

    ISheet hiddenSheet = workbook.CreateSheet("Hidden");
    workbook.SetSheetHidden(workbook.GetSheetIndex(hiddenSheet), true);
    IRow hiddenRow = hiddenSheet.CreateRow(0);
    hiddenRow.CreateCell(0).SetCellValue("TEMPLATE-SIGNATURE");
    hiddenRow.CreateCell(1).SetCellValue("UNIQUE-IDENTIFIER12345");


    using var stream = new MemoryStream();
    workbook.Write(stream);

    return stream.ToArray();
  }

  private static ICellStyle CreateHeaderStyle(XSSFWorkbook workbook, bool centered)
  {
    ICellStyle headerStyle = workbook.CreateCellStyle();
    headerStyle.FillForegroundColor = IndexedColors.LightTurquoise.Index;
    headerStyle.FillPattern = FillPattern.SolidForeground;

    IFont headerFont = workbook.CreateFont();
    headerFont.IsBold = true;
    headerStyle.SetFont(headerFont);
    headerStyle.Alignment = centered
      ? HorizontalAlignment.Center
      : HorizontalAlignment.Left;

    return headerStyle;
  }

  private static ICellStyle CreateContentStyle(XSSFWorkbook workbook, bool centered)
  {
    ICellStyle contentStyle = workbook.CreateCellStyle();
    contentStyle.WrapText = true;
    contentStyle.Alignment = centered ? HorizontalAlignment.Center : HorizontalAlignment.Left;

    return contentStyle;
  }

  private static void AddColumnValidation(ISheet sheet, int columnIndex, int maxTextLength)
  {
    IDataValidationHelper validationHelper = sheet.GetDataValidationHelper();
    var textLengthConstraint = validationHelper.CreateTextLengthConstraint(
        operatorType: OperatorType.LESS_OR_EQUAL,
        formula1: maxTextLength.ToString(),
        formula2: null
    );

    var cellRangeAddressList = new CellRangeAddressList(1, 1048575, columnIndex, columnIndex);
    var dataValidation = validationHelper.CreateValidation(textLengthConstraint, cellRangeAddressList);
    dataValidation.ShowErrorBox = true;
    dataValidation.CreateErrorBox(
        title: "Error de validación",
        text: $"El texto no puede exceder los {maxTextLength} caracteres."
    );
    sheet.AddValidationData(dataValidation);
  }

  private static void AddColumnConditionalFormatting(ISheet sheet, int columnIndex, int maxTextLength)
  {
    var sheetConditionalFormatting = sheet.SheetConditionalFormatting;
    IConditionalFormattingRule rule = sheetConditionalFormatting.CreateConditionalFormattingRule(
        $"AND(LEN(TRIM({GetColumnLetter(columnIndex)}2)) <= {maxTextLength}, LEN(TRIM({GetColumnLetter(columnIndex)}2)) > 0)"
    );
    IPatternFormatting pattern = rule.CreatePatternFormatting();
    pattern.FillBackgroundColor = IndexedColors.LightGreen.Index;
    pattern.FillPattern = FillPattern.SolidForeground;

    var regions = new CellRangeAddress[] { new(1, 1048575, columnIndex, columnIndex) };
    sheetConditionalFormatting.AddConditionalFormatting(regions, rule);
  }

  private static string GetColumnLetter(int columnIndex)
  {
    return ((char)('A' + columnIndex)).ToString();
  }
}

public class ColumnConfig
{
  public string HeaderName { get; set; } = string.Empty;
  public int MaxLength { get; set; } = 0;
  public int ColumnIndex { get; set; }
  public bool Centered { get; set; } = false;
}
