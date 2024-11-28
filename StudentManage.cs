using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManage
{
    public partial class StudentManage : Form
    {
        QuanLySinhVienEntities database = new QuanLySinhVienEntities();
        List<SinhVien> danhSachSinhVien;

        DataTable dataTable;

        public StudentManage()
        {
           
            InitializeComponent();
            initDataTable();
          
        }
        private void initDataTable()
        {
            dataTable = new DataTable();
            dataTable.Columns.Add("MaSinhVien", typeof(int));
            dataTable.Columns.Add("HoTen", typeof(string));
            dataTable.Columns.Add("NgaySinh", typeof(DateTime));
            dataTable.Columns.Add("Lop", typeof(string));
            dataTable.Columns.Add("GioiTinh", typeof(string));
        }
        private void StudentManage_Load(object sender, EventArgs e)
        {
            danhSachSinhVien = database.SinhViens.ToList();
            foreach (var item in danhSachSinhVien)
            {
                dataTable.Rows.Add(item.MaSinhVien, item.HoTen, item.NgaySinh, item.Lop,item.GioiTinh);
            }

            dataGridView.DataSource = dataTable;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            int id = int.Parse(lbMaSinhVien.Text);
            string fullname = tbFullName.Text;
            string dob = tbDateOfBirth.Text;
            string lop = tbClass.Text;
            string sex = rbMale.Checked ? "nam" : "nữ";

            SinhVien sinhVien = new SinhVien(id, fullname, DateTime.Parse(dob), lop, sex);

            dataTable.Rows.Add(id, fullname, dob, lop, sex);

            database.SinhViens.Add(sinhVien);
            database.SaveChanges();
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                string id = selectedRow.Cells["MaSinhVien"].Value.ToString();
                string fullname = selectedRow.Cells["HoTen"].Value.ToString();
                string dob = selectedRow.Cells["NgaySinh"].Value.ToString().Split(' ')[0];
                string lop = selectedRow.Cells["Lop"].Value.ToString();
                string sex = selectedRow.Cells["GioiTinh"].Value.ToString();
                lbMaSinhVien.Text = id;
                tbFullName.Text = fullname;
                tbDateOfBirth.Text = dob;
                tbClass.Text = lop;
                if (sex == "nam")
                {
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;

                }
            }
        }

        private void clickUpdate(object sender, MouseEventArgs e)
        {
            int id = int.Parse(lbMaSinhVien.Text);
            string fullname = tbFullName.Text;
            string dob = tbDateOfBirth.Text;
            string lop = tbClass.Text;
            string sex = rbMale.Checked ? "nam" : "nữ";

            var sinhVien = database.SinhViens.FirstOrDefault(sv => sv.MaSinhVien == id);

            if (sinhVien != null)
            {
                // Cập nhật thông tin
                sinhVien.HoTen = fullname;
                sinhVien.NgaySinh = DateTime.Parse(dob);
                sinhVien.Lop = lop;
                sinhVien.GioiTinh = sex;

                // Lưu thay đổi
                database.SaveChanges();

                MessageBox.Show($"Cập nhật thành công sinh viên có mã : {id}");
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên với ID được chỉ định.");
            }
        }
    }
}
