﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SistemaLlavesWebAPI.Dal;

#nullable disable

namespace SistemaLlavesWebAPI.Data.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Shared.Models.Categorias", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriaId"));

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Shared.Models.Clientes", b =>
                {
                    b.Property<int>("ClienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClienteId"));

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RNC")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ClienteId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Shared.Models.Compras", b =>
                {
                    b.Property<int>("CompraId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompraId"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<double>("Itbis")
                        .HasColumnType("float");

                    b.Property<double>("Subtotal")
                        .HasColumnType("float");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("CompraId");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("Shared.Models.ComprasDetalle", b =>
                {
                    b.Property<int>("CompraDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CompraDetalleId"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("CompraId")
                        .HasColumnType("int");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("CompraDetalleId");

                    b.HasIndex("CompraId");

                    b.HasIndex("ProductoId");

                    b.ToTable("ComprasDetalle");
                });

            modelBuilder.Entity("Shared.Models.Garantias", b =>
                {
                    b.Property<int>("GarantiaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GarantiaId"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("FinGarantia")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("InicioGarantia")
                        .HasColumnType("datetime2");

                    b.HasKey("GarantiaId");

                    b.ToTable("Garantias");
                });

            modelBuilder.Entity("Shared.Models.MetodosPagos", b =>
                {
                    b.Property<int>("MetodoPagoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MetodoPagoId"));

                    b.Property<string>("TipoMetodoPago")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MetodoPagoId");

                    b.ToTable("MetodosPagos");
                });

            modelBuilder.Entity("Shared.Models.Productos", b =>
                {
                    b.Property<int>("ProductoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductoId"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<double>("Costo")
                        .HasColumnType("float");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("Descuento")
                        .HasColumnType("float");

                    b.Property<int?>("GarantiaId")
                        .HasColumnType("int");

                    b.Property<double?>("Itbis")
                        .HasColumnType("float");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.Property<int>("ProveedorId")
                        .HasColumnType("int");

                    b.HasKey("ProductoId");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("GarantiaId");

                    b.HasIndex("ProveedorId");

                    b.ToTable("Productos");
                });

            modelBuilder.Entity("Shared.Models.Proveedores", b =>
                {
                    b.Property<int>("ProovedorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProovedorId"));

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProovedorId");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("Shared.Models.Ventas", b =>
                {
                    b.Property<int>("VentaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VentaId"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("Fecha")
                        .HasColumnType("date");

                    b.Property<int>("MetodoPagoId")
                        .HasColumnType("int");

                    b.Property<bool>("VentaDevuelta")
                        .HasColumnType("bit");

                    b.HasKey("VentaId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("MetodoPagoId");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("Shared.Models.VentasDetalle", b =>
                {
                    b.Property<int>("VentasDetalleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("VentasDetalleId"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<double>("ItbisTotal")
                        .HasColumnType("float");

                    b.Property<int>("ProductoId")
                        .HasColumnType("int");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.Property<int>("VentaId")
                        .HasColumnType("int");

                    b.HasKey("VentasDetalleId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("VentaId");

                    b.ToTable("VentasDetalle");
                });

            modelBuilder.Entity("Shared.Models.ComprasDetalle", b =>
                {
                    b.HasOne("Shared.Models.Compras", null)
                        .WithMany("ComprasDetalles")
                        .HasForeignKey("CompraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Models.Productos", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");
                });

            modelBuilder.Entity("Shared.Models.Productos", b =>
                {
                    b.HasOne("Shared.Models.Categorias", "Categoria")
                        .WithMany()
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Models.Garantias", "Garantia")
                        .WithMany()
                        .HasForeignKey("GarantiaId");

                    b.HasOne("Shared.Models.Proveedores", "Proveedor")
                        .WithMany()
                        .HasForeignKey("ProveedorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("Garantia");

                    b.Navigation("Proveedor");
                });

            modelBuilder.Entity("Shared.Models.Ventas", b =>
                {
                    b.HasOne("Shared.Models.Clientes", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Models.MetodosPagos", "MetodoPago")
                        .WithMany()
                        .HasForeignKey("MetodoPagoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("MetodoPago");
                });

            modelBuilder.Entity("Shared.Models.VentasDetalle", b =>
                {
                    b.HasOne("Shared.Models.Productos", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shared.Models.Ventas", "Venta")
                        .WithMany()
                        .HasForeignKey("VentaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Producto");

                    b.Navigation("Venta");
                });

            modelBuilder.Entity("Shared.Models.Compras", b =>
                {
                    b.Navigation("ComprasDetalles");
                });
#pragma warning restore 612, 618
        }
    }
}
