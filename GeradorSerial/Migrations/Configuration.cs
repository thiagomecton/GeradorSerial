namespace GeradorSerial.Migrations
{
    using GeradorSerial.Models;
    using MySql.Data.Entity;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            SetSqlGenerator("MySql.Data.MySqlClient", new MySqlMigrationSqlGenerator());
            SetHistoryContextFactory("MySql.Data.MySqlClient", (conn, schema) => new MySqlHistoryContext(conn, schema));
            CodeGenerator = new MySqlMigrationCodeGenerator();
        }

        protected override void Seed(Models.MyContext context)
        {
            if (!context.Usuarios.Any())
            {
                context.Usuarios.Add(new Usuario { Nome = "Admin", Cpf = "01234567890", Senha = "admin", Email = "admin@admin.com", Ativo = true, Tipo = EnumTipoUsuario.Administrador });
                context.SaveChanges();
            }
        }
    }
}
