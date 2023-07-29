using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlogsite.Entities;
using MyBlogsite.Models;
using MyBlogsite.Service;

namespace MyBlogsite.Controllers
{
    [Route("User")]
    [Controller]
    public class UserController : Controller
    {
        #region Fields
        private readonly INewsManageRepository _newsManageRepository;
        private readonly IMapper _mapper;
        #endregion
        #region Ctor
        public UserController(INewsManageRepository newsManageRepository, IMapper mapper)
        {
            _newsManageRepository = newsManageRepository ?? throw new ArgumentNullException(nameof(newsManageRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        #endregion
        #region Methods
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (ModelState.IsValid)
            {
                await _newsManageRepository.CreateUserAsync(user);
            }

            return View(user);
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            var user = await _newsManageRepository
               .GetUserProfileAsync(userId);
            var userMap = _mapper.Map<IEnumerable<UserDto>>(user);

            return View(userMap);
        }
        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            if (ModelState.IsValid)
            {
                var user = await _newsManageRepository.Login(userName, password);

                if (user != null)
                {
                    return View(user);
                }
                else
                {
                    // Kullanıcı adı veya şifre hatalı ise yapılacak işlemler
                    // ...
                }
            }
            return View();
        }
        #endregion
    }
}
