﻿using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BaiTapHSK
{
    public partial class SanPham : Form
    {
        public SanPham()
        {
            InitializeComponent();
        }

        private void SanPham_Load(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand smd = new SqlCommand("select * from tblSanPham", cnn))
                {
                    cnn.Open();
                    using (SqlDataAdapter adt = new SqlDataAdapter(smd))
                    {
                        DataTable dt = new DataTable();
                        adt.Fill(dt);
                        dg_SP.DataSource = dt;
                    }
                    dg_SP.Columns[0].HeaderText = "Mã Sản Phẩm";
                    dg_SP.Columns[1].HeaderText = "Tên Sản Phẩm";
                    dg_SP.Columns[2].HeaderText = "Mã Nhà Cung Cấp";
                    dg_SP.Columns[3].HeaderText = "Số lượng";
                    dg_SP.Columns[4].HeaderText = "Giá Hàng";
                    dg_SP.Columns[5].HeaderText = "Trọng lượng";
                    dg_SP.Columns[6].HeaderText = "Màu sắc";
                    dg_SP.Columns[7].HeaderText = "Loại son";
                    dg_SP.Columns[8].HeaderText = "Hãng sản phẩm";
                    dg_SP.Columns[9].HeaderText = "Ngày hết hạn";

                }

            }
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cnn;
                    cmd.CommandText = "Themsp";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@maNCC", txtmancc.Text);
                    cmd.Parameters.AddWithValue("@masp", txtmasp.Text);
                    cmd.Parameters.AddWithValue("@tensp", txttensp.Text);
                    cmd.Parameters.AddWithValue("@Soluong", txtsoluong.Text);
                    cmd.Parameters.AddWithValue("@giahang", txttgiahang.Text);
                    cmd.Parameters.AddWithValue("@trongluong", txtTrongluong.Text);
                    cmd.Parameters.AddWithValue("@mausac", txtMausac.Text);
                    cmd.Parameters.AddWithValue("@loaison", txtLoaison.Text);
                    cmd.Parameters.AddWithValue("@hangsanpham", txtHangsanpham.Text);
                    cmd.Parameters.AddWithValue("@ngayhethan", txtNgayhethan.Text);




                    using (SqlCommand check = new SqlCommand("select *from tblNhaCC", cnn))
                    {
                        bool KT = false;
                        cnn.Open();
                        using (SqlDataReader reader = check.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (txtmasp.Text == reader.ToString())
                                {
                                    MessageBox.Show("Mã khách hàng này đã tồn tại. Mời nhập mã khác");
                                    KT = true;
                                }

                            }
                            reader.Close();

                        }
                        if (KT == false)
                        {
                            cmd.ExecuteNonQuery();
                            SanPham_Load(sender, e);

                        }

                    }


                }
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            string db = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(db))
            {
                if (MessageBox.Show("Bạn có chắc chắn muốn xóa bản ghi này?", "thong bao", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = cnn;
                        cmd.CommandText = "Xoasp";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Masp", txtmasp.Text);
                        cnn.Open();
                        cmd.ExecuteNonQuery();
                        SanPham_Load(sender, e);

                    }
                }
            }
        }

        private void btnsua_Click(object sender, EventArgs e)
        {
            string constr = ConfigurationManager.ConnectionStrings["QLMP"].ConnectionString;
            using (SqlConnection cnn = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.Connection = cnn;
                    cmd.CommandText = "SuaSP";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MaNCC", txtmancc.Text);
                    cmd.Parameters.AddWithValue("@masp", txtmasp.Text);
                    cmd.Parameters.AddWithValue("@tensp", txttensp.Text);
                    cmd.Parameters.AddWithValue("@Soluong", txtsoluong.Text);
                    cmd.Parameters.AddWithValue("@giahang", txttgiahang.Text);
                    cmd.Parameters.AddWithValue("@trongluong", txtTrongluong.Text);
                    cmd.Parameters.AddWithValue("@mausac", txtMausac.Text);
                    cmd.Parameters.AddWithValue("@loason", txtLoaison.Text);
                    cmd.Parameters.AddWithValue("@hangsanpham", txtHangsanpham.Text);
                    cmd.Parameters.AddWithValue("@ngayhethan", txtNgayhethan.Text);
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    SanPham_Load(sender, e);
                }
            }
        }

        private void btnthoat_Click(object sender, EventArgs e)
        {

            
            if (MessageBox.Show("Bạn có muốn thoát không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
                
           
        }

        private void btntimkiem_Click(object sender, EventArgs e)
        {
            if (txtmasp.Text != "" && txttensp.Text != "")
            {
                string rowFilter = string.Format("{0} like '{1}' and {2} like '{3}'", "sMaSP", "*" + txtmasp.Text + "*", "sTenSP", "*" + txttensp.Text + "*");
                (dg_SP.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
            }
            else
            {
                if (txtmasp.Text != "")
                {
                    string rowFilter = string.Format("{0} like '{1}'", "sMaSP", "*" + txtmasp.Text + "*");
                    (dg_SP.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                }
                else
                {
                    string rowFilter = string.Format("{0} like '{1}'", "sTenSP", "*" + txttensp.Text + "*");
                    (dg_SP.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                }
                if(txtHangsanpham.Text != "")
                {
                    string rowFilter = string.Format("{0} like '{1}'", "sHangSanPham", "*" + txtHangsanpham.Text + "*");
                    (dg_SP.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                }
                if(txtMausac.Text != "")
                {
                    string rowFilter = string.Format("{0} like '{1}'", "sMauSac", "*" + txtMausac.Text + "*");
                    (dg_SP.DataSource as DataTable).DefaultView.RowFilter = rowFilter;

                }
                if(txtLoaison.Text != "")
                {
                    string rowFilter = string.Format("{0} like '{1}'", "sLoaiSon", "*" + txtLoaison.Text + "*");
                    (dg_SP.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                }
                if (txtTrongluong.Text != "")
                {
                    string rowFilter = string.Format("{0} like '{1}'", "fTrongLuong", "*" + txtTrongluong.Text + "*");
                    (dg_SP.DataSource as DataTable).DefaultView.RowFilter = rowFilter;
                }
            }

        }

        private void dg_SP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtmancc.Text = dg_SP.CurrentRow.Cells["iMaNCC"].Value.ToString();
            txtmasp.Text = dg_SP.CurrentRow.Cells["sMaSP"].Value.ToString();
            txttensp.Text = dg_SP.CurrentRow.Cells["sTenSP"].Value.ToString();
            txttgiahang.Text = dg_SP.CurrentRow.Cells["fGiaHang"].Value.ToString();
            txtsoluong.Text = dg_SP.CurrentRow.Cells["iSoLuong"].Value.ToString();
            txtTrongluong.Text = dg_SP.CurrentRow.Cells["fTrongLuong"].Value.ToString();
            txtMausac.Text = dg_SP.CurrentRow.Cells["sMausac"].Value.ToString();
            txtLoaison.Text = dg_SP.CurrentRow.Cells["sLoaiSon"].Value.ToString();
            txtHangsanpham.Text = dg_SP.CurrentRow.Cells["sHangSanPham"].Value.ToString();
            txtNgayhethan.Text = dg_SP.CurrentRow.Cells["dNgayHetHan"].Value.ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            txtmasp.Text = "";
            txtmancc.Text = "";
            txtsoluong.Text = "";
            txttensp.Text = "";
            txttgiahang.Text = "";
            txtTrongluong.Text = "";
            txtMausac.Text = "";
            txtLoaison.Text = "";
            txtHangsanpham.Text = "";
            txtNgayhethan.Text = "";
        }
    }
}
