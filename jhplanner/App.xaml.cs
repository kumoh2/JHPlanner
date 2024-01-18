using System.Windows;
using jhplanner.Data; // 데이터 컨텍스트의 네임스페이스를 임포트

namespace jhplanner
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 데이터베이스 초기화
            using (var context = new AppDbContext())
            {
                // 데이터베이스가 없으면 생성
                context.Database.EnsureCreated();
            }

            // 기타 애플리케이션 초기화 코드...
        }
    }
}