using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Repository
{
    public class QuaTrinhPhuCapThamNienNhaGiaoRepository : Repository<QuaTrinhPhuCapThamNienNhaGiao>
    {
        public QuaTrinhPhuCapThamNienNhaGiaoRepository(QLNSSGU_1Entities db) : base(db)
        {
        }

        public List<QuaTrinhPhuCapThamNienNhaGiao> GetListQuaTrinhPhuCap(string mavienchuc)
        {
            VienChucRepository vienChucRepository = new VienChucRepository(_db);
            int idvienchuc = vienChucRepository.GetIdVienChuc(mavienchuc);
            var anonymList = _db.QuaTrinhPhuCapThamNienNhaGiaos.Where(x => x.idVienChuc == idvienchuc).Select(y => new
            { y.idQuaTrinhPhuCap, y.idVienChuc, y.heSoPhuCap, y.ngayBatDau, y.ngayNangPhuCap, y.linkVanBanDinhKem }).ToList();
            List<QuaTrinhPhuCapThamNienNhaGiao> listQuaTrinh = new List<QuaTrinhPhuCapThamNienNhaGiao>();
            for(int i = anonymList.Count - 1; i >= 0; i--)
            {
                listQuaTrinh.Add(new QuaTrinhPhuCapThamNienNhaGiao
                {
                    idQuaTrinhPhuCap = anonymList[i].idQuaTrinhPhuCap,
                    idVienChuc = anonymList[i].idVienChuc,
                    heSoPhuCap = anonymList[i].heSoPhuCap,
                    ngayBatDau = anonymList[i].ngayBatDau,
                    ngayNangPhuCap = anonymList[i].ngayNangPhuCap,
                    linkVanBanDinhKem = anonymList[i].linkVanBanDinhKem
                });
            }
            return listQuaTrinh;
        }

        public QuaTrinhPhuCapThamNienNhaGiao GetObjectById(int idquatrinhphucap)
        {
            return _db.QuaTrinhPhuCapThamNienNhaGiaos.Where(x => x.idQuaTrinhPhuCap == idquatrinhphucap).FirstOrDefault();
        }

        public List<string> GetListLinkVanBanDinhKem(string maVienChucForGetListLinkVanBanDinhKem)
        {
            return _db.QuaTrinhPhuCapThamNienNhaGiaos.Where(x => x.VienChuc.maVienChuc == maVienChucForGetListLinkVanBanDinhKem).Select(y => y.linkVanBanDinhKem).ToList();
        }

        public void DeleteById(int id)
        {
            var a = _db.QuaTrinhPhuCapThamNienNhaGiaos.Where(x => x.idQuaTrinhPhuCap == id).FirstOrDefault();
            _db.QuaTrinhPhuCapThamNienNhaGiaos.Remove(a);
        }
    }
}
