using Microsoft.AspNetCore.Mvc;
using PhotoProject.Data.Base;
using PhotoProject.Data.ViewModels;
using PhotoProject.Models;

namespace PhotoProject.Data.Services
{
    public class CommentService : EntityBaseRepository<CommentModel>, ICommentService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public CommentService(AppDbContext context, IWebHostEnvironment hostEnvironment) : base(context)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task AddNewCommentAsync(CommentModel comment)
        {
                await _context.AddAsync(comment);
                await _context.SaveChangesAsync();

        }

        public Task<CommentModel> GetCommentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}