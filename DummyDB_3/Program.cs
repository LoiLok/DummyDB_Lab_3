using DummyDB_3;
using System;
using System.Text;
using System.Reflection;
using System.Globalization;

namespace DummyDB_3
{
    class Program
    {
        static void Main()
        {
            ShowBooks();
        }
        static void ShowBooks()
        {
            List<Book> books = FillArrayBooks();
            List<Reader> readers = FillArrayReaders();
            List<ReaderBook> readersBooks = FillArrayReadersBooks();
            string[] columnsName = new string[] { "Автор", "Название", "Читает", "Взял" };
            int[] maxLength = GetMaxLengthColumns(books,readers,readersBooks);
            int lengthOfTable = maxLength.Sum() + columnsName.Count()*2 + 1;
            Console.WriteLine(CreateTable.FormatToColumnsNames(columnsName, maxLength));
            Console.WriteLine(new String('-', lengthOfTable));
            foreach (Book book in books)
            {
                Console.WriteLine(CreateTable.FillTableData(book, readers, readersBooks, maxLength));
            }
        }
        private static int[] GetMaxLengthColumns(List<Book> books, List<Reader> readers, List<ReaderBook> readersBooks)
        {
            int maxLengthAuthor = 0;
            int maxLengthTitle = 0;
            for(int i = 0; i < books.Count; i++)
            {
                if (books[i].Author.Length > maxLengthAuthor)
                {
                    maxLengthAuthor = books[i].Author.Length;
                }
                if (books[i].Title.Length > maxLengthTitle)
                {
                    maxLengthTitle = books[i].Title.Length;
                }
            }
            int maxLengthBorrowTime = 0;
            int maxLengthReader = 0;
            for(int i = 0; i < readersBooks.Count; i++)
            {
                if (readersBooks[i].ReturnTime == DateTime.MinValue)
                {
                    for(int j = 0; j < readers.Count; j++)
                    {
                        if (readersBooks[i].ReaderId == readers[j].Id && readers[j].Name.Length > maxLengthReader)
                        {
                            maxLengthReader = readers[j].Name.Length;
                        }
                    }
                    if (readersBooks[i].BorrowTime.ToString().Length > maxLengthBorrowTime)
                    {
                        maxLengthBorrowTime = $"{readersBooks[i].BorrowTime.ToString("d")}".Length;
                    }
                }
            }
            int[] maxLengthColumns = new int[] { maxLengthAuthor, maxLengthTitle, maxLengthReader, maxLengthBorrowTime };
            return maxLengthColumns;
        }
        private static List<Book> FillArrayBooks()
        {
            string[] booksData = File.ReadAllLines("..\\..\\..\\Data\\Book.csv");
            List<Book> books = new List<Book>();
            for (int i = 1; i < booksData.Length; i++)
            {
                books.Add(CsvParser.ParseBook(booksData[i]));
            }
            return books;
        }
        private static List<Reader> FillArrayReaders()
        {
            string[] readersData = File.ReadAllLines("..\\..\\..\\Data\\Reader.csv");
            List<Reader> readers = new List<Reader>();
            for (int i = 1; i < readersData.Length; i++)
            {
                readers.Add(CsvParser.ParseReader(readersData[i]));
            }
            return readers;
        }
        private static List<ReaderBook> FillArrayReadersBooks()
        {
            string[] readersBooksData = File.ReadAllLines("..\\..\\..\\Data\\ReadersBook.csv");
            List<ReaderBook> readersBooks = new List<ReaderBook>();
            for (int i = 1; i < readersBooksData.Length; i++)
            {
                readersBooks.Add(CsvParser.ParseReadersBooks(readersBooksData[i]));
            }
            return readersBooks;
        }
    }
}