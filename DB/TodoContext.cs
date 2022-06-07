using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoList.Authentication;
using TodoList.Entities;

namespace TodoList.DB
{
    public class TodoContext : IdentityDbContext<ApplicationUser>
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Product>()
                    .HasIndex(b => b.Sku)
                    .IsUnique();

            builder.Entity<Category>().HasData(
              new Category() { Id = 1, Name = "Văn Học" },
              new Category() { Id = 2, Name = "Giáo Khoa" },
              new Category() { Id = 3, Name = "Thiếu Nhi"},
              new Category() { Id = 4, Name = "Tâm Lý - Kỹ Năng Sống" },
              new Category() { Id = 5, Name = "Manga - Comic" },
              new Category() { Id = 6, Name = "Sách Học Ngoại Ngữ" },
              new Category() { Id = 7, Name = "Kinh Tế" },
              new Category() { Id = 8, Name = "Khoa Học Kỹ Thuật" },
              new Category() { Id = 9, Name = "Lịch Sử - Địa Lý - Tôn Giáo" },
              new Category() { Id = 10, Name = "Nuôi Dạy Con" },
              new Category() { Id = 11, Name = "Chính Trị - Pháp Lý - Triết Học" },
              new Category() { Id = 12, Name = "Nữ Công Gia Chánh" }
              );

            builder.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    Sku = "S1",
                    CategoryId = 1,
                    Name = "Khuôn Mặt Người Khác",
                    Price = 103000,
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    Quantity = 20,
                },
                new Product()
                {
                    Id = 2,
                    Sku = "S2",
                    CategoryId = 2,
                    Name = "Hội Vệ Nhân",
                    Price = 152000,
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    Quantity = 20,
                },
                new Product()
                {
                    Id = 3,
                    Sku = "S3",
                    CategoryId = 3,
                    Name = "Khuôn Mặt Người Khác",
                    Price = 103.530,
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    Quantity = 20,
                },
                new Product()
                {
                    Id = 4,
                    Sku = "S4",
                    CategoryId = 4,
                    Name = "Giọt rừng",
                    Price = 86000,
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    Quantity = 20,
                },
                new Product()
                {
                    Id = 5,
                    Sku = "S5",
                    CategoryId = 5,
                    Name = "Người rủ ngủ",
                    Price = 120000,
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    Quantity = 20,
                },
                new Product()
                {
                    Id = 6,
                    Sku = "S6",
                    CategoryId = 6,
                    Name = "Mong Mẹ Hãy Yêu Lấy Chính Mình",
                    Price = 86000,
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    Quantity = 20,
                },
                new Product()
                {
                    Id = 7,
                    Sku = "S7",
                    CategoryId = 7,
                    Name = "The Friend - Bạn Đồng Hành",
                    Price = 95000,
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    Quantity = 20,
                },
                new Product()
                {
                    Id = 8,
                    Sku = "S8",
                    CategoryId = 2,
                    Name = "Khải Vi Về Cõi Vô Hình",
                    Price = 130000,
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    Quantity = 20,
                },
                new Product()
                {
                    Id = 9,
                    Sku = "S9",
                    CategoryId = 3,
                    Name = "Thần Thoại Hy Lạp",
                    Price = 191000,
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    Quantity = 20,
                },
                new Product()
                {
                    Id = 10,
                    Sku = "S10",
                    CategoryId = 4,
                    Name = "Lì quá để nói quài",
                    Price = 67000,
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    Quantity = 20,
                },
                new Product()
                {
                    Id = 11,
                    Sku = "S11",
                    CategoryId = 5,
                    Name = "Cỏ Khô Lầm Lỡ Muốt Mùa",
                    Price = 180000,
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    Quantity = 20,
                },
                new Product()
                {
                    Id = 12,
                    Sku = "S12",
                    CategoryId = 7,
                    Name = "Hiểm Họa Ở Nhà Kết",
                    Price = 91000,
                    Description = String.Join(" ", Faker.Lorem.Sentences(3)),
                    Quantity = 20,
                }
                );
        }
    }
}
