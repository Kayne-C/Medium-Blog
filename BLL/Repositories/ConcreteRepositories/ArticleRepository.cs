using BLL.Repositories.BaseRepositories;
using DAL.Context;
using ENTITIES.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories.ConcreteRepositories
{
    public class ArticleRepository : BaseRepository<Article>
    {
        public ArticleRepository(MediumDbContext context) : base(context)
        {
        }
    }
}
