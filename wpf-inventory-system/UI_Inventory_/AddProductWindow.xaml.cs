using System.Windows;
using Microsoft.EntityFrameworkCore;
using wpf_inventory_system;

namespace UI_inventory
{
    /// <summary>
    /// AddProductWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public AddProductWindow()
        {
            InitializeComponent();
        }

        private void BtnCheck_Click(object sender, RoutedEventArgs e)
        {
            string name = TxtName.Text;
            if (int.TryParse(TxtPrice.Text, out int price) &&
                int.TryParse(Txtinven.Text, out int inventory))
            {
                var product = new Product
                {
                    ProductName = name,
                    ProductPrice = price,
                    ProductInventory = inventory
                };
                using var context = new ApplicationDbContext();
                context.Products.Add(product);
                context.SaveChanges();

                MessageBox.Show("DB 저장 성공!");

                this.Close();

            }
            else
            {
                MessageBox.Show("숫자를 올바르게 입력하세요!");
                return;
            }

        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
