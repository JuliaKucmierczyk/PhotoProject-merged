using PhotoProject.Data.Base;
using PhotoProject.Data.ViewModels;
using PhotoProject.Models;

namespace PhotoProject.Data.Services
{
    public interface ICommentService : IEntityBaseRepository<CommentModel>
    {
        Task<CommentModel> GetCommentByIdAsync(int id);
        Task AddNewCommentAsync(CommentModel comment);
    }
}
