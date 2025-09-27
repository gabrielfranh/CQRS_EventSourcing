using CQRS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Post.Common.Dtos;
using Post.Query.Api.Dtos;
using Post.Query.Api.Queries;
using Post.Query.Domain.Entities;

namespace Post.Query.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PostLookupController(ILogger<PostLookupController> logger, IQueryDispatcher<PostEntity> queryDispatcher) : ControllerBase
    {
        private readonly ILogger<PostLookupController> _logger = logger;
        private readonly IQueryDispatcher<PostEntity> _queryDispatcher = queryDispatcher;

        [HttpGet]
        public async Task<ActionResult> GetAllPostsAsync()
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindAllPostsQuery());
                var postsCount = posts.Count;
                if (posts is null || postsCount == 0)
                    return NoContent();

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = $"Successfully returned {postsCount} post{(postsCount > 1 ? "s" : string.Empty)}."
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to retrieve all posts!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }

        }

        [HttpGet("byId/{postId}")]
        public async Task<ActionResult> GetPostByIdAsync(Guid postId)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostByIdQuery
                {
                    Id = postId
                });

                var postsCount = posts.Count;
                if (posts is null || postsCount == 0)
                    return NoContent();

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = $"Successfully returned post"
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to get post by id!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }

        }

        [HttpGet("byAuthor/{author}")]
        public async Task<ActionResult> GetPostByAuthorAsync(string author)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostsByAuthorQuery
                {
                    Author = author
                });

                var postsCount = posts.Count;
                if (posts is null || postsCount == 0)
                    return NoContent();

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = $"Successfully returned post"
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to get post by author!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }

        }

        [HttpGet("withComments")]
        public async Task<ActionResult> GetPostWithCommentsAsync()
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindAllPostsWithCommentsQuery());

                var postsCount = posts.Count;
                if (posts is null || postsCount == 0)
                    return NoContent();

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = $"Successfully returned post"
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to get posts with comments!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }

        }
        
        [HttpGet("withLikes/{numberOfLikes}")]
        public async Task<ActionResult> GetPostByAuthorAsync(int numberOfLikes)
        {
            try
            {
                var posts = await _queryDispatcher.SendAsync(new FindPostsWithLikesQuery
                {
                    NumberOfLikes = numberOfLikes 
                });

                var postsCount = posts.Count;
                if (posts is null || postsCount == 0)
                    return NoContent();

                return Ok(new PostLookupResponse
                {
                    Posts = posts,
                    Message = $"Successfully returned post"
                });
            }
            catch (Exception ex)
            {
                const string SAFE_ERROR_MESSAGE = "Error while processing request to get posts by likes!";
                _logger.LogError(ex, SAFE_ERROR_MESSAGE);
                return StatusCode(StatusCodes.Status500InternalServerError, new BaseResponse
                {
                    Message = SAFE_ERROR_MESSAGE
                });
            }

        }
    }
}