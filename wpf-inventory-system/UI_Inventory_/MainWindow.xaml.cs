using System.Windows;

namespace UI_inventory
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Btnadd_Click(object sender, RoutedEventArgs e)
        {
            string name = Prompt.ShowDialog("상품명을 입력하세요:", "상품 추가");
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
