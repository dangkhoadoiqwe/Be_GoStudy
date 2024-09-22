using AutoMapper;
using DataAccess.Model;
using DataAccess.Repositories;
using GO_Study_Logic.ViewModel;

namespace GOStudy_Logic.Service
{
    public interface ICommentService
    {
        Task AddCommentAsync(Comment commentViewModel);
        Task<Comment_View_Model> GetCommentByIdAsync(int commentId); 
        Task UpdateCommentAsync(Comment_View_Model commentViewModel);
        Task<bool> DeleteCommentAsync(int commentId);
        Task<List<Comment>> GetCommentsByPostIdAsync(int postId);
    }
    public class CommentService: ICommentService
    {
        private readonly ICommentRepository _repo;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task AddCommentAsync(Comment commentViewModel)
        {
            var commentEntity = _mapper.Map<Comment>(commentViewModel);
            await _repo.AddCommentAsync(commentEntity);
        }

        public async Task<Comment_View_Model> GetCommentByIdAsync(int commentId)
        {
            var comment = await _repo.GetCommentByIdAsync(commentId);
            if (commentId == null)
                return null;
            var commentViewModel = _mapper.Map<Comment_View_Model>(comment);

            return commentViewModel;
        }

        public async Task UpdateCommentAsync(Comment_View_Model commentViewModel)
        {
            var updatedComment = _mapper.Map<Comment>(commentViewModel);

            var existingComment = await _repo.GetCommentByIdAsync(commentViewModel.CommentId);
            if (existingComment != null)
            {
                existingComment.Content = updatedComment.Content;
                existingComment.CommentId = updatedComment.CommentId;
                existingComment.PostId = updatedComment.PostId;
                existingComment.UserId = updatedComment.UserId;
                existingComment.CreatedAt = updatedComment.CreatedAt;
                
                await _repo.UpdateCommentAsync(existingComment);
            }
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            var comment = _repo.GetCommentByIdAsync(commentId);

            if (comment == null)
            {
                return false;
            }
            await _repo.DeleteCommentAsync(commentId);
            return true;
        }

        public Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
        {
           return _repo.GetCommentsByPostIdAsync(postId);
        }
    }
}