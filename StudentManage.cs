using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows;
using MessageBox = System.Windows.MessageBox;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;


namespace StudentManage
{
    public partial class StudentManage : Form
    {

        QuanLySinhVienEntities database = new QuanLySinhVienEntities();
        List<SinhVien> danhSachSinhVien;
        

        DataTable dataTable;

        public StudentManage()
        {
            danhSachSinhVien = database.SinhViens.ToList();
            InitializeComponent();
            initDataTable();
          
        }
        private void initDataTable()
        {   
            dataTable = new DataTable("Students");
            dataTable.Columns.Add(ConstantApp.columnId, typeof(int));
            dataTable.Columns.Add(ConstantApp.columnFullname, typeof(string));
            dataTable.Columns.Add(ConstantApp.columnDOB, typeof(DateTime));
            dataTable.Columns.Add(ConstantApp.columnClass, typeof(string));
            dataTable.Columns.Add(ConstantApp.columnSex, typeof(string));
            dataTable.Columns.Add(ConstantApp.columnArgScore, typeof(double));

            dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns[ConstantApp.columnId] };
        }
        private void StudentManage_Load(object sender, EventArgs e)
        {
            setDataTable(danhSachSinhVien);
            setDataGridView();

            cbSort.SelectedIndex = 0;
        }

        private void setDataTable(List<SinhVien> list)
        {
            foreach (var sinhvien in list)
            {
                if (sinhvien.Diems.Count() != 0)
                {
                    double diemtrungbinh = averageScore(sinhvien);
                    dataTable.Rows.Add(sinhvien.MaSinhVien, sinhvien.HoTen, sinhvien.NgaySinh, sinhvien.Lop, sinhvien.GioiTinh, diemtrungbinh);
                }
                else
                {
                    dataTable.Rows.Add(sinhvien.MaSinhVien, sinhvien.HoTen, sinhvien.NgaySinh, sinhvien.Lop, sinhvien.GioiTinh, 0.0);
                }
            }
        }

        private void setDataGridView()
        {
            dataGridView.DataSource = dataTable;
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            lbQuantityStudent.Text = $"Số lượng sinh viên : {danhSachSinhVien.Count}";
        }

        private double averageScore(SinhVien sinhVien)
        {
            double sum = 0.0;
            int countScore = sinhVien.Diems.Count();


            foreach (var score in sinhVien.Diems)
            {
                sum += score.DiemSo;
            }

            return Math.Floor(sum / countScore * 100) / 100;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EnterInfo enterInfo = new EnterInfo();

            DialogResult result = enterInfo.ShowDialog();
            if (result == DialogResult.OK)
            {
                MessageBox.Show("Thêm thành công");
                SinhVien sinhVien = enterInfo.sinhVien;
                danhSachSinhVien = database.SinhViens.ToList();
                changeDataGridView(danhSachSinhVien);
                lbQuantityStudent.Text = $"Số lượng sinh viên : {database.SinhViens.Count()}";
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            SinhVien sinhVien = getStudentSelected();
            EnterInfo enterInfo = new EnterInfo(false, sinhVien);

            DialogResult result = enterInfo.ShowDialog();
            if (result == DialogResult.OK)
            {

                if (dataGridView.CurrentRow != null)
                {
                    MessageBox.Show($"Cập nhật thành công");

                    foreach (var sv in danhSachSinhVien)
                    {
                        if (sinhVien.MaSinhVien == sv.MaSinhVien)
                        {
                            sv.HoTen = enterInfo.sinhVien.HoTen;
                            sv.NgaySinh = enterInfo.sinhVien.NgaySinh;
                            sv.Lop = enterInfo.sinhVien.Lop;
                            sv.GioiTinh = enterInfo.sinhVien.GioiTinh;
                        }
                    }
                    changeDataGridView(danhSachSinhVien);
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên với ID được chỉ định.");
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            SinhVien sinhVien = getStudentSelected();

            MessageBoxResult result = MessageBox.Show(
            $"Bạn có chắc chắn muốn sinh viên có mã : {sinhVien.MaSinhVien}", 
            "Xóa mục", 
            MessageBoxButton.YesNo, 
            MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                SinhVien sinhVienFound = database.SinhViens.FirstOrDefault(sv => sv.MaSinhVien == sinhVien.MaSinhVien);
               
                if (sinhVienFound != null)
                {
                    var diems = database.Diems.Where(sc => sc.MaSinhVien == sinhVienFound.MaSinhVien).ToList();
                    if (diems.Count() != 0)
                    {
                        foreach (var diem in diems)
                        {
                            database.Diems.Remove(diem);
                        }

                    }

                    //dataGridView.Rows.RemoveAt(dataGridView.SelectedRows[0].Index);
                    database.SinhViens.Remove(sinhVienFound);
                    database.SaveChanges();
                    MessageBox.Show($"Xóa thành công sinh viên : {sinhVien.MaSinhVien} - {sinhVien.HoTen}");
                }

                danhSachSinhVien = database.SinhViens.ToList();
                changeDataGridView(danhSachSinhVien);
            }
        }

        private void btnAddScore_Click(object sender, EventArgs e)
        {
            SinhVien sinhVien = getStudentSelected();
            ScoreManage scoreManage = new ScoreManage(database, sinhVien);

            DialogResult result = scoreManage.ShowDialog();

            if (result == DialogResult.OK)
            {
                danhSachSinhVien = database.SinhViens.ToList();
                changeDataGridView(danhSachSinhVien);

                MessageBox.Show("Cập nhật điểm thành công");
            }
        }
        private SinhVien getStudentSelected()
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                SinhVien sinhVien = new SinhVien();
                DataGridViewRow selectedRow = dataGridView.SelectedRows[0];
                string id = selectedRow.Cells[ConstantApp.columnId].Value.ToString();
                string fullname = selectedRow.Cells[ConstantApp.columnFullname].Value.ToString();
                string dob = selectedRow.Cells[ConstantApp.columnDOB].Value.ToString().Split(' ')[0];
                string lop = selectedRow.Cells[ConstantApp.columnClass].Value.ToString();
                string sex = selectedRow.Cells[ConstantApp.columnSex].Value.ToString();

                sinhVien.MaSinhVien = int.Parse(id);
                sinhVien.HoTen = fullname;
                sinhVien.NgaySinh = DateTime.Parse(dob);
                sinhVien.Lop = lop;
                sinhVien.GioiTinh = sex;

                return sinhVien;
            }

            return null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<SinhVien> danhSachSinhVienSearch;
            string search = tbSearch.Text;
            if (!search.Equals(""))
            {
                danhSachSinhVienSearch = danhSachSinhVien.Where(sv =>
                {
                    string lastName = sv.HoTen.Split(' ').Last();
                    return lastName.ToLower().Contains(search.ToLower());
                }).ToList();

                changeDataGridView(danhSachSinhVienSearch);
            }
            else
            {
                changeDataGridView(danhSachSinhVien);
            }

        }

        private void cbSort_SelectionChangeCommitted(object sender, EventArgs e)
        {
            List<SinhVien> danhSachSinhVienSort;
            int selectedIndex = cbSort.SelectedIndex;
            if (selectedIndex == 0)
            {
                changeDataGridView(danhSachSinhVien);
            }
            else if(selectedIndex == 1)
            {
                danhSachSinhVienSort = danhSachSinhVien.OrderByDescending(sv => sv.HoTen.Split(' ').Last()).ToList();
                changeDataGridView(danhSachSinhVienSort);
            }
            else if (selectedIndex == 2)
            {
                danhSachSinhVienSort = danhSachSinhVien.OrderByDescending(sv => averageScore(sv)).ToList();
                changeDataGridView(danhSachSinhVienSort);
            }
        }

        private void changeDataGridView(List<SinhVien> list)
        {
            dataTable.Clear();
            dataGridView.DataSource = null;
            setDataTable(list);
            setDataGridView();
        }

    }
}
