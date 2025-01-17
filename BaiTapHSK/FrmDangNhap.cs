﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapHSK
{
    public partial class FrmDangNhap : Form
    {
        public FrmDangNhap()
        {
            InitializeComponent();
        }

        private void FrmDangNhap_Load(object sender, EventArgs e)
        {
                
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmDangKi dangki = new FrmDangKi();
            this.Close();
            dangki.Show();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string conn = "Data Source=MSI\\MINHCHIEN;Initial Catalog=QLBanMyPham;Integrated Security=True";
            string querry = "SELECT * FROM dbo.tblTaiKhoan Where @sTaiKhoan = sTenTaiKhoan and @sMatKhau = sMatKhau";

            SqlConnection sqlConnection = new SqlConnection(conn);
            if (txtMatKhau.Text == "" || txtTenDN.Text == "")
            {
                MessageBox.Show("Tên tài khoản và mật khẩu không được để trống!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (sqlConnection.State != ConnectionState.Open)
            {
                sqlConnection.Open();
            }
            SqlCommand sqlCommand = new SqlCommand(querry,sqlConnection);
            sqlCommand.Parameters.AddWithValue("@sTaiKhoan", txtTenDN.Text);
            sqlCommand.Parameters.AddWithValue("@sMatKhau", txtMatKhau.Text);
            SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    MessageBox.Show("Bạn đã đang nhập thành công . Bạn có thể sử dụng các chức năng ngay bây giờ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK; 
                    //HeThong heThong = new HeThong(IsLogger);
                    TrangChu trangChu = new TrangChu();
                    trangChu.Show();

                }
            }
            else
            {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu! Vui lòng kiểm tra lại", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            reader.Close(); 
            sqlConnection.Close();
        }
    }
}
