using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hàng_hóa
{
    public partial class Form1 : Form
    {
        private List<Product> productList = new List<Product>();
        private int generatedProductId = 1;
        public Form1()
        {
            InitializeComponent();
        }

        private void btn_thoat_Click(object sender, EventArgs e)
        {
            DialogResult rt = MessageBox.Show("Ban co thuc su muon thoat khong?","Thong bao",MessageBoxButtons.YesNo);
            if (rt == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txt_ten.Text = string.Empty;
            txt_sl.Text = string.Empty;
            txt_giatien.Text = string.Empty;
            dataGridView1.ClearSelection();
            //dataGridView1.Refresh();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Khởi tạo DataGridView
            dataGridView1.AutoGenerateColumns = true;

            // Thiết lập nguồn dữ liệu cho DataGridView là productList
            dataGridView1.DataSource = productList;


        }
        public bool kt(string s)
        {
            double a = 0;
            bool success = double.TryParse(s, out a);
            if (success)
            {
                if (a < 0)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }
        public bool ktten(string s)
        {
            int a = s.Length;
            for (int i = 0; i < a; i++)
            {
                if (s[i] >= '0' && s[i] <= '9') { return false; }
            }
            return true;
        }
        private void btn_them_Click(object sender, EventArgs e)
        {
            if (txt_ten.Text == "" || txt_sl.Text == "" || txt_giatien.Text == "")
            {
                MessageBox.Show("Bạn hãy nhập đầy đủ thông tin sản phẩm!", "Thông báo");
            }
            else if (ktten(txt_ten.Text) == false)
            {
                MessageBox.Show("ten hang hoa khong hop le!");
                this.txt_ten.Focus();

            }
            else if (kt(txt_sl.Text) == false)
            {
                MessageBox.Show("so luong khong hop le!");
                this.txt_sl.Focus();
            }
            else if (kt(txt_giatien.Text) == false)
            {
                MessageBox.Show("gia tien  khong hop le!");
                this.txt_giatien.Focus();
            }
            else
            {
                // Thêm sản phẩm mới
                Product product = new Product
                {
                    ProductId = generatedProductId++,
                    TenSanPham = txt_ten.Text,
                    SoLuong = Convert.ToInt32(txt_sl.Text),
                    GiaTien = Convert.ToDecimal(txt_giatien.Text)
                };

                productList.Add(product);

                // Cập nhật DataGridView
                RefreshDataGridView();

                List<Product> Results = new List<Product>();

                foreach (Product products in productList)
                {

                    Results.Add(products);

                }

                dataGridView1.DataSource = Results;

                txt_ten.Text = string.Empty;
                txt_sl.Text = string.Empty;
                txt_giatien.Text = string.Empty;
                txt_ten.Focus();



            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có sản phẩm nào được chọn không
            if (dataGridView1.CurrentRow != null)
            {
                if (txt_ten.Text == "" || txt_sl.Text == "" || txt_giatien.Text == "")
                {
                    MessageBox.Show("Bạn hãy nhập đầy đủ thông tin sản phầm để sửa!");
                }
                else
                {
                    // Lấy chỉ số của sản phẩm được chọn
                    int selectedIndex = dataGridView1.CurrentCell.RowIndex;

                    // Cập nhật thông tin của sản phẩm
                    productList[selectedIndex].TenSanPham = txt_ten.Text;
                    productList[selectedIndex].SoLuong = Convert.ToInt32(txt_sl.Text);
                    productList[selectedIndex].GiaTien = Convert.ToDecimal(txt_giatien.Text);

                    // Cập nhật DataGridView
                    RefreshDataGridView();

                    List<Product> Results = new List<Product>();

                    foreach (Product products in productList)
                    {

                        Results.Add(products);

                    }

                    dataGridView1.DataSource = Results;

                    txt_ten.Text = string.Empty;
                    txt_sl.Text = string.Empty;
                    txt_giatien.Text = string.Empty;
                    txt_ten.Focus();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Index != -1)
            {
                // Lấy chỉ số của sản phẩm được chọn
                int selectedIndex = dataGridView1.CurrentCell.RowIndex;

                // Xóa sản phẩm khỏi danh sách
                productList.RemoveAt(selectedIndex);

                // Cập nhật DataGridView
                RefreshDataGridView();

                List<Product> Results = new List<Product>();

                foreach (Product products in productList)
                {

                    Results.Add(products);

                }

                dataGridView1.DataSource = Results;
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }


        private void btn_timkiem_Click(object sender, EventArgs e)
        {
            if (txt_findname.Text == "")
            { MessageBox.Show("Bạn hãy nhập tên hàng hóa cần tìm kiếm!", "Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information); }
            else
            {
                // Tìm kiếm sản phẩm theo tên hoặc lớp
                string keyword = txt_findname.Text.ToLower();
                List<Product> searchResults = new List<Product>();

                foreach (Product product in productList)
                {
                    if (product.TenSanPham.ToLower().Contains(keyword) || product.SoLuong.ToString().Contains(keyword))
                    {
                        searchResults.Add(product);
                    }
                }

                dataGridView1.DataSource = searchResults;

                txt_findname.Text = String.Empty;

                txt_findname.Focus();
            }
        }
        private void RefreshDataGridView()
        {
            // Cập nhật DataGridView
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = productList;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Cập nhật DataGridView với tất cả sản phẩm
            List<Product> Results = new List<Product>();

            foreach (Product product in productList)
            {
                
                    Results.Add(product);
                
            }

            dataGridView1.DataSource = Results;
           
        }
    }
    public class Product
    {
        public int ProductId { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal GiaTien { get; set; }
    }

}
