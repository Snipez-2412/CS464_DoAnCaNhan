using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn
{
    public partial class frm_Main : Form
    {
        public frm_Main()
        {
            InitializeComponent();
            frm_QLSanPham qlSP = new frm_QLSanPham();
            qlSP.MdiParent = this;
            qlSP.Show();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frm_QLSanPham"] == null)
            {
                frm_QLSanPham qlSP = new frm_QLSanPham();
                qlSP.MdiParent = this;
                qlSP.Show();
            }
            else { Application.OpenForms["frm_QLSanPham"].Activate(); }
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frm_QLNhanVien"] == null)
            {
                frm_QLNhanVien qlNV = new frm_QLNhanVien();
                qlNV.MdiParent = this;
                qlNV.Show();
            }
            else { Application.OpenForms["frm_QLNhanVien"].Activate(); }
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frm_QLKhachHang"] == null)
            {
                frm_QLKhachHang qlKH = new frm_QLKhachHang();
                qlKH.MdiParent = this;
                qlKH.Show();
            }
            else { Application.OpenForms["frm_QLKhachHang"].Activate(); }
        }

        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frm_QLTaiKhoan"] == null)
            {
                frm_QLTaiKhoan qlTK = new frm_QLTaiKhoan();
                qlTK.MdiParent = this;
                qlTK.Show();
            }
            else { Application.OpenForms["frm_QLTaiKhoan"].Activate(); }
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frm_QLHoaDon"] == null)
            {
                frm_QLHoaDon qlHD = new frm_QLHoaDon();
                qlHD.MdiParent = this;
                qlHD.Show();
            }
            else { Application.OpenForms["frm_QLHoaDon"].Activate(); }
        }

        private void báoCáoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["frm_BaoCao"] == null)
            {
                frm_BaoCao formBC = new frm_BaoCao();
                formBC.MdiParent = this;
                formBC.Show();
            }
            else { Application.OpenForms["frm_BaoCao"].Activate(); }
        }

        
    }
}
