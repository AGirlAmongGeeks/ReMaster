using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ReMaster.EntityFramework;

namespace ReMaster.EntityFramework.Migrations
{
    [DbContext(typeof(ReMasterDbContext))]
    [Migration("20170505221206_AddColumnToCompany")]
    partial class AddColumnToCompany
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReMaster.EntityFramework.Model.Company", b =>
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

                    b.Property<bool>("IsDeleted");

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

            modelBuilder.Entity("ReMaster.EntityFramework.Model.ProjectMetaData", b =>
                {
                    b.Property<string>("Key")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Value");

                    b.HasKey("Key");

                    b.ToTable("ProjectMetaDatas");
                });
        }
    }
}
