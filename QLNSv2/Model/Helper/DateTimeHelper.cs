using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Model.Helper
{
    public class DateTimeHelper
    {
        public static DateTime? ParseDatetimeMatchDatetimeDatabase(string date)
        {
            if (date != string.Empty)
            {
                if(date.Contains(' '))
                    date = date.Split(' ')[0];
                if (date.Contains('.'))
                    date = date.Replace('.', '/');
                bool matchNgayThangNam = Regex.IsMatch(date, @"^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$");
                bool matchThangNam = Regex.IsMatch(date, @"(0[1-9]|10|11|12)/20[0-9]{2}$");
                bool matchNam = Regex.IsMatch(date, @"^(181[2-9]|18[2-9]\d|19\d\d|2\d{3}|30[0-3]\d|304[0-8])$");
                if (date.Length <= 10 && matchNgayThangNam)
                {
                    string[] arrDateDayCheck = date.Split('/'); //TH1
                    if (arrDateDayCheck[0].Length == 1)
                        date = "0" + date;

                    string[] arrDateMonthCheck = date.Split('/'); //TH2, vd: 6/1/2012 -> qa TH1 sẽ là 06/1/2012, nhưng arr[0] vẫn là 6
                    if (arrDateMonthCheck[1].Length == 1)         // => phải tạo lại arrDateMonthCheck để arr[0] update lại là 06
                        date = arrDateMonthCheck[0] + '/' + "0" + arrDateMonthCheck[1] + '/' + arrDateMonthCheck[2];
                    return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                if (date.Length <= 7 && matchThangNam)
                {
                    date = "01/" + date;
                    string[] arrDate = date.Split('/');
                    if (arrDate[1].Length == 1)
                        date = arrDate[0] + '/' + "0" + arrDate[1] + '/' + arrDate[2];
                    return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                if (date.Length <= 4 && matchNam)
                {
                    date = "31/12/" + date;
                    return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                return null;
            }
            return null;
        }
    }
}
