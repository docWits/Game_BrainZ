using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace WpfGameApp
{
    /// <summary>
    /// Работа с базой данных через ADO.NET
    /// </summary>
    class Database
    {
        /// <summary>
        /// Соединение с базой данных
        /// </summary>
        private MySqlConnection connection;

        /// <summary>
        /// Конструктор
        /// </summary>
        public Database()
        {
            // Описание соединения с БД
            connection = new MySqlConnection("Server=mysql.hosting.nic.ru;Database=orioner_csharp;Uid=orioner_cs;Pwd=BrainZ@cr0c!21;");
            // Фактическое соединение с БД
            connection.Open();

            // Протоколирование запуска
            AddUserLog();
        }


        /// <summary>
        /// Добавление записи в протокол работы пользователя
        /// </summary>
        private void AddUserLog()
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO UserLog (UserName) VALUE (@name)";
            command.Parameters.AddWithValue("name", Environment.UserName);
            int result = command.ExecuteNonQuery();
            if (result != 1)
            {
                // [!] такого точно не бывает
                throw new Exception("Неожиданный результат выполнения операции с БД");
            }
        }

        /// <summary>
        /// Чтение списка серверов
        /// </summary>
        /// <returns></returns>
        public List<Entities.Server> GetServers()
        {
            var list = new List<Entities.Server>();
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "SELECT Name, Price, Size, Weight, CPUs, Storage FROM Servers";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                var server = new Entities.Server();

                // Последовательная обработка всех столбцов таблицы
                for (int col = 0; col < reader.FieldCount; col++)
                {
                    // Имя столбца таблицы, например 'Price'
                    string column = reader.GetName(col);
                    // Имя типа данных столбца таблицы, например, 'Int32'
                    string type = reader.GetFieldType(col).Name;
                    // Свойство объекта server с именем, равным имени столбца таблицы
                    System.Reflection.PropertyInfo prop = server.GetType().GetProperty(column);
                    // Обработка поддерживаемых типов данных
                    switch (type)
                    {
                        case nameof(String):                            
                            prop.SetValue(server, reader.GetString(col));
                            break;
                        case nameof(Int32):
                            prop.SetValue(server, reader.GetInt32(col));
                            break;
                        case nameof(Single):
                            prop.SetValue(server, reader.GetDouble(col));
                            break;
                        default:
                            throw new Exception($"Тип данных '{type}' пока не поддерживается");
                    }
                }


                list.Add(server);
            }
            return list;
        }
    }
}

