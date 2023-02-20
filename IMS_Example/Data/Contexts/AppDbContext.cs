using IMS_Example.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace IMS_Example.Data.Contexts
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        #region Property
        public DbSet<Users> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<Module> Modules { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Devices> Devices { get; set; }
        public DbSet<Permission_Group> Permission_Groups { get; set; }
        public DbSet<Permission_Use_Menu> Permission_Use_Menus { get; set; }
        #endregion


        #region Fluent API
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Users>(e =>
            {
                e.ToTable("Users").HasIndex(k => k.userCode).IsUnique();
                e.HasKey(k => k.id);
                e.Property(k => k.userCode).IsRequired().HasMaxLength(30).HasColumnType("varchar");
                e.Property(k => k.userPassword).IsRequired().HasMaxLength(1000).HasColumnType("varchar");
                e.Property(k => k.isDeleted);
                e.Property(k => k.firstName).HasMaxLength(50).HasColumnType("varchar");
                e.Property(k => k.lastName).HasMaxLength(50).HasColumnType("varchar");
                e.Property(k => k.phoneNumber).HasMaxLength(11);
                e.Property(k => k.dOB).HasColumnType("date");
                e.Property(k => k.address).HasMaxLength(200).HasColumnType("varchar");
                e.Property(k => k.university).HasMaxLength(100).HasColumnType("varchar");
                e.Property(k => k.email).HasMaxLength(50);
                e.Property(k => k.emailPassword).HasMaxLength(1000);
                e.Property(k => k.skype).HasMaxLength(100);
                e.Property(k => k.skypePassword).HasMaxLength(1000);
                e.Property(k => k.workStatus);
                e.Property(k => k.dateStartWork).HasDefaultValue(DateTime.Now).HasColumnType("date");
                e.Property(k => k.dateLeave).HasColumnType("date");
                e.Property(k => k.maritalStatus);
                e.Property(k => k.IdGroup);
                e.Property(k => k.reasonResignation).HasMaxLength(1000);
                e.Property(k => k.IdGroup);
            });

            //phan quyen use menu
            modelBuilder.Entity<Group>(e =>
            {
                e.ToTable("Group");
                e.HasKey(e => e.Id);
                e.Property(e => e.NameGroup).IsRequired();
                e.Property(e => e.Key).HasColumnType("varchar");
                e.Property(e => e.Discription).HasColumnType("text");
                e.Property(e => e.userCreated);
                e.Property(e => e.dateCreated).HasColumnType("date");
                e.HasIndex("Key").IsUnique();
            });

            //phan quyen use menu
            modelBuilder.Entity<Roles>(e =>
            {
                e.ToTable("Roles");
                e.HasKey(e => e.idRole);
                e.Property(e => e.nameRole).IsRequired().HasColumnType("varchar");
                e.Property(e => e.description).HasColumnType("varchar");
                
            });

            modelBuilder.Entity<Permission_Use_Menu>(e =>
            {
                e.ToTable("Permission_Use_Menu");
                e.HasKey(e => e.Id);
                e.Property(e => e.IdUser).IsRequired();
                e.Property(e => e.IdMenu).IsRequired();
                e.Property(e => e.Add).IsRequired().HasDefaultValue(0);
                e.Property(e => e.Update).IsRequired().HasDefaultValue(0);
                e.Property(e => e.Delete).IsRequired().HasDefaultValue(0);
                e.Property(e => e.Export).IsRequired().HasDefaultValue(0);
            });

            modelBuilder.Entity<Menu>(e =>
            {
                e.ToTable("Menu");
                e.HasKey(e => e.id);
                e.Property(e => e.idModule);
                e.Property(e => e.title).HasColumnType("Text");
                e.Property(e => e.icon).HasColumnType("Text");
                e.Property(e => e.action).HasColumnType("Text");
                e.Property(e => e.view).HasColumnType("Text");
                e.Property(e => e.controller).HasColumnType("Text");
                e.Property(e => e.isDeleted);
                e.Property(e => e.parent);
            });

            modelBuilder.Entity<Module>(e =>
            {
                e.ToTable("Module");
                e.HasKey(e => e.id);
                e.Property(e => e.nameModule);
                e.Property(e => e.note);
                e.Property(e => e.idSort);
            });

            modelBuilder.Entity<Permission_Group>(e =>
            {
                e.ToTable("Permission_Group");
                e.HasKey(e => e.Id);
                e.Property(e => e.IdGroup).IsRequired();
                e.Property(e => e.IdModule).IsRequired();
            });

            //equipment
            modelBuilder.Entity<Devices>(e =>
            {
                e.ToTable("Devices");
                e.HasKey(e => e.IdDevice);
                e.Property(e => e.DeviceName).IsRequired();
                e.Property(e => e.Info).IsRequired();
                e.Property(e => e.IsDeleted).IsRequired().HasDefaultValue(0);
                e.Property(e => e.UserCreated).IsRequired();
                e.Property(e => e.UserUpdated).IsRequired().HasDefaultValue(0);
                e.Property(e => e.DateUpdated).HasColumnType("timestamp").IsRequired().HasDefaultValue(DateTime.MinValue);
                e.Property(e => e.Note).HasColumnType("text");
            });

            modelBuilder.Entity<Projects>(e =>
            {
                e.ToTable("Projects").HasIndex(e => e.ProjectCode).IsUnique();
                e.Property(e => e.StartDate).HasColumnType("date");
                e.Property(e => e.EndDate).HasColumnType("date");
                e.Property(e => e.DateCreated).HasColumnType("date");
                e.Property(e => e.DateUpdate).HasColumnType("date");

            });


        }
        #endregion
    }
}
