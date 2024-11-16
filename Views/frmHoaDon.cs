using System;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using ShoeStore.Controls;

namespace ShoeStore.Views
{
    public partial class frmHoaDon : Form
    {
        Status status = new Status();
        HoaDon hoaDon = new HoaDon();
        NhanVien nhanVien = new NhanVien();
        KhachHang khachHang = new KhachHang();

        public frmHoaDon()
        {
            InitializeComponent();
			
		}

		private void frmHoaDon_Load(object sender, EventArgs e)
		{
			ngayketthuc.MaxDate = DateTime.Now;
			ngaybatdau.MaxDate = DateTime.Now;

			// Set default date range (can be adjusted as needed)
			DateTime startDate = ngaybatdau.Value;
			DateTime endDate = ngayketthuc.Value;

			// Load data for the default date range
			hoaDon.LoadDanhSach(startDate, endDate);

			// Load the list view with the loaded data
			LoadListView(); // Tải danh sách khi form được tải
		}


		// Hàm để tải dữ liệu vào ListView
		public void LoadListView()
        {
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.Items.Clear();
            lv.Columns[1].Width = 133;

            DataTable dt = hoaDon.HoaDon_tb;

           

            DataRow[] foundRows;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ListViewItem lvi = lv.Items.Add((i + 1).ToString());

                string str = dt.Rows[i]["idHoaDon"].ToString();
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["idNV"].ToString();
                foundRows = nhanVien.Nhanvien_tb.Select("idNV='" + str + "'");
                if (foundRows.Length > 0)
                {
                    str = foundRows[0]["tenNV"].ToString();
                    lvi.SubItems.Add(str);
                }

                str = dt.Rows[i]["idKH"].ToString();
                foundRows = khachHang.KhachHang_tb.Select("idKH='" + str + "'");
                if (foundRows.Length > 0)
                {
                    str = foundRows[0]["tenKH"].ToString();
                    lvi.SubItems.Add(str);
                }

                str = DateTime.Parse(dt.Rows[i]["ngayInHoaDon"].ToString()).ToString("dd/MM/yyyy");
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["soLuong"].ToString();
                lvi.SubItems.Add(str);

                str = dt.Rows[i]["thanhTien"].ToString();
                lvi.SubItems.Add(str);
            }
        }



        // Sự kiện khi bấm vào nút Tải lại
        private void btnLoad_Click(object sender, EventArgs e)
        {
            // Kiểm tra nếu ngày kết thúc lớn hơn ngày bắt đầu
            if (ngayketthuc.Value < ngaybatdau.Value)
            {
                MessageBox.Show("Ngày kết thúc không thể nhỏ hơn ngày bắt đầu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Nếu sai thì không tiếp tục thực hiện
            }

         

            // Nếu các điều kiện trên đều đúng, gọi LoadDanhSach với ngày bắt đầu và ngày kết thúc
            DateTime startDate = ngaybatdau.Value;
            DateTime endDate = ngayketthuc.Value;

            // Gọi phương thức LoadDanhSach với khoảng thời gian đã chọn
            hoaDon.LoadDanhSach(startDate, endDate);
            LoadListView(); // Tải lại danh sách vào ListView
        }



        private void btnChiTietHoaDon_Click(object sender, EventArgs e)
        {
            if (lv.SelectedIndices.Count > 0)
            {
                string idHoaDon = lv.Items[lv.SelectedIndices[0]].SubItems[1].Text;
                frmChiTietHoaDon formChiTietHoaDon = new frmChiTietHoaDon(idHoaDon);
                formChiTietHoaDon.ShowDialog();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 hoá đơn trước", "Lỗi",
                   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
