using System.Collections.Generic;

namespace Net.Bluewalk.LidlPlus.Models
{
    public class ApiList<T>
        where T : class
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalCount { get; set; }
        public List<T> Records { get; set; }

        public ApiList()
        {
            Records = new List<T>();
        }
    }
}