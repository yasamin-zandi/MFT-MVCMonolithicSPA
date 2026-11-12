using Microsoft.EntityFrameworkCore;
using MonolithicSPA.Models.DomainModels.ProductType;
using MonolithicSPA.Models;

namespace MonolithicSPA.Models.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ProjectDbContext context)
        {
            // ۱. اگه دیتابیس یا جدول‌ها وجود ندارن مطمئن می‌شیم ساخته بشن
            context.Database.Migrate();

            // ۲. چک می‌کنیم آیا قبلاً دسته‌بندی ثبت شده یا نه؟
            // اگر قبلاً دیتایی وجود داشته باشه، دوباره اد نمی‌کنه تا تکراری نشه!
            if (context.Category.Any())
            {
                return; // دیتابیس قبلاً بذرپاشی شده، برگرد
            }

            // ۳. مقادیر اولیه دسته‌بندی‌ها
            var categories = new Category[]
            {
                new Category { Title = "کالای دیجیتال " },
                new Category { Title = "لوازم خانگی" },
                new Category { Title = "پوشاک و مد" },
                new Category { Title = "کتاب و لوازم‌التحریر" },
                new Category { Title = "ورزش و سفر" }
            };

            // اضافه کردن به جدول و ذخیره در دیتابیس
            context.Category.AddRange(categories);
            context.SaveChanges();
        }
    }
}
