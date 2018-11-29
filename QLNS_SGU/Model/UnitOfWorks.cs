using Model.Entities;
using Model.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UnitOfWorks
    {
        private QuanTriVienRepository _quanTriVienRepository;
        private ThongTinCaNhanRepository _thongTinCaNhanRepository;
        private BacRepository _bacRepository;
        private HocHamHocViVienChucRepository _hocHamHocViVienChucRepository;
        private ChucVuDonViVienChucRepository _chucVuDonViVienChucRepository;
        private ChucVuRepository _chucVuRepository;
        private ChungChiVienChucRepository _chungChiVienChucRepository;
        private ChuyenNganhRepository _chuyenNganhRepository;
        private DonViRepository _donViRepository;
        private HopDongVienChucRepository _hopDongVienChucRepository;
        private LoaiChucVuRepository _loaiChucVuRepository;
        private LoaiHocHamHocViRepository _loaiHocHamHocViRepository;
        private LoaiChungChiRepository _loaiChungChiRepository;
        private LoaiDonViRepository _loaiDonViRepository;
        private LoaiHopDongRepository _loaiHopDongRepository;
        private LoaiNganhRepository _loaiNganhRepository;
        private NgachRepository _ngachRepository;
        private NganhDaoTaoRepository _nganhDaoTaoRepository;
        private NganhVienChucRepository _nganhVienChucRepository;
        private QuaTrinhLuongRepository _quaTrinhLuongRepository;
        private ToChuyenMonRepository _toChuyenMonRepository;
        private TrangThaiRepository _trangThaiRepository;
        private TrangThaiVienChucRepository _trangThaiVienChucRepository;
        private VienChucRepository _vienChucRepository;
        private GoogleDriveFileRepository _googleDriveFileRepository;
        private DangHocNangCaoRepository _dangHocNangCaoRepository;
        private GridViewMainDataRepository _gridViewDataRepository;
        private DanTocRepository _danTocRepository;
        private TonGiaoRepository _tonGiaoRepository;       
        private QuaTrinhPhuCapThamNienNhaGiaoRepository _quaTrinhPhuCapThamNienNhaGiaoRepository;
        private BaoHiemXaHoiRepository _baoHiemXaHoiRepository;
        private QuaTrinhGianDoanBaoHiemXaHoiRepository _quaTrinhGianDoanBaoHiemXaHoiRepository;
        private QuaTrinhDanhGiaVienChucRepository _quaTrinhDanhGiaVienChucRepository;
        private DanhMucThoiGianRepository _danhMucThoiGianRepository;
        private MucDoDanhGiaRepository _mucDoDanhGiaRepository;
        private HardDriveFileRepository _hardDriveFileRepository;

        private QLNSSGU_1Entities _db;
        public UnitOfWorks(QLNSSGU_1Entities db)
        {
            _db = db;
        }

        public HardDriveFileRepository HardDriveFileRepository
        {
            get
            {
                if (_hardDriveFileRepository == null)
                {
                    _hardDriveFileRepository = new HardDriveFileRepository(_db);
                }
                return _hardDriveFileRepository;
            }
        }

        public MucDoDanhGiaRepository MucDoDanhGiaRepository
        {
            get
            {
                if (_mucDoDanhGiaRepository == null)
                {
                    _mucDoDanhGiaRepository = new MucDoDanhGiaRepository(_db);
                }
                return _mucDoDanhGiaRepository;
            }
        }

        public DanhMucThoiGianRepository DanhMucThoiGianRepository
        {
            get
            {
                if (_danhMucThoiGianRepository == null)
                {
                    _danhMucThoiGianRepository = new DanhMucThoiGianRepository(_db);
                }
                return _danhMucThoiGianRepository;
            }
        }

        public QuaTrinhDanhGiaVienChucRepository QuaTrinhDanhGiaVienChucRepository
        {
            get
            {
                if (_quaTrinhDanhGiaVienChucRepository == null)
                {
                    _quaTrinhDanhGiaVienChucRepository = new QuaTrinhDanhGiaVienChucRepository(_db);
                }
                return _quaTrinhDanhGiaVienChucRepository;
            }
        }

        public QuaTrinhGianDoanBaoHiemXaHoiRepository QuaTrinhGianDoanBaoHiemXaHoiRepository
        {
            get
            {
                if (_quaTrinhGianDoanBaoHiemXaHoiRepository == null)
                {
                    _quaTrinhGianDoanBaoHiemXaHoiRepository = new QuaTrinhGianDoanBaoHiemXaHoiRepository(_db);
                }
                return _quaTrinhGianDoanBaoHiemXaHoiRepository;
            }
        }

        public BaoHiemXaHoiRepository BaoHiemXaHoiRepository
        {
            get
            {
                if (_baoHiemXaHoiRepository == null)
                {
                    _baoHiemXaHoiRepository = new BaoHiemXaHoiRepository(_db);
                }
                return _baoHiemXaHoiRepository;
            }
        }

        public QuaTrinhPhuCapThamNienNhaGiaoRepository QuaTrinhPhuCapThamNienNhaGiaoRepository
        {
            get
            {
                if (_quaTrinhPhuCapThamNienNhaGiaoRepository == null)
                {
                    _quaTrinhPhuCapThamNienNhaGiaoRepository = new QuaTrinhPhuCapThamNienNhaGiaoRepository(_db);
                }
                return _quaTrinhPhuCapThamNienNhaGiaoRepository;
            }
        }

        public TonGiaoRepository TonGiaoRepository
        {
            get
            {
                if (_tonGiaoRepository == null)
                {
                    _tonGiaoRepository = new TonGiaoRepository(_db);
                }
                return _tonGiaoRepository;
            }
        }

        public DanTocRepository DanTocRepository
        {
            get
            {
                if (_danTocRepository == null)
                {
                    _danTocRepository = new DanTocRepository(_db);
                }
                return _danTocRepository;
            }
        }

        public GridViewMainDataRepository GridViewDataRepository
        {
            get
            {
                if (_gridViewDataRepository == null)
                {
                    _gridViewDataRepository = new GridViewMainDataRepository(_db);
                }
                return _gridViewDataRepository;
            }
        }

        public DangHocNangCaoRepository DangHocNangCaoRepository
        {
            get
            {
                if (_dangHocNangCaoRepository == null)
                {
                    _dangHocNangCaoRepository = new DangHocNangCaoRepository(_db);
                }
                return _dangHocNangCaoRepository;
            }
        }

        public GoogleDriveFileRepository GoogleDriveFileRepository
        {
            get
            {
                if (_googleDriveFileRepository == null)
                {
                    _googleDriveFileRepository = new GoogleDriveFileRepository(_db);
                }
                return _googleDriveFileRepository;
            }
        }

        public QuanTriVienRepository QuanTriVienRepository
        {
            get
            {
                if (_quanTriVienRepository == null)
                {
                    _quanTriVienRepository = new QuanTriVienRepository(_db);
                }
                return _quanTriVienRepository;
            }
        }

        public ThongTinCaNhanRepository ThongTinCaNhanRepository
        {
            get
            {
                if (_thongTinCaNhanRepository == null)
                {
                    _thongTinCaNhanRepository = new ThongTinCaNhanRepository(_db);
                }
                return _thongTinCaNhanRepository;
            }
        }

        public LoaiChucVuRepository LoaiChucVuRepository
        {
            get
            {
                if (_loaiChucVuRepository == null)
                {
                    _loaiChucVuRepository = new LoaiChucVuRepository(_db);
                }
                return _loaiChucVuRepository;
            }
        }

        public BacRepository BacRepository
        {
            get
            {
                if (_bacRepository == null)
                {
                    _bacRepository = new BacRepository(_db);
                }
                return _bacRepository;
            }
        }

        public HocHamHocViVienChucRepository HocHamHocViVienChucRepository
        {
            get
            {
                if (_hocHamHocViVienChucRepository == null)
                {
                    _hocHamHocViVienChucRepository = new HocHamHocViVienChucRepository(_db);
                }
                return _hocHamHocViVienChucRepository;
            }
        }

        public ChucVuDonViVienChucRepository ChucVuDonViVienChucRepository
        {
            get
            {
                if (_chucVuDonViVienChucRepository == null)
                {
                    _chucVuDonViVienChucRepository = new ChucVuDonViVienChucRepository(_db);
                }
                return _chucVuDonViVienChucRepository;
            }
        }

        public ChucVuRepository ChucVuRepository
        {
            get
            {
                if (_chucVuRepository == null)
                {
                    _chucVuRepository = new ChucVuRepository(_db);
                }
                return _chucVuRepository;
            }
        }

        public ChungChiVienChucRepository ChungChiVienChucRepository
        {
            get
            {
                if (_chungChiVienChucRepository == null)
                {
                    _chungChiVienChucRepository = new ChungChiVienChucRepository(_db);
                }
                return _chungChiVienChucRepository;
            }
        }

        public ChuyenNganhRepository ChuyenNganhRepository
        {
            get
            {
                if (_chuyenNganhRepository == null)
                {
                    _chuyenNganhRepository = new ChuyenNganhRepository(_db);
                }
                return _chuyenNganhRepository;
            }
        }

        public DonViRepository DonViRepository
        {
            get
            {
                if (_donViRepository == null)
                {
                    _donViRepository = new DonViRepository(_db);
                }
                return _donViRepository;
            }
        }

        public HopDongVienChucRepository HopDongVienChucRepository
        {
            get
            {
                if (_hopDongVienChucRepository == null)
                {
                    _hopDongVienChucRepository = new HopDongVienChucRepository(_db);
                }
                return _hopDongVienChucRepository;
            }
        }

        public LoaiHocHamHocViRepository LoaiHocHamHocViRepository
        {
            get
            {
                if (_loaiHocHamHocViRepository == null)
                {
                    _loaiHocHamHocViRepository = new LoaiHocHamHocViRepository(_db);
                }
                return _loaiHocHamHocViRepository;
            }
        }

        public LoaiChungChiRepository LoaiChungChiRepository
        {
            get
            {
                if (_loaiChungChiRepository == null)
                {
                    _loaiChungChiRepository = new LoaiChungChiRepository(_db);
                }
                return _loaiChungChiRepository;
            }
        }

        public LoaiDonViRepository LoaiDonViRepository
        {
            get
            {
                if (_loaiDonViRepository == null)
                {
                    _loaiDonViRepository = new LoaiDonViRepository(_db);
                }
                return _loaiDonViRepository;
            }
        }

        public LoaiHopDongRepository LoaiHopDongRepository
        {
            get
            {
                if (_loaiHopDongRepository == null)
                {
                    _loaiHopDongRepository = new LoaiHopDongRepository(_db);
                }
                return _loaiHopDongRepository;
            }
        }

        public LoaiNganhRepository LoaiNganhRepository
        {
            get
            {
                if (_loaiNganhRepository == null)
                {
                    _loaiNganhRepository = new LoaiNganhRepository(_db);
                }
                return _loaiNganhRepository;
            }
        }

        public NgachRepository NgachRepository
        {
            get
            {
                if (_ngachRepository == null)
                {
                    _ngachRepository = new NgachRepository(_db);
                }
                return _ngachRepository;
            }
        }

        public NganhDaoTaoRepository NganhDaoTaoRepository
        {
            get
            {
                if (_nganhDaoTaoRepository == null)
                {
                    _nganhDaoTaoRepository = new NganhDaoTaoRepository(_db);
                }
                return _nganhDaoTaoRepository;
            }
        }

        public NganhVienChucRepository NganhVienChucRepository
        {
            get
            {
                if (_nganhVienChucRepository == null)
                {
                    _nganhVienChucRepository = new NganhVienChucRepository(_db);
                }
                return _nganhVienChucRepository;
            }
        }

        public QuaTrinhLuongRepository QuaTrinhLuongRepository
        {
            get
            {
                if (_quaTrinhLuongRepository == null)
                {
                    _quaTrinhLuongRepository = new QuaTrinhLuongRepository(_db);
                }
                return _quaTrinhLuongRepository;
            }
        }

        public ToChuyenMonRepository ToChuyenMonRepository
        {
            get
            {
                if (_toChuyenMonRepository == null)
                {
                    _toChuyenMonRepository = new ToChuyenMonRepository(_db);
                }
                return _toChuyenMonRepository;
            }
        }

        public TrangThaiRepository TrangThaiRepository
        {
            get
            {
                if (_trangThaiRepository == null)
                {
                    _trangThaiRepository = new TrangThaiRepository(_db);
                }
                return _trangThaiRepository;
            }
        }

        public TrangThaiVienChucRepository TrangThaiVienChucRepository
        {
            get
            {
                if (_trangThaiVienChucRepository == null)
                {
                    _trangThaiVienChucRepository = new TrangThaiVienChucRepository(_db);
                }
                return _trangThaiVienChucRepository;
            }
        }

        public VienChucRepository VienChucRepository
        {
            get
            {
                if (_vienChucRepository == null)
                {
                    _vienChucRepository = new VienChucRepository(_db);
                }
                return _vienChucRepository;
            }
        }

        public void Save()
        {
            if (_db != null)
            {
                using (var dbTrans = _db.Database.BeginTransaction())
                {
                    try
                    {
                        int rows = _db.SaveChanges();
                        dbTrans.Commit();
                    }
                    catch (Exception ex)
                    {
                        //Debug.WriteLine(ex);
                        dbTrans.Rollback();
                    }
                }
            }
            else return;
        }
    }
}
