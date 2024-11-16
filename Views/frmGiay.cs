using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShoeStore.Controls;

namespace ShoeStore.Views
{
    public partial class frmGiay : Form
    {
        Models.Database db=new Models.Database();
        Status status = new Status();
        Giay giay;
        string idLoaiGiay = "0";
        public frmGiay()
        {
            InitializeComponent();
        }
        public frmGiay(string idLoaiGiay)
        {
            InitializeComponent();
            this.idLoaiGiay = idLoaiGiay;
           
        }
        private void frmGiay_Load(object sender, EventArgs e)
        {
            giay = new Giay(this.idLoaiGiay);
            LoadCBTenGiay();
            LoadListView();

            // Reset các ComboBox về giá trị mặc định
            cbTenGiay.SelectedIndex = -1;
            cbMauSac.SelectedIndex = -1;
            cbSize.SelectedIndex = -1;
            txtGiaBan.Text = "";  // Đặt giá trị giá bán mặc định là rỗng
           
        }
        public void LoadListView()
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.Items.Clear();
            lv.Columns[1].Width = 133;

            string str;
            DataTable dt = giay.Giay_tb;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem lvi;
                lvi = lv.Items.Add((i + 1).ToString());

                str = dt.Rows[i]["tenLoaiGiay"].ToString();
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["tenHangGiay"].ToString();
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["mauSac"].ToString();
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["size"].ToString();
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["soLuongGiay"].ToString();
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["giaBan"].ToString();
                lvi.SubItems.Add(str);
            }
        }
        private void LoadCBTenGiay()
        {
            DataTable dt = giay.DanhMuc_tb;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ComboboxItem item = new ComboboxItem();
                string str = dt.Rows[i]["tenLoaiGiay"].ToString(); // + " - " + dt.Rows[i]["mauSac"].ToString() + " - " + dt.Rows[i]["size"].ToString();
                item.Text = str;
                item.Value = dt.Rows[i]["idLoaiGiay"].ToString();
                cbTenGiay.Items.Add(item);
            }
            if (cbTenGiay.Items.Count > 0)
            {
                cbTenGiay.SelectedIndex = 0;

            }
        }
        private void lv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lv.SelectedIndices.Count > 0)
            {
                // Lấy thông tin từ mục được chọn trong ListView
                string str = lv.Items[lv.SelectedIndices[0]].SubItems[1].Text;
                cbTenGiay.SelectedIndex = cbTenGiay.FindStringExact(str);

                // Kiểm tra cbMauSac trước khi truy cập
                if (cbMauSac.Items.Count > 0)
                {
                    cbMauSac.SelectedIndex = cbMauSac.FindStringExact(lv.Items[lv.SelectedIndices[0]].SubItems[3].Text);
                }

                // Kiểm tra cbSize trước khi truy cập
                if (cbSize.Items.Count > 0)
                {
                    cbSize.SelectedIndex = cbSize.FindStringExact(lv.Items[lv.SelectedIndices[0]].SubItems[4].Text);
                }

                txtGiaBan.Text = lv.Items[lv.SelectedIndices[0]].SubItems[6].Text;

               

               
            }
        }

        private void cbTenGiay_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Kiểm tra xem có mục nào được chọn không
            if (cbTenGiay.SelectedIndex >= 0)
            {
                // Lấy mục được chọn từ cbTenGiay
                ComboboxItem selectedItem = (ComboboxItem)cbTenGiay.SelectedItem;
                string selectedValue = selectedItem.Value.ToString();

                // Truy vấn màu sắc giày dựa trên idLoaiGiay được chọn
                DataTable dt = db.Execute("SELECT DISTINCT mauSac FROM giay WHERE idLoaiGiay=" + selectedValue+"");

                // Cập nhật các giá trị màu sắc vào cbMauSac
                cbMauSac.DataSource = dt;
                cbMauSac.DisplayMember = "mauSac";
                cbMauSac.ValueMember = "mauSac";
            }
        }


        private void btnTimKiemTen_Click(object sender, EventArgs e)
        {
            if (txtTimKiem.Text.Trim() != "")
            {
                giay.TimKiem(txtTimKiem.Text.Trim());
                LoadListView();
            }
            else
            {
                giay = new Giay(this.idLoaiGiay);
                LoadListView();
            }
        }
        private void btnLoad_Click(object sender, EventArgs e)
        {
            frmGiay_Load(sender,e );
        } 
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            string giaban = txtGiaBan.Text.Trim();
            if (giaban != "")
            {
                if (IsPositiveNumber(giaban) == false)
                {
                    MessageBox.Show("Giá bán chỉ chứa giá trị số dương lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (giay.CapNhat(lv.SelectedIndices[0], giaban) == status.Success)
                {
                    MessageBox.Show("Giày đã được cập nhật thành công", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadListView();
                    txtGiaBan.Text = "";
                }
                else
                {
                    MessageBox.Show("Giày bị trùng", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string idLoaiGiay = (cbTenGiay.SelectedItem as ComboboxItem).Value.ToString();
            // Kiểm tra và lấy giá trị màu sắc nhập vào từ ComboBox
            string mausac = cbMauSac.Text.Trim(); // Lấy giá trị nhập từ bàn phím hoặc chọn từ ComboBox
            string size = cbSize.SelectedItem.ToString();

            if (giay.Them(idLoaiGiay, mausac, size) == status.Success)
            {
                MessageBox.Show("Giày đã được thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadListView();
            }
            else
            {
                MessageBox.Show("Giày bị trùng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public static bool IsPositiveNumber(string number)
        {
            if (IsNumber(number) == false)
            {
                return false;
            }

            int num = int.Parse(number);
            if(num > 0)
            {
                return true;
            }
            return false;
        }
        public static bool IsNumber(string number)
        {
            try
            {
                int sdt = int.Parse(number);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        private void txtTimKiem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnTimKiemTen_Click(sender, e);
            }
        }

        private void groupBoxThongTinChiTiet_Enter(object sender, EventArgs e)
        {

        }
    }
}
