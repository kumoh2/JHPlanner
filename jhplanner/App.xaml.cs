using jhplanner.Data;
using Microsoft.UI.Xaml;


namespace jhplanner
{
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }
        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            base.OnLaunched(args);

            // 데이터베이스 초기화
            using (var context = new AppDbContext())
            {
                // 데이터베이스가 없으면 생성
                context.Database.EnsureCreated();
            }
            m_window = new Views.MainWindow();
            m_window.Activate();
        }

        private Window m_window;
    }
}
