using Contracts;
using System.Collections.Generic;

namespace FileWriter
{
    public abstract class FileWriter<T> where T : PersistableModel<T>
    {
        private string Header { get; set; }
        public FileWriter(string header)
        {
            Header = header;
        }
        public string MakeOutPut(DownloadFileTypes fileType, IEnumerable<T> input)
        {
            var res = Header;
            foreach (var item in input)
            {
                res += FormatRow(fileType, item);
            }
            return res;
        }

        private string FormatRow(DownloadFileTypes fileType, T item)
        {
            return $"{FileContentConsts.NewLine}{item.Flattern(fileType)}{FileContentConsts.ColumnDelimeter}";
        }

        public string MakeOutPut(DownloadFileTypes fileType, params T[] input)
        {
            var res = Header;
            foreach (var item in input)
            {
                res += FormatRow(fileType, item);
            }
            return res;
        }
    }
}