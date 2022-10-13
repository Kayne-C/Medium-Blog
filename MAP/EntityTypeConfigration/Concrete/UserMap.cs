using ENTITIES.Entity.Concrete;
using MAP.EntityTypeConfigration.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAP.EntityTypeConfigration.Concrete
{
    public class UserMap : BaseMap<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Username).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(20).IsRequired();
            builder.Property(x => x.FirstName).HasMaxLength(20).IsRequired();
            builder.Property(x => x.LastName).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(25).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(12).IsRequired();
            builder.Property(x => x.Website).HasMaxLength(25);
            builder.Property(x => x.AboutMe).HasMaxLength(200);
            builder.Property(x => x.Image).IsRequired(false);
            builder.HasData(new User { Username = "bayzin", Password = "1234", Id = 1, FirstName = "Barkin", LastName = "Bayzin", Email = "barkin@gmail.com", Phone =" 523432546" });
            base.Configure(builder);
        }
    }
}
