using System;
using System.Data;
using ShoeStore.Models;

namespace ShoeStore.Controls
{
    class HoaDon
    {
        private Status status = new Status();
        private Database database = new Database();
        private DataTable hoaDon_tb;
        private string str;

        public DataTable HoaDon_tb { get => hoaDon_tb; }

        public HoaDon()
        {
            LoadDanhSach();
        }

        /// <summary>
        /// Load danh sách hóa đơn trong khoảng thời gian từ startDate đến endDate.
        /// </summary>
        public void LoadDanhSach(DateTime? startDate = null, DateTime? endDate = null)
        {
            str = "SELECT * FROM HOADON, NHANVIEN, KHACHHANG " +
                  "WHERE HOADON.status = 1 " +
                  "AND HOADON.idNV = NHANVIEN.idNV " +
                  "AND HOADON.idKH = KHACHHANG.idKH ";

            // Nếu có ngày bắt đầu và ngày kết thúc, thêm điều kiện lọc
            if (startDate.HasValue && endDate.HasValue)
            {
                str += "AND HOADON.ngayInHoaDon >= '" + startDate.Value.ToString("yyyy-MM-dd") + "' " +
                       "AND HOADON.ngayInHoaDon <= '" + endDate.Value.ToString("yyyy-MM-dd") + "'";
            }

            // In câu lệnh SQL ra để kiểm tra
            Console.WriteLine(str);

            this.hoaDon_tb = database.Execute(str);
        }


        /// <summary>
        /// Load hóa đơn dựa trên idHoaDon.
        /// </summary>
        public void LoadHoaDon(string idHoaDon)
        {
            str = "SELECT * FROM HOADON WHERE HOADON.status = 1 AND HOADON.idHoaDon = '" + idHoaDon + "'";
            this.hoaDon_tb = database.Execute(str);
        }

        /// <summary>
        /// Thêm mới hóa đơn và trả về idHoaDon vừa được tạo.
        /// </summary>
        public string ThemHoaDon(string idNV, string idKH, string ngayInHoaDon)
        {
            str = "execute pr_ThemHoaDon '" + idNV + "', '" + idKH + "', '" + ngayInHoaDon + "'";
            database.ExecuteNonQuery(str);

            // Lấy idHoaDon vừa thêm
            str = "SELECT idHoaDon FROM HOADON WHERE idNV = '" + idNV + "' AND idKH = '" + idKH + "'";
            DataTable dt = database.Execute(str);

            if (dt.Rows.Count >= 1)
            {
                return dt.Rows[dt.Rows.Count - 1]["idHoaDon"].ToString();
            }
            else
            {
                return status.Nonexistence;
            }
        }

        /// <summary>
        /// Xóa toàn bộ hóa đơn và các chi tiết hóa đơn liên quan.
        /// </summary>
        public string XoaByToanBoHoaDon(string idHoaDon)
        {
            str = "execute pr_XoaToanBoHoaDon '" + idHoaDon + "'";
            database.ExecuteNonQuery(str);
            LoadDanhSach(); // Tải lại danh sách hóa đơn sau khi xóa
            return status.Success;
        }
        public DataTable TinhTongThanhTienTheoThang()
        {
            int currentMonth = DateTime.Now.Month;
            DataTable resultTable = new DataTable();
            resultTable.Columns.Add("Thang", typeof(int));
            resultTable.Columns.Add("TongThanhTien", typeof(decimal));

            for (int month = 1; month <= currentMonth; month++)
            {
                str = "SELECT SUM(HOADON.thanhtien ) AS TongThanhTien " +
                       "FROM HOADON " +
                       "WHERE HOADON.status = 1 " +
                       "AND MONTH(HOADON.ngayInHoaDon) = " + month + " " +
                       "AND YEAR(HOADON.ngayInHoaDon) = " + DateTime.Now.Year;

                DataTable dt = database.Execute(str);
                decimal tongThanhTien = dt.Rows.Count > 0 && dt.Rows[0]["TongThanhTien"] != DBNull.Value
                                        ? Convert.ToDecimal(dt.Rows[0]["TongThanhTien"])
                                        : 0;

                resultTable.Rows.Add(month, tongThanhTien);
            }

            return resultTable;
        }

    }
}
