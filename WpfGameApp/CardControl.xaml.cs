using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfGameApp.Entities;

namespace WpfGameApp
{
    /// <summary>
    /// Логика взаимодействия для CardControl.xaml
    /// </summary>
    public partial class CardControl : UserControl
    {
        /// <summary>
        /// Предназначенна ли данная карточка для шкафа
        /// </summary>
        private bool isRackCard = false;

        /// <summary>
        /// Предназначенна ли данная карточка для шкафа
        /// </summary>
        public bool IsRackCard
        {
            get { return isRackCard; }
            set
            {
                isRackCard = value;

                if (isRackCard)
                    card.Background = Brushes.Aquamarine;
            }
        }

        private double dx;
        private double dy;

        public CardControl()
        {
            InitializeComponent();
        }

        public CardControl(bool isRackCard) : this()
        {
            IsRackCard = isRackCard;
        }

        /// <summary>
        /// Нажатие кнопки мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // Запомнить смещение для последующей перерисовки
            dx = Canvas.GetLeft((UIElement)sender) - e.GetPosition(this).X;
            dy = Canvas.GetTop((UIElement)sender) - e.GetPosition(this).Y;

            // Сохранение данных для перетаскивания
            var data = new DataObject();

            // Сохранение контекста данных
            if (DataContext != null)
            {
                data.SetData("Card", DataContext);
            }

            // Начало операции перетаскивания
            DragDrop.DoDragDrop((DependencyObject)sender, data, DragDropEffects.Copy);
        }

      

        /// <summary>
        /// Обработка операции перетаскивания в соответсвии с типом карточки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Параметры события</param>
        private void card_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.Copy;
            // Перетаскиваемые данные
            IDataObject data = e.Data;
            // Перетаскиваемая сущнось
            object card = data.GetData("Card");

            if (IsRackCard && !(card is Rack))
            {
                e.Effects = DragDropEffects.None;
            }
            else if (!IsRackCard && (card is Rack))
            {
                e.Effects = DragDropEffects.None;
            }
        }

        /// <summary>
        /// Перемещение мыши
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grid_MouseMove(object sender, MouseEventArgs e)
        {
            // Проверка на нажатие левой кнопки мыши
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                double x = e.GetPosition(this).X + dx;
                double y = e.GetPosition(this).Y + dy;
                Canvas.SetLeft((UIElement)sender, x);
                Canvas.SetTop((UIElement)sender, y);
            }
        }

        /// <summary>
        /// Изменение контекста карточки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CardControl_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            switch (e.NewValue.GetType().Name)
            {
                case nameof(Server):
                    Server server = e.NewValue as Server;
                    card.Background = Brushes.LightPink;

                    property1.Text = server.Name;
                    property2.Text = $"{server.CPUs} CPU";
                    property3.Text = $"{server.Size}U";
                    property4.Text = $"{server.Weight} кг";
                    property5.Text = $"{server.Count} шт";
                    property6.Text = $"{server.Price} USD";
                    break;

                case nameof(KvmConsole):
                    KvmConsole kvmConsole = e.NewValue as KvmConsole;
                    card.Background = Brushes.Bisque;

                    property1.Text = kvmConsole.Name;
                    property2.Text = $"{kvmConsole.Weight} кг";
                    property3.Text = $"{kvmConsole.Count} шт";
                    property4.Text = $"{kvmConsole.Price} USD";
                    property5.Text = "";
                    property6.Text = "";
                    break;

                case nameof(NetworkSwitch):
                    NetworkSwitch networkSwitch = e.NewValue as NetworkSwitch;
                    card.Background = Brushes.DarkKhaki;

                    property1.Text = networkSwitch.Name;
                    property2.Text = $"{networkSwitch.Weight} кг";
                    property3.Text = $"{networkSwitch.Count} шт";
                    property4.Text = $"{networkSwitch.Price} USD";
                    property5.Text = "";
                    property6.Text = "";
                    break;

                case nameof(Storage):
                    Storage storage = e.NewValue as Storage;
                    card.Background = Brushes.Tan;

                    property1.Text = storage.Name;
                    property2.Text = $"{storage.Weight} кг";
                    property3.Text = $"{storage.Size} ТБ";
                    property4.Text = $"{storage.Price} USD";
                    property5.Text = "";
                    property6.Text = "";
                    break;

                case nameof(Rack):
                    Rack rack = e.NewValue as Rack;
                    card.Background = Brushes.Aquamarine;

                    property1.Text = rack.Name;
                    property2.Text = $"{rack.Capacity} кг";
                    property3.Text = $"{rack.Count} шт";
                    property4.Text = $"{rack.Price} USD";
                    property5.Text = rack.Vendor?.Name;
                    property6.Text = "";
                    break;

                default:
                    // [!] протоколирование возможной ошибки
                    break;
            }          
        }

        /// <summary>
        /// Входжение перетаскиваемого объекта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void card_DragEnter(object sender, DragEventArgs e)
        {
            border.StrokeDashArray = new DoubleCollection() { 3, 1 };
            border.Stroke = Brushes.LightBlue;
        }

        /// <summary>
        /// Объект утащили прочь
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void card_DragLeave(object sender, DragEventArgs e)
        {
            border.StrokeDashArray = null;
            border.Stroke = Brushes.Navy;
        }
    }
}
