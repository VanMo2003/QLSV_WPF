using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentManage
{
    public partial class EnterInfo : Form
    {
        QuanLySinhVienEntities database = new QuanLySinhVienEntities();
        private bool isAdd { get; set; } = true;
        public SinhVien sinhVien { get; set; }

        public EnterInfo()
        {
            InitializeComponent();
        }

        public EnterInfo(bool isAdd, SinhVien sinhVien)
        {
            this.isAdd = isAdd;
            this.sinhVien = sinhVien;
            InitializeComponent();
        }
        private void EnterInfo_Load(object sender, EventArgs e)
        {
            if (!isAdd)
            {
                lbTitle.Text = $"Chỉnh sửa sinh viên có mã : {this.sinhVien.MaSinhVien}";

                tbFullName.Text = sinhVien.HoTen;
                tbDateOfBirth.Text = sinhVien.NgaySinh.ToString().Split(' ')[0];
                tbClass.Text = sinhVien.Lop;
                if(sinhVien.GioiTinh == "nam")
                {
                    rbMale.Checked = true;
                }
                else
                {
                    rbFemale.Checked = true;
                }

            }
        }
        private void btnOke_Click(object sender, EventArgs e)
        {
            this.sinhVien = getInfo();
            if (this.sinhVien == null)
            {
                MessageBox.Show("Nhập đầy đủ thông tin");
            }
            else
            {
                if (this.isAdd)
                {
                    this.sinhVien = database.SinhViens.Add(this.sinhVien);
                }
                else
                {
                    var sinhVienFound = database.SinhViens.FirstOrDefault(sv => sv.MaSinhVien == sinhVien.MaSinhVien);
                    if (sinhVienFound != null)
                    {
                        sinhVienFound.MaSinhVien = sinhVien.MaSinhVien;
                        sinhVienFound.HoTen = sinhVien.HoTen;
                        sinhVienFound.NgaySinh = sinhVien.NgaySinh;
                        sinhVienFound.Lop = sinhVien.Lop;
                        sinhVienFound.GioiTinh = sinhVien.GioiTinh;
                    }
                }
                database.SaveChanges();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
          
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.sinhVien = getInfo();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private SinhVien getInfo()
        {
            SinhVien sinhVien = new SinhVien();

            string fullname = tbFullName.Text;
            string dob = tbDateOfBirth.Text;
            string lop = tbClass.Text;
            string sex = rbMale.Checked ? "nam" : "nữ";

            if (!fullname.Equals("") && !dob.Equals("") && !lop.Equals(""))
            {
                if (!isAdd)
                {
                    sinhVien.MaSinhVien = int.Parse(lbTitle.Text.Split(':')[1].Trim());
                }
                sinhVien.HoTen = fullname;
                sinhVien.NgaySinh = DateTime.Parse(dob);
                sinhVien.Lop = lop;
                sinhVien.GioiTinh = sex;
            }
            else
            {
                sinhVien = null;
            }

            return sinhVien;
        }
        public static bool IsObjectEmpty(object obj)
        {
            if (obj == null) return true;

            foreach (PropertyInfo property in obj.GetType().GetProperties())
            {
                object value = property.GetValue(obj);
                if (value != null && !(value is string && string.IsNullOrWhiteSpace((string)value)))
                {
                    return false;
                }
            }

            return true;
        }

    }
}
