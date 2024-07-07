using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml;

namespace DoAn
{
    public partial class frm_BaoCao : Form
    {
        public frm_BaoCao()
        {
            InitializeComponent();
        }

        private void frm_BaoCao_Load(object sender, EventArgs e)
        {
            // Thêm các loại báo cáo vào ComboBox
            cb_BaoCao.Items.Add("Doanh thu");
            cb_BaoCao.Items.Add("Tồn kho");
            cb_BaoCao.Items.Add("Hoạt động bán hàng");
            cb_BaoCao.SelectedIndex = 0; // Chọn mục đầu tiên mặc định
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            string reportType = cb_BaoCao.SelectedItem.ToString();

            DataTable dt = GetReportData(reportType);
            gv_BaoCao.DataSource = dt;
        }

        private DataTable GetReportData(string reportType)
        {
            DataTable dt = new DataTable();
            // Giả lập dữ liệu báo cáo, bạn cần thay thế bằng truy vấn từ cơ sở dữ liệu
            dt.Columns.Add("Ngày");
            dt.Columns.Add("Chi tiết");
            dt.Columns.Add("Giá trị");

            for (int i = 0; i < 10; i++)
            {
                dt.Rows.Add(DateTime.Now.AddDays(i).ToString("dd/MM/yyyy"), "Chi tiết " + i, i * 1000);
            }

            return dt;
        }

        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF files (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ExportToPdf(gv_BaoCao, saveFileDialog.FileName);
            }
        }

        private void ExportToPdf(DataGridView dgv, string fileName)
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
            document.Open();

            PdfPTable table = new PdfPTable(dgv.Columns.Count);

            // Thêm tiêu đề cột
            foreach (DataGridViewColumn column in dgv.Columns)
            {
                table.AddCell(new Phrase(column.HeaderText));
            }

            // Thêm dữ liệu hàng
            foreach (DataGridViewRow row in dgv.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    table.AddCell(new Phrase(cell.Value?.ToString()));
                }
            }

            document.Add(table);
            document.Close();
        }

        

       
    }
}
