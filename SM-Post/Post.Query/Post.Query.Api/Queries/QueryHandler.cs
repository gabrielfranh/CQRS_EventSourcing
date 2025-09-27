using Post.Query.Domain.Entities;
using Post.Query.Domain.Repositories;

namespace Post.Query.Api.Queries
{
    public class QueryHandler(IPostRepository postRepository) : IQueryHandler
    {
        private readonly IPostRepository _postRepository = postRepository;

        public Task<List<PostEntity>> HandleAsync(FindAllPostsQuery query)
        {
            return _postRepository.ListAllAsync();
        }

        public async Task<List<PostEntity>> HandleAsync(FindPostByIdQuery query)
        {
            var post = await _postRepository.GetByIdAsync(query.Id);
            return post is null ? [] : [post];
        }

        public async Task<List<PostEntity>> HandleAsync(FindPostsByAuthorQuery query)
        {
            return await _postRepository.ListByAuthorAsync(query.Author);
        }

        public async Task<List<PostEntity>> HandleAsync(FindAllPostsWithCommentsQuery query)
        {
            return await _postRepository.ListWithCommentAsync();
        }

        public async Task<List<PostEntity>> HandleAsync(FindPostsWithLikesQuery query)
        {
            return await _postRepository.ListWithLikesAsync(query.NumberOfLikes);
        }
    }
}