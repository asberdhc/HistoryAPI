using HistoryAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HistoryAPI.Controllers
{
    public class HistoryController : ApiController
    {
        YouTubeEntities _dbContext = new YouTubeEntities();

        //GET api/history
        [HttpGet]
        public List<VideoWatchedDto> GetVideosWatched(int amount = 10)
        {
            var lastVIdeosWatched = _dbContext.History
                                      .Where(h => h.IsEnabled == true)
                                      .OrderByDescending(v => v.Id)
                                      .Take(amount)
                                      .Select(h => new VideoWatchedDto {
                                          Id = h.Id,
                                          VideoId = h.VideoId,
                                          IsEnabled = h.IsEnabled,
                                          Date = h.Date
                                      });
            return lastVIdeosWatched.ToList();
        }

        //POST api/history
        [HttpPost]
        public VideoWatchedDto AddToHistory(string videoId)
        {
            History video = new History();
            video.VideoId = videoId;
            video.IsEnabled = true;
            video.Date = DateTime.Now;
            _dbContext.History.Add(video);
            _dbContext.SaveChanges();
            var videoGuardado = _dbContext.History.Where(x => x.VideoId == videoId).Select(x => new VideoWatchedDto
            {
                Id = x.Id,
                VideoId = x.VideoId,
                IsEnabled = x.IsEnabled,
                Date = x.Date
            }).ToList();
            var v = videoGuardado[0];
            return v;
        }

        //DELETE api/history/{id}
        [HttpDelete]
        public void DeleteFromHistory(int id)
        {
            var itemToRemove = _dbContext.History.FirstOrDefault(h => h.Id == id);
            if (itemToRemove != null)
                itemToRemove.IsEnabled = false;
            _dbContext.SaveChanges();
        }
    }
}
