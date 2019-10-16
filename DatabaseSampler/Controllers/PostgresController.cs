﻿using System.Threading.Tasks;
using DatabaseSampler.Application.DataGenerator;
using DatabaseSampler.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseSampler.Controllers
{
    [AllowAnonymous]
    public class PostgresController : Controller
    {
        private readonly IDataGenerator _dataGenerator;
        private readonly IPostgresSqlService _postgresSqlService;
        
        public PostgresController(IDataGenerator dataGenerator, IPostgresSqlService postgresSqlService)
        {
            _dataGenerator = dataGenerator;
            _postgresSqlService = postgresSqlService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateStudent()
        {
            // How to Configure PostgreSQL in Entity Framework Core – Code Maze
            //https://code--maze-com.cdn.ampproject.org/v/s/code-maze.com/configure-postgresql-ef-core/amp/?usqp=mq331AQCKAE%3D&amp_js_v=0.1

            var student = _dataGenerator.CreateStudent();
            var id = await _postgresSqlService.AddStudentAsync(student);

            return View("Index");
        }
    }
}