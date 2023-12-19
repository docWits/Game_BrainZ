using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace WpfGameApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Количество ячеек шкафа на окне
        /// </summary>
        private const int cellsInRack = 8;

        /// <summary>
        /// Высота ячейки
        /// </summary>
        private const double height = 70;

        /// <summary>
        /// Поле между ячейками
        /// </summary>
        private const double spanY = 20;

        /// <summary>
        /// Список ячеек шкафа
        /// </summary>
        public static readonly List<CardControl> cells;

        /// <summary>
        /// Сохраняемое состояние игры
        /// </summary>
        private Entities.GameState state;

        static MainWindow()
        {
            cells = new List<CardControl>();
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Загрузка окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CreateShelf();
            CreateCards();

            // Считывание имени файла сохранения из реестра
            RegistryKey currentUser = Registry.CurrentUser;
            RegistryKey applicationKey = currentUser?.OpenSubKey("CreateYourDC");
            string fileName = applicationKey?.GetValue("SaveFileName")?.ToString();

            if (fileName != null)
            {
                // [!] контроль существования файла
                state = Entities.GameState.Load(fileName);
                SetDataContextToCells();
            }
        }

        /// <summary>
        /// Установка контекста ячейкам в шкафу
        /// </summary>
        private void SetDataContextToCells()
        {
            for (int i = 0; i < state.Entities.Count; i++)
            {
                cells[i].DataContext = state.Entities[i];
            }
        }

        /// <summary>
        /// Создание ячеек для шкафа
        /// </summary>
        private void CreateShelf()
        {
            // Формирование шкафа
            for (int i = 0; i < cellsInRack; i++)
            {
                var item = new CardControl()
                {
                    AllowDrop = true
                };
                item.Drop += card_Drop;
                Canvas.SetLeft(item, 30);
                Canvas.SetTop(item, 30 + i * (height + spanY));

                // Сохраним ячейку шкафа в списке
                cells.Add(item);

                canvas.Children.Add(item);
            }

            cells[cells.Count - 1].IsRackCard = true;
        }

        /// <summary>
        /// Создание начальных карточек
        /// </summary>
        private void CreateCards()
        {
            //var db = new Database();

            // Соединение с новой БД 
            var ef = new DB();
            // Запрос списка шкафов
            var list = ef.Racks.ToList();

            int y = 80;
            foreach (var server in ef.Servers.ToList())
            {
                var serverCard = new CardControl()
                {
                    DataContext = server
                };
                Canvas.SetLeft(serverCard, 300);
                Canvas.SetTop(serverCard, y);
                canvas.Children.Add(serverCard);
                y += 100;
            }

            y = 80;
            foreach (var rack in ef.Racks.ToList())
            {
                var rackCard = new CardControl(isRackCard: true)
                {
                    DataContext = rack
                };
                Canvas.SetLeft(rackCard, 550);
                Canvas.SetTop(rackCard, y);
                canvas.Children.Add(rackCard);
                y += 100;
            }

            y = 80;
            foreach (var storage in ef.Storages.ToList())
            {
                var storageCard = new CardControl(isRackCard: true)
                {
                    DataContext = storage
                };
                Canvas.SetLeft(storageCard, 800);
                Canvas.SetTop(storageCard, y);
                canvas.Children.Add(storageCard);
                y += 100;
            }

            y = 80;
            foreach (var kvm in ef.Kvms.ToList())
            {
                var kvmCard = new CardControl(isRackCard: true)
                {
                    DataContext = kvm
                };
                Canvas.SetLeft(kvmCard, 1050);
                Canvas.SetTop(kvmCard, y);
                canvas.Children.Add(kvmCard);
                y += 100;
            }

            y = 80;
            foreach (var networkSwitch in ef.Switches.ToList())
            {
                var switchCard = new CardControl(isRackCard: true)
                {
                    DataContext = networkSwitch
                };
                Canvas.SetLeft(switchCard, 1300);
                Canvas.SetTop(switchCard, y);
                canvas.Children.Add(switchCard);
                y += 100;
            }
        }

        /// <summary>
        /// Завершение перетаскивания
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void card_Drop(object sender, DragEventArgs e)
        {
            // Принятые данные
            IDataObject data = e.Data;

            // Карточка, на которую мы "уронили
            CardControl card = (CardControl)sender;

            // Смена контекста
            card.DataContext = data.GetData("Card");

            // Обновление сводной таблицы
            List<CalcRow> list = new List<CalcRow>();

            Entities.Rack rack = null;
            foreach (CardControl cell in cells) // все ячейки шкафа
            {
                if (cell.DataContext != null) // только заполненные ячейки
                {
                    if (cell.DataContext is Entities.Equipment equipment)
                    {
                        var row = new CalcRow()
                        {
                            Weight = equipment.Weight
                        };
                        list.Add(row);
                    }
                    else if (cell.DataContext is Entities.Rack)
                    {
                        rack = (Entities.Rack)cell.DataContext;
                    }
                }
            }
            int total = list.Sum(x => x.Weight);
            list.Add(new CalcRow()
            {
                Name = "Итого",
                Weight = total
            });

            // Если в конструкции есть шкаф
            if (rack != null)
            {
                list.Add(new CalcRow()
                {
                    Name = "Шкаф",
                    Weight = rack.Capacity
                });
                list.Add(new CalcRow()
                {
                    Name = "Запас",
                    Weight = rack.Capacity - total
                });
            }

            calc.ItemsSource = list;
        }

        /// <summary>
        /// Выход из приложения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Сохранение состояния игры в файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Диалог для выбора имени файла
                var dialog = new System.Windows.Forms.SaveFileDialog()
                {
                    Filter = "Файлы (*.xml)|*.xml|Файлы (*.json)|*.json|Все файлы (*.*)|*.*"
                };

                // Выбор файла для сохранения
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    state = new Entities.GameState();
                    foreach (CardControl cell in cells)
                    {
                        if (cell.DataContext != null)
                        {
                            var entity = cell.DataContext as Entities.Costable;
                            state.Entities.Add(entity);
                            // Расчет стоимости
                            state.Price += entity.Price;
                            // Расчет нагрузки
                            if (entity is Entities.Rack)
                            {
                                state.Capacity += (entity as Entities.Rack).Capacity;
                            }
                            else if (entity is Entities.Equipment)
                            {
                                state.Capacity -= (entity as Entities.Equipment).Weight;
                            }
                            else
                            {
                                // [!] такого быть не должно
                                throw new Exception("Непонятное оборудование в стойке");
                            }
                        }
                        else
                        {
                            state.Entities.Add(new Entities.Server());
                        }
                    }
                    // Отображение результатов в интерфейсе
                    price.Text = state.Price.ToString();
                    capacity.Text = state.Capacity.ToString();

                    switch (dialog.FilterIndex)
                    {
                        case 1:
                            // Сериализация в XML
                            state.Save(dialog.FileName);
                            break;

                        case 2:
                            // Сериализация в JSON
                            string json = System.Text.Json.JsonSerializer.Serialize(state);
                            System.IO.File.WriteAllText(dialog.FileName, json);
                            break;

                        default:
                            // сообщение
                            break;
                    }

                    //Запись имени файла сохранения в реестр
                    RegistryKey currentUserKey = Registry.CurrentUser;
                    RegistryKey applicationKey = currentUserKey.CreateSubKey("CreateYourDC");
                    applicationKey.SetValue("SaveFileName", dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                string message = string.Empty;
                do
                {
                    message += ex.Message + Environment.NewLine;
                    ex = ex.InnerException;
                }
                while (ex != null);
                System.Windows.Forms.MessageBox.Show(message);
            }
        }

        /// <summary>
        /// Загрузка игры из XML-файла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void load_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Диалог для выбора имени файла
                var dialog = new System.Windows.Forms.OpenFileDialog()
                {
                    Filter = "Файлы (*.xml)|*.xml|Все файлы (*.*)|*.*"
                };
                // Выбор файла для загрузки
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    state = Entities.GameState.Load(dialog.FileName);
                    SetDataContextToCells();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new Services.WcfServiceClient();
                string name = Environment.UserName;
                if (client.Register(name))
                {
                    System.Windows.Forms.MessageBox.Show("OK");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var client = new Services.WcfServiceClient();
                string name = Environment.UserName;
                if (client.Unregister(name))
                {
                    System.Windows.Forms.MessageBox.Show("OK");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
    }
}
