function exportingStockOverview(e) {
    var workbook = new ExcelJS.Workbook();
    var worksheet = workbook.addWorksheet('StockOverview');

    DevExpress.excelExporter.exportDataGrid({
        component: e.component,
        worksheet: worksheet,
        topLeftCell: { row: 4, column: 1 }
    }).then(function (dataGridRange) {
        // header
        // https://github.com/exceljs/exceljs#rows
        var headerRow = worksheet.getRow(2);
        headerRow.height = 30;
        // https://github.com/exceljs/exceljs#merged-cells
        worksheet.mergeCells(2, 1, 2, 8);

        // https://github.com/exceljs/exceljs#value-types
        headerRow.getCell(1).value = 'Stock Overview';
        // https://github.com/exceljs/exceljs#fonts
        headerRow.getCell(1).font = { name: 'Segoe UI Light', size: 22 };
        // https://github.com/exceljs/exceljs#alignment
        headerRow.getCell(1).alignment = { horizontal: 'center' };

        // footer
        var footerRowIndex = dataGridRange.to.row + 2;
        var footerRow = worksheet.getRow(footerRowIndex);
        worksheet.mergeCells(footerRowIndex, 1, footerRowIndex, 8);

        footerRow.getCell(1).value = 'Aucxis EPS';
        footerRow.getCell(1).font = { color: { argb: 'BFBFBF' }, italic: true };
        footerRow.getCell(1).alignment = { horizontal: 'right' };
    }).then(function () {
        // https://github.com/exceljs/exceljs#writing-xlsx
        workbook.xlsx.writeBuffer().then(function (buffer) {
            saveAs(new Blob([buffer], { type: 'application/octet-stream' }), 'StockOverview.xlsx');
        });
    });
    e.cancel = true;
}