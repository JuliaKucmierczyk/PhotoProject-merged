
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoProject.Areas.Identity.Data;
using PhotoProject.Models;

public class AppDbContext : IdentityDbContext<PhotoProjectUser>
{
    public DbSet<AlbumModel> Albums { get; set; }
    public DbSet<CommentModel> Comments { get; set; }
    public DbSet<PhotoModel> Photos { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<AlbumPhotoModel> AlbumPhotos { get; set; }
    public DbSet<RateModel> Rates { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //AlbumPhoto
        modelBuilder.Entity<AlbumPhotoModel>().HasKey(ap => new
        {
            ap.Id,
        });
        modelBuilder.Entity<AlbumPhotoModel>().HasOne(a => a.Album).WithMany(ap => ap.AlbumsPhotos).HasForeignKey(a => a.AlbumId);
        modelBuilder.Entity<AlbumPhotoModel>().HasOne(a => a.Photo).WithMany(ap => ap.AlbumsPhotos).HasForeignKey(a => a.PhotoId);

        // cascade delete comments/photo
        modelBuilder.Entity<PhotoModel>()
     .HasMany<CommentModel>(c => c.Comments)
     .WithOne(s => s.Photo)
     .OnDelete(DeleteBehavior.Cascade);







        base.OnModelCreating(modelBuilder);
    }
}

