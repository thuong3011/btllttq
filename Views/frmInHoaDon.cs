using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ShoeStore.Controls;
using OfficeOpenXml;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

namespace ShoeStore.Views
{
	public partial class frmInHoaDon : Form
	{
		string idHoaDon = "0";
		Status status = new Status();
		KhachHang khachHang = new KhachHang();
		HoaDon hoaDon = new HoaDon();
		ChiTietHoaDon chiTietHoaDon = new ChiTietHoaDon();
		Giay giay = new Giay();
		DanhMuc danhMuc = new DanhMuc();
		string idKH;
		User user;
		public frmInHoaDon(User user)
		{
			InitializeComponent();
			this.user = user;
			lblNgayNhap.Text = (DateTime.Now).ToString("dd/MM/yyyy");
		

		}
		private void loadlai_Click(object sender, EventArgs e)
		{
			
			frmInHoaDon_Load(sender, e);

		}
		private void frmInHoaDon_Load(object sender, EventArgs e)
		{  

			btnThanhToan.Enabled = false;
			btnHuy.Enabled = false;
			txtTenKH.Text = "";
			txtSdtKH.Text = "";
			txtSoLuong.Text = "";
			cbTenGiay.Text = "";
			LoadCBTenGiay(sender, e);
			LoadListView();
			lblIdHoaDon.Text = this.idHoaDon;
			if (user.PhanQuyen == "0")
			{
				btnDangXuat.Hide();
			}

		
		}
		private void LoadCBTenGiay(object sender, EventArgs e)
		{
			// Xóa các item cũ để tránh trùng lặp
			cbTenGiay.Items.Clear();

			// Lấy dữ liệu từ bảng giày
			DataTable dt = giay.Giay_tb;

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				// Kiểm tra null và chuyển đổi kiểu dữ liệu
				if (dt.Rows[i]["soLuongGiay"] != DBNull.Value
					&& dt.Rows[i]["giaBan"] != DBNull.Value
					&& Convert.ToInt32(dt.Rows[i]["soLuongGiay"]) > 0
					&& Convert.ToInt32(dt.Rows[i]["giaBan"]) > 0)
				{
					ComboboxItem item = new ComboboxItem
					{
						Text = $"{dt.Rows[i]["tenLoaiGiay"]} - {dt.Rows[i]["mauSac"]} - {dt.Rows[i]["size"]}",
						Value = dt.Rows[i]["idGiay"].ToString()
					};
					cbTenGiay.Items.Add(item);
				}
			}

			// Thiết lập item được chọn nếu có dữ liệu
			if (cbTenGiay.Items.Count > 0)
			{
				cbTenGiay.SelectedIndex = 0;
				// Gọi SelectionChangeCommitted chỉ khi cần thiết
				cbTenGiay_SelectionChangeCommitted(sender, e);
			}
			else
			{
				MessageBox.Show("Kho đang trống chờ lấy hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		private void cbTenGiay_TextChanged(object sender, EventArgs e)
		{
			// Lấy giá trị người dùng nhập vào và chuyển về chữ thường để so sánh không phân biệt chữ hoa hay thường
			string searchText = cbTenGiay.Text.ToLower();

			// Nếu không có gì nhập vào, không cần tìm kiếm và hiển thị lại tất cả các item
			if (string.IsNullOrEmpty(searchText))
			{
				LoadCBTenGiay(sender, e);  // Gọi lại hàm LoadCBTenGiay để tải lại tất cả các item
				return;
			}

			// Xóa các item cũ trong ComboBox
			cbTenGiay.Items.Clear();

			// Lấy dữ liệu từ bảng giày
			DataTable dt = giay.Giay_tb;

			// Lọc dữ liệu và thêm vào ComboBox
			foreach (DataRow row in dt.Rows)
			{
				if (row["soLuongGiay"] != DBNull.Value && row["giaBan"] != DBNull.Value
					&& Convert.ToInt32(row["soLuongGiay"]) > 0 && Convert.ToInt32(row["giaBan"]) > 0)
				{
					string displayText = $"{row["tenLoaiGiay"]} - {row["mauSac"]} - {row["size"]}".ToLower();

					// Kiểm tra nếu giá trị người dùng nhập vào có khớp với item trong danh sách
					if (displayText.Contains(searchText))
					{
						ComboboxItem item = new ComboboxItem
						{
							Text = $"{row["tenLoaiGiay"]} - {row["mauSac"]} - {row["size"]}",
							Value = row["idGiay"].ToString()
						};
						cbTenGiay.Items.Add(item);
					}
				}
			}

			// Mở dropdown nếu có ít nhất 1 item phù hợp
			cbTenGiay.DroppedDown = cbTenGiay.Items.Count > 0;
		}


		public void LoadListView()
		{
			lv.View = View.Details;
			lv.FullRowSelect = true;
			lv.Items.Clear();

			string str;
			chiTietHoaDon.LoadDanhSachChiTiet(this.idHoaDon);
			DataTable dt = chiTietHoaDon.ChiTietHoaDon_tb;
			DataRow[] foundRows;

			for (int i = 0; i < dt.Rows.Count; i++)
			{
				ListViewItem lvi;
				lvi = lv.Items.Add((i + 1).ToString());

				str = dt.Rows[i]["idGiay"].ToString();
				lvi.SubItems.Add(str);

				foundRows = giay.Giay_tb.Select("idGiay='" + str + "'");
				str = foundRows[0]["idLoaiGiay"].ToString();
				foundRows = danhMuc.DanhMuc_tb.Select("idLoaiGiay='" + str + "'");
				str = foundRows[0]["tenLoaiGiay"].ToString();
				lvi.SubItems.Add(str);

				str = dt.Rows[i]["mauSac"].ToString();
				lvi.SubItems.Add(str);

				str = dt.Rows[i]["size"].ToString();
				lvi.SubItems.Add(str);

				str = dt.Rows[i]["soLuong"].ToString();
				lvi.SubItems.Add(str);

				str = dt.Rows[i]["donGia"].ToString();
				lvi.SubItems.Add(str);

				str = dt.Rows[i]["thanhTien"].ToString();
				lvi.SubItems.Add(str);
			}
			hoaDon.LoadHoaDon(this.idHoaDon);
			dt = hoaDon.HoaDon_tb;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				lblTongSoLuong.Text = dt.Rows[i]["soLuong"].ToString();
				lblTongThanhTien.Text = dt.Rows[i]["thanhTien"].ToString();

			}
		}
		private void cbTenGiay_SelectedIndexChanged(object sender, EventArgs e)
		{
		}
		private void lv_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lv.SelectedIndices.Count > 0)
			{
				string str;
				string ten = lv.Items[lv.SelectedIndices[0]].SubItems[2].Text;
				string mausac = lv.Items[lv.SelectedIndices[0]].SubItems[3].Text;
				string size = lv.Items[lv.SelectedIndices[0]].SubItems[4].Text;
				str = ten + " - " + mausac + " - " + size;

				cbTenGiay.SelectedIndex = cbTenGiay.FindStringExact(str);
				txtSoLuong.Text = lv.Items[lv.SelectedIndices[0]].SubItems[5].Text;
				lblGiaBan.Text = lv.Items[lv.SelectedIndices[0]].SubItems[6].Text;
			}
		}
		private void btnLayidKhachHang_Click(object sender, EventArgs e)
		{
			if (kiemTraTaoPhieuHoaDonKoThongBao() != status.Exist)
			{
				string ten = txtTenKH.Text.Trim();
				string sdt = txtSdtKH.Text.Trim();
				if (sdt != "")
				{
					if (IsPhoneNumber(sdt) == false || sdt.Length < 10)
					{
						MessageBox.Show("Số điện thoại không được chứa ký tự đặc biệt và độ dài từ 10 số trở lên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}

					// Kiểm tra số điện thoại trong CSDL
					this.idKH = khachHang.TimIDKhachHang(sdt);

					if (this.idKH == status.Nonexistence) // Nếu không tồn tại khách hàng
					{
						DialogResult result = MessageBox.Show("Khách hàng chưa có trong hệ thống. Bạn có muốn thêm mới không?",
															  "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

						if (result == DialogResult.Yes) // Người dùng chọn "Yes"
						{
							// Kiểm tra nếu txtTenKH trống
							if (string.IsNullOrEmpty(txtTenKH.Text.Trim()))
							{
								MessageBox.Show("Vui lòng nhập tên khách hàng .", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								return; // Dừng quá trình nếu không nhập tên
							}

							// Insert khách hàng mới
							khachHang.Them(txtTenKH.Text.Trim(), sdt);

							// Lấy lại ID khách hàng sau khi thêm
							this.idKH = khachHang.TimIDKhachHang(sdt);

							if (this.idKH == status.Nonexistence) // Nếu vẫn không lấy được ID
							{
								MessageBox.Show("Thêm khách hàng mới thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
								return;
							}
						}
						else // Người dùng chọn "No"
						{
							return; // Dừng quá trình nếu không thêm mới khách hàng
						}
					}


					// Hiển thị tên khách hàng
					txtTenKH.Text = khachHang.KhachHang_tb.Rows[0]["tenKH"].ToString();

					// Tạo phiếu hóa đơn
					string ngayNhapKho = (DateTime.Now).ToString("MM/dd/yyyy");
					this.idHoaDon = hoaDon.ThemHoaDon(user.IdUser, idKH, ngayNhapKho);

					if (this.idHoaDon == status.Nonexistence)
					{
						MessageBox.Show("Lỗi tạo hóa đơn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						this.idHoaDon = "0";
						lblIdHoaDon.Text = this.idHoaDon;
						chiTietHoaDon.LoadDanhSachChiTiet(this.idHoaDon);
					}
					else
					{
						btnHuy.Enabled=true;
						this.chiTietHoaDon = new ChiTietHoaDon();
						this.chiTietHoaDon.LoadDanhSachChiTiet(this.idHoaDon);
						lblIdHoaDon.Text = this.idHoaDon;
						MessageBox.Show("Lấy khách hàng và tạo phiếu hóa đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				else
				{
					MessageBox.Show("Bạn chưa nhập tên hoặc số điện thoại của khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			else
			{
				MessageBox.Show("Thanh toán hoặc hủy hóa đơn hiện tại trước khi nhập khách hàng và tạo hóa đơn mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

	
		private void btnThem_Click(object sender, EventArgs e)
		{
			if (kiemTraTaoPhieuHoaDon() == status.Exist)
			{
				string idGiay = (cbTenGiay.SelectedItem as ComboboxItem).Value.ToString();
				string soluong = txtSoLuong.Text;
				string giaBan = lblGiaBan.Text;

				if (idGiay != "" && giaBan != "" && soluong != "")
				{
					if (IsPositiveNumber(giaBan) == false || IsPositiveNumber(soluong) == false)
					{
						MessageBox.Show("Giá bán và số lượng chỉ chứa giá trị dương lớn hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (chiTietHoaDon.ThemChiTiet(this.idHoaDon, idGiay, giaBan, soluong) == status.Success)
					{
						btnThanhToan.Enabled = true;
						MessageBox.Show("Giày đã được thêm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
						LoadListView();
						//Function trả ra tổng số lượng và tổng thành tiền
						hoaDon.LoadHoaDon(this.idHoaDon);
						DataTable dt = hoaDon.HoaDon_tb;
						for (int i = 0; i < dt.Rows.Count; i++)
						{
							lblTongSoLuong.Text = dt.Rows[i]["soLuong"].ToString();
							lblTongThanhTien.Text = dt.Rows[i]["thanhTien"].ToString();
						}
					}
					else
					{
						MessageBox.Show("Giày bị trùng", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					}
				}
				else
				{
					MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}
		public static bool IsPositiveNumber(string number)
		{
			if (IsNumber(number) == false)
			{
				return false;
			}

			int num = int.Parse(number);
			if (num > 0)
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

		public static bool IsPhoneNumber(string number)
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
		private void btnThanhToan_Click(object sender, EventArgs e)
		{
			if (kiemTraTaoPhieuHoaDon() == status.Exist && Convert.ToInt32(lblTongSoLuong.Text)>0)
			{
				if (lv.Items.Count <= 0)
				{
					MessageBox.Show("Hoá đơn chưa có bất cứ chi tiết nào để thanh toán", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}
				//Transaction thanh toán đã được xử lý bởi trigger

				

				SaveFileDialog saveFileDialog = new SaveFileDialog();
				saveFileDialog.Title = "Export Excel";
				saveFileDialog.Filter = "Excel (*.xlsx)|*.xlsx";
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					try
					{
						ExportExcel(saveFileDialog.FileName);
						MessageBox.Show("Xuất hóa đơn thành công!");
					}
					catch (Exception ex)
					{
						MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
					}
				}
				
				//Sau khi thanh toán, set idHoaDon về 0
				this.idHoaDon = "0";
				lblIdHoaDon.Text = this.idHoaDon;
				chiTietHoaDon.LoadDanhSachChiTiet(this.idHoaDon);
				frmInHoaDon_Load(sender, e);

			}
			else
			{
				MessageBox.Show("Chưa có sản phẩm!");
			}
		}

		//In ra Excel
		private void ExportExcel(string path)
		{
			try
			{
				// Khởi tạo Excel application
				Excel.Application application = new Excel.Application();
				application.Application.Workbooks.Add(Type.Missing);

				// Thông tin từ giao diện
				string sdt = txtSdtKH.Text.Trim();
				string tenKH = txtTenKH.Text.Trim();
				string tongSoLuong = lblTongSoLuong.Text.Trim();
				string tongTien = lblTongThanhTien.Text.Trim();

				// Thông tin nhân viên
				string maNV = user.IdUser;
				string tenNV = user.Name;

				// Thêm tên cửa hàng
				Excel.Range shopName = application.get_Range("A1", "E1");
				shopName.Merge();
				shopName.Value = "TINA Shop";
				shopName.Font.Bold = true;
				shopName.Font.Size = 10;
				shopName.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
				shopName.HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

				// Thêm tiêu đề hóa đơn
				Excel.Range invoiceTitle = application.get_Range("A2", "E2");
				invoiceTitle.Merge();
				invoiceTitle.Value = "HÓA ĐƠN BÁN";
				invoiceTitle.Font.Bold = true;
				invoiceTitle.Font.Size = 14;
				invoiceTitle.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
				invoiceTitle.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

				// Thêm thông tin khách hàng
				application.Cells[4, 1] = "Tên khách hàng:";
				application.Cells[4, 2] = tenKH;
				application.Cells[5, 1] = "Số điện thoại:";
				application.Cells[5, 2] = sdt;
				application.Cells[6, 1] = "Tổng Tiền:";
				application.Cells[6, 2] = tongTien;
				application.Cells[7, 1] = "Tổng Số Lượng:";
				application.Cells[7, 2] = tongSoLuong;

				// Ghi tiêu đề cột
				for (int i = 0; i < lv.Columns.Count; i++)
				{
					application.Cells[9, i + 1] = lv.Columns[i].Text;
					Excel.Range headerCell = application.Cells[9, i + 1];
					headerCell.Font.Bold = true;
					headerCell.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
					headerCell.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
				}

				// Ghi dữ liệu từ ListView
				for (int i = 0; i < lv.Items.Count; i++)
				{
					ListViewItem item = lv.Items[i];
					for (int j = 0; j < item.SubItems.Count; j++)
					{
						application.Cells[i + 10, j + 1] = item.SubItems[j].Text ?? string.Empty;
					}
				}

				// Thêm thông tin nhân viên cuối bảng
				int lastRow = lv.Items.Count + 11;
				application.Cells[lastRow, 1] = "Nhân viên:";
				application.Cells[lastRow, 2] = $"{tenNV} (Mã NV: {maNV})";

				// Điều chỉnh kích thước cột tự động
				application.Columns.AutoFit();

				// Lưu tệp Excel
				application.ActiveWorkbook.SaveCopyAs(path);
				application.ActiveWorkbook.Saved = true;
				application.Quit();

				// Giải phóng tài nguyên
				Marshal.ReleaseComObject(application);
				
			}
			catch (Exception ex)
			{
				MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}


		private void btnIn_Click(object sender, EventArgs e)
		{

		}

		private void btnHuy_Click(object sender, EventArgs e)
		{
			if (kiemTraTaoPhieuHoaDon() == status.Exist)
			{
				btnHuy.Enabled = true;
				//xoá hoá đơn và chi tiết hoá đơn hiện tại
				hoaDon = new HoaDon();
				hoaDon.XoaByToanBoHoaDon(this.idHoaDon);

				MessageBox.Show("Phiếu hoá đơn đã được xoá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
				//set idHoaDon = 0
				this.idHoaDon = "0";
				lblIdHoaDon.Text = this.idHoaDon;
				chiTietHoaDon.LoadDanhSachChiTiet(this.idHoaDon);
				LoadListView();
				hoaDon.LoadHoaDon(this.idHoaDon);
				DataTable dt = hoaDon.HoaDon_tb;
				for (int i = 0; i < dt.Rows.Count; i++)
				{
					lblTongSoLuong.Text = dt.Rows[i]["soLuong"].ToString();
					lblTongThanhTien.Text = dt.Rows[i]["thanhTien"].ToString();

				}
			frmInHoaDon_Load(sender, e);
			}
		}
		private string kiemTraTaoPhieuHoaDon()
		{
			if (this.idHoaDon == "0")
			{
				MessageBox.Show("Bạn chưa tạo phiếu hoá đơn. Vui lòng nhập khách hàng và bấm nút Tìm/Thêm khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return status.Nonexistence;
			}
			else
			{
				return status.Exist;
			}
		}
		private string kiemTraTaoPhieuHoaDonKoThongBao()
		{
			if (this.idHoaDon == "0")
			{
				return status.Nonexistence;
			}
			else
			{
				return status.Exist;
			}
		}

		private void cbTenGiay_SelectionChangeCommitted(object sender, EventArgs e)
		{
			DataRow[] foundRows;
			string idGiay = (cbTenGiay.SelectedItem as ComboboxItem).Value.ToString();
			foundRows = giay.Giay_tb.Select("idGiay='" + idGiay + "'");

			if (foundRows.Length > 0)
			{
				lblSoLuongTrongKho.Text = "/" + foundRows[0]["soLuongGiay"].ToString();
				lblGiaBan.Text = foundRows[0]["giaBan"].ToString();

				// Đặt giá trị tối đa cho NumericUpDown
				int maxSoLuong = Convert.ToInt32(foundRows[0]["soLuongGiay"]);
				txtSoLuong.Maximum = maxSoLuong;
			}
		}



		private void btnXoaChiTietHD_Click(object sender, EventArgs e)
		{
			if (kiemTraTaoPhieuHoaDon() == status.Exist)
			{
				if (lv.SelectedIndices.Count > 0)
				{
					string str = "";
					str = lv.Items[lv.SelectedIndices[0]].SubItems[1].Text;
					if (MessageBox.Show("Bạn có chắc chắn là muốn xóa  giày ’" + str + "’ không ?", "Hỏi lại", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						int deleteIndex = lv.SelectedIndices[0];
						if (chiTietHoaDon.XoaChiTiet(this.idHoaDon, deleteIndex) == status.Success)
						{
							LoadListView();
						}
						else
						{
							MessageBox.Show("Xoá không thành công", "Lỗi",
							   MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						}
					}
				}
				else
				{
					MessageBox.Show("Bạn phải chọn 1 nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		private void btnDangXuat_Click(object sender, EventArgs e)
		{
			frmDangNhap form = new frmDangNhap();
			form.Show();
			this.Hide();
		}

		private void txtSdtKH_TextChanged(object sender, EventArgs e)
		{
			string sdt = txtSdtKH.Text.Trim();

			// Kiểm tra nếu người dùng đã nhập ít nhất 10 ký tự
			if (sdt.Length >= 10 && IsPhoneNumber(sdt))
			{
				// Tìm ID khách hàng theo số điện thoại
				this.idKH = khachHang.TimIDKhachHang(sdt);

				if (this.idKH != status.Nonexistence)
				{
					// Lấy thông tin khách hàng từ cơ sở dữ liệu
					DataRow[] foundRows = khachHang.KhachHang_tb.Select($"sdt = '{sdt}'");

					if (foundRows.Length > 0)
					{
						txtTenKH.Text = foundRows[0]["tenKH"].ToString();
					}
				}
				else
				{
					txtTenKH.Text = ""; // Xoá tên khách hàng nếu không tìm thấy
				}
			}
			else
			{
				txtTenKH.Text = "";
			}
		}

		private void groupBoxThongTinChiTiet_Enter(object sender, EventArgs e)
		{

		}

		private void lblSoLuongTrongKho_Click(object sender, EventArgs e)
		{

		}
	}
}
