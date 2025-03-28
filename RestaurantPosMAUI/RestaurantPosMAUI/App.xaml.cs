using RestaurantPosMAUI.Data;
using System.Threading.Tasks;

namespace RestaurantPosMAUI
{
    public partial class App : Application
    {
        private readonly DatabaseService _databaseService;
        public App(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            Task.Run(async () => await databaseService.InitializeDatabaseAsync());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}