using jhplanner.Data;
using jhplanner.Views;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;


namespace jhplanner
{
    public partial class App : Application
    {
        public static MainWindow MainWindowInstance { get; set; }
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

            if (MainWindowInstance == null)
            {
                MainWindowInstance = new MainWindow();
            }
            //MainWindowInstance.AppWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            MainWindowInstance.Activate();
        }

        public static void CloseMainWindow()
        {
            MainWindowInstance?.Close();
            MainWindowInstance = null;
        }
    }
}
