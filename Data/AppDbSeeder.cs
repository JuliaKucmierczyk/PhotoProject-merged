//using PhotoProject.Models;

//namespace PhotoProject.Data
//{
//    public class AppDbSeeder
//    {
//        public static void Seed(IApplicationBuilder applicationBuilder)
//        {
//            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
//            {
//                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
//                context.Database.EnsureCreated();

//                if (!context.Users.Any())
//                {
//                    context.Users.AddRange(new List<UserModel>() {
//                        new UserModel()
//                        {
//                            Name = "Jan",
//                        },
//                        new UserModel()
//                        {
//                            Name = "Alicja",
//                        }
//                        });
//                    context.SaveChanges();
//                }

//                if (!context.Photos.Any())
//                {
//                    context.Photos.AddRange(new List<PhotoModel>()
//                    {
//                        new PhotoModel()
//                        {
//                            AuthorId = 1,
//                            Name = "Lewandowski",
//                            Access = AccessLevel.Public,
//                            ImageURL = "https://pbs.twimg.com/profile_images/1560186554781519873/wq6vdCir_400x400.jpg",
//                            Tags = "tag1, tag2, tag3",

//                        },
//                        new PhotoModel()
//                        {
//                            AuthorId = 2,
//                            Name = "Pudzian",
//                            Access = AccessLevel.Public,
//                            ImageURL = "https://i.wpimg.pl/730x0/m.fitness.wp.pl/1-874458f31f0148a360011450ebb898.jpg",
//                            Tags = "tag_dsa, tagwdqwdq",
//                        }
//                    });
//                    context.SaveChanges();
//                }

//                if (!context.Comments.Any())
//                {
//                    context.Comments.AddRange(new List<CommentModel>()
//                    {
//                        new CommentModel()
//                        {
//                            AuthorId = 1,
//                            Comments = "Lewandowski",
//                            PhotoId = 2,

//                        },
//                        new CommentModel()
//                        {
//                            AuthorId = 2,
//                            Comments = "Pudzian",
//                            PhotoId = 1,
//                        }
//                    });
//                    context.SaveChanges();
//                }
//                if (!context.Albums.Any())
//                {
//                    context.Albums.AddRange(new List<AlbumModel>()
//                    {
//                        new AlbumModel()
//                    {
//                        Name = "album1",
//                        Access = AccessLevel.Public,
//                        ImageURL = "https://www.blenderrap.pl/wp-content/uploads/2018/08/Travis-Scott-Astroworld.png",
//                        UrlName = "asd",
//                    },
//                        new AlbumModel()
//                    {
//                        Name = "album2",
//                        Access = AccessLevel.Public,
//                        ImageURL = "https://www.blenderrap.pl/wp-content/uploads/2018/08/Travis-Scott-Astroworld.png",
//                        UrlName = "asd2",
//                    }
//                    });
//                    context.SaveChanges();
//                }

//                if (!context.AlbumPhotos.Any())
//                {
//                    context.AlbumPhotos.AddRange(new List<AlbumPhotoModel>()
//                    {
//                        new AlbumPhotoModel()
//                        {
//                            AlbumId = 1,
//                            PhotoId = 1,
//                        },
//                        new AlbumPhotoModel()
//                        {
//                            AlbumId = 2,
//                            PhotoId = 2,
//                        }
//                });
//                    context.SaveChanges();
//                }


//            }
//        }
//    }
//}