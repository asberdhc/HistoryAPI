using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoryAPI.Models
{
    public class VideoWatchedDto
    {
        public int Id { get; set; }
        public string VideoId { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime Date { get; set; }
    }
}