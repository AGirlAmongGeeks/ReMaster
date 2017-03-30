using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ReMaster.EntityFramework;

namespace ReMaster.EntityFramework.Migrations
{
    [DbContext(typeof(ReMasterDbContext))]
    partial class ReMasterDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReMaster.Core.Model.Company.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddessAnomaly");

                    b.Property<string>("BuildingNo");

                    b.Property<string>("CeidgId");

                    b.Property<string>("City");

                    b.Property<string>("District");

                    b.Property<string>("Email");

                    b.Property<string>("Fax");

                    b.Property<string>("NIP");

                    b.Property<string>("Name");

                    b.Property<string>("OwnerFirstName");

                    b.Property<string>("OwnerLastName");

                    b.Property<string>("PKDCodes");

                    b.Property<string>("Phone");

                    b.Property<string>("PostalCity");

                    b.Property<string>("PostalCode");

                    b.Property<string>("Regon");

                    b.Property<string>("Status");

                    b.Property<string>("Voivodeship");

                    b.Property<string>("Website");

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });
        }
    }
}
