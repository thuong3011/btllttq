using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ShoeStore.Models;

namespace ShoeStore.Controls
{
    class NhapKho
    {
        private Status status = new Status();
        private Database database = new Database();
        private DataTable nhanvien_tb;
        private DataTable nhacungcap_tb;
        private DataTable nhapkho_tb = new DataTable();
        private string str;

        public DataTable Nhacungcap_tb { get => nhacungcap_tb; }
        public DataTable Nhapkho_tb { get => nhapkho_tb; }
        public DataTable Nhanvien_tb { get => nhanvien_tb; }

        public NhapKho()
        {
            LoadDanhSach();
            NhanVien nhanvien_tb = new NhanVien();
            this.nhanvien_tb = nhanvien_tb.Nhanvien_tb;
            NhaCungCap nhacungcap_tb = new NhaCungCap();
            this.nhacungcap_tb = nhacungcap_tb.NhaCungCap_tb;
        }
		public void LoadDanhSach(DateTime? startDate = null, DateTime? endDate = null)
		{
			str = "select * from NHAPKHO, NHANVIEN, NHACUNGCAP " +
				  "where NHAPKHO.status=1 and NHAPKHO.idNV = NHANVIEN.idNV and NHAPKHO.idNCC = NHACUNGCAP.idNCC";

			if (startDate.HasValue && endDate.HasValue)
			{
				str += " AND nhapkho.ngaynhapkho >= '" + startDate.Value.ToString("yyyy-MM-dd") + "' " +
					   "AND nhapkho.ngaynhapkho <= '" + endDate.Value.ToString("yyyy-MM-dd") + "'";
			}

			Console.WriteLine(str); // Debugging the SQL query

			this.nhapkho_tb = database.Execute(str); // Ensure this returns the correct filtered DataTable
		}


		public string Them(string idNV, string idNCC, string ngayNhapKho)
        {
            //proc thêm phiếu nhập kho.
            //str = "insert into NHAPKHO (idNV, idNCC, ngayNhapKho) values('" + idNV + "', '" + idNCC + "', '" + ngayNhapKho + "')";
            str = "execute pr_themphieunhapkho '" + idNV + "', '" + idNCC + "', '" + ngayNhapKho + "'";
            database.ExecuteNonQuery(str);
            LoadDanhSach();
            return status.Success;
        }
    }
}
