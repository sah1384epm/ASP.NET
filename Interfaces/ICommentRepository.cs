using API.Models;
using API.Helpers;
namespace API.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync(CommentQueryObject queryObject);
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment commentModel);
        Task<Comment?> DeleteAsync(int id);
        Task<Comment?> UpdateAsync(int id, Comment commentModel);
    }
}