using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DummyDB_3
{
    public class CreateTable
    {
        public static string FormatToColumnsNames(string[] columnsName, int[] maxLength)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"| {columnsName[0].PadRight(maxLength[0])}");
            sb.Append($"| {columnsName[1].PadRight(maxLength[1])}");
            sb.Append($"| {columnsName[2].PadRight(maxLength[2])}");
            sb.Append($"| {columnsName[3].PadRight(maxLength[3])}|");
            return sb.ToString();

        }
        public static string FillTableData(Book book, List<Reader> readers, List<ReaderBook> readersBooks, int[] maxLength)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"| {book.Author.PadRight(maxLength[0])}");
            sb.Append($"| {book.Title.PadRight(maxLength[1])}");
            bool isBookTaken = false;
            for(int i = 0; i < readersBooks.Count; i++)
            {
                if (readersBooks[i].ReturnTime == DateTime.MinValue && readersBooks[i].BookId == book.Id)
                {
                    isBookTaken = true;
                }
            }
            string name = "";
            string time = "";
            if(isBookTaken)
            {
                for (int i = 0; i < readersBooks.Count; i++)
                {
                    for (int j = 0; j < readers.Count; j++)
                    {
                        if (readersBooks[i].ReaderId == readers[j].Id && readersBooks[i].BookId == book.Id)
                        {
                            time = $"{readersBooks[i].BorrowTime.ToString("d")}";
                            name = readers[j].Name;
                        }
                    }
                }
                sb.Append($"| {name.PadRight(maxLength[2])}");
                sb.Append($"| {time.PadRight(maxLength[3])}|");
            }
            else
            {
                sb.Append($"| {name.PadRight(maxLength[2])}");
                sb.Append($"| {time.PadRight(maxLength[3])}|");
            }
            return sb.ToString();
        }
    }
}
