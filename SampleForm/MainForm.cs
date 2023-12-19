using System;
using System.Windows.Forms;

namespace SampleForm
{
    public partial class MainForm : Form
    {
        /// <summary>
        /// Строковые данные
        /// </summary>
        private string data;

        /// <summary>
        /// Признак нажатия кнопки мышки
        /// </summary>
        private bool pressed;

        private int x;
        private int y;

        private System.Drawing.Color color;

        /// <summary>
        /// Конструктор без параметров
        /// </summary>
        public MainForm()
        {
            // Поддержка дизайнера формы
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик события - пункт меню "Выход"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Закрытие формы -> завершение приложения
            Close();
        }

        /// <summary>
        /// Обработчик события - пункт меню "Копировать"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Отправить в буфер обмена заголовок окна
                Clipboard.SetText(Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обработчик события - пункт меню "Вставить"
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Параметры события</param>
        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                // Взять текст из буфера обмена
                data = Clipboard.GetText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Нажатие кнопки мыши 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void card_MouseDown(object sender, MouseEventArgs e)
        {
            pressed = true;
            x = e.X;
            y = e.Y;
        }

        /// <summary>
        /// Перемещение мыши
        /// </summary>
        /// <param name="sender">Перемещаемый объект</param>
        /// <param name="e"></param>
        private void card_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (pressed)
                {
                    Control control = (Control)sender;
                    control.Location = new System.Drawing.Point
                        (e.X + control.Location.X - x,
                        e.Y + control.Location.Y - y);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void card_MouseUp(object sender, MouseEventArgs e)
        {
            pressed = false;
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Создание объекта с одновременной инициализацией
            TextBox text = new TextBox()
            {
                Text = "КАРТОЧКА",
                Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204))),
                ReadOnly = true,
                Location = new System.Drawing.Point(0, menu.Height)
            };
            // Обработчики событий
            text.MouseUp += card_MouseUp;
            text.MouseDown += card_MouseDown;
            text.MouseMove += card_MouseMove;
            // Добавление элемента управления на форму
            Controls.Add(text);
        }

        private void card_MouseEnter(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            color = control.BackColor;
            control.BackColor = System.Drawing.Color.LightCoral;
        }

        private void card_MouseLeave(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            control.BackColor = color;
        }       
    }
}
