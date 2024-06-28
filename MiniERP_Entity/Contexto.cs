using Microsoft.EntityFrameworkCore;
using MiniERP_Entity.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniERP_Entity
{
    public class Contexto : DbContext
    {
        public DbSet<Relatorio> Relatorios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<ClienteProduto> ClientesProdutos { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public Contexto()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=miniERP_Entity;User ID=sa;Password=Gabri123!");
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nota>()
               .HasOne(n => n.Cliente)
               .WithMany(c => c.Notas)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Nota>()
                .HasMany(n => n.clienteProdutos)
                .WithOne(cp => cp.Nota)
                .OnDelete(DeleteBehavior.NoAction);

            //aqui faço a relação de 1:n 1 fornecedor pode ter muitos produtos, criando a FK de fornecedor na tabela produtos
            modelBuilder.Entity<Produto>()
                .HasOne(p => p.Fornecedor)
                .WithMany(f => f.Produtos)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<ClienteProduto>()
               .HasOne(i => i.Produto);
            
            
            //declaro a propriedade decimal
            modelBuilder.Entity<ClienteProduto>()
                .Property(cp => cp.PrecoTotal)
                .HasColumnType("decimal(18,2)");

            //declaro a propriedade decimal
            modelBuilder.Entity<Produto>()
                .Property(p => p.Preco)
                .HasColumnType("decimal(18,2)");

            //declaro a propriedade decimal
            modelBuilder.Entity<Nota>()
                .Property(p => p.PrecoTotalCompra)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Nota>()
                .Property(n => n.Data)
                .HasDefaultValueSql("GETDATE()");
        }

    }
}
