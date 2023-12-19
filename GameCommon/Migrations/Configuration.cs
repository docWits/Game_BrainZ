namespace WpfGameApp.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    /// <summary>
    /// Конфигурация миграции базы данных
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<WpfGameApp.DB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }

        /// <summary>
        /// Начальная инициализация БД
        /// </summary>
        /// <param name="db"></param>
        protected override void Seed(WpfGameApp.DB db)
        {
            // Добавление неизвестного вендора
            var vendor = new Entities.Vendor()
            {
                ID = Guid.Empty, // нулевой идентификатор
                Name = "Неизвестный вендор",
                Site = "unknown.org"
            };
            db.Vendors.AddOrUpdate(vendor);

            // Так как в таблице "Карточки" не было указано название моделей, в качестве названий были взяты наименования производителей

            // У емкости должен быть тип Double.
            // Чтобы не создавать миграцию, каждому серверу значение емкости присвоено 1
            db.Servers.AddOrUpdateByName(
                new Entities.Server()
                {
                    Name = "Lenovo",
                    Price = 20,
                    Weight = 10,
                    CPUs = 2,
                    Size = 1,
                    Storage = 1,
                    VendorID = Guid.Empty
                },
                new Entities.Server()
                {
                    Name = "Dell",
                    Price = 30,
                    Weight = 15,
                    CPUs = 4,
                    Size = 1,
                    Storage = 1,
                    VendorID = Guid.Empty
                },
                new Entities.Server()
                {
                    Name = "Fujitsu",
                    Price = 40,
                    Weight = 20,
                    CPUs = 6,
                    Size = 2,
                    Storage = 1,
                    VendorID = Guid.Empty
                },
                new Entities.Server()
                {
                    Name = "HP",
                    Price = 35,
                    Weight = 25,
                    CPUs = 6,
                    Size = 2,
                    Storage = 1,
                    VendorID = Guid.Empty
                },
                new Entities.Server()
                {
                    Name = "IBM",
                    Price = 40,
                    Weight = 20,
                    CPUs = 6,
                    Size = 2,
                    Storage = 1,
                    VendorID = Guid.Empty
                }
            );

            //  This method will be called after migrating to the latest version.
            db.Racks.AddOrUpdateByName(
                new Entities.Rack()
                {
                    Name = "TS IT",
                    Capacity = 400,
                    Price = 100,
                    Count = 42,
                    VendorID = Guid.Empty
                },
                new Entities.Rack()
                {
                    Name = "NetShelter",
                    Capacity = 500,
                    Price = 150,
                    Count = 42,
                    VendorID = Guid.Empty
                }
            );

            db.Storages.AddOrUpdateByName(
                new Entities.Storage()
                {
                    Name = "EMC",
                    Price = 300,
                    Weight = 40,
                    Size = 6,
                    VendorID = Guid.Empty
                },
                new Entities.Storage()
                {
                    Name = "IBM",
                    Price = 550,
                    Weight = 50,
                    Size = 6,
                    VendorID = Guid.Empty
                },
                new Entities.Storage()
                {
                    Name = "HP",
                    Price = 750,
                    Weight = 60,
                    Size = 6,
                    VendorID = Guid.Empty
                },
                new Entities.Storage()
                {
                    Name = "Fujitsu",
                    Price = 900,
                    Weight = 70,
                    Size = 6,
                    VendorID = Guid.Empty
                }
            );

            // Не указан размер, так как данное свойство отсутствует
            // Свойство не добавлено, чтобы не делать лишнюю миграцию
            db.Kvms.AddOrUpdateByName(
                new Entities.KvmConsole()
                {
                    Name = "APC",
                    Price = 50,
                    Weight = 15,
                    Count = 1,
                    VendorID = Guid.Empty
                },
                new Entities.KvmConsole()
                {
                    Name = "IBM",
                    Price = 70,
                    Weight = 10,
                    Count = 1,
                    VendorID = Guid.Empty
                },
                new Entities.KvmConsole()
                {
                    Name = "HP",
                    Price = 90,
                    Weight = 15,
                    Count = 1,
                    VendorID = Guid.Empty
                }
            );

            // У NetworkSwitch тоже отсутствует размер
            db.Switches.AddOrUpdateByName(
                new Entities.NetworkSwitch()
                {
                    Name = "Cisco",
                    Price = 200,
                    Weight = 5,
                    Count = 1,
                    VendorID = Guid.Empty
                },
                new Entities.NetworkSwitch()
                {
                    Name = "D-Link",
                    Price = 300,
                    Weight = 10,
                    Count = 1,
                    VendorID = Guid.Empty
                },
                new Entities.NetworkSwitch()
                {
                    Name = "ZTE",
                    Price = 250,
                    Weight = 10,
                    Count = 1,
                    VendorID = Guid.Empty
                },
                new Entities.NetworkSwitch()
                {
                    Name = "Huawei",
                    Price = 150,
                    Weight = 15,
                    Count = 1,
                    VendorID = Guid.Empty
                }
            );
        }
    }
}
