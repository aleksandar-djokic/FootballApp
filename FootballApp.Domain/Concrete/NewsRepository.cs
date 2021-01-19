using FootballApp.Domain.Abstract;
using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Concrete
{
    public class NewsRepository:INewsRepository
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        public void Create(string Title,string Text)
        {
            var News = new News { Title = Title, Text = Text, Time = DateTime.Now };
            context.News.Add(News);
            context.SaveChanges();
        }

        public void Edit(int Id, string Title, string Text)
        {
            var news = context.News.FirstOrDefault(x => x.Id == Id);
            if (news != null)
            {
                news.Title = Title;
                news.Text = Text;
                news.Time = DateTime.Now;
                context.SaveChanges();
            }
        }

        public IEnumerable<News> GetNews()
        {
            return context.News;
        }

        public News GetNewsById(int id)
        {
            var result = context.News.FirstOrDefault(x => x.Id == id);
            return result;
        }
    }
}
