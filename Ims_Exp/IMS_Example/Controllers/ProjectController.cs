using AutoMapper;
using IMS_Example.Data.Contexts;
using IMS_Example.Data.DTOs.ProjectDTO;
using IMS_Example.Data.Models;
using IMS_Example.Services.PaginationServices;
using IMS_Example.Services.TokenServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using IMS_Example.Data.DTOs;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace IMS_Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly TokenServices _tokenServices;
        private readonly IPaginationServices<Projects> _paginationServices;
        private readonly IMapper _mapper;

        public ProjectController(AppDbContext context, TokenServices tokenServices, IPaginationServices<Projects> paginationServices, IMapper mapper)
        {
            _context = context;
            _tokenServices = tokenServices;
            _paginationServices = paginationServices;
            _mapper = mapper;
        }

        [HttpGet("getAllProject")]
        [Authorize(Roles = "permission_group: True module: project")]
        public ActionResult getAllProject()
        {
            try
            {
                var dsProject = _context.Projects.ToList();
                return Ok(dsProject);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("addProject")]
        [Authorize(Roles = "permission_group: True module: project")]
        [Authorize(Roles = "module: project add: 1")]

        public async Task<IActionResult> Create(AddNewProjectDTO addNewProjectDTO)
        {
            try
            {
                var existProject = _context.Projects.Where(x => x.ProjectCode == addNewProjectDTO.ProjectCode);

                if (existProject.Any())
                {
                    return BadRequest("Project code is exist!");
                }

                var p = _mapper.Map<Projects>(existProject);
                var pro = await _context.Projects.SingleOrDefaultAsync();

                if(pro == null && p.StartDate < p.EndDate)
                {
                    if (p.IsOnGitlab)
                    {
                        p.EndDate = null;
                    }
                    _context.Add(p);
                    _context.SaveChanges();
                    return Ok();
                }
                return BadRequest("Wrong data enter");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("DeleteProject/{id}")]
        [Authorize(Roles = "permission_group: True module: project")]
        [Authorize(Roles = "module: project delete: 1")]

        public IActionResult DeleteProject(int id, IdUserChangeProjectDTO request)
        {
            try
            {
                var pro = _context.Projects.SingleOrDefault(p => p.Id == id);
                if(pro == null)
                {
                    return NotFound("Object isn't Founded");
                }
                else
                {
                    pro.IsDeleted = true;
                    pro.UserUpdate = request.UserId;
                    pro.DateUpdate = DateTime.UtcNow;
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("FinishProject/{id}")]
        [Authorize(Roles = "permission_group: True module: project")]
        [Authorize(Roles = "module: project update: 1")]

        public IActionResult FinishProject(int id, IdUserChangeProjectDTO request)
        {
            try
            {
                var pro = _context.Projects.SingleOrDefault(x => x.Id == id);
                if(pro == null)
                {
                    return NotFound("Object Is Not Founded! ");
                }
                else
                {
                    pro.IsFinished = true;
                    pro.UserUpdate = request.UserId;
                    pro.DateUpdate = DateTime.UtcNow;
                    _context.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getById/{id}")]
        [Authorize(Roles = "permission_group: True module: project")]

        public IActionResult getById(int id)
        {
            try
            {
                var pro = _context.Projects.SingleOrDefault(p => p.Id == id);
                if (pro == null)
                {
                    return NotFound("Object isn't Founded");
                }
                return Ok(pro);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getProjectIsDelete")]
        [Authorize(Roles = "permission_group: True module: project")]

        public IActionResult getProjectIsDelete()
        {
            try
            {
                var pro = _context.Projects.Where(p => p.IsDeleted == true);
                if(pro == null)
                {
                    return NotFound("Object isn't Founded");
                }
                return Ok(pro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getProjectByDayBefore/{day}")]
        [Authorize(Roles = "permission_group: True module: project")]

        public IActionResult getProjectByDayBefore(DateTime day)
        {
            try
            {
                var pro = _context.Projects.Where(p => p.EndDate < day);
                if(pro == null)
                {
                    return NotFound("Object isn't Founded");
                }
                return Ok(pro);
            }
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getProjectByName/{name}")]
        [Authorize(Roles = "permission_group: True module: project")]

        public IActionResult getProjectByName(string name)
        {
            try
            {
                var pro = _context.Projects.SingleOrDefault(p => p.Name == name);
                if (pro == null)
                {
                    return NotFound("Object isn't Founded");
                }
                return Ok(pro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getProjectIsNotFinished")]
        [Authorize(Roles = "permission_group: True module: project")]

        public IActionResult getProjectIsNotFinished()
        {
            try
            {
                var pro = _context.Projects.Where(p => p.IsFinished == false);
                if (pro == null)
                {
                    return NotFound("Object isn't Founded");
                }
                return Ok(pro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("getLenghOfProject/{name}")]
        [Authorize(Roles = "permission_group: True module: project")]

        public IActionResult getLenghOfProject(string name)
        {
            try
            {
                var pro = _context.Projects.SingleOrDefault(p => p.Name == name);
                if (pro == null)
                {
                    return NotFound("Object isn't Founded");
                }
                else
                {
                    TimeSpan count = (TimeSpan)(pro.EndDate - pro.StartDate);
                    double day = count.TotalDays;
                    return Ok(day);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet]
        [Route("getProjectById/{Id}")]
        [Authorize(Roles = "permission_group: True module: project")]

        public async Task<ActionResult<Projects>> getProjectById(int id)
        {
            try
            {
                var pro = await _context.Projects.SingleOrDefaultAsync(p => p.Id == id);
                if (pro == null)
                {
                    return NotFound("Object isn't Founded");
                }
                return Ok(pro);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut]
        [Route("updateProject/{id}")]
        [Authorize(Roles = "permission_group: True module: project")]
        [Authorize(Roles = "module: project update: 1")]

        public async Task<ActionResult> updateProject(int id, EditProjectDTO editProjectDTO)
        {
            try
            {
                var pro = await _context.Projects.FindAsync(id);
                if(pro == null)
                {
                    return NotFound();
                }
                var proUpdate = _mapper.Map<EditProjectDTO, Projects>(editProjectDTO, pro);
                _context.Update(proUpdate);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private async Task<bool> checkToken(HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["Authorization"];
            if (token == null) return false;

            token = token.Substring("Bearer ".Length).Trim();
            var response = _tokenServices.Decode(token);
            var user = await _context.Users.Where(user => user.id == response.id && user.userCode == response.userCode && user.IdGroup == response.group).SingleOrDefaultAsync() ;

            if(user == null) return false;
            return true;
        }

        [HttpPost("getAll")]
        [Authorize(Roles = "permission_group: True module: project")]
        public async Task<IActionResult> getAll(Paginate p)
        {
            if(p.pageIndex == 0)
            {
                p.pageIndex = 1;
            }
            if(p.pageSizeEnum == 0)
            {
                p.pageSizeEnum= 2;
            }
            var keyword = await _context.Projects.Where(
                i => string.IsNullOrEmpty(p.word) || 
                i.Id.ToString().Contains(p.word) ||
                i.StartDate.ToString().Contains(p.word) ||
                i.EndDate.ToString().Contains(p.word) ||
                i.UserId.ToString().Contains(p.word) ||
                i.Leader.ToString().Contains(p.word) ||
                i.UserCreated.ToString().Contains(p.word) ||
                i.DateCreated.ToString().Contains(p.word) ||
                i.UserUpdate.ToString().Contains(p.word) ||
                i.ProjectCode.ToString().Contains(p.word) ||
                i.Name.ToString().Contains(p.word) ||
                i.Description.ToString().Contains(p.word) 
                ).Skip((p.pageIndex-1) * p.pageSizeEnum).Take(p.pageSizeEnum).ToListAsync();

            var listSort = keyword.OrderBy(i => i.DateCreated).ToList();
            var pageIndex = p.pageIndex;
            var pageSize = p.pageSizeEnum;
            var resultPage = await _paginationServices.paginationListTableAsync(listSort, pageIndex, pageSize);
            if (resultPage._sucess)
            {
                return Ok(resultPage);
            }
            return BadRequest(resultPage);
        }

        [HttpGet("getProjectByDayAfter")]
        [Authorize(Roles = "permission_group: True module: project")]
        public async Task<ActionResult<Projects>> getProjectByDayAfter(DateTime day)
        {
            var project = await _context.Projects.Where(x => x.EndDate > day).ToListAsync();
            if(project == null)
            {
                return BadRequest();
            }
            return Ok(project);
        }

        [HttpGet("getProjectFinished")]
        [Authorize(Roles = "permission_group: True module: project")]
        public async Task<ActionResult<IEnumerable<Projects>>> getProjectFinished()
        {
            var project = await _context.Projects.Where(x => x.IsFinished == true).ToListAsync();   
            if(project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }


        [HttpGet("getUserProject/{Id}")]
        [Authorize(Roles = "permission_group: True module: project")]
        public async Task<ActionResult> getUserProject(int Id)
        {
            var project = await _context.Projects.Where(x => x.Id == Id).SingleOrDefaultAsync();
            if (project == null)
            {
                return NotFound();
            }
            var idUser = project.UserId;
            return Ok(idUser);
        }

        [HttpGet("getProjectOnGitlab")]
        [Authorize(Roles = "permission_group: True module: project")]
        public async Task<ActionResult> getProjectOnGitlab()
        {
            var project = await _context.Projects.Where(x => x.IsOnGitlab == true).ToListAsync();
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        //[HttpGet("UserInProject/{idproject}")]
        //[Authorize(Roles = "permission_group: True module: project")]
        //public async Task<ActionResult> UserInProject(int idproject)
        //{
        //    try { 
        //    var listUser = from x in _context.Users
        //                   join c in _context.Member_Project on x.id equals c.member
        //                   join d in _context.Projects on c.idProject equals d.Id
        //                   where d.Id == idproject
        //                   select new
        //                   {
        //                       x,
        //                       name = x.lastName + " " + x.firsNam,
        //                    };
        //    return Ok(listUser);
        //    }
        //    catch(Exception ex)
        //    {
        //        return BadRequest();
        //    }
        //}

    }
}
