using FootballApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApp.Domain.Abstract
{
    public interface INewsRepository
    {
        IEnumerable<News> GetNews();
        void Create(string Title, string Text);
        News GetNewsById(int id);
        void Edit(int Id, string Title, string Text);
        IEnumerable<News> GetLastFive();
    }
}
