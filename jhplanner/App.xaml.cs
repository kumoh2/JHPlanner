using jhplanner.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows;

namespace jhplanner
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            using (var context = new AppDbContext())
            {
                // 데이터베이스 마이그레이션 적용
                context.Database.Migrate();
            }

            // 나머지 초기화 코드...
        }
    }
}