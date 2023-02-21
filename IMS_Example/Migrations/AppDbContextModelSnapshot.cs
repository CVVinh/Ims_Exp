﻿// <auto-generated />
using System;
using IMS_Example.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IMS_Example.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("IMS_Example.Data.Models.Devices", b =>
                {
                    b.Property<int>("IdDevice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("IdDevice"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp")
                        .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

                    b.Property<string>("DeviceName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Info")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<string>("Note")
                        .HasColumnType("text");

                    b.Property<int>("UserCreated")
                        .HasColumnType("integer");

                    b.Property<int>("UserUpdated")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.HasKey("IdDevice");

                    b.ToTable("Devices", (string)null);
                });

            modelBuilder.Entity("IMS_Example.Data.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Discription")
                        .HasColumnType("text");

                    b.Property<int>("IsDeleted")
                        .HasColumnType("integer");

                    b.Property<string>("Key")
                        .HasColumnType("varchar");

                    b.Property<string>("NameGroup")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("dateCreated")
                        .HasColumnType("date");

                    b.Property<DateTime?>("dateModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int?>("userCreated")
                        .HasColumnType("integer");

                    b.Property<int?>("userModified")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("Key")
                        .IsUnique();

                    b.ToTable("Group", (string)null);
                });

            modelBuilder.Entity("IMS_Example.Data.Models.Menu", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("action")
                        .IsRequired()
                        .HasColumnType("Text");

                    b.Property<string>("controller")
                        .IsRequired()
                        .HasColumnType("Text");

                    b.Property<string>("icon")
                        .IsRequired()
                        .HasColumnType("Text");

                    b.Property<int>("idModule")
                        .HasColumnType("integer");

                    b.Property<int>("isDeleted")
                        .HasColumnType("integer");

                    b.Property<int>("parent")
                        .HasColumnType("integer");

                    b.Property<string>("title")
                        .IsRequired()
                        .HasColumnType("Text");

                    b.Property<string>("view")
                        .IsRequired()
                        .HasColumnType("Text");

                    b.HasKey("id");

                    b.ToTable("Menu", (string)null);
                });

            modelBuilder.Entity("IMS_Example.Data.Models.Module", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("idSort")
                        .HasColumnType("integer");

                    b.Property<int>("isDeleted")
                        .HasColumnType("integer");

                    b.Property<string>("nameModule")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Module", (string)null);
                });

            modelBuilder.Entity("IMS_Example.Data.Models.Permission_Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Access")
                        .HasColumnType("boolean");

                    b.Property<int>("IdGroup")
                        .HasColumnType("integer");

                    b.Property<int>("IdModule")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Permission_Group", (string)null);
                });

            modelBuilder.Entity("IMS_Example.Data.Models.Permission_Use_Menu", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Add")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("Delete")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("Export")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<int>("IdMenu")
                        .HasColumnType("integer");

                    b.Property<int>("IdUser")
                        .HasColumnType("integer");

                    b.Property<int>("Update")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.Property<DateTime?>("dateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("dateModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("idModule")
                        .HasColumnType("integer");

                    b.Property<int?>("userCreated")
                        .HasColumnType("integer");

                    b.Property<int?>("userModified")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Permission_Use_Menu", (string)null);
                });

            modelBuilder.Entity("IMS_Example.Data.Models.Projects", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("date");

                    b.Property<DateTime>("DateUpdate")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("date");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsFinished")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOnGitlab")
                        .HasColumnType("boolean");

                    b.Property<int>("Leader")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ProjectCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("character varying(10)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.Property<int>("UserCreated")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("UserUpdate")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ProjectCode")
                        .IsUnique();

                    b.ToTable("Projects", (string)null);
                });

            modelBuilder.Entity("IMS_Example.Data.Models.Roles", b =>
                {
                    b.Property<int>("idRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("idRole"));

                    b.Property<string>("description")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.Property<bool>("disabled")
                        .HasColumnType("boolean");

                    b.Property<string>("nameRole")
                        .IsRequired()
                        .HasColumnType("varchar");

                    b.HasKey("idRole");

                    b.ToTable("Roles", (string)null);
                });

            modelBuilder.Entity("IMS_Example.Data.Models.Users", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("IdGroup")
                        .HasColumnType("integer");

                    b.Property<string>("address")
                        .HasMaxLength(200)
                        .HasColumnType("varchar");

                    b.Property<DateTime?>("dOB")
                        .HasColumnType("date");

                    b.Property<DateTime?>("dateCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("dateLeave")
                        .HasColumnType("date");

                    b.Property<DateTime?>("dateModified")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("dateStartWork")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("date")
                        .HasDefaultValue(new DateTime(2023, 2, 20, 17, 57, 17, 888, DateTimeKind.Local).AddTicks(2955));

                    b.Property<string>("email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("emailPassword")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("firstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<byte?>("gender")
                        .HasColumnType("smallint");

                    b.Property<string>("identitycard")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<byte>("isDeleted")
                        .HasColumnType("smallint");

                    b.Property<string>("lastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar");

                    b.Property<byte?>("maritalStatus")
                        .HasColumnType("smallint");

                    b.Property<string>("phoneNumber")
                        .HasMaxLength(11)
                        .HasColumnType("character varying(11)");

                    b.Property<string>("reasonResignation")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("refreshToken")
                        .HasColumnType("text");

                    b.Property<DateTime?>("refreshTokenExpiryTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("skype")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("skypePassword")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("university")
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("userCode")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("varchar");

                    b.Property<int?>("userCreated")
                        .HasColumnType("integer");

                    b.Property<int?>("userModified")
                        .HasColumnType("integer");

                    b.Property<string>("userPassword")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("varchar");

                    b.Property<byte>("workStatus")
                        .HasColumnType("smallint");

                    b.Property<int?>("yearGraduated")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("userCode")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
