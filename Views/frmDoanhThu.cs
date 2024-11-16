using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using ShoeStore.Controls;

namespace ShoeStore.Views
{
    public partial class frmDoanhThu : Form
    {
        private HoaDon hoaDon = new HoaDon();
        Models.Database db = new Models.Database();
        
        public frmDoanhThu()
        {
           
            InitializeComponent();
        }

        private void frmDoanhThu_Load(object sender, EventArgs e)
        {
             chi.Enabled = false;
            thu.Enabled = false;
            // Lấy dữ liệu doanh thu theo tháng
            DataTable doanhThuTheoThang = hoaDon.TinhTongThanhTienTheoThang();

            // Tạo biểu đồ doanh thu
            chart1.Series["Tổng doanh thu"].Points.Clear(); // Clear trước khi vẽ mới

            foreach (DataRow row in doanhThuTheoThang.Rows)
            {
                int thang = (int)row["Thang"];
                decimal tongThanhTien = (decimal)row["TongThanhTien"];

                // Thêm điểm vào biểu đồ (Tháng, Tổng doanh thu)
                chart1.Series["Tổng doanh thu"].Points.AddXY(thang, tongThanhTien);
            }

            // Thiết lập thêm thuộc tính cho chart (nếu cần)
            chart1.ChartAreas[0].AxisX.Title = "Tháng";
            chart1.ChartAreas[0].AxisY.Title = "Tổng doanh thu";
            chart1.Legends[0].Title = "Tổng doanh thu theo tháng";

            // Lấy tổng chi từ bảng NHAPKHO
            DataTable dt = db.Execute("select sum(thanhtien) from NHAPKHO WHERE YEAR(ngayNhapKho ) = YEAR(GETDATE())");
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                // Kiểm tra xem giá trị có phải là kiểu decimal không trước khi ép kiểu
                decimal chiValue = 0;
                if (decimal.TryParse(dt.Rows[0][0].ToString(), out chiValue))
                {
                    chi.Text = chiValue.ToString("C"); // Định dạng thành tiền
                }
                else
                {
                    chi.Text = "0"; // Nếu không thể ép kiểu thì hiển thị 0
                }
            }
            else
            {
                chi.Text = "0"; // Nếu không có dữ liệu thì hiển thị 0
            }

            // Lấy tổng thu từ bảng HOADON
            dt = db.Execute("select sum(thanhtien) from HOADON WHERE YEAR(ngayinhoadon ) = YEAR(GETDATE())");
            if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                // Kiểm tra xem giá trị có phải là kiểu decimal không trước khi ép kiểu
                decimal thuValue = 0;
                if (decimal.TryParse(dt.Rows[0][0].ToString(), out thuValue))
                {
                    thu.Text = thuValue.ToString("C"); // Định dạng thành tiền
                }
                else
                {
                    thu.Text = "0"; // Nếu không thể ép kiểu thì hiển thị 0
                }
            }
            else
            {
                thu.Text = "0"; // Nếu không có dữ liệu thì hiển thị 0
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            // Xử lý sự kiện nếu cần
        }
        
    }
}
