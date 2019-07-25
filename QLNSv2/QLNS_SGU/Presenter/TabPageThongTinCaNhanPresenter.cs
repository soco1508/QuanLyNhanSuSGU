using Model;
using QLNS_SGU.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Entities;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Globalization;
using System.Diagnostics;
using Model.Helper;
using System.Drawing;
using System.Drawing.Imaging;

namespace QLNS_SGU.Presenter
{
    public interface ITabPageThongTinCaNhanPresenter : IPresenterArgument
    {
        void LoadForm();
        void Save();
        void SaveAndClose();
        void LaDangVienChanged(object sender, EventArgs e);
        void GioiTinhChanged(object sender, EventArgs e);
        void HoChanged(object sender, EventArgs e);
        void TenChanged(object sender, EventArgs e);
        void NgaySinhChanged(object sender, EventArgs e);
        void NgayThamGiaCongTacChanged(object sender, EventArgs e);
        void NgayVaoNganhChanged(object sender, EventArgs e);
        void NgayVeTruongChanged(object sender, EventArgs e);
        void SoDienThoaiChanged(object sender, EventArgs e);
        void NoiSinhChanged(object sender, EventArgs e);
        void QueQuanChanged(object sender, EventArgs e);
        void DanTocChanged(object sender, EventArgs e);
        void TonGiaoChanged(object sender, EventArgs e);
        void HoKhauThuongTruChanged(object sender, EventArgs e);
        void NoiOHienNay(object sender, EventArgs e);
        void NgayVaoDangChanged(object sender, EventArgs e);
        void VanHoaChanged(object sender, EventArgs e);
        void SoCMNDChanged(object sender, EventArgs e);
        void GhiChuChanged(object sender, EventArgs e);
        void PicChanged(object sender, EventArgs e);
        void NgayTuyenDungChinhThucChanged(object sender, EventArgs e);
        void EmailChanged(object sender, EventArgs e);
        void GioChuanChanged(object sender, EventArgs e);
        void RotateByLeftMouseClick(object sender, MouseEventArgs e);
    }
    public class TabPageThongTinCaNhanPresenter : ITabPageThongTinCaNhanPresenter
    {
        private TabPageThongTinCaNhan _view;
        bool checkAddNew = false;
        bool gioiTinhChanged = false;
        bool hoChanged = false;
        bool tenChanged = false;
        bool ngaySinhChanged = false;
        bool ngayThamGiaCongTacChanged = false;
        bool ngayVaoNganhChanged = false;
        bool ngayVeTruongChanged = false;
        bool soDienThoaiChanged = false;
        bool noiSinhChanged = false;
        bool queQuanChanged = false;
        bool danTocChanged = false;
        bool tonGiaoChanged = false;
        bool hoKhauThuongTruChanged = false;
        bool noiOHienNayChanged = false;
        bool laDangVienChanged = false;
        bool ngayVaoDangChanged = false;
        bool vanHoaChanged = false;
        bool soCMNDChanged = false;
        bool ghiChuChanged = false;
        bool picChanged = false;
        bool ngayTuyenDungChinhThucChanged = false;
        bool emailChanged = false;
        bool gioChuanChanged = false;
        public object UI => _view;
        public TabPageThongTinCaNhanPresenter(TabPageThongTinCaNhan view) => _view = view;
        public void Initialize(string mavienchuc)
        {
            _view.Attach(this);
            if(mavienchuc == string.Empty)
            {
                checkAddNew = true;
                _view.TXTMaVienChuc.Enabled = true;
            }
            else
            {
                _view.TXTMaVienChuc.Text = mavienchuc;
                _view.TXTMaVienChuc.Enabled = false;
            }
        }
        private void LoadCbxData()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<DanToc> listDanToc = unitOfWorks.DanTocRepository.GetListDanToc();
            _view.CBXDanToc.Properties.DataSource = listDanToc;
            _view.CBXDanToc.Properties.DisplayMember = "tenDanToc";
            _view.CBXDanToc.Properties.ValueMember = "idDanToc";
            _view.CBXDanToc.Properties.DropDownRows = listDanToc.Count;
            _view.CBXDanToc.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idDanToc", string.Empty));
            _view.CBXDanToc.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenDanToc", string.Empty));
            _view.CBXDanToc.Properties.Columns[0].Visible = false;
            List<TonGiao> listTonGiao = unitOfWorks.TonGiaoRepository.GetListTonGiao();
            _view.CBXTonGiao.Properties.DataSource = listTonGiao;
            _view.CBXTonGiao.Properties.DisplayMember = "tenTonGiao";
            _view.CBXTonGiao.Properties.ValueMember = "idTonGiao";
            _view.CBXTonGiao.Properties.DropDownRows = listTonGiao.Count;
            _view.CBXTonGiao.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("idTonGiao", string.Empty));
            _view.CBXTonGiao.Properties.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo("tenTonGiao", string.Empty));
            _view.CBXTonGiao.Properties.Columns[0].Visible = false;
        }
        private void SelectEmptyValueCbx()
        {            
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            _view.CBXDanToc.EditValue = unitOfWorks.DanTocRepository.SelectIdEmptyValue();
            _view.CBXTonGiao.EditValue = unitOfWorks.TonGiaoRepository.SelectIdEmptyValue();
        }
        private byte[] ConvertImageToBinary(string filename)
        {
            byte[] img = null;
            if (filename == string.Empty)
            {
                return img;
            }
            else
            {
                Image image = _view.PICVienChuc.Image.Clone() as Image;
                MemoryStream stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                img = stream.ToArray();
                //FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
                //BinaryReader br = new BinaryReader(fs);
                //img = br.ReadBytes((int)fs.Length);
                return img;
            }
        }
        private System.Drawing.Image ConvertBinaryToImage(byte[] img)
        {
            if (img == null) return null;
            else
            {
                MemoryStream ms = new MemoryStream(img);
                return System.Drawing.Image.FromStream(ms);
            }
        }
        private void InsertData()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            List<string> listMaVienChuc = unitOfWorks.VienChucRepository.GetListMaVienChuc();
            string mavienchuc = _view.TXTMaVienChuc.Text.Trim();
            if (listMaVienChuc.Any(x => x == mavienchuc) == false)
            {
                unitOfWorks.VienChucRepository.Insert(new VienChuc
                {
                    maVienChuc = mavienchuc,
                    gioiTinh = unitOfWorks.VienChucRepository.ReturnGenderToDatabase(_view.RADGioiTinh.SelectedIndex),
                    ho = _view.TXTHo.Text.Trim(),
                    ten = _view.TXTTen.Text.Trim(),
                    ngaySinh = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgaySinh.Text),
                    ngayThamGiaCongTac = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayThamGiaCongTac.Text),
                    ngayVeTruong = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayVeTruong.Text),
                    ngayVaoNganh = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayVaoNganh.Text),
                    ngayVaoDang = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayVaoDang.Text),
                    laDangVien = _view.CHKLaDangVien.Checked,
                    sDT = _view.TXTSoDienThoai.Text,
                    noiSinh = _view.TXTNoiSinh.Text,
                    queQuan = _view.TXTQueQuan.Text,
                    idDanToc = Convert.ToInt32(_view.CBXDanToc.EditValue),
                    idTonGiao = Convert.ToInt32(_view.CBXTonGiao.EditValue),
                    vanHoa = _view.TXTVanHoa.Text,
                    hoKhauThuongTru = _view.TXTHoKhauThuongTru.Text,
                    noiOHienNay = _view.TXTNoiOHienNay.Text,
                    soChungMinhNhanDan = _view.TXTSoCMND.Text,
                    ghiChu = _view.TXTGhiChu.Text,
                    anh = ConvertImageToBinary(_view.PICVienChuc.GetLoadedImageLocation()),
                    ngayTuyenDungChinhThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayTuyenDungChinhThuc.Text),
                    email = _view.TXTEmail.Text,
                    gioChuan = Convert.ToInt32(_view.TXTGioChuan.EditValue)
                });
                unitOfWorks.Save();
                MainPresenter.LoadDataToMainGrid();
                MainPresenter.MoveRowManaging(mavienchuc);
                XtraMessageBox.Show("Lưu dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TabPageQuaTrinhCongTacPresenter.maVienChucFromTabPageThongTinCaNhan = _view.TXTMaVienChuc.Text;
                TabPageQuaTrinhLuongPresenter.maVienChucFromTabPageThongTinCaNhan = _view.TXTMaVienChuc.Text;
                TabPageChuyenMonPresenter.maVienChucFromTabPageThongTinCaNhan = _view.TXTMaVienChuc.Text;
                TabPageTrangThaiPresenter.maVienChucFromTabPageThongTinCaNhan = _view.TXTMaVienChuc.Text;
                TabPageBaoHiemXaHoiPresenter.maVienChuc = _view.TXTMaVienChuc.Text;
                TabPagePhuCapThamNienNhaGiaoPresenter.maVienChucFromTabPageThongTinCaNhan = _view.TXTMaVienChuc.Text;
                TabPageDanhGiaVienChucPresenter.maVienChucFromTabPageThongTinCaNhan = _view.TXTMaVienChuc.Text;
                _view.TXTMaVienChuc.Enabled = false;                
            }
            else
            {
                XtraMessageBox.Show("Mã viên chức đã tồn tại. Vui lòng nhập lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void UpdateData()
        {
            UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
            string mavienchuc = _view.TXTMaVienChuc.Text.Trim();
            int idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc);
            VienChuc vienChuc = unitOfWorks.VienChucRepository.GetById(idVienChuc);
            if(hoChanged)
            {
                vienChuc.ho = _view.TXTHo.Text;
                hoChanged = false;
            }
            if(tenChanged)
            {
                vienChuc.ten = _view.TXTTen.Text;
                tenChanged = false;
            }
            if(gioiTinhChanged)
            {
                vienChuc.gioiTinh = unitOfWorks.VienChucRepository.ReturnGenderToDatabase(_view.RADGioiTinh.SelectedIndex);
                gioiTinhChanged = false;
            }
            if(ngaySinhChanged)
            {
                vienChuc.ngaySinh = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgaySinh.Text);
                ngaySinhChanged = false;
            }
            if(ngayThamGiaCongTacChanged)
            {
                vienChuc.ngayThamGiaCongTac = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayThamGiaCongTac.Text);
                ngayThamGiaCongTacChanged = false;
            }
            if(ngayVeTruongChanged)
            {
                vienChuc.ngayVeTruong = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayVeTruong.Text);
                ngayVeTruongChanged = false;
            }
            if(ngayVaoNganhChanged)
            {
                vienChuc.ngayVaoNganh = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayVaoNganh.Text);
                ngayVaoNganhChanged = false;
            }
            if(laDangVienChanged)
            {
                vienChuc.laDangVien = _view.CHKLaDangVien.Checked;
                laDangVienChanged = false;
            }
            if(ngayVaoDangChanged)
            {
                vienChuc.ngayVaoDang = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayVaoDang.Text);
                ngayVaoDangChanged = false;
            }
            if(soDienThoaiChanged)
            {
                vienChuc.sDT = _view.TXTSoDienThoai.Text;
                soDienThoaiChanged = false;
            }
            if (noiSinhChanged)
            {
                vienChuc.noiSinh = _view.TXTNoiSinh.Text;
                noiSinhChanged = false;
            }
            if(queQuanChanged)
            {
                vienChuc.queQuan = _view.TXTQueQuan.Text;
                queQuanChanged = false;
            }
            if(danTocChanged)
            {
                vienChuc.idDanToc = Convert.ToInt32(_view.CBXDanToc.EditValue);
                danTocChanged = false;
            }
            if(tonGiaoChanged)
            {
                vienChuc.idTonGiao = Convert.ToInt32(_view.CBXTonGiao.EditValue);
                tonGiaoChanged = false;
            }
            if(vanHoaChanged)
            {
                vienChuc.vanHoa = _view.TXTVanHoa.Text;
                vanHoaChanged = false;
            }
            if(hoKhauThuongTruChanged)
            {
                vienChuc.hoKhauThuongTru = _view.TXTHoKhauThuongTru.Text;
                hoKhauThuongTruChanged = false;
            }
            if(noiOHienNayChanged)
            {
                vienChuc.noiOHienNay = _view.TXTNoiOHienNay.Text;
                noiOHienNayChanged = false;
            }
            if(soCMNDChanged)
            {
                vienChuc.soChungMinhNhanDan = _view.TXTSoCMND.Text;
                soCMNDChanged = false;
            }
            if(ghiChuChanged)
            {
                vienChuc.ghiChu = _view.TXTGhiChu.Text;
                ghiChuChanged = false;
            }
            if(picChanged)
            {
                vienChuc.anh = ConvertImageToBinary(_view.PICVienChuc.GetLoadedImageLocation());
                picChanged = false;
            }
            if (ngayTuyenDungChinhThucChanged)
            {
                vienChuc.ngayTuyenDungChinhThuc = DateTimeHelper.ParseDatetimeMatchDatetimeDatabase(_view.DTNgayTuyenDungChinhThuc.Text);
                ngayTuyenDungChinhThucChanged = false;
            }
            if (emailChanged)
            {
                vienChuc.email = _view.TXTEmail.Text;
                emailChanged = false;
            }
            if (gioChuanChanged)
            {
                vienChuc.gioChuan = Convert.ToInt32(_view.TXTGioChuan.EditValue);
                gioChuanChanged = false;
            }
            unitOfWorks.Save();
            MainPresenter.LoadDataToMainGrid();
            MainPresenter.RefreshRightViewThongTinCaNhan();
            MainPresenter.MoveRowManaging(mavienchuc);
            XtraMessageBox.Show("Cập nhật dữ liệu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void LoadForm()
        {
            LoadCbxData();
            string mavienchuc = _view.TXTMaVienChuc.Text;          
            if (mavienchuc != string.Empty)
            {
                UnitOfWorks unitOfWorks = new UnitOfWorks(new QLNSSGU_1Entities());
                int idVienChuc = unitOfWorks.VienChucRepository.GetIdVienChuc(mavienchuc);
                VienChuc vienChuc = unitOfWorks.VienChucRepository.GetById(idVienChuc);
                _view.PICVienChuc.Image = ConvertBinaryToImage(vienChuc.anh);
                _view.RADGioiTinh.SelectedIndex = unitOfWorks.VienChucRepository.ReturnGenderToTabThongTinCaNhan(vienChuc.gioiTinh);
                _view.TXTHo.Text = vienChuc.ho;
                _view.TXTTen.Text = vienChuc.ten;
                _view.DTNgaySinh.EditValue = vienChuc.ngaySinh;
                _view.DTNgayThamGiaCongTac.EditValue = vienChuc.ngayThamGiaCongTac;
                _view.DTNgayVeTruong.EditValue = vienChuc.ngayVeTruong;
                _view.DTNgayVaoNganh.EditValue = vienChuc.ngayVaoNganh;
                _view.TXTSoDienThoai.Text = vienChuc.sDT;
                _view.TXTNoiSinh.Text = vienChuc.noiSinh;
                _view.TXTQueQuan.Text = vienChuc.queQuan;
                _view.TXTHoKhauThuongTru.Text = vienChuc.hoKhauThuongTru;
                _view.TXTNoiOHienNay.Text = vienChuc.noiOHienNay;
                _view.CHKLaDangVien.Checked = (bool)vienChuc.laDangVien;
                _view.DTNgayVaoDang.EditValue = vienChuc.ngayVaoDang;
                _view.TXTVanHoa.Text = vienChuc.vanHoa;
                _view.CBXDanToc.EditValue = vienChuc.idDanToc;
                _view.CBXTonGiao.EditValue = vienChuc.idTonGiao;
                _view.TXTSoCMND.Text = vienChuc.soChungMinhNhanDan;
                _view.DTNgayTuyenDungChinhThuc.EditValue = vienChuc.ngayTuyenDungChinhThuc;
                _view.TXTEmail.Text = vienChuc.email;
                _view.TXTGioChuan.EditValue = vienChuc.gioChuan;
                _view.TXTGhiChu.Text = vienChuc.ghiChu;
            }
            else
            {
                SelectEmptyValueCbx();
            }
        }

        public void Save()
        {
            string mavienchuc = _view.TXTMaVienChuc.Text;
            string ho = _view.TXTHo.Text;
            string ten = _view.TXTTen.Text;
            if(checkAddNew == true)
            {
                if(mavienchuc == string.Empty)
                {
                    _view.TXTMaVienChuc.ErrorText = "Vui lòng nhập Mã Viên Chức.";
                    _view.TXTMaVienChuc.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if (ho == string.Empty)
                {
                    _view.TXTHo.ErrorText = "Vui lòng nhập Họ.";
                    _view.TXTHo.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if (ten == string.Empty)
                {
                    _view.TXTTen.ErrorText = "Vui lòng nhập Tên.";
                    _view.TXTTen.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if(mavienchuc != string.Empty && ho != string.Empty && ten != string.Empty)
                {
                    InsertData();
                    checkAddNew = false;
                }                
            }
            else
            {
                if (mavienchuc == string.Empty)
                {
                    _view.TXTMaVienChuc.ErrorText = "Vui lòng nhập Mã Viên Chức.";
                    _view.TXTMaVienChuc.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if (ho == string.Empty)
                {
                    _view.TXTHo.ErrorText = "Vui lòng nhập Họ.";
                    _view.TXTHo.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if (ten == string.Empty)
                {
                    _view.TXTTen.ErrorText = "Vui lòng nhập Tên.";
                    _view.TXTTen.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if (mavienchuc != string.Empty && ho != string.Empty && ten != string.Empty)
                {
                    UpdateData();
                }
            }
        }
        
        public void SaveAndClose()
        {
            string mavienchuc = _view.TXTMaVienChuc.Text;
            string ho = _view.TXTHo.Text;
            string ten = _view.TXTTen.Text;
            if (checkAddNew == true)
            {
                if (mavienchuc == string.Empty)
                {
                    _view.TXTMaVienChuc.ErrorText = "Vui lòng nhập Mã Viên Chức.";
                    _view.TXTMaVienChuc.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if (ho == string.Empty)
                {
                    _view.TXTHo.ErrorText = "Vui lòng nhập Họ.";
                    _view.TXTHo.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if (ten == string.Empty)
                {
                    _view.TXTTen.ErrorText = "Vui lòng nhập Tên.";
                    _view.TXTTen.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if (mavienchuc != string.Empty && ho != string.Empty && ten != string.Empty)
                {
                    InsertData();
                    checkAddNew = false;
                    CreateAndEditPersonInfoPresenter.CloseForm();
                }
            }
            else
            {
                if (mavienchuc == string.Empty)
                {
                    _view.TXTMaVienChuc.ErrorText = "Vui lòng nhập Mã Viên Chức.";
                    _view.TXTMaVienChuc.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if (ho == string.Empty)
                {
                    _view.TXTHo.ErrorText = "Vui lòng nhập Họ.";
                    _view.TXTHo.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if (ten == string.Empty)
                {
                    _view.TXTTen.ErrorText = "Vui lòng nhập Tên.";
                    _view.TXTTen.ErrorIconAlignment = ErrorIconAlignment.MiddleRight;
                }
                if (mavienchuc != string.Empty && ho != string.Empty && ten != string.Empty)
                {
                    UpdateData();
                    CreateAndEditPersonInfoPresenter.CloseForm();
                }
            }
        }

        public void LaDangVienChanged(object sender, EventArgs e)
        {
            laDangVienChanged = true;
            if(_view.CHKLaDangVien.Checked == false)
                _view.DTNgayVaoDang.Text = string.Empty;
        }

        public void GioiTinhChanged(object sender, EventArgs e)
        {
            gioiTinhChanged = true;
        }

        public void HoChanged(object sender, EventArgs e)
        {
            hoChanged = true;
        }

        public void TenChanged(object sender, EventArgs e)
        {
            tenChanged = true;
        }

        public void NgaySinhChanged(object sender, EventArgs e)
        {
            ngaySinhChanged = true;
        }

        public void NgayThamGiaCongTacChanged(object sender, EventArgs e)
        {
            ngayThamGiaCongTacChanged = true;
        }

        public void NgayVaoNganhChanged(object sender, EventArgs e)
        {
            ngayVaoNganhChanged = true;
        }

        public void NgayVeTruongChanged(object sender, EventArgs e)
        {
            ngayVeTruongChanged = true;
        }

        public void SoDienThoaiChanged(object sender, EventArgs e)
        {
            soDienThoaiChanged = true;
        }

        public void NoiSinhChanged(object sender, EventArgs e)
        {
            noiSinhChanged = true;
        }

        public void QueQuanChanged(object sender, EventArgs e)
        {
            queQuanChanged = true;
        }

        public void DanTocChanged(object sender, EventArgs e)
        {
            danTocChanged = true;
        }

        public void TonGiaoChanged(object sender, EventArgs e)
        {
            tonGiaoChanged = true;
        }

        public void HoKhauThuongTruChanged(object sender, EventArgs e)
        {
            hoKhauThuongTruChanged = true;
        }

        public void NoiOHienNay(object sender, EventArgs e)
        {
            noiOHienNayChanged = true;
        }

        public void NgayVaoDangChanged(object sender, EventArgs e)
        {
            ngayVaoDangChanged = true;
        }

        public void VanHoaChanged(object sender, EventArgs e)
        {
            vanHoaChanged = true;
        }

        public void SoCMNDChanged(object sender, EventArgs e)
        {
            soCMNDChanged = true;
        }

        public void GhiChuChanged(object sender, EventArgs e)
        {
            ghiChuChanged = true;
        }

        public void PicChanged(object sender, EventArgs e)
        {
            picChanged = true;
        }

        public void NgayTuyenDungChinhThucChanged(object sender, EventArgs e)
        {
            ngayTuyenDungChinhThucChanged = true;
        }

        public void EmailChanged(object sender, EventArgs e)
        {
            emailChanged = true;
        }

        public void GioChuanChanged(object sender, EventArgs e)
        {
            gioChuanChanged = true;
        }

        public void RotateByLeftMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _view.PICVienChuc.Image != null)
            {
                Image image = _view.PICVienChuc.Image.Clone() as Image;
                image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                _view.PICVienChuc.Image.Dispose();
                _view.PICVienChuc.Image = image;
            }
        }
    }
}
