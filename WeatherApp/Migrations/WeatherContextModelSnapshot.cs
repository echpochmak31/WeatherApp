﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WeatherApp.DataAccess;

#nullable disable

namespace WeatherApp.Migrations
{
    [DbContext(typeof(WeatherContext))]
    partial class WeatherContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WeatherApp.Models.LocationGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("location_group");
                });

            modelBuilder.Entity("WeatherApp.Models.LocationGroupItem", b =>
                {
                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric")
                        .HasColumnOrder(0);

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric")
                        .HasColumnOrder(1);

                    b.Property<int>("LocationGroupId")
                        .HasColumnType("integer");

                    b.HasKey("Latitude", "Longitude");

                    b.HasIndex("LocationGroupId");

                    b.ToTable("location_group_item");
                });

            modelBuilder.Entity("WeatherApp.Models.LocationWeather", b =>
                {
                    b.Property<decimal>("LocationGroupItemLatitude")
                        .HasColumnType("numeric");

                    b.Property<decimal>("LocationGroupItemLongitude")
                        .HasColumnType("numeric");

                    b.Property<int>("ConditionCode")
                        .HasColumnType("integer")
                        .HasColumnName("condition_code");

                    b.Property<string>("ConditionImageUrl")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("condition_image_url");

                    b.Property<string>("ConditionText")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("condition_text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("country");

                    b.Property<decimal>("FeelsLikeСelsius")
                        .HasColumnType("numeric")
                        .HasColumnName("feels_like_c");

                    b.Property<DateTimeOffset>("LastUpdated")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("last_updated");

                    b.Property<decimal>("Latitude")
                        .HasColumnType("numeric")
                        .HasColumnName("latitude");

                    b.Property<decimal>("Longitude")
                        .HasColumnType("numeric")
                        .HasColumnName("longitude");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("Region")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("region");

                    b.Property<decimal>("TemperatureСelsius")
                        .HasColumnType("numeric")
                        .HasColumnName("temp_c");

                    b.HasKey("LocationGroupItemLatitude", "LocationGroupItemLongitude");

                    b.ToTable("location_weather");
                });

            modelBuilder.Entity("WeatherApp.Models.Security.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.HasKey("Id");

                    b.ToTable("user");
                });

            modelBuilder.Entity("WeatherApp.Models.LocationGroup", b =>
                {
                    b.HasOne("WeatherApp.Models.Security.User", "User")
                        .WithMany("LocationGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WeatherApp.Models.LocationGroupItem", b =>
                {
                    b.HasOne("WeatherApp.Models.LocationGroup", "LocationGroup")
                        .WithMany("Items")
                        .HasForeignKey("LocationGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocationGroup");
                });

            modelBuilder.Entity("WeatherApp.Models.LocationWeather", b =>
                {
                    b.HasOne("WeatherApp.Models.LocationGroupItem", "LocationGroupItem")
                        .WithMany()
                        .HasForeignKey("LocationGroupItemLatitude", "LocationGroupItemLongitude")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LocationGroupItem");
                });

            modelBuilder.Entity("WeatherApp.Models.LocationGroup", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("WeatherApp.Models.Security.User", b =>
                {
                    b.Navigation("LocationGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
