using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

namespace WpfGameApp
{
    /// <summary>
    /// База данных MySQL
    /// </summary>
    [DbConfigurationType(typeof (MySql.Data.Entity.MySqlEFConfiguration))]
    public class DB : DbContext
    {
        // Контекст настроен для использования строки подключения "DB" из файла конфигурации  
        // приложения (App.config или Web.config). По умолчанию эта строка подключения указывает на базу данных 
        // "WpfGameApp.DB" в экземпляре LocalDb. 
        // 
        // Если требуется выбрать другую базу данных или поставщик базы данных, измените строку подключения "DB" 
        // в файле конфигурации приложения.
        public DB() : base("name=DB")
        {
        }

        /// <summary>
        /// Разрешение автоматической миграции базы данных
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DB, Migrations.Configuration>());
        }

        // Добавьте DbSet для каждого типа сущности, который требуется включить в модель. Дополнительные сведения 
        // о настройке и использовании модели Code First см. в статье http://go.microsoft.com/fwlink/?LinkId=390109.

        
        public virtual DbSet<Entities.KvmConsole> Kvms { get; set;  }

        /// <summary>
        /// Перечень игроков
        /// </summary>
        public virtual DbSet<Entities.Player> Players { get; set; }

        /// <summary>
        /// Монтажные шкафы
        /// </summary>
        public virtual DbSet<Entities.Rack> Racks { get; set; }

        public virtual DbSet<Entities.Server> Servers { get; set; }

        public virtual DbSet<Entities.Storage> Storages { get; set; }

        public virtual DbSet<Entities.NetworkSwitch> Switches { get; set; }

        /// <summary>
        /// Производители оборудования
        /// </summary>
        public virtual DbSet<Entities.Vendor> Vendors { get; set; }
    }
}