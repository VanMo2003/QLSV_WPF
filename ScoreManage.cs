using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManage
{
    public partial class ScoreManage : Form
    {
        QuanLySinhVienEntities database = new QuanLySinhVienEntities();
        DataTable dataTable;
        public SinhVien sinhVien { get; set; }
        List<Diem> diems { get; set; }
        public ScoreManage(SinhVien sinhVien)
        {
            InitializeComponent();
            initDataTable();
            this.sinhVien = sinhVien;
            diems = sinhVien.Diems.ToList();
        }

        private void initDataTable()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add(ConstantApp.columnNameSub, typeof(string));
            dataTable.Columns.Add(ConstantApp.columnScore, typeof(double));
        }
        private void ScoreManage_Load(object sender, EventArgs e)
        {
            List<Diem> scores = database.Diems.Where(score => score.MaSinhVien == sinhVien.MaSinhVien).ToList();

            if (scores.Count() != 0)
            {
                foreach (var score in scores)
                {
                    dataTable.Rows.Add(score.MonHoc, score.DiemSo);
                }
            }
            else
            {
                foreach (var sub in ConstantApp.subjects)
                {
                    dataTable.Rows.Add(sub, 0.0);
                }
            }
           

            dgrScoreManage.DataSource = dataTable;
            dgrScoreManage.Columns[ConstantApp.columnNameSub].ReadOnly = true;
            foreach (DataGridViewColumn column in dgrScoreManage.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void btnOke_Click(object sender, EventArgs e)
        {
            List<Diem> diems = sinhVien.Diems.ToList();

            foreach (DataGridViewRow row in dgrScoreManage.Rows)
            {
                if (!row.IsNewRow)
                {
                    string subName = row.Cells[ConstantApp.columnNameSub].Value?.ToString(); 
                    string score = row.Cells[ConstantApp.columnScore].Value?.ToString(); 

                    SinhVien sinhVienFound = database.SinhViens.Find(sinhVien.MaSinhVien);
                    if (diems.Count() == 0)
                    {
                        Diem diem = new Diem();

                        diem.MaSinhVien = sinhVienFound.MaSinhVien;
                        diem.MonHoc = subName;
                        diem.DiemSo = double.Parse(score);

                        sinhVienFound.Diems.Add(diem);
                        sinhVien.Diems.Add(diem);
                    }
                    else
                    {
                        Diem diemFound = database.Diems.Where(sc => sc.MonHoc == subName && sc.MaSinhVien == sinhVienFound.MaSinhVien).FirstOrDefault();
                        diemFound.DiemSo = double.Parse(score);

                        sinhVien.Diems.Where(sc => sc.MonHoc == subName).FirstOrDefault().DiemSo = double.Parse(score);
                    }

                    database.SaveChanges();
                }
            }

            database.SaveChanges();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgrScoreManage_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgrScoreManage.Columns[e.ColumnIndex].Name == ConstantApp.columnScore)
            {
                if (!string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    if (double.TryParse(e.FormattedValue.ToString(), out double value))
                    {
                        if (value < 0 || value > 10) 
                        {
                            MessageBox.Show("Chỉ được nhập số trong khoảng từ 0 đến 10!", "Lỗi nhập dữ liệu",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            e.Cancel = true; 
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng nhập một số hợp lệ!", "Lỗi nhập dữ liệu",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        e.Cancel = true; 
                    }
                }
            }
        }
    }
}
