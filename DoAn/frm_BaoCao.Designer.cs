namespace DoAn
{
    partial class frm_BaoCao
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.btn_Loc = new System.Windows.Forms.Button();
            this.gv_BaoCao = new System.Windows.Forms.DataGridView();
            this.cb_BaoCao = new System.Windows.Forms.ComboBox();
            this.btn_PDF = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gv_BaoCao)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_Loc
            // 
            this.btn_Loc.Location = new System.Drawing.Point(218, 12);
            this.btn_Loc.Name = "btn_Loc";
            this.btn_Loc.Size = new System.Drawing.Size(75, 23);
            this.btn_Loc.TabIndex = 2;
            this.btn_Loc.Text = "Lọc";
            this.btn_Loc.UseVisualStyleBackColor = true;
            this.btn_Loc.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // gv_BaoCao
            // 
            this.gv_BaoCao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gv_BaoCao.Location = new System.Drawing.Point(12, 41);
            this.gv_BaoCao.Name = "gv_BaoCao";
            this.gv_BaoCao.Size = new System.Drawing.Size(776, 397);
            this.gv_BaoCao.TabIndex = 0;
            // 
            // cb_BaoCao
            // 
            this.cb_BaoCao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_BaoCao.FormattingEnabled = true;
            this.cb_BaoCao.Location = new System.Drawing.Point(12, 12);
            this.cb_BaoCao.Name = "cb_BaoCao";
            this.cb_BaoCao.Size = new System.Drawing.Size(200, 21);
            this.cb_BaoCao.TabIndex = 1;
            // 
            // btn_PDF
            // 
            this.btn_PDF.Location = new System.Drawing.Point(299, 12);
            this.btn_PDF.Name = "btn_PDF";
            this.btn_PDF.Size = new System.Drawing.Size(75, 23);
            this.btn_PDF.TabIndex = 3;
            this.btn_PDF.Text = "Xuất PDF";
            this.btn_PDF.UseVisualStyleBackColor = true;
            this.btn_PDF.Click += new System.EventHandler(this.btnExportPDF_Click);
            // 
            // frm_BaoCao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_PDF);
            this.Controls.Add(this.btn_Loc);
            this.Controls.Add(this.cb_BaoCao);
            this.Controls.Add(this.gv_BaoCao);
            this.Name = "frm_BaoCao";
            this.Text = "Báo Cáo";
            this.Load += new System.EventHandler(this.frm_BaoCao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gv_BaoCao)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.DataGridView gv_BaoCao;
        private System.Windows.Forms.ComboBox cb_BaoCao;
        private System.Windows.Forms.Button btn_PDF;
        private System.Windows.Forms.Button btn_Loc;
    }
}
