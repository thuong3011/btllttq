namespace ShoeStore.Views
{
    partial class frmHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("ListViewGroup", System.Windows.Forms.HorizontalAlignment.Left);
            this.btnChiTietHoaDon = new System.Windows.Forms.Button();
            this.groupBoxThongTinBoPhan = new System.Windows.Forms.GroupBox();
            this.lv = new System.Windows.Forms.ListView();
            this.colSTT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colIdNhapKho = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNV = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colKH = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNgayNhapKho = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSoLuong = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTongThanhTien = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnLoad = new System.Windows.Forms.Button();
            this.ngaybatdau = new System.Windows.Forms.DateTimePicker();
            this.ngayketthuc = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxThongTinBoPhan.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnChiTietHoaDon
            // 
            this.btnChiTietHoaDon.Location = new System.Drawing.Point(12, 472);
            this.btnChiTietHoaDon.Name = "btnChiTietHoaDon";
            this.btnChiTietHoaDon.Size = new System.Drawing.Size(165, 26);
            this.btnChiTietHoaDon.TabIndex = 35;
            this.btnChiTietHoaDon.Text = "Chi tiết hoá đơn";
            this.btnChiTietHoaDon.UseVisualStyleBackColor = true;
            this.btnChiTietHoaDon.Click += new System.EventHandler(this.btnChiTietHoaDon_Click);
            // 
            // groupBoxThongTinBoPhan
            // 
            this.groupBoxThongTinBoPhan.Controls.Add(this.lv);
            this.groupBoxThongTinBoPhan.Location = new System.Drawing.Point(12, 49);
            this.groupBoxThongTinBoPhan.Name = "groupBoxThongTinBoPhan";
            this.groupBoxThongTinBoPhan.Size = new System.Drawing.Size(791, 417);
            this.groupBoxThongTinBoPhan.TabIndex = 33;
            this.groupBoxThongTinBoPhan.TabStop = false;
            this.groupBoxThongTinBoPhan.Text = "Thông tin chung";
          
            // 
            // lv
            // 
            this.lv.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSTT,
            this.colIdNhapKho,
            this.colNV,
            this.colKH,
            this.colNgayNhapKho,
            this.colSoLuong,
            this.colTongThanhTien});
            listViewGroup3.Header = "ListViewGroup";
            listViewGroup3.Name = "listViewGroup1";
            this.lv.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup3});
            this.lv.HideSelection = false;
            this.lv.Location = new System.Drawing.Point(35, 19);
            this.lv.Name = "lv";
            this.lv.Size = new System.Drawing.Size(728, 423);
            this.lv.TabIndex = 1;
            this.lv.UseCompatibleStateImageBehavior = false;
            this.lv.View = System.Windows.Forms.View.Details;
         
            // 
            // colSTT
            // 
            this.colSTT.Text = "STT";
            // 
            // colIdNhapKho
            // 
            this.colIdNhapKho.Text = "Id Hoá đơn";
            this.colIdNhapKho.Width = 89;
            // 
            // colNV
            // 
            this.colNV.Text = "Nhân viên";
            this.colNV.Width = 81;
            // 
            // colKH
            // 
            this.colKH.Text = "Khách hàng";
            this.colKH.Width = 102;
            // 
            // colNgayNhapKho
            // 
            this.colNgayNhapKho.Text = "Ngày in hoá đơn";
            this.colNgayNhapKho.Width = 136;
            // 
            // colSoLuong
            // 
            this.colSoLuong.Text = "Tổng số lượng";
            this.colSoLuong.Width = 101;
            // 
            // colTongThanhTien
            // 
            this.colTongThanhTien.Text = "Tổng thành tiền";
            this.colTongThanhTien.Width = 122;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(480, 472);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(132, 26);
            this.btnLoad.TabIndex = 36;
            this.btnLoad.Text = "Load lại";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // ngaybatdau
            // 
            this.ngaybatdau.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ngaybatdau.Location = new System.Drawing.Point(238, 13);
            this.ngaybatdau.Name = "ngaybatdau";
            this.ngaybatdau.Size = new System.Drawing.Size(105, 20);
            this.ngaybatdau.TabIndex = 37;
            // 
            // ngayketthuc
            // 
            this.ngayketthuc.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.ngayketthuc.Location = new System.Drawing.Point(381, 13);
            this.ngayketthuc.Name = "ngayketthuc";
            this.ngayketthuc.Size = new System.Drawing.Size(100, 20);
            this.ngayketthuc.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(212, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 39;
            this.label1.Text = "Từ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(349, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 40;
            this.label2.Text = "đến";
            // 
            // frmHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 523);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ngayketthuc);
            this.Controls.Add(this.ngaybatdau);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnChiTietHoaDon);
            this.Controls.Add(this.groupBoxThongTinBoPhan);
            this.Name = "frmHoaDon";
            this.Text = "Hoá đơn";
            this.Load += new System.EventHandler(this.frmHoaDon_Load);
            this.groupBoxThongTinBoPhan.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnChiTietHoaDon;
        private System.Windows.Forms.GroupBox groupBoxThongTinBoPhan;
        private System.Windows.Forms.ListView lv;
        private System.Windows.Forms.ColumnHeader colSTT;
        private System.Windows.Forms.ColumnHeader colIdNhapKho;
        private System.Windows.Forms.ColumnHeader colNV;
        private System.Windows.Forms.ColumnHeader colKH;
        private System.Windows.Forms.ColumnHeader colNgayNhapKho;
        private System.Windows.Forms.ColumnHeader colSoLuong;
        private System.Windows.Forms.ColumnHeader colTongThanhTien;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DateTimePicker ngaybatdau;
        private System.Windows.Forms.DateTimePicker ngayketthuc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}